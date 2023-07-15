using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSounds : MonoBehaviour
{
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        //Get the audio source
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //As soon as he stopped playing destroy the gameobject
        if (!source.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}