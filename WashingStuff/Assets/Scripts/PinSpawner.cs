using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinSpawner : MonoBehaviour
{
    //public List<GameObject> pinPrefabs;
    public SerializableDictionary<string, GameObject> pinPrefabs;

    public int redCount = 0;
    public int blueCount = 0;
    public int greenCount = 0;

    public List<Transform> spawnPoints;
    public List<PinCounter> pinCounters;

    void Start()
    {
        pinCounters[0].pinAmount = redCount;
        pinCounters[1].pinAmount = blueCount;
        pinCounters[2].pinAmount = greenCount;
        for (int i = 0; i < redCount; i++)
        {
            Instantiate(pinPrefabs["red"], spawnPoints[0].position, spawnPoints[0].rotation, spawnPoints[0]);
        }
        for (int i = 0; i < blueCount; i++)
        {
            Instantiate(pinPrefabs["blue"], spawnPoints[1].position, spawnPoints[1].rotation, spawnPoints[1]);
        }
        for (int i = 0; i < greenCount; i++)
        {
            Instantiate(pinPrefabs["green"], spawnPoints[2].position, spawnPoints[2].rotation, spawnPoints[2]);
        }
    }

    void Update()
    {

    }
}
