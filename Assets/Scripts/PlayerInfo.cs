using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInfo : NetworkBehaviour
{
    // Start is called before the first frame update
    [SyncVar]
    public string PlayerName = "";

    public TextMesh nameText;
    public override void OnStartClient()
    {
        
    }
    void Start()
    {
        if (isLocalPlayer)
        {
            PlayerName = Manager.name;
            Manager.toggleMobileHUD();
            nameText.text = PlayerName;
        }

    }

    public void updateDisplayedName(string name) {
        nameText.text = name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
