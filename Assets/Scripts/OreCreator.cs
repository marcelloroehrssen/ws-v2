using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreCreator : MonoBehaviour
{
    public Camera visible;
    public GameObject ore;
    public Transform oreContainer;

    [Range(0,1)]
    public float instantiationProbs = 1;
    public int numCol = 18;
    public int numRow = 10;
    [Range(0, 1)]
    public float tollerance = 0;

    private bool[,] map;

    // Start is called before the first frame update
    void Start()
    {
        map = new bool[numRow, numCol];

        Init();
    }

    public void Init()
    {
        for (int i = 0; i < numRow; i++)
        {
            for (int j = 0; j < numCol; j++)
            {
                if (i == (numRow / 2) && j == (numCol / 2))
                {
                    map[i, j] = false;
                    continue;
                }
                map[i, j] = Random.Range(0f, 1f) <= instantiationProbs ? true : false;
            }
        }

        Draw(map);
    }

    private void Draw(bool[,] map)
    {
        ClearMap();

        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = visible.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;
        float camHeight = 2.0f * camHalfHeight;

        float widthCell = camWidth / numRow;
        float heightCell = camHeight / numCol;

        for (int i = 0;  i < numRow; i++)
        {
            for (int j = 0; j < numCol; j++)
            {
                if (!map[i,j])
                {
                    continue;
                }
                GameObject go = Instantiate(ore);
                go.transform.SetParent(oreContainer);
                go.transform.position = new Vector3(i * widthCell, j * heightCell, 0) 
                    - new Vector3(camHalfWidth, camHalfHeight, 0) 
                    + new Vector3(widthCell/2, heightCell/2, 0)
                    + new Vector3(Random.Range(-tollerance, tollerance), Random.Range(-tollerance, tollerance), 0);
                go.transform.rotation = Quaternion.identity;
            }
        }
    }

    private void ClearMap()
    {
        GameObject[] ores = GameObject.FindGameObjectsWithTag("Ore");
        foreach (GameObject ore in ores)
        {
            Destroy(ore);
        }
    }
}
