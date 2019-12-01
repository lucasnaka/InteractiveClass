using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRay : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------------
    /* public ParticleSystem alpha;
    public ParticleSystem add;
    public ParticleSystem glow;
    public ParticleSystem sparks;

    void DiminuirIncendio()
    {
        var alpha_main = alpha.emission;
        float constMin = alpha_main.rate.constantMin;
        float constMax = alpha_main.rate.constantMax;
        alpha_main.rate = new ParticleSystem.MinMaxCurve(constMin/2, constMax/2);

        var add_main = add.emission;
        constMin = add_main.rate.constantMin;
        constMax = add_main.rate.constantMax;
        add_main.rate = new ParticleSystem.MinMaxCurve(constMin/2, constMax/2);

        var glow_main = glow.emission;
        constMin = glow_main.rate.constantMin;
        constMax = glow_main.rate.constantMax;
        glow_main.rate = new ParticleSystem.MinMaxCurve(constMin/2, constMax/2);

        var sparks_main = sparks.emission;
        constMin = sparks_main.rate.constantMin;
        constMax = sparks_main.rate.constantMax;
        sparks_main.rate = new ParticleSystem.MinMaxCurve(constMin/2, constMax/2);
    }

    void ApagarFogo()
    {
        var alpha_main = alpha.emission;
        alpha_main.rate = 0;

        var add_main = add.emission;
        add_main.rate = 0;

        var glow_main = glow.emission;
        glow_main.rate = 0;

        var sparks_main = sparks.emission;
        sparks_main.rate = 0;
    }

    float waitTime = 3.0f;
    float timeStamp = Mathf.Infinity;

    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            timeStamp = Time.time + waitTime;
        }

        if(Input.GetKeyUp("space"))
        {
            timeStamp = Mathf.Infinity;
        }

        if (Time.time >= timeStamp) {
            CheckMouseClick();
            timeStamp = Time.time + waitTime;
        }
    }

    int finishFire = 0;

    void CheckMouseClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out hit, reachRange))
        {
            finishFire++;
            if(finishFire == 3)
                ApagarFogo();
            else
                DiminuirIncendio();
        }
    }*/

    // --------------------------------------------------------------------------------------------------------

    int reachRange = 100;
    public GameObject mao_direita;

    void Start() {
        
    }

    public static bool com_extintor1 = false;
    public static bool com_extintor2 = false;

    public void RaybeamStart()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast (transform.position, transform.forward, out hitInfo, reachRange)) {
            //Debug.DrawRay (transform.position, transform.forward * hitInfo.distance, Color.green);
            if(hitInfo.collider.gameObject.name == "Extintor1" && !com_extintor2)
            {
                //hitInfo.collider.gameObject.GetComponent<Rigidbody>().isKinematic  = false;
                hitInfo.collider.gameObject.transform.parent = mao_direita.transform;
                hitInfo.collider.gameObject.transform.localPosition = new Vector3(0,0,0);
                com_extintor1 = true;
            }

            if (hitInfo.collider.gameObject.name == "Extintor2" && !com_extintor1)
            {
                //hitInfo.collider.gameObject.GetComponent<Rigidbody>().isKinematic  = false;
                hitInfo.collider.gameObject.transform.parent = mao_direita.transform;
                hitInfo.collider.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                com_extintor2 = true;
            }
        }
    }

    public GameObject extintor1;
    public GameObject extintor2;

    void Update(){
        if(Input.GetKeyDown("space")){
            if(mao_direita == null)
                mao_direita = GameObject.FindGameObjectWithTag("mao_direita");
            if(!com_extintor1 && !com_extintor2)
                RaybeamStart();
            else if(com_extintor1)
            {
                extintor1 = GameObject.Find("Extintor1");
                com_extintor1 = false;
                extintor1.transform.parent = null;
            }
            else if (com_extintor2)
            {
                extintor2 = GameObject.Find("Extintor2");
                com_extintor2 = false;
                extintor2.transform.parent = null;
            }
        }
    }
}
