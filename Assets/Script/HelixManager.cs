using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixManager : MonoBehaviour
{
    public GameObject[] rings;

    public int noOfRings;
    public float ringDistance = 5f;
    float yPos;

    private void Start()
    {
        noOfRings = GameManager.CurrentLevelIndex + 5;
        for (int i = 0; i < noOfRings; i++)
        {
            if (i == 0) {
                //создать первое кольцо
                SpawnRings(0);
            }
            else
            {
                //создать средние кольца, кроме 0, последнего
                SpawnRings(Random.Range(1, rings.Length - 1));
            }
        }
        //создать посл. кольцо
        SpawnRings(rings.Length - 1);
    }
    void SpawnRings(int index)
    {
        GameObject newRing = Instantiate(rings[index], new Vector3(transform.position.x, yPos, transform.position.z), Quaternion.identity);
        yPos -= ringDistance;
        newRing.transform.parent = transform;
    }
}
