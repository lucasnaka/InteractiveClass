using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CodeLock : NetworkBehaviour
{
    int codeLength;
    int placeInCode;

    public string code = "1974";
    public string attemptedCode;
    public string message = "testando";

    public Transform toOpen1;
    public Transform toOpen2;

    public GameObject textFire;

    Animator animator1;
    Animator animator2;

    void Start()
    {
        placeInCode = 0;
        codeLength = code.Length;
        animator1 = toOpen1.GetComponent<Animator>();
        animator2 = toOpen2.GetComponent<Animator>();
        textFire = GameObject.Find("Keypad/TextFire");
        textFire.SetActive(false);
    }

    void CheckCode()
    {
        if(attemptedCode == code)
        {
            //StartCoroutine(Open());
            Open();
        }
        else
        {
            Debug.Log("Wrong Code");
        }
    }

    [Command]
    void CmdRotateDoor()
    {
        RpcRotateDoor();
    }

    [ClientRpc]
    void RpcRotateDoor()
    {
        animator1.SetInteger("Door1State", 1);
        animator2.SetInteger("Door2State", 1);
        textFire.SetActive(true);
    }

        //IEnumerator Open()
    void Open()
    {
        //animator1.SetInteger("Door1State", 1);
        //animator2.SetInteger("Door2State", 1);
        if (isServer)
            RpcRotateDoor();
        else
            CmdRotateDoor();

        //toOpen1.Rotate(new Vector3(0, 90, 0), Space.World);

        //toOpen2.Rotate(new Vector3(0, -90, 0), Space.World);
    }

    public void SetValue(string value)
    {
        placeInCode++;

        if(placeInCode <= codeLength)
        {
            attemptedCode += value;
        }

        if(placeInCode == codeLength)
        {
            CheckCode();

            attemptedCode = attemptedCode.Substring(1,3);
            placeInCode = codeLength - 1;
        }
        print("attemptedCode: " + attemptedCode);
    }

    [Command]
    void CmdEraseText()
    {
        RpcEraseText();
    }

    [ClientRpc]
    void RpcEraseText()
    {
        textFire.SetActive(false);
    }
    void Update()
    {
        if (ExtinguishFire.finishFire == 3)
        {
            if (isServer)
                RpcEraseText();
            else
                CmdEraseText();
        }
    }
}
