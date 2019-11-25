using UnityEngine;
using UnityEngine.Networking;

namespace CustomHud
{
    public class CustomHud : MonoBehaviour
    {
        private string IpAddress = "localhost";
        private string Port = "7777";
        public float GuiOffset;
        private bool _started;

        public void Start()
        {
            _started = false;
            

        }
        public void OnGUI()
        {
            GUILayout.Space(GuiOffset);
          ;
           
            if (!_started)
            {
                if (GUILayout.Button("Host", GUILayout.Width(300)))
                {
                    print("Host");
                    _started = true;
                    NetworkManager.singleton.networkPort = int.Parse(Port);
                    NetworkManager.singleton.StartHost();
                }
                GUIStyle myStyle = new GUIStyle(GUI.skin.button);
                myStyle.fontSize = 50;
                GUILayout.Space(25);
                IpAddress = GUILayout.TextField(IpAddress, myStyle);
                //Port = GUILayout.TextField(Port, 5);
                if (GUILayout.Button("Connect", GUILayout.Width(300)))
                {
                    _started = true;
                    NetworkManager.singleton.networkAddress = IpAddress;
                    NetworkManager.singleton.networkPort = int.Parse(Port);
                    NetworkManager.singleton.StartClient();
                }
            }
            else
            {
                if (GUILayout.Button("Disconnect", GUILayout.Width(300)))
                {
                    _started = false;
                    NetworkManager.singleton.StopHost();
                }
            }
        }
    }
}