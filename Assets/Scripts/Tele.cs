using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tele : MonoBehaviour
{
    Light luz;
    // Start is called before the first frame update
    void Start()
    {
        luz = GetComponent<Light>();
        StartCoroutine(TileoLuz());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator TileoLuz()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            luz.intensity = Random.Range(0.7f, 0.9f);
        }
    }
}
