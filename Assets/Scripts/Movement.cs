using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Movement : NetworkBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update

    public float shiftSpeed = 16.0f;
    public bool showInstructions = true;
    public GameObject player;
    bool extintor1_pego = false;
    bool buttonHoldDown = false;
    bool paraFrente = true;
    private Vector3 startEulerAngles;
    private Vector3 startMousePosition;
    private float realTime;

    public float speed = 1.1f;
    public Animator m_Animator;
    GameObject camera;
    GameObject cameraVR;

    public UnityEvent holdButtonDown;
    public UnityEvent holdButtonUp;
    void Start()
    {
        if (isLocalPlayer)
        {
            
            Manager.cameraVR = GameObject.FindGameObjectWithTag("MainCameraVR");           
            camera = GameObject.FindGameObjectWithTag("MainCamera");            
           
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            player = players[players.Length -1] ;
            Manager.atualizaNome(Manager.name);
            Manager.camera = camera;
            Manager.player = player;
            Manager.m_Animator = player.transform.GetChild(0).gameObject.GetComponent<Animator>();
        }
        
    }


    void Update()
    {
        if (Manager.player != null && Manager.camera != null)
        {
            if (Manager.VRAplication)
            {
                Manager.camera.transform.position = new Vector3(Manager.player.transform.position.x, Manager.player.transform.position.y, Manager.player.transform.position.z);
                Manager.player.transform.eulerAngles = new Vector3(Manager.player.transform.eulerAngles.x, Manager.cameraVR.transform.eulerAngles.y, Manager.player.transform.eulerAngles.z);
            }
            else
            {
                Manager.camera.transform.position = new Vector3(Manager.player.transform.position.x, Manager.player.transform.position.y + 2.1f, Manager.player.transform.position.z);
                Manager.camera.transform.rotation = Manager.player.transform.rotation;
            }
            
            
            Vector3 pos = Manager.player.transform.position;
            Vector3 vetor;
            if (Manager.VRAplication) {
                 vetor = new Vector3(Manager.cameraVR.gameObject.transform.forward.x, 0, Manager.cameraVR.gameObject.transform.forward.z);

            }
            else
            {
                vetor = new Vector3(Manager.camera.transform.forward.x, 0, Manager.camera.transform.forward.z);

            }
            Manager.m_Animator.SetFloat("walk", 0);
            if (buttonHoldDown)
            {
                if (paraFrente)
                {
                     Manager.m_Animator.SetFloat("walk", 1);
                    pos += vetor * speed * Time.deltaTime;
                    Manager.player.transform.position = pos;

                }
                else
                {
                     Manager.m_Animator.SetFloat("walk", 1);
                    pos -= vetor * speed * Time.deltaTime;
                    Manager.player.transform.position = pos;

                }


            }
            //Verifica se é uma versao em Realidade virtual/Desktop
            //if (Manager.VRAplication)
           // {
                if (Input.GetKey("w"))
                {
                    Manager.m_Animator.SetFloat("walk", 1);
                    pos += vetor * speed * Time.deltaTime;
                    Manager.player.transform.position = pos;
                }
                if (Input.GetKey("s"))
                {
                    Manager.m_Animator.SetFloat("walk", 1);
                    pos -= vetor * speed * Time.deltaTime;
                    Manager.player.transform.position = pos;
                }
                if (Input.GetKey("d"))
                {
                    Manager.m_Animator.SetFloat("walk", 1);

                    pos += Manager.camera.transform.right * speed * Time.deltaTime;
                    Manager.player.transform.position = pos;
                }
                if (Input.GetKey("a"))
                {
                    Manager.m_Animator.SetFloat("walk", 1);
                    //teste
                    pos -= Manager.camera.transform.right * speed * Time.deltaTime;
                    Manager.player.transform.position = pos;
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

                Manager.player.transform.position += Manager.player.transform.TransformDirection(delta);

                Vector3 mousePosition = Input.mousePosition;

                if (Input.GetMouseButtonDown(1) /* right mouse */)
                {
                    startMousePosition = mousePosition;
                    startEulerAngles = Manager.player.transform.localEulerAngles;

                }

                if (Input.GetMouseButton(1) /* right mouse */)
                {
                    Vector3 offset = mousePosition - startMousePosition;
                    Manager.camera.transform.localEulerAngles = startEulerAngles + new Vector3(-offset.y * 360.0f / Screen.height, offset.x * 360.0f / Screen.width, 0.0f);
                    Manager.player.transform.localEulerAngles = startEulerAngles + new Vector3(0.0f, offset.x * 360.0f / Screen.width, 0.0f);
            }
           // }
           // else
           // {
                //Se nao uma versao em realidadeVirtual/Desktop entao é mobile
              //  GyroModifyCamera();
           // }
        }

    }

    void GyroModifyCamera()
    {
        transform.rotation = GyroToUnity(Input.gyro.attitude);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }


    public void goForward()
    {
        paraFrente = true;

    }

    public void goBack()
    {
        paraFrente = false;

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        buttonHoldDown = true;

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        buttonHoldDown = false;

    }



}
