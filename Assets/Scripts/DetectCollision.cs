using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{

    public Vector3 basePosition;
    public GameObject indicator;
    public Transform center;
    [SerializeField] AuthController controller;
    
    private void Start()
    {
        basePosition = transform.GetChild(0).transform.position;
        center = GameObject.Find("Center").transform;
        controller = GameObject.Find("GameManager").GetComponent<AuthController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand") && other.gameObject.transform.position.y > basePosition.y)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null && !controller.havePlayed && controller.canRecord)
            {
                audioSource.PlayOneShot(audioSource.clip);
                double width = indicator.GetComponent<Renderer>().bounds.size.x;
                Vector3 position = new Vector3((float)((width + 0.1) * center.childCount), 2, 5);
                Instantiate(
                    indicator,
                    position,
                    Quaternion.identity,
                    center
                );
                controller.strings.Add(gameObject.name);
                controller.havePlayed = true;
            }
        }
    }
}
