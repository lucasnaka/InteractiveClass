using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public static string name = "Client_Host";
    public static GameObject objetoRecebido;
    public static GameObject camera;
    public static GameObject player;
    public static Animator m_Animator;
    static Text HudName;
    public static bool VRAplication = false;
    public static GameObject mobileHUD;
    public static TCPServer serverTCP;
    public static TCPClient clientTCP;

    public static Dictionary<string, bool> objectsToCatch = new Dictionary<string, bool>();

    public static void startServerTCP() {
        serverTCP = new TCPServer();
        serverTCP.setStart();
    }

    void Start()
    {
        objectsToCatch.Add("Extintor1", false);
        objectsToCatch.Add("Extintor2", false);
        objectsToCatch.Add("ChestKey", false);
        objectsToCatch.Add("crowbarOld1", false);
        objectsToCatch.Add("crowbarOld2", false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void atualizaNome(string name1)
    {
        HudName = GameObject.FindGameObjectWithTag("Name").GetComponent<Text>();
        HudName.text = name1;

    }

    public static void escondeObjeto(string tagObjeto)
    {

        objetoRecebido = GameObject.FindGameObjectWithTag(tagObjeto);
        if (objetoRecebido != null)
            objetoRecebido.SetActive(false);

    }

    public static void toggleMobileHUD()
    {
        mobileHUD.SetActive(!mobileHUD.activeSelf);


    }
}
