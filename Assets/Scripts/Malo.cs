using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Malo : MonoBehaviour
{
    NavMeshAgent navegador;
    GameObject player;
    bool ruta;
    public float distanciaDeteccion = 50f;
    Animator anim;
    [SerializeField] Transform posMano;
    [SerializeField] LayerMask esGolpeable;
    AudioSource source;
    [SerializeField] AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navegador = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        source.clip = clips[0];
        source.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        navegador.SetDestination(player.transform.position);
        if (!navegador.pathPending && navegador.remainingDistance <= distanciaDeteccion)
        {
            anim.SetBool("run", true);
            //navegador.SetDestination(player.transform.position);
            navegador.speed = 9f;
            Debug.Log("Te encontré");
        }
        else if (navegador.remainingDistance > distanciaDeteccion)
        {
            anim.SetBool("run", false);
            navegador.speed = 4f;
            // StartCoroutine(RutinaDeBusqueda());           
        }
        if (navegador.remainingDistance <= 6f && !navegador.pathPending)
        {
             Vector3 dirAlPlayer = (player.transform.position - transform.position).normalized;
             Quaternion rotacionAPlayer = Quaternion.LookRotation(dirAlPlayer, transform.up);
             transform.rotation = Quaternion.Slerp(transform.rotation, rotacionAPlayer, 15 * Time.deltaTime);
             navegador.isStopped = true;
             anim.SetBool("attack", true);
        }
    }
    void OnAttacking()
    {
        //El método overlap devuelve un array de colliders entonces le metemos en un variable array colliders
        Collider[] colls = Physics.OverlapSphere(posMano.position, 1f, esGolpeable);  //El ultimo valor esGolpeable hace referencia a la layer.

        if (colls.Length > 0) //Si es mayor que 0 es que has tocado al player porq solo el tiene la layer Player (que es la que esGolpeable se lo has dicho por el inspector)
        {
            Player playerScript = colls[0].gameObject.GetComponent<Player>(); //Cogemos el script player como si fuera un componente para llamar al método que le hace daño al player en su script
            playerScript.TakeDmg(1);
        }


    }
    void OnFinishZombieAttack() //Creamos el método.
    {
        if (navegador.remainingDistance > 2f) //Cálcula a cuanta distancia esta del player. Y si esta a rango no hace nada y si no se mete en el if y deja de atacar y vuelve a perseguir.
        {
            //1.Reinicio el movimiento 
            navegador.isStopped = false;
            //2. Dejo de atacar (vuelvo a correr)
            anim.SetBool("attack", false);

        }
    }

    IEnumerator RutinaDeBusqueda()
    {
        if (!ruta)
        {
            ruta = true;   
            Debug.Log("Busco");
            navegador.SetDestination(player.transform.position + new Vector3(Random.Range(10f, 20f), 0, Random.Range(10f, 20f) ) );
            yield return new WaitForSeconds(2f);
            ruta = false;
        }
       
    }
    
}
