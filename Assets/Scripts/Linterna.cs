using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    bool encendido = false;
    static public float bateria = 180f; // 3 mins de bateria. Static y public para poder acceder a la variable bateria desde otro Script
    // En este caso la recogemos en el script de la bateria para que cuando cojas la recarga la bateria se iguale a la regarga.
    [SerializeField] float batMaxima = 300f;
    [SerializeField] float batMinima = 0f;
    [SerializeField] GameObject linterna1;
    [SerializeField] GameObject linterna2;
    [SerializeField] GameObject linternaModelo;
    bool linternaSacada = true; // Para activar o desactivar el modelo.
    FirstPersonAIO scriptPlayer;
    Animator animPlayer;
    [SerializeField] GameObject player;
    MeshRenderer modeloLinterna;
    // [SerializeField] float bateriaRecarga = 120f;
    [SerializeField] GameObject barra1;
    [SerializeField] GameObject barra2;
    [SerializeField] GameObject barra3;
    AudioSource source;
    [SerializeField] AudioClip[] clips;
    private void Start()
    {
        animPlayer = player.GetComponent<Animator>();
        modeloLinterna = gameObject.GetComponent<MeshRenderer>();
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        Debug.Log(bateria);
        if (encendido)
        {
            bateria -= 1 * Time.deltaTime;

        }
        else
        {
            linterna1.SetActive(false);
            linterna2.SetActive(false);
            encendido = false;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!encendido && bateria > batMinima && linternaSacada)
            {
                Debug.Log("Enciendo");
                linterna1.SetActive(true);
                linterna2.SetActive(true);
                animPlayer.SetTrigger("click");
                source.clip = clips[1];
                source.Play();
                
                encendido = true;
            }
            else if (encendido && linternaSacada)
            {
                Debug.Log("Apago");
                linterna1.SetActive(false);
                linterna2.SetActive(false);
                animPlayer.SetTrigger("click");
                source.clip = clips[1];
                source.Play();

                encendido = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!linternaSacada)
            {
                linternaSacada = true;
                modeloLinterna.enabled = true;
            }
            else
            {
                linternaSacada = false;
                modeloLinterna.enabled = false;
                encendido = false;                
            }
        }

        if (bateria <= batMinima && encendido)
        {
            linterna1.SetActive(false);
            linterna2.SetActive(false);
            encendido = false;
        }
        // Para que la bateria nunca pase del máximo, si coges una pila que da un minuto más (máximo 300s) y tenías 260s hace que no se pase de 300s.
        if (bateria >= batMaxima)
        {
            bateria = batMaxima;
        }
        // Para que la batéria nunca pase del mínimo 0, no se siga restando y tenga valores negativos.
        else if (bateria <= batMinima)
        {
            bateria = batMinima;
        }

        CanvasYSonido();
        
    }
    void CanvasYSonido()
    {
        if (bateria >= 200f) //Barra de bateria 3
        {
            barra3.SetActive(true);

        }
        else if (bateria < 200f)
        {
            
            barra3.SetActive(false);
        }

        if (bateria >= 100f) //Barra de bateria 2
        {
            barra2.SetActive(true);
        }
        else if (bateria < 100f)
        {
            
            barra2.SetActive(false);
        }

        if (bateria > 0f) //Barra de bateria 1
        {
            barra1.SetActive(true);
        }
        else if (bateria <= 0f)
        {
            source.clip = clips[0];
            source.Play();
            barra1.SetActive(false);
        }
    }
}
