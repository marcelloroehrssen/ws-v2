using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplingSearcher : MonoBehaviour
{
    public GameObject launcherPivot;

    public Player player;
    public PlayerStats stats;

    public LineRenderer radar;
    public float graplingRadarVelocity = 2f;
    public float graplingMaxDistance = 5f;
    float graplingDistancePrinted = 0f;


    // Update is called once per frame
    void Update()
    {
        if (stats.gameIsEnded || stats.gameIsPaused)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 direction = new Vector2(
                Mathf.Cos(launcherPivot.transform.rotation.eulerAngles.z * Mathf.Deg2Rad),
                Mathf.Sin(launcherPivot.transform.rotation.eulerAngles.z * Mathf.Deg2Rad)
            );
            Debug.DrawRay(launcherPivot.transform.position, direction, Color.green, 10);
            RaycastHit2D hit = Physics2D.Raycast(launcherPivot.transform.position, direction, graplingMaxDistance);

            if (hit.collider != null)
            {
                player.Move(hit.collider.gameObject);
                CheckOreDistance(hit.collider.gameObject);
            }
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            float angle = 0f;
            float angleScale = 0.01f;
            int size = (int)((1f / angleScale) + 1f);
            radar.positionCount = size;
            for (int i = 0; i < size; i++)
            {
                angle += (2.0f * Mathf.PI * angleScale);
                float x = graplingDistancePrinted * Mathf.Cos(angle) + launcherPivot.transform.position.x;
                float y = graplingDistancePrinted * Mathf.Sin(angle) + launcherPivot.transform.position.y;
                radar.SetPosition(i, new Vector3(x, y, 0));
            }
            graplingDistancePrinted += graplingRadarVelocity * Time.deltaTime;
            graplingDistancePrinted = Mathf.Clamp(graplingDistancePrinted, 0, graplingMaxDistance);
            if (graplingDistancePrinted == graplingMaxDistance)
            {
                graplingDistancePrinted = 0;
            }
        } else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            graplingDistancePrinted = 0;
            radar.positionCount = 0;
        }
    }

    private void CheckOreDistance(GameObject destination)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Ore");
        foreach (GameObject item in gos)
        {
            if (destination.Equals(item))
            {
                continue;
            }
            float distance = Vector2.Distance(item.transform.position, destination.transform.position);
            Debug.DrawLine(item.transform.position, destination.transform.position, Color.green, 10);
            if (distance <= graplingMaxDistance)
            {
                return;
            }
        }
        stats.EndGame("Non ci sono più pepite vicino a te!");
    }
}
