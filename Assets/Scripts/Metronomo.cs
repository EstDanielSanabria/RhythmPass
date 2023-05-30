using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronomo : MonoBehaviour
{
    public float bpm = 200f;  // Define el tempo en BPM (beats por minuto).
    public int beats = 0;
    private float beatInterval;  // Intervalo de tiempo entre pulsos.
    public float nextBeatTime;  // Tiempo para el próximo pulso.

    public GameObject indicator;
    public Transform center;

    private AudioSource audioSource;
    [SerializeField] AuthController controller;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        center = GameObject.Find("Center").transform;
        controller = GameObject.Find("GameManager").GetComponent<AuthController>();
        CalculateBeatInterval();
        StartMetronome();
    }

    private void Update()
    {
        if (Time.time >= nextBeatTime && controller.canRecord)
        {
            if (controller.playsPerBeat == 0)
            {
                double width = indicator.GetComponent<Renderer>().bounds.size.x;
                Vector3 position = new Vector3(1 - (float)((width + 0.1) * center.childCount), 2, 3);
                Instantiate(
                    indicator,
                    position,
                    Quaternion.identity,
                    center
                );
                controller.strings.Add(gameObject.name);
            }
            PlayBeatSound();
            nextBeatTime += beatInterval;
            controller.playsPerBeat = 0;
        }
    }

    public void CalculateBeatInterval()
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

    private void Reset()
    {
        nextBeatTime= Time.time + 2*beatInterval;
    }

}
