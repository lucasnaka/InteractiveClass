﻿using System.Collections;
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
 
    float waitTime = 3.0f;
    float timeStamp = Mathf.Infinity;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            timeStamp = Time.time + waitTime;
        }

        if(Input.GetMouseButtonUp(0))
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
    }
}
