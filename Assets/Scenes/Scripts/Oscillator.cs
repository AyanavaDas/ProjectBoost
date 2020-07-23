using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [Range(0,1)] float movementFactor=0f;
    [SerializeField] float timePeriod=4f;
    Vector3 initPosition;
    void Start()
    {
        initPosition = transform.position;

    }

   
    void Update()
    {
        if (timePeriod <= Mathf.Epsilon)
            return;

        float frequency = Time.time / timePeriod;
        //1/2(sin x +1) transform added//
        movementFactor = (Mathf.Sin((2 * Mathf.PI) * frequency)+1)/2f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = initPosition + offset;


    }
}
