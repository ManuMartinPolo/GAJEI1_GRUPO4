using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personajo : MonoBehaviour
{

    public AudioSource audiosoun;
    public AudioClip[] clips;
    

    
    void Start()
    {      
        audiosoun.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        


    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {

            
               audiosoun.enabled = true;

            //Bicho Grito
            //Bicho speed++ o algo de eso
        }
    }
}
    

