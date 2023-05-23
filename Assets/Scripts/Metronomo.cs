using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronomo : MonoBehaviour
{
    public float bpm = 200f;  // Define el tempo en BPM (beats por minuto).
    public int beats = 0;
    private float beatInterval;  // Intervalo de tiempo entre pulsos.
    private float nextBeatTime;  // Tiempo para el próximo pulso.

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        CalculateBeatInterval();
        StartMetronome();
    }

    private void Update()
    {
        if (Time.time >= nextBeatTime)
        {
            PlayBeatSound();
            nextBeatTime += beatInterval;
            beats++;
        }
    }

    private void CalculateBeatInterval()
    {
        beatInterval = 60f / bpm;
    }

    private void StartMetronome()
    {
        nextBeatTime = Time.time + beatInterval;
    }

    private void PlayBeatSound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
