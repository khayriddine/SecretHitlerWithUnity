using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserService : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator UpdateUser(Action cb)
    {

        //UnityWebRequest x = UnityWebRequest.Get(GameManager.Instance.url+"/");
        //yield return x.SendWebRequest();
        yield return new WaitForSeconds(7);
        cb.Invoke();
    }
}
