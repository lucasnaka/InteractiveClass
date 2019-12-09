using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class GravityRay : NetworkBehaviour
{
    int reachRange = 100;
    public GameObject mao_direita;
    public GameObject camera;
    public GameObject hitObject;

    [SyncVar]
    public bool extintor1_pego;
    [SyncVar]
    public bool extintor2_pego;
    [SyncVar]
    public bool chave_pega;
    [SyncVar]
    public bool pe_cabra1_pego;
    [SyncVar]
    public bool pe_cabra2_pego;

    TCPServer serverTCP;
    TCPClient clientTCP;
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        extintor1_pego = false;
        extintor2_pego = false;
        chave_pega = false;
        pe_cabra1_pego = false;
        pe_cabra2_pego = false;

        if (!isServer)
        {
            clientTCP = new TCPClient();
           Manager.clientTCP = clientTCP;
            clientTCP.setStart();

        }
        
       
            
            
    }

    /*public void RaybeamStart()
    {

        RaycastHit hitInfo;
        if(Physics.Raycast (camera.transform.position, camera.transform.forward, out hitInfo, reachRange)) {
            if(objectsToCatch.ContainsKey(hitInfo.collider.gameObject.name))
            {
                hitObject = hitInfo.collider.gameObject;
                hitObject.transform.parent = mao_direita.transform;
                hitObject.transform.localPosition = new Vector3(0, 0, 0);
                objectsToCatch[hitObject.name] = true;
            }
        }
    }*/

    [Command] // roda no servidor
    void CmdRaybeamStart()
    {
        RpcRaybeamStart();
    }

    [ClientRpc] // roda nos clientes
    void RpcRaybeamStart()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, reachRange))
        {
            if (Manager.objectsToCatch.ContainsKey(hitInfo.collider.gameObject.name))
            {
                hitObject = hitInfo.collider.gameObject;
                hitObject.transform.parent = mao_direita.transform;
                hitObject.transform.localPosition = new Vector3(0, 0, 0);
                Manager.objectsToCatch[hitObject.name] = true;
                extintor1_pego = Manager.objectsToCatch["Extintor1"];
                extintor2_pego = Manager.objectsToCatch["Extintor2"];
                chave_pega = Manager.objectsToCatch["ChestKey"];
                pe_cabra1_pego = Manager.objectsToCatch["crowbarOld1"];
                pe_cabra2_pego = Manager.objectsToCatch["crowbarOld2"];

                extintor1_pego = true;
                updateInfo();
            }
            //if (hitInfo.collider.gameObject.name == "Extintor1")
            //{
            //    extintor1_pego = true;
            //    Manager.objectsToCatch["Extintor1"] = true;
            //    hitObject = hitInfo.collider.gameObject;
            //    hitObject.transform.parent = mao_direita.transform;
            //    hitObject.transform.localPosition = new Vector3(0, 0, 0);
            //}
            //else if (hitInfo.collider.gameObject.name == "Extintor2")
            //{
            //    extintor2_pego = true;
            //}
            //else if (hitInfo.collider.gameObject.name == "ChestKey")
            //{
            //    chave_pega = true;
            //}
            //else if (hitInfo.collider.gameObject.name == "crowbarOld1")
            //{
            //    pe_cabra1_pego = true;
            //}
            //else if (hitInfo.collider.gameObject.name == "crowbarOld2")
            //{
            //    pe_cabra2_pego = true;
            //}
        }
    }

    public GameObject objectCaught;

    public GameObject[] objects;
    public GameObject myLocalPlayer;

    public GameObject FindLocalNetworkPlayer()
    {
        NetworkManager networkManager = NetworkManager.singleton;
        List<PlayerController> pc = networkManager.client.connection.playerControllers;

        for (int i = 0; i < pc.Count; i++)
        {
            GameObject obj = pc[i].gameObject;
            NetworkBehaviour netBev = obj.GetComponent<NetworkBehaviour>();

            if (pc[i].IsValid && netBev != null && netBev.isLocalPlayer)
            {
                return pc[i].gameObject;
            }
        }
        return null;
    }

    //Envia a informacao via TCP
    void updateInfo() {
        
        string msg = "Extintor1=" + extintor1_pego + ";" + "Extintor2=" + extintor2_pego + ";" + "ChestKey=" + chave_pega + ";"
                 + "crowbarOld1=" + pe_cabra1_pego + ";" + "crowbarOld2=" + pe_cabra2_pego;

        if (isClient && !isServer)
            Manager.clientTCP.SendMessage(msg);
        else
            Manager.serverTCP.SendMessage(msg);
    }

    void Update()
    {
        
        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
        {

            if (mao_direita == null)
            {
                myLocalPlayer = FindLocalNetworkPlayer();
                // Se trocar Player1 prefab por Player prefab, colocar a seguinte linha de comando:
                // mao_direita = myLocalPlayer.transform.Find("IdleMan/mixamorig4:Hips/mixamorig4:Spine/mixamorig4:Spine1/mixamorig4:Spine2/mixamorig4:RightShoulder/mixamorig4:RightArm/mixamorig4:RightForeArm/mixamorig4:RightHand/mixamorig4:RightHandIndex1").gameObject;
                mao_direita = myLocalPlayer.transform.Find("IdleMan (1)/mixamorig4:Hips/mixamorig4:Spine/mixamorig4:Spine1/mixamorig4:Spine2/mixamorig4:RightShoulder/mixamorig4:RightArm/mixamorig4:RightForeArm/mixamorig4:RightHand/mixamorig4:RightHandIndex1").gameObject;
            }

            if (!Manager.objectsToCatch.ContainsValue(true))
            {
                if (isServer)
                    RpcRaybeamStart();
                else
                    CmdRaybeamStart();
            }

            else
            {
                string objectName = "";
                foreach (string key in Manager.objectsToCatch.Keys)
                {
                    if (Manager.objectsToCatch[key] == true)
                        objectName = key;
                }

                objectCaught = GameObject.Find(objectName);
                Manager.objectsToCatch[objectName] = false;
                objectCaught.transform.parent = null;
            }
        }

    }
}
