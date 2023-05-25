using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRecord : MonoBehaviour
{

    [SerializeField] AuthController authController;
    public float time;
    Transform center;
    public AudioSource audioSource;
    public Metronomo metronomo;

    private void Start()
    {
        authController = GameObject.Find("GameManager").GetComponent<AuthController>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        center = GameObject.Find("Center").transform;
        metronomo = GameObject.Find("GameManager").GetComponent<Metronomo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            Debug.Log(Time.time - time);
            if((Time.time - time) < 1f && !authController.canRecord)
            {
                foreach (Transform child in center)
                {
                    Destroy(child.gameObject);
                }
                authController.canRecord = true;
                authController.strings.Clear();
                metronomo.nextBeatTime= Time.time;
            }
            audioSource.PlayOneShot(audioSource.clip);
        }
        time = Time.time;
    }
}
