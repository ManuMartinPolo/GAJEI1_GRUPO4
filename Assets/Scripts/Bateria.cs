using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bateria : MonoBehaviour
{
    
    
    private void Start()
    {
        
    }

    
    private void Update()
    {
        transform.Rotate(new Vector3(0, 70f, 0) * Time.deltaTime);
    }

}
