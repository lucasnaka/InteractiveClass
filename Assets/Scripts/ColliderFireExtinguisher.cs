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

    void OnTriggerStay(Collider collision){
        print("sjvbhahuvihbklj");
        if(collision.gameObject.name == "Extintor1"){
            print("----------- colidiram ---------");
        }
    }
}
