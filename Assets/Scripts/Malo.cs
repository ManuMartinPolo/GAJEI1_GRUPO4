using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Malo : MonoBehaviour
{
    NavMeshAgent navegador;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navegador = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navegador.SetDestination(player.transform.position);
    }
}
