using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManagar : MonoBehaviour
{
    [SerializeField] GameObject menuOpciones;
    [SerializeField] GameObject panelOpciones;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnableOptions()
    {
        panelOpciones.SetActive(false);
        menuOpciones.SetActive(true);
    }

    public void AbleOptions()
    {
        panelOpciones.SetActive(true);
        menuOpciones.SetActive(false);
    }
}
