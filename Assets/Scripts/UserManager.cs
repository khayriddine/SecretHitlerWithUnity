using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UserManager : MonoBehaviour
{
    public string url = "https://secret-hitler-backend.herokuapp.com/";
    [SerializeField]
    private Text username;
    [SerializeField]
    private Text password;

    private void OnEnable()
    {
        GameManager.Instance.playerLoggedIn += LogIn;
    }

    private void OnDisable()
    {
        GameManager.Instance.playerLoggedIn -= LogIn;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator getAllUsers()
    {
        string url = GameManager.Instance.url+"api/users";
        UnityWebRequest x = UnityWebRequest.Get(url);
        yield return x.SendWebRequest();

        Debug.Log(x.downloadHandler.text);
    }
    public void LogIn()
    {
        StartCoroutine("logIn");
    }
    IEnumerator logIn()
    {
        string name = username.text;
        string pwd = password.text;
        if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pwd))
        {
            Debug.Log("check your username or password");
            yield break;
        }
        else
        {

            var user = new  User() { name = name, password = pwd };
            string json = JsonUtility.ToJson(user);
            var request = new UnityWebRequest(url + "api/users/authenticate", "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            Debug.Log("Status Code: " + request.downloadHandler.text);
            GameManager.Instance.CurrentUser = JsonUtility.FromJson<User>(request.downloadHandler.text);
        }
    }
}
