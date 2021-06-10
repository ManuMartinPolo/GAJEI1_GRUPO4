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
    AudioSource source,sourceAmbiente;
    [SerializeField] GameObject nota;
    [SerializeField] Transform manoIzqpos;
    [SerializeField] Text bateriaTachado,combustibleTachado,ruedaTachado,colocadaTachado, bateriaColocadaTachado, llaveTachado, herramientasTachado,textWin;
    [SerializeField] AudioClip [] clips;
    bool ruedaEncontrada,bateriaEncontrada;
    [SerializeField] GameObject sonidoAmbiente,shader,shaderBateria,shaderRadio, shaderRuedaVacia,shaderLlave,shaderCombustible,shaderRueda,shaderBateriaHueco,shaderHerramientas,shaderPuerta;
    [SerializeField] float bateriaRecarga = 120f;
    [SerializeField] Linterna scriptLinterna;
    [SerializeField] Personajo scriptRadio;
    public int vidas = 3;
    [SerializeField] Image sangreDer, sangreIzq;
    [SerializeField] Animator animFadeOut,animMuerte;
    Player script;
    [SerializeField] Malo scriptMalo;
    int objetos;
    


    void Start()
    {
        anim = GetComponent<Animator>(); //Coges el animator habiendole declarado antes
        controller = GetComponent<CharacterController>();
        camara = Camera.main.gameObject;     //Forma para coger el componente de la cámara
        source = GetComponent<AudioSource>();
        script = GetComponent<Player>();
        sourceAmbiente = sonidoAmbiente.GetComponent<AudioSource>();                
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(scriptMalo.distanciaDeteccion);
        Debug.Log(objetos);
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
        if (objetos == 1)
        {
            scriptMalo.distanciaDeteccion = 60f;
        }
        else if (objetos == 2)
        {
            scriptMalo.distanciaDeteccion = 80f;
        }
        else if (objetos == 3)
        {
            scriptMalo.distanciaDeteccion = 100f;
        }
        else if (objetos == 4)
        {
            scriptMalo.distanciaDeteccion = 120f;
        }
        else if (objetos == 5)
        {
            scriptMalo.distanciaDeteccion = 140f;
        }
        else if (objetos == 6)
        {
            scriptMalo.distanciaDeteccion = 300f;
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
            velocidad = 10f;
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
                bateriaEncontrada = true;
            }
            if (colls[0].gameObject.CompareTag("Combustible"))
            {
                Destroy(colls[0].gameObject);
                combustibleTachado.gameObject.SetActive(true);
                objetos++;
            }
            if (colls[0].gameObject.CompareTag("Rueda"))
            {
                Destroy(colls[0].gameObject);
                ruedaTachado.gameObject.SetActive(true);
                ruedaEncontrada = true;
                objetos++;
            }
            if (colls[0].gameObject.CompareTag("RuedaActivar") && ruedaEncontrada)
            {
                colls[0].gameObject.GetComponent<MeshRenderer>().enabled = true;
                colocadaTachado.gameObject.SetActive(true);
                objetos++;
            }
            if (colls[0].gameObject.CompareTag("Bateria"))
            {               
                anim.SetTrigger("recarga");
                Linterna.bateria += bateriaRecarga;
                Destroy(colls[0].gameObject);
            }
            if (colls[0].gameObject.CompareTag("PonerBateria") && bateriaEncontrada)
            {
                colls[0].gameObject.GetComponent<MeshRenderer>().enabled = true;
                bateriaColocadaTachado.gameObject.SetActive(true);
                objetos++;
            }
            if (colls[0].gameObject.CompareTag("Radio"))
            {
                AudioSource audioRadio = colls[0].gameObject.GetComponent<AudioSource>();
                audioRadio.clip = scriptRadio.clips[0];
                audioRadio.Play();                
            }
            if (colls[0].gameObject.CompareTag("Llave"))
            {
                Destroy(colls[0].gameObject);
                llaveTachado.gameObject.SetActive(true);
                objetos++;
            }
            if (colls[0].gameObject.CompareTag("Herramientas"))
            {
                Destroy(colls[0].gameObject);
                herramientasTachado.gameObject.SetActive(true);
                objetos++;
            }
            
            if (colls[0].gameObject.CompareTag("Puerta") && objetos == 6)
            {                                
                shaderPuerta.SetActive(false);
                source.clip = clips[2];
                source.Play();
                animFadeOut.SetTrigger("fadeout");
                textWin.gameObject.SetActive(true);
                sourceAmbiente.Stop();
                scriptMalo.enabled = false;
                
            }

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shader"))
        {
            other.transform.parent.GetChild(0).gameObject.SetActive(true);

        }
        if (other.gameObject.CompareTag("ShaderBateriaCoche"))
        {
            shaderBateria.SetActive(true);
        }
        if (other.gameObject.CompareTag("ShaderRadio"))
        {
            shaderRadio.SetActive(true);
        }
        if (other.gameObject.CompareTag("AreaRuedaVacia"))
        {
            shaderRuedaVacia.SetActive(true);
        }
        if (other.gameObject.CompareTag("AreaLlave"))
        {
            shaderLlave.SetActive(true);
        }
        if (other.gameObject.CompareTag("AreaCombustible"))
        {
            shaderCombustible.SetActive(true);
        }
        if (other.gameObject.CompareTag("AreaRueda"))
        {
            shaderRueda.SetActive(true);
        }
        if (other.gameObject.CompareTag("PonerBateriaArea"))
        {
            shaderBateriaHueco.SetActive(true);
        }
        if (other.gameObject.CompareTag("AreaHerramientas"))
        {
            shaderHerramientas.SetActive(true);
        }
        
        if (objetos == 6 && other.gameObject.CompareTag("AreaPuerta"))
        {
            shaderPuerta.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Shader"))
        {
            other.transform.parent.GetChild(0).gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("ShaderBateriaCoche"))
        {
            shaderBateria.SetActive(false);
        }
        if (other.gameObject.CompareTag("ShaderRadio"))
        {
            shaderRadio.SetActive(false);
        }
        if (other.gameObject.CompareTag("AreaRuedaVacia"))
        {
            shaderRuedaVacia.SetActive(false);
        }
        if (other.gameObject.CompareTag("AreaLlave"))
        {
            shaderLlave.SetActive(false);
        }
        if (other.gameObject.CompareTag("AreaCombustible"))
        {
            shaderCombustible.SetActive(false);
        }
        if (other.gameObject.CompareTag("AreaRueda"))
        {
            shaderRueda.SetActive(false);
        }
        if (other.gameObject.CompareTag("PonerBateriaArea"))
        {
            shaderBateriaHueco.SetActive(false);
        }
        if (other.gameObject.CompareTag("AreaHerramientas"))
        {
            shaderHerramientas.SetActive(false);
        }
        
    }
    public void TakeDmg (int dmg)
    {
        if (vidas > 0)
        {
            vidas -= dmg;
            Debug.Log("golpe");
            source.clip = clips[1];
            source.Play();
            if (vidas == 2)
            {
                sangreDer.gameObject.SetActive(true);
            }
            if (vidas == 1)
            {
                sangreIzq.gameObject.SetActive(true);
            }

        }
        if (vidas <= 0)
        {
            animFadeOut.SetTrigger("fadeout");
            animMuerte.SetTrigger("muerte");
            script.enabled = false;
        }
        
    }


}
