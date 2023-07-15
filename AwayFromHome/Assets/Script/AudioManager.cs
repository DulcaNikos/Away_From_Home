using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    //Sound Effects
    public AudioClip sfx_gotHurt, sfx_collectibles, sfx_enemy;

    //Sound Object
    public GameObject soundObject;

    void Awake()
    {
        audioManager = this;
    }

    public void PlaySFX(string sfx)
    {
        switch (sfx)
        {
            //In every case call SfxObjectCreation to do his things
            case "got_hurt":
                SfxObjectCreation(sfx_gotHurt);
                break;
            case "collectibles":
                SfxObjectCreation(sfx_collectibles);
                break;
            case "enemy":
                SfxObjectCreation(sfx_enemy);
                break;
            default:
                break;
        }
    }

    void SfxObjectCreation(AudioClip audioClip)
    {
        //Create new SoundObject 
        GameObject newObject = Instantiate(soundObject, transform);
        //Assign the audioclip to its audiosource
        newObject.GetComponent<AudioSource>().clip = audioClip;
        //Play the audio;
        newObject.GetComponent<AudioSource>().Play();
    }
}