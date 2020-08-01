using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    private User _currentUser;
    public User CurrentUser
    {
        get { return _currentUser; }
        set
        {
            _currentUser = value;
            Task.Run(() => RoomService.StartConnection(_currentUser.userId));
        }
    }
    public string url = "https://secret-hitler-backend.herokuapp.com/";
    //public string url = "http://localhost:5000/";
    public RoomService RoomService;
    public UserService UserService;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(GameObject).Name;
                    _instance = go.AddComponent<GameManager>();
                    
                }
            }
            return _instance;
        }
    }
    //delegates: 
    public delegate void PlayerLoggedInEventHandler();
    public delegate void GetRoomsEventHandler(List<Room> rooms);
    public delegate void CreateRoomEventHandler(Room room);

    public event CreateRoomEventHandler NewRoomCreated;
    public event GetRoomsEventHandler RoomsGetsCalled;
    public event PlayerLoggedInEventHandler playerLoggedIn;

    //unity events:

    public RoomEvent DisplayJoiningUsers;
    public RoomEvent GoToRoomEvent;
    //propreties
    public bool isDarkMode = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            RoomService = gameObject.AddComponent<RoomService>();
            UserService = gameObject.AddComponent<UserService>();
            if (DisplayJoiningUsers == null)
                DisplayJoiningUsers = new RoomEvent();
            if (GoToRoomEvent == null)
                GoToRoomEvent = new RoomEvent();
            //DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //locally mthpds : 


    //event calls 
    public void OnPlayerLoggedIn()
    {
        if(playerLoggedIn != null)
        {
            playerLoggedIn();
        }
        
    }
    public void WhenRoomsGetsCalled(List<Room> rooms)
    {
        if (RoomsGetsCalled != null)
        {
            RoomsGetsCalled(rooms);
        }
    }
    public void OnRoomCreation(Room room)
    {
        if(NewRoomCreated != null)
        {
            NewRoomCreated(room);
        }
    }
}
