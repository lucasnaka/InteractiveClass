using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR;
public class MyMessage : MessageBase
{
    public NetworkInstanceId netId;
    public string stuff;
}
public class PlayerInfo : NetworkBehaviour
{
    // Start is called before the first frame update
    //[SyncVar]
    public string PlayerName = "name";
    short MyMsgId = 1000;
    public TextMesh nameText;
    public override void OnStartClient()
    {
        //NetworkManager.singleton.client.RegisterHandler(MyMsgId, OnMyMsg);
        if(Manager.VRAplication)
            XRSettings.enabled = true;
    }



    [Command] // roda no servidor
    void CmdAddClientToServer(GameObject player, string name)
    {
        print("Client conectado: " + player.GetComponent<PlayerInfo>().PlayerName);
        if (Manager.playersInScene == null)
        {
            Manager.playersInScene = new List<GameObject>();
            Manager.playersInfoInScene = new List<string>();
        }
        Manager.playersInScene.Add(player);
        Manager.playersInfoInScene.Add(name);
        RpcInformaClienteConectado(player, name);

    }


    [ClientRpc] // roda nos clientes
    void RpcInformaClienteConectado(GameObject player, string name)
    {
        if (Manager.playersInScene == null)
        {
            Manager.playersInScene = new List<GameObject>();
            Manager.playersInfoInScene = new List<string>();
        }
        Manager.playersInScene.Add(player);
        Manager.playersInfoInScene.Add(name);

    }

    void Start()
    {
        if (isLocalPlayer)
        {
            PlayerName = Manager.name;
            Manager.toggleMobileHUD();
            nameText.text = PlayerName;
            CmdAddClientToServer(this.gameObject, PlayerName);
        }

    }

    public void updateDisplayedName(string name)
    {
        nameText.text = name;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
