using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("chest/cap").GetComponent<Valve.VR.InteractionSystem.CircularDrive>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("chest/cap").GetComponent<Valve.VR.InteractionSystem.CircularDrive>().enabled && Manager.objectsToCatch["ChestKey"])
        {
            GameObject.Find("chest/cap").GetComponent<Valve.VR.InteractionSystem.CircularDrive>().enabled = true;
        }
    }
}
