﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GravityRay : NetworkBehaviour
{
    int reachRange = 100;
    public GameObject mao_direita;
    public GameObject extintor1;
    public GameObject camera;
    public GameObject hitObject;
    [SyncVar]
    public bool extintor1_pego = false;



    Dictionary<string, bool> objectsToCatch = new Dictionary<string, bool>();

    void Start() {
        objectsToCatch.Add("Extintor1", false);
        objectsToCatch.Add("Extintor2", false);
        objectsToCatch.Add("ChestKey", false);
        objectsToCatch.Add("crowbarOld1", false);
        objectsToCatch.Add("crowbarOld2", false);
        extintor1 = GameObject.FindGameObjectWithTag("Extintor1");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void RaybeamStart()
    {

        RaycastHit hitInfo;
        if(Physics.Raycast (camera.transform.position, camera.transform.forward, out hitInfo, reachRange)) {
            if(objectsToCatch.ContainsKey(hitInfo.collider.gameObject.name))
            {
                hitObject = hitInfo.collider.gameObject;
                //hitObject.transform.parent = mao_direita.transform;
               // hitObject.transform.localPosition = new Vector3(0, 0, 0);
                objectsToCatch[hitObject.name] = true;
               


            }
        }
    }
    /* [Command]
     void CmdClientToServer(string teste)
     {
         //if (isClient)
         //{
             print("CLient: peguei o extintor");
             RpcTeste(teste);
             RpcLigaObjetoMao("ExtintorMao", Manager.name);
         // }
     }


     [ClientRpc]
     void RpcTeste(string teste)
     {
        // if (isServer)
        // {
             extintor1_pego = true;

             Manager.atualizaNome(teste);
            // extintor1.SetActive(false);
             //extintor1.SetActive(false);
             print("Servidor: pegou o extintor");

       //  }

     }*/

    [Command] // roda no servidor
    void CmdEscondeObjetoNoServidor(string tagObjeto, string PlayerName)
    {
        print("Client: peguei o " + tagObjeto+" - "+ PlayerName);
        Manager.escondeObjeto(tagObjeto);
        RpcLigaObjetoMao(tagObjeto, PlayerName);
        
    }


    [ClientRpc] // roda nos clientes
    void RpcLigaObjetoMao(string tagObjeto, string playerName)
    {

        if (isLocalPlayer)
        {
            Manager.escondeObjeto(tagObjeto);
            GameObject[] clients = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject client in clients)
            {
                PlayerInfo info = client.GetComponent<PlayerInfo>();

                info.updateDisplayedName(info.PlayerName);


            }

            //os objetos devem ter a tag assim Extintor_1
            if (tagObjeto == "Extintor1" || tagObjeto == "Extintor2")
                tagObjeto = "Extintor";
           // GameObject[] objetos = GameObject.FindGameObjectsWithTag(tagObjeto + "Mao");

            //mao_direita = objetos[objetos.Length-1].transform.GetChild(0).gameObject.SetActive(true);
            //GameObject objeto = GameObject.FindGameObjectWithTag(tagObjeto + "Mao");
            //mao_direita.transform.GetChild(0).gameObject.SetActive(true);
        }
            
        

        
           

        
       
    }
    public GameObject objectCaught;
    public static bool com_extintor1 = false;
    public static bool com_extintor2 = false; 
    public static bool com_chaveBau = false;
    public static bool com_crowbarOld1 = false;
    public static bool com_crowbarOld2 = false;

    

    public GameObject[] objects;

    void Update(){
        if(Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
        {
            //CmdClientToServer(Manager.name);

            if (mao_direita == null)
            {
                objects = GameObject.FindGameObjectsWithTag("mao_direita");
                mao_direita = objects[objects.Length - 1];
            }
            if (!objectsToCatch.ContainsValue(true))
            {
               
                RaybeamStart();   
                if(hitObject != null)
                 CmdEscondeObjetoNoServidor(hitObject.tag, Manager.name);
                
                   
            }
                
            else
            {
                string objectName = "";
                foreach (string key in objectsToCatch.Keys)
                {
                    if(objectsToCatch[key] == true)
                        objectName = key;
                }
                objectCaught = GameObject.Find(objectName);
                objectsToCatch[objectName] = false;
                objectCaught.transform.parent = null;
            }
        }

        com_extintor1 = objectsToCatch["Extintor1"];
        com_extintor2 = objectsToCatch["Extintor2"];
        com_chaveBau = objectsToCatch["ChestKey"];
        com_crowbarOld1 = objectsToCatch["crowbarOld1"];
        com_crowbarOld2 = objectsToCatch["crowbarOld2"];

    }
}
