using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    public void SetVolume (float volume)
    {
        mixer.SetFloat("volumenGeneral", volume);
    }

}
