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

    public static bool com_extintor1 = false;
    public static bool com_extintor2 = false;

    string[] objectsNames = new string[5]{"Extintor1", "Extintor2", "ChestKey", "crowbarOld1", "crowbarOld2" };

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

    void Update(){
        if(Input.GetKeyDown("space")){
            if (mao_direita == null)
            {
                mao_direita = GameObject.FindGameObjectWithTag("mao_direita");
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


    }
}
