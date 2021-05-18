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
    public GameObject linterna1;
    public GameObject linterna2;
    [SerializeField] GameObject linternaModelo;
    bool linternaSacada = true; // Para activar o desactivar el modelo.
    FirstPersonAIO scriptPlayer;
    Animator animPlayer;
    private void Start()
    {
        animPlayer = scriptPlayer.gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        Debug.Log(bateria);
        if (encendido)
        {
            bateria -= 1 * Time.deltaTime;

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!encendido && bateria > batMinima && linternaSacada)
            {
                Debug.Log("Enciendo");
                linterna1.SetActive(true);
                linterna2.SetActive(true);
                
                encendido = true;
            }
            else if (encendido && linternaSacada)
            {
                Debug.Log("Apago");
                linterna1.SetActive(false);
                linterna2.SetActive(false);
                
                encendido = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!linternaSacada)
            {
                linternaModelo.SetActive(true);                              
            }
            else if (linternaSacada)
            {
                linternaModelo.SetActive(false);
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
        
    }
}
