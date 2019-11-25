using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishFire : MonoBehaviour
{
    public ParticleSystem alpha;
    public ParticleSystem add;
    public ParticleSystem glow;
    public ParticleSystem sparks;

    void ApagarFogo()
    {
        if(faseDoFogo == 4)
        {
            var alpha_main = alpha.main;
            alpha_main.loop = false;
        }
        
        if(faseDoFogo == 3)
        {
            var add_main = add.main;
            add_main.loop = false;
        }
        
        if(faseDoFogo == 2)
        {
            var glow_main = glow.main;
            glow_main.loop = false;
        }
        
        if(faseDoFogo == 1)
        {
            var sparks_main = sparks.main;
            sparks_main.loop = false;
        }
    }

    int reachRange = 100;
    int faseDoFogo = 1;

    void Update()
    {
        var waitTime = 5.0;
        var charge = 0.0;

        if(Input.GetMouseButtonDown(0))
        {
            charge += Time.deltaTime;
        }

        if(Input.GetMouseButtonUp(0))
        {
            charge = 0.0;
        }

        if (charge >= waitTime) {
            CheckHitObj();
            Debug.Log("Apagando o fogo");
            charge = 0.0;
            faseDoFogo++;
        }
    }

    void CheckHitObj()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out hit, reachRange))
        {
            ApagarFogo();
        }
    }
}
