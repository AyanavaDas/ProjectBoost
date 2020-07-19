using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        
    }

    private void ProcessInput()
    {

        if(Input.GetKey(KeyCode.Space))
        {
            
        }
        //can thrust and rotate
        if(Input.GetKey(KeyCode.A))
        {

        }
        else if(Input.GetKey(KeyCode.D))
        {

        }
    }
}
