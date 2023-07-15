using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        //If there isnt any saved date , set the music volume to 1
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            //Otherwise we call the load function
            Load();
        }
    }

    void ChangleVolume()
    {
        //The volume of our game will be equals to the volume of our slider
        AudioListener.volume = volumeSlider.value;
        //We call it whenever the player changes the value of the slider  
        Save();
    }

    void Load()
    {
        //Setting the value of the slider to be equals to the value that has been stored 
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    void Save()
    {
        //Will save the value of volume
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
