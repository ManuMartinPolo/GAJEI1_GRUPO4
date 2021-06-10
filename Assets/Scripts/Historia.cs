using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Historia : MonoBehaviour
{
    float timer;
    AudioSource source;
    [SerializeField] AudioClip[] clips;
    bool intro;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * 40 * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >=5 && !intro)
        {
            intro = true;
            source.clip = clips[0];
            source.Play();
        }
        if (timer >= 75f)
        {
            SceneManager.LoadScene("EscenaManu");
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("EscenaManu");
        }
    }

}
