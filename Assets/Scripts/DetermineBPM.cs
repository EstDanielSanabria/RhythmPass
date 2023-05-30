using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetermineBPM : MonoBehaviour
{

    public int minBpm = 60;
    public int maxBpm = 100;

    public Metronomo metronomo;

    public TextMesh indicadorBPM;

    private void Start()
    {
        metronomo = GameObject.Find("GameManager").GetComponent<Metronomo>();
        indicadorBPM = transform.GetChild(0).GetComponent<TextMesh>();
    }

    void Update()
    {
        metronomo.bpm = minBpm + Mathf.FloorToInt(((transform.position.y - 1) / 2) * maxBpm);
        indicadorBPM.text = metronomo.bpm.ToString();
        metronomo.CalculateBeatInterval();
    }
}
