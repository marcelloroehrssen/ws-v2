using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceKeeper : MonoBehaviour
{
    private static AudioSourceKeeper _instance;

    void Awake()
    {
        if (!_instance)
            _instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
