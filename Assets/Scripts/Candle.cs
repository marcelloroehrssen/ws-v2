using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    float intensity;
    public float velocity;

    void Start()
    {
        intensity = GetComponent<Light>().intensity;
        velocity = Random.Range(.001f, .1f);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Light>().intensity = intensity + Mathf.PingPong(Time.time * velocity, .1f);
    }
}
