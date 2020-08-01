using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System.Text;

public class RoomService : MonoBehaviour
{
    

    private static HubConnection connection;
    // Start is called before the first frame update
    public async Task StartConnection(int id)
    {
        try
        {
            await connection.StartAsync();

            Debug.Log("Connection started");
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
        await NewUser(GameManager.Instance.CurrentUser.userId);
    }
    public async Task NewUser(int userId)
    {
        try
        {
            await connection.InvokeAsync("NewUser", userId);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    // Update is called once per frame 
    void Update() 
    {
        
    }

    private void Start()
    {
    connection = new HubConnectionBuilder()
    .WithUrl(GameManager.Instance.url + "hubUsers")
    .Build();
        connection.Closed += async (error) =>
        {
            await Task.Delay(Random.Range(0, 5) * 1000);
            await connection.StartAsync();
        };

        connection.On<List<Room>>("GetRooms", (Rooms) =>
        {
            GameManager.Instance.WhenRoomsGetsCalled(Rooms);
        });
        connection.On<Room>("NavigationToRoom", (Room) =>
        {
            GameManager.Instance.GoToRoomEvent.Invoke(Room);
        });
        connection.On<Room>("UpdateRoom", (Room) =>
        {
            GameManager.Instance.OnRoomCreation(Room);
        });
        connection.On<string>("Notify", (type) =>
        {
            Debug.Log(type);
        });
    }
   
    public async Task CreateRoom(Room room)
    {
        try
        {
            await connection.InvokeAsync("CreateRoom", room);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public async Task RemoveRoom(int roomId)
    {
        try
        {
            await connection.InvokeAsync("RemoveRoom", roomId);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    
    public IEnumerator JoinRoom(User user)
    {

        string json = JsonUtility.ToJson(user);
        var request = new UnityWebRequest(GameManager.Instance.url+ "api/users/" + user.userId.ToString(), "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();;
        try
        {
            connection.InvokeAsync("JoinRoom", user,user.roomId);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public async Task LeaveRoom(User user)
    {
        try
        {
            await connection.InvokeAsync("LeaveRoom", user.userId, user.roomId);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public async Task StartRoom(Room room)
    {
        try
        {
            await connection.InvokeAsync("NavigaToRoom", room);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    IEnumerator getAllUsers()
    {
        string url = GameManager.Instance.url+"api/users";
        UnityWebRequest x = UnityWebRequest.Get(url);
        yield return x.SendWebRequest();

        Debug.Log(x.downloadHandler.text);
    }

}
