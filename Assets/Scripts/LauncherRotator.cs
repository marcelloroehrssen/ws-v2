using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherRotator : MonoBehaviour
{
    public PlayerStats stats;

    public GameObject launcherPivot;

    public float rotatorSpeed;
    public float rotationSpeedReduction = 3;

    public float initialAngle = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        launcherPivot.transform.Rotate(0,0, initialAngle, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.gameIsEnded || stats.gameIsPaused)
        {
            return;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            float actualRotation = rotatorSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                actualRotation = rotatorSpeed / rotationSpeedReduction;
            }
            float deltaAngle = actualRotation * (Input.GetKey(KeyCode.A) ? 1 : -1);

            launcherPivot.transform.Rotate(0, 0, deltaAngle, Space.Self);
        }
    }
}
