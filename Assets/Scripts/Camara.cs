using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    float sensibilidadX = 300;
    float sensibilidadY = 500;
    float camRotationX;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Para bloquear el raton en el centro de la escena y que no se vea.

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadX * Time.deltaTime; // Si movemos el raton en Y; lo que haremos sera cambiar rotación de 
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadY * Time.deltaTime;

        camRotationX -= mouseY;

        camRotationX = Mathf.Clamp(camRotationX, -90, 90); // Corrijo camRotationX para que siempre este entre 90 y -90

        transform.localRotation = Quaternion.Euler(camRotationX, 0, 0); //Aplico rotación local en el eje X.

        transform.parent.Rotate(new Vector3(0, 1, 0) * mouseX); //Roto al padre (cuerpo) con el movimiento del raton.
    }
}
