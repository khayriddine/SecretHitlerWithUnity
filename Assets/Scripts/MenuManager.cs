using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    private RoomService roomService;
    public string url = "https://secret-hitler-backend.herokuapp.com/";
    [SerializeField]
    private Text roomName;
    [SerializeField]
    private Slider roomSize;
    [SerializeField]
    private GameObject createRoomButton;


    [SerializeField]
    private GameObject LoginPanel;
    [SerializeField]
    private GameObject HomePanel;
    [SerializeField]
    private GameObject CreateRoomPanel;
    [SerializeField]
    private GameObject JoiningUsersPanel;
    [SerializeField]
    private GameObject RoomLine;
    [SerializeField]
    private Transform RoomsView;


    [SerializeField]
    private GameObject JoininUserPrefab;
    [SerializeField]
    private Transform JoininUsersView;
    private List<GameObject> UsersJoining;

    private List<GameObject> roomsObj;
    private List<Room> rooms;
    private bool updateRooms = false;
    private bool isRoomCreated = false;
    private void OnEnable()
    {
        
        GameManager.Instance.playerLoggedIn += DisplayHome;
        GameManager.Instance.RoomsGetsCalled += DisplayRooms;
        GameManager.Instance.NewRoomCreated += AddNewRoom;

    }

    private void OnDisable()
    {
        GameManager.Instance.playerLoggedIn -= DisplayHome;
        GameManager.Instance.RoomsGetsCalled -= DisplayRooms;
        GameManager.Instance.NewRoomCreated -= AddNewRoom;
    }
    // Start is called before the first frame update
    void Start()
    {
        roomService = GameManager.Instance.RoomService;
        roomsObj = new List<GameObject>();
        GameManager.Instance.DisplayJoiningUsers.AddListener(displayJoiningUsersPanel);
        GameManager.Instance.GoToRoomEvent.AddListener(GoToRoom);
        UsersJoining = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (updateRooms)
        {
            displayRooms();
            updateRooms = false;
        }
        if (isRoomCreated)
        {
            displayRooms();
            isRoomCreated = false;
            displayHomeFromCreateRoom();
        }
        /*
        if(string.IsNullOrEmpty(roomName.text))
        {
            createRoomButton.SetActive(true);
        }
        else
        {
            createRoomButton.SetActive(false);
        }*/
    }

    public void CreateRoom()
    {

        var room = new Room()
        {
            adminId = GameManager.Instance.CurrentUser.userId,
            name = roomName.text,
            numberOfPlayer = (int)roomSize.value,
            usersJoining = new List<User>()
        };
        roomService.CreateRoom(room);
    }


    private void DisplayHome()
    {
        LoginPanel.SetActive(false);
        HomePanel.SetActive(true);
    }
    public void DisplayRooms(List<Room> rs)
    {

        rooms = rs;
        updateRooms = true;

    }
    private void displayRooms()
    {
        try
        {
            if (rooms != null)
            {

                foreach (var go in roomsObj)
                {
                    Destroy(go);
                }
                roomsObj.Clear();
                foreach (var r in rooms)
                {
                    if (r != null)
                    {
                        var go = GameObject.Instantiate(RoomLine, RoomsView);
                        go.GetComponent<RoomLine>().AssignRoom(r);
                        roomsObj.Add(go);
                    }
                }
            }
        }
        catch (System.Exception ex)
        {

            Debug.LogError(ex.Message);
        }
    }

    public void AddNewRoom(Room r)
    {
        rooms.Add(r);
        isRoomCreated = true;

    }
    private void displayHomeFromCreateRoom()
    {
        CreateRoomPanel.SetActive(false);
        HomePanel.SetActive(true);
    }
    private void displayJoiningUsersPanel(Room room)
    {
        JoiningUsersPanel.SetActive(true);
        if (room.usersJoining != null)
        {

            foreach (var go in UsersJoining)
            {
                Destroy(go);
            }
            UsersJoining.Clear();
            foreach (var u in room.usersJoining)
            {
                if (u != null)
                {
                    var go = GameObject.Instantiate(JoininUserPrefab, JoininUsersView);
                    go.GetComponent<JoininUserLine>().AssignRoom(u);
                    UsersJoining.Add(go);
                }
            }
        }
    }
    

    public void CloseJoiningUserPanel()
    {
        JoiningUsersPanel.SetActive(false);
    }

    public void GoToRoom(Room room)
    {

        try
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                SceneManager.LoadScene("RoomScene");

            });
        }
        catch (System.Exception ex)
        {

            Debug.LogError(ex.Message);
        }
        
    }


}
