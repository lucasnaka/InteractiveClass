using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenExitDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("PortaSaidaDireita").GetComponent<Valve.VR.InteractionSystem.CircularDrive>().enabled = false;
        GameObject.Find("PortaSaidaEsquerda").GetComponent<Valve.VR.InteractionSystem.CircularDrive>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("PortaSaidaDireita").GetComponent<Valve.VR.InteractionSystem.CircularDrive>().enabled && GravityRay.com_crowbarOld1 && GravityRay.com_crowbarOld2)
        {
            GameObject.Find("PortaSaidaDireita").GetComponent<Valve.VR.InteractionSystem.CircularDrive>().enabled = true;
        }

        if (!GameObject.Find("PortaSaidaEsquerda").GetComponent<Valve.VR.InteractionSystem.CircularDrive>().enabled && GravityRay.com_crowbarOld1 && GravityRay.com_crowbarOld2)
        {
            GameObject.Find("PortaSaidaEsquerda").GetComponent<Valve.VR.InteractionSystem.CircularDrive>().enabled = true;
        }
    }
}
