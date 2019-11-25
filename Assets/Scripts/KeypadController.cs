using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadController : MonoBehaviour
{

    CodeLock codeLock;

    int reachRange = 100;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CheckHitObj();
        }
    }

    void CheckHitObj()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out hit, reachRange))
        {
            codeLock = hit.transform.gameObject.GetComponentInParent<CodeLock>();

            if(codeLock != null)
            {
                string value = hit.transform.name;
                codeLock.SetValue(value);
            }
        }
    }

    /* private Rigidbody target;
    private bool isActive = false;

    private void Attract() {
        target.isKinematic = true; // desativa a fisica
        target.MovePosition(transform.position + transform.forward);
    }

    public void RaybeamStop()
    {
        isActive = false;
        target = null;
    }

    public void RaybeamStart()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast (transform.position,
                            transform.forward,
                            out hitInfo, reachRange)) {
            Debug.DrawRay (transform.position,
                           transform.forward * hitInfo.distance,
                           Color.green);
            if(hitInfo.collider.attachedRigidBody != null){
                target = hitInfo.collider.attachedRigidBody;
                isActive = true;
            }
        }
    }

    void Update(){
        if(Input.GetKeyDown("space")){
            RaybeamStart();
        }
        if(Input.GetKeyUp("space")){
            RaybeamStop();
        }
        if(isActive){
            Attract();
        }
    } */
}
