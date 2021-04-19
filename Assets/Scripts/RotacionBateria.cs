using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionBateria : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 70f, 0)* Time.deltaTime);
    }
}
