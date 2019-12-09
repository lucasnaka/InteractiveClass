using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadController : MonoBehaviour
{
    public static GameObject camera;
    CodeLock codeLock;

    int reachRange = 100;
    void Start()
    {
        codeLock = new CodeLock();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckHitObj();
        }
    }
    void CheckHitObj()
    {
        RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(camera && Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, reachRange))
        {
            if (hit.collider.gameObject.name == "0" ||
                hit.collider.gameObject.name == "1" ||
                hit.collider.gameObject.name == "2" ||
                hit.collider.gameObject.name == "3" ||
                hit.collider.gameObject.name == "4" ||
                hit.collider.gameObject.name == "5" ||
                hit.collider.gameObject.name == "6" ||
                hit.collider.gameObject.name == "7" ||
                hit.collider.gameObject.name == "8" ||
                hit.collider.gameObject.name == "9")
            {
                string value = hit.collider.gameObject.name;
                codeLock.SetValue(value);
            }
                
                //codeLock = hit.transform.gameObject.GetComponentInParent<CodeLock>();

            //if (codeLock != null)
            //{
            //    string value = hit.transform.name;
            //    codeLock.SetValue(value);
            //}
        }
    }
}
