using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishFire : MonoBehaviour
{
    public ParticleSystem alpha;
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

    int reachRange = 100;
 
    float waitTime = 4.0f;
    float timeStamp = 0.0f;

    void Update()
    {
        if (RayDetectFire() && GravityRay.com_extintor1 && GravityRay.com_extintor2)
        {
            timeStamp += Time.deltaTime;
        }

        if (!RayDetectFire() || !GravityRay.com_extintor1 || !GravityRay.com_extintor2)
        {
            timeStamp = 0;
        }

        if (timeStamp >= waitTime)
        {
            CheckMouseClick();
            timeStamp = 0;
        }
    }

    public bool RayDetectFire()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, reachRange))
        {
            //Debug.DrawRay(transform.position, transform.forward * hitInfo.distance, Color.green);
            if (hitInfo.collider.gameObject.name == "PS_Parent")
            {
                return true;
            }
        }
        return false;
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
    }
}
