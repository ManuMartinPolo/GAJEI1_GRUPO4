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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navegador = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navegador.remainingDistance <= distanciaDeteccion && !navegador.pathPending)
        {
            navegador.SetDestination(player.transform.position);
            navegador.speed = 6f;
            Debug.Log("Te encontré");
        }
        else if (navegador.remainingDistance > distanciaDeteccion && !navegador.pathPending)
        {
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
