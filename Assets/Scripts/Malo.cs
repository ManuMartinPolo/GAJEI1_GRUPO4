using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Malo : MonoBehaviour
{
    NavMeshAgent navegador;
    GameObject player;
    bool ruta;
    float distanciaDeteccion = 25f;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navegador = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navegador.remainingDistance <=3 && !navegador.pathPending)
        {
            navegador.isStopped = true;
            

        }
        if (navegador.remainingDistance <= distanciaDeteccion && !navegador.pathPending)
        {
            navegador.SetDestination(player.transform.position);
            navegador.isStopped = false;
            navegador.speed = 6.5f;
            Debug.Log("Te encontré");
        }
        else if (navegador.remainingDistance > distanciaDeteccion && !navegador.pathPending)
        {
            navegador.isStopped = false;
            navegador.speed = 3.5f;
            StartCoroutine(RutinaDeBusqueda());

           
        }
    }

    IEnumerator RutinaDeBusqueda()
    {
        if (!ruta)
        {
            ruta = true;   
            Debug.Log("Busco");
            navegador.SetDestination(player.transform.position + new Vector3(Random.Range(10f, 40f), 0, Random.Range(10f, 40f) ) );
            yield return new WaitForSeconds(10f);
            ruta = false;
        }
       
    }
}
