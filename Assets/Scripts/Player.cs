using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform player;

    [HideInInspector]
    public GameObject destination;

    public float moveSpeed;

    public LineRenderer graplingRenderer;

    public PlayerStats stats;

    public AudioSource oreObtained;

    public void Move(GameObject destination)
    {
        if (stats.gameIsEnded || stats.gameIsPaused)
        {
            return;
        }
        this.destination = destination;
    }

    public void Init()
    {
        player.transform.position = Vector3.zero;
    }

    public void Update()
    {
        if (destination == null)
        {
            return;
        }
        if ((player.transform.position - destination.transform.position).magnitude <= 0.1)
        {
            Ore ore = destination.GetComponent<Ore>();
            if (ore != null)
            {
                stats.AddPoints(ore.value);
            }
            Destroy(destination);
            oreObtained.Play();

            return;
        }

        graplingRenderer.SetPosition(0, player.transform.position);
        graplingRenderer.SetPosition(1, destination.transform.position);

        float step = moveSpeed * Time.deltaTime;
        player.transform.position = Vector2.MoveTowards(player.transform.position, destination.transform.position, step);
    }
}
