using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionCamara : MonoBehaviour
{
    float timer;
    float sentido = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.Rotate(new Vector3(sentido, 0, 0) * 30 * Time.deltaTime);
        if (timer > 2)
        {
            sentido = (-1) * sentido;
            timer = 0;
        }
    }
}
