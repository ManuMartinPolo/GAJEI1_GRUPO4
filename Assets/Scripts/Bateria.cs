using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bateria : MonoBehaviour
{
    [SerializeField] float bateriaRecarga = 120f;
    Animator anim;
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim = other.GetComponent<Animator>();
            anim.SetTrigger("recarga");
            Linterna.bateria += bateriaRecarga;
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0, 70f, 0) * Time.deltaTime);
    }

}
