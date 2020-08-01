using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JoininUserLine : MonoBehaviour
{
    [SerializeField]
    private Image UserIcon;
    [SerializeField]
    private Text UserName;
    
    private User user;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignRoom(User u)
    {
        user = u;
        UserName.text = u.name;
    }

    
}
