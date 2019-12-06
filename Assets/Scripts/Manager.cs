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
    public static GameObject cameraVR;
    public static GameObject player;
    public static Animator m_Animator;
    static Text HudName;
    public static bool VRAplication = true;
    public static GameObject mobileHUD;
    public static List<GameObject> playersInScene;
    public static List<string> playersInfoInScene;
    void Start()
    {

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
