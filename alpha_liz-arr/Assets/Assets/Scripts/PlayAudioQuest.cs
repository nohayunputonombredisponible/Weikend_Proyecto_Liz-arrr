using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioQuest : MonoBehaviour
{
    private AudioSource audio;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            audio.Play();
        }
    }

}

