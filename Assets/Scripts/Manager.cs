using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Manager : NetworkBehaviour
{
    // Start is called before the first frame update
    public static string name = "Client_Host";
    public static GameObject objetoRecebido;
    static Text HudName;
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
        objetoRecebido.SetActive(false);

    }
}
