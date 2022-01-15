using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTensionSound : MonoBehaviour
{
    // Start is called before the first frame update
    FadeOut fadeOut;
    AudioSource audioSource;
    bool played;
    public Canvas canvas;
    AudioSource musicAudioSource;
    void Start()
    {
        fadeOut = GetComponent<FadeOut>();
        audioSource = GetComponent<AudioSource>();
        musicAudioSource = canvas.GetComponent<AudioSource>();
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut.active && !played)
        {
            musicAudioSource.Stop();
            audioSource.PlayOneShot(audioSource.clip, 1f);
            played = true;
        }
    }
}
