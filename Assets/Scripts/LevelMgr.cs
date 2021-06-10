using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelMgr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void CargaNivel(string EscenaManu)
    {
        SceneManager.LoadScene("Intro");
    }
}
