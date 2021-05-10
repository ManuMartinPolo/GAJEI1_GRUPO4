using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController controller;
    float gravedad = -35;
    int velocidad = 5;
    Vector3 movimientoY;
    float alturaSalto = 2.5f;
    float z; // Declaramos la variable arriba para que podamos usarla en el if del sprint.
    [SerializeField] LayerMask esSaltable;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
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
    }
}
