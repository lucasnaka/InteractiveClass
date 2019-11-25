using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float smooth = 30.0f;

    // Update is called once per frame
    void Update()
    {
        if(hasAuthority == false){
            return;
        }
        
        // Rotation
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up * smooth * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(-Vector3.up * smooth * Time.deltaTime);
        

        // Translate
        if(Input.GetKeyDown(KeyCode.Space))
            this.transform.Translate(0, 2, 0);

        if ( Input.GetKeyDown(KeyCode.DownArrow) )
            this.transform.Translate(1, 0, 0);

        if ( Input.GetKeyDown(KeyCode.UpArrow) )
            this.transform.Translate(-1, 0, 0);

        if ( Input.GetKeyDown(KeyCode.RightArrow) )
            this.transform.Translate(0, 0, 1);

        if ( Input.GetKeyDown(KeyCode.LeftArrow) )
            this.transform.Translate(0, 0, -1);

        if(Input.GetKeyDown(KeyCode.F))
        {
            Destroy(gameObject);
        }
    }
}
