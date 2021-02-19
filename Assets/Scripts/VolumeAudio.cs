using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeAudio : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; //R�f�rence � mon audioMixer
    [SerializeField] private string nameVol; //Nom du parametre dans mon audioMixer
    [SerializeField] private Slider slider; //R�f�rence au slider

    public void SetVolume(float volume)
    {
        volume = slider.value;
        audioMixer.SetFloat(nameVol, volume); //Attribue a la valeur nameParam la valeur de la variable volume
    }
}
