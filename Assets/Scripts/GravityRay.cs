using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRay : MonoBehaviour
{
    int reachRange = 100;
    public GameObject mao_direita;

    Dictionary<string, bool> objectsToCatch = new Dictionary<string, bool>();

    void Start() {
        objectsToCatch.Add("Extintor1", false);
        objectsToCatch.Add("Extintor2", false);
        objectsToCatch.Add("ChestKey", false);
        objectsToCatch.Add("crowbarOld1", false);
        objectsToCatch.Add("crowbarOld2", false);
    }

    public void RaybeamStart()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast (transform.position, transform.forward, out hitInfo, reachRange)) {
            if(objectsToCatch.ContainsKey(hitInfo.collider.gameObject.name))
            {
                hitInfo.collider.gameObject.transform.parent = mao_direita.transform;
                hitInfo.collider.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                objectsToCatch[hitInfo.collider.gameObject.name] = true;
            }
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
        if(Input.GetKeyDown("space")){
            if (mao_direita == null)
            {
                objects = GameObject.FindGameObjectsWithTag("mao_direita");
                mao_direita = objects[objects.Length - 1];
            }
            if(!objectsToCatch.ContainsValue(true))
                RaybeamStart();
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
