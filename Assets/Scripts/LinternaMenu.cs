using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinternaMenu : MonoBehaviour
{
    Light luz;
    // Start is called before the first frame update
    void Start()
    {
        luz = GetComponent<Light>();
        StartCoroutine(VariacionLuz());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator VariacionLuz ()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            luz.intensity = Random.Range(5f, 6.8f);
        }
    }
}
