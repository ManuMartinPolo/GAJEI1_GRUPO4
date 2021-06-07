using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    CharacterController controller;
    float gravedad = -35;
    float velocidad = 6;
    Vector3 movimientoY;
    float alturaSalto = 2.5f;
    float z; // Declaramos la variable arriba para que podamos usarla en el if del sprint.
    [SerializeField] LayerMask esSaltable,esInteractuable;
    Animator anim;
    GameObject camara;
    AudioSource source;
    [SerializeField] GameObject nota;
    [SerializeField] Transform manoIzqpos;
    [SerializeField] Text bateriaTachado,combustibleTachado,ruedaTachado,colocadaTachado;
    [SerializeField] AudioClip [] clips;
    bool ruedaEncontrada;

    void Start()
    {
        anim = GetComponent<Animator>(); //Coges el animator habiendole declarado antes
        controller = GetComponent<CharacterController>();
        camara = Camera.main.gameObject;     //Forma para coger el componente de la cámara
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        if (Input.GetKeyDown(KeyCode.I))
        {
            source.clip = clips[0];
            source.Play();
            nota.SetActive(!nota.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.OverlapSphere(camara.transform.position + camara.transform.forward, 2f,esInteractuable).Length > 0)
            {
                anim.SetTrigger("pick");
            }
        }
        
    }
    void Movimiento()
    {
        z = Input.GetAxisRaw("Vertical"); // Nos movemos en Z, no en Y (Si no nos iriamos para arriba).
        float x = Input.GetAxisRaw("Horizontal");
        //transform.Translate(new Vector3(x, 0, z).normalized * velocidad * Time.deltaTime, Space.Self);

        Vector3 direccion = transform.forward * z + transform.right * x; // Tienes que decirle en que dos ejes se va a mover. x 

        controller.Move(direccion * velocidad * Time.deltaTime);

        movimientoY.y += gravedad * Time.deltaTime; //Para acumularlo en Y   .y Por Time.deltaTime para que no se acumule por frame.

        controller.Move(movimientoY * Time.deltaTime); //Meto el vector que me hace bajar/subir en mi controller. Se multiplica 2 veces por Time.deltaTime para que sea al cuadrado m/segundo al cuadrado



        //Para acumular fuerza de salto mientras mantienes pulsado
        //Si levanto el espacio y el overlap en los pies toca algo "esSaltable" puede saltar.
        if (Input.GetKey(KeyCode.Space))
        {
            alturaSalto += 3f * Time.deltaTime;
            if (alturaSalto > 20) // Para limitarla.
            {
                alturaSalto = 20;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && Physics.OverlapSphere(transform.position + new Vector3(0, -1.5f, 0), 1f, esSaltable).Length > 0) //El metodo de devuelve un array de todos los collider que toque el overlap entonces con . length >0 dice que al menos un collider que toca es saltable y puede saltar.
        {
            movimientoY.y = Mathf.Sqrt(alturaSalto * (-2 * gravedad));   // La formula es RaizCuadrada de h * (-2 * gravedad)
                                                                         //Sqrt = Raiz Cuadrada
            alturaSalto = 2.5f;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidad = 9f;
            anim.SetBool("running", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            velocidad = 6f;
            anim.SetBool("running", false);
        }
        
        
       
    }
    void PickOverLap ()
    {
        Collider[] colls = Physics.OverlapSphere(manoIzqpos.position, 3f, esInteractuable);
        if (colls.Length > 0)
        {
            if (colls[0].gameObject.CompareTag("BateriaCoche"))
            {
                Destroy(colls[0].gameObject);
                bateriaTachado.gameObject.SetActive(true);             
            }
            if (colls[0].gameObject.CompareTag("Combustible"))
            {
                Destroy(colls[0].gameObject);
                combustibleTachado.gameObject.SetActive(true);
            }
            if (colls[0].gameObject.CompareTag("Rueda"))
            {
                Destroy(colls[0].gameObject);
                ruedaTachado.gameObject.SetActive(true);
                ruedaEncontrada = true;
            }
            if (colls[0].gameObject.CompareTag("RuedaActivar") && ruedaEncontrada)
            {
                colls[0].gameObject.GetComponent<MeshRenderer>().enabled = true;
                colocadaTachado.gameObject.SetActive(true);
            }
        }
        
    }
  
   
}
