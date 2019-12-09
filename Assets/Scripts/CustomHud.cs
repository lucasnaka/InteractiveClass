﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR;

namespace CustomHud
{
    public class CustomHud : MonoBehaviour
    {
        private string IpAddress = "localhost";
        private string Port = "7777";
        public float GuiOffset;
        private bool _started;
        public string name = "player";
        public GameObject mobileHUD;

        public void Start()
        {
            _started = false;
            Manager.mobileHUD = GameObject.FindGameObjectWithTag("MobileHUD");
            Manager.toggleMobileHUD();
          //  XRSettings.enabled = false;

        }
        public void OnGUI()
        {
            GUILayout.Space(GuiOffset);
            GUIStyle myStyle = new GUIStyle(GUI.skin.button);
            myStyle.fontSize = 80;
            GUILayout.Space(40);

            if (!_started)
            {
                if (GUILayout.Button("Host", myStyle))
                {
                    print("Host");
                    _started = true;
                    NetworkManager.singleton.networkPort = int.Parse(Port);
                    NetworkManager.singleton.StartHost();
                    Manager.name = name;
                    Manager.atualizaNome(name);
                    Manager.startServerTCP();
                }
               
                IpAddress = GUILayout.TextField(IpAddress, myStyle);
                name = GUILayout.TextField(name, myStyle);
                //Port = GUILayout.TextField(Port, 5);
                if (GUILayout.Button("Connect", myStyle))
                {
                    _started = true;
                    NetworkManager.singleton.networkAddress = IpAddress;
                    NetworkManager.singleton.networkPort = int.Parse(Port);
                    NetworkManager.singleton.StartClient();
                    Manager.name = name;
                    Manager.atualizaNome(name);
                }
            }
            else
            {
                if (GUILayout.Button("Disconnect", myStyle))
                {
                    _started = false;
                    NetworkManager.singleton.StopHost();
                }
            }
        }
    }
}