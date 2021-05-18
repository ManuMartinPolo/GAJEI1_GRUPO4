using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bateria : MonoBehaviour
{
    [SerializeField] float bateriaRecarga = 120f;
    [SerializeField] GameObject barra1;
    [SerializeField] GameObject barra2;
    [SerializeField] GameObject barra3;
    [SerializeField] AudioSource bateriaBaja;
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
        if (Linterna.bateria >= 200f) //Barra de bateria 3
        {
            barra3.SetActive(true);
            
        }
        else if (Linterna.bateria < 200f)
        {
            bateriaBaja.Play();
            
            barra3.SetActive(false);
        }

        if (Linterna.bateria >= 100f) //Barra de bateria 2
        {
            barra2.SetActive(true);           
        }
        else if (Linterna.bateria < 100f)
        {
            bateriaBaja.Play();
            barra2.SetActive(false);
        }

        if (Linterna.bateria > 0f) //Barra de bateria 1
        {
            barra1.SetActive(true);
        }
        else if (Linterna.bateria <= 0f)
        {
            bateriaBaja.Play();
            barra1.SetActive(false);
        }

        
    }





}
