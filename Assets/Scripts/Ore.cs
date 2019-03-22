using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public int minValue = 1;

    public int maxValue = 5;
    
    public int value;

    public void Start()
    {
        value = Random.Range(minValue, maxValue);
        transform.localScale += Vector3.one * (value / 35);
    }
}
