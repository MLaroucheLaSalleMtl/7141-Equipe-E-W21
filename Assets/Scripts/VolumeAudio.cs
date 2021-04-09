using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeAudio : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; //Référence à mon audioMixer
    [SerializeField] private string nameVol; //Nom du parametre dans mon audioMixer
    [SerializeField] private Slider slider; //Référence au slider

    public void SetVolume(float volume)
    {
        volume = slider.value;
        audioMixer.SetFloat(nameVol, volume); //Attribue a la valeur nameParam la valeur de la variable volume
        PlayerPrefs.SetFloat(nameVol, volume);
        PlayerPrefs.Save();
    }

    //void PlaySound()
    //{
    //    StartCoroutine("StopSound");
    //}

    //IEnumerator StopSound()
    //{
    //    while(slider.value < 0.1f)
    //    {
    //        yield return null;
    //    }

    //    yield return new WaitForSeconds(0.1f);
    //}

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(nameVol);
    }
}
