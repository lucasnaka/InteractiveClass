using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public string PlayerName = "";
    void Start()
    {
        PlayerName = Manager.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
