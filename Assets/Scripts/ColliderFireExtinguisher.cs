using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFireExtinguisher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        print("sjvbhahuvihbklj");
        if(collider.gameObject.name == "Gun"){
            print("----------- colidiram ---------");
        }
    }
}
