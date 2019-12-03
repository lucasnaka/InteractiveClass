using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        Manager.atualizaNome(Manager.name);
    }

    public float shiftSpeed = 16.0f;
    public bool showInstructions = true;
    bool extintor1_pego = false;
    
    

    private Vector3 startEulerAngles;
    private Vector3 startMousePosition;
    private float realTime;

    public float speed = 1.1f;
    public Animator m_Animator;
    GameObject camera;
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        camera.transform.position = new Vector3(transform.position.x, transform.position.y+2.1f, transform.position.z); 
        camera.transform.rotation = transform.rotation;
        Vector3 pos = transform.position;
        Vector3 vetor = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z);
        m_Animator.SetFloat("walk", 0);
        if (Input.GetKey("w"))
        {
            m_Animator.SetFloat("walk", 1);
            pos += vetor * speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            m_Animator.SetFloat("walk", 1);
            pos -= vetor * speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            m_Animator.SetFloat("walk", 1);

            pos += camera.transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            m_Animator.SetFloat("walk", 1);
            //teste
            pos -= camera.transform.right * speed * Time.deltaTime;
        }
        float right = 0.0f;
        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = shiftSpeed;
        }

        float forward = 0.0f;
        float realTimeNow = Time.realtimeSinceStartup;
        float deltaRealTime = realTimeNow - realTime;
        realTime = realTimeNow;

        Vector3 delta = new Vector3(right, 0.0f, forward) * currentSpeed * deltaRealTime;

        transform.position += transform.TransformDirection(delta);

        Vector3 mousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(1) /* right mouse */)
        {
            startMousePosition = mousePosition;
            startEulerAngles = transform.localEulerAngles;
        }

        if (Input.GetMouseButton(1) /* right mouse */)
        {
            Vector3 offset = mousePosition - startMousePosition;
            transform.localEulerAngles = startEulerAngles + new Vector3(-offset.y * 360.0f / Screen.height, offset.x * 360.0f / Screen.width, 0.0f);
        }


        transform.position = pos;
    }
}
