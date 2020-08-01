using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomLine : MonoBehaviour
{
    private RoomService roomService;
    [SerializeField]
    Text Name;
    [SerializeField]
    Text Size;
    [SerializeField]
    Text Admin;
    [SerializeField]
    Button Show;
    [SerializeField]
    Dropdown Actions;



    private Room room;
    // Start is called before the first frame update
    void Start()
    {
        roomService = GameManager.Instance.RoomService;
        Actions.onValueChanged.AddListener(delegate {
            DropdownValueChanged(Actions);
        });
        Actions.options.Clear();
        Actions.options.Add(new Dropdown.OptionData() { text = "Play" });
        Actions.options.Add(new Dropdown.OptionData() { text = "Join" });
        Actions.options.Add(new Dropdown.OptionData() { text = "Leave" });
        Actions.options.Add(new Dropdown.OptionData() { text = "Remove" });

        Show.onClick.AddListener(() =>
        {
            if (GameManager.Instance.DisplayJoiningUsers != null)
            {
                GameManager.Instance.DisplayJoiningUsers.Invoke(room);
            }
        });
    }

    private void DropdownValueChanged(Dropdown actions)
    {
        try
        {
            switch (actions.value)
            {
                case 3: GameManager.Instance.RoomService.StartRoom(room); break;
                case 1:
                    {
                        var user = GameManager.Instance.CurrentUser;
                        if (user != null)
                        {
                            if (room.usersJoining == null)
                                room.usersJoining = new List<User>();
                            user.roomId = room.roomId;

                            StartCoroutine(GameManager.Instance.RoomService.JoinRoom(GameManager.Instance.CurrentUser));
                        }
                        break;
                    }
                case 2:
                    {
                        var user = GameManager.Instance.CurrentUser;
                        if (user != null)
                        {
                            if (room.usersJoining == null)
                                room.usersJoining = new List<User>();
                            user.roomId = room.roomId;

                            GameManager.Instance.RoomService.LeaveRoom(GameManager.Instance.CurrentUser);
                        }
                        break;
                    }
                case 0: GameManager.Instance.RoomService.RemoveRoom(room.roomId); break;
            }
        }
        catch (Exception ex)
        {

            Debug.LogError(ex.Message);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void UpdateRoomLine(string n,string s, string a)
    {
        Name.text = n;
        Size.text = s;
        Admin.text = a;
    }*/

    public void AssignRoom(Room r)
    {
        room = r;
        Name.text = room.name;
        Size.text = room.numberOfPlayer.ToString();
        Admin.text = room.adminId.ToString();
    }

 
}
