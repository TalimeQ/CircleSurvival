using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arcade.Spawn;


public class GameController : MonoBehaviour
{
    [SerializeField]
    GameMode currentGamemode;
    [SerializeField]
    Spawner circleSpawner;


    float timeBetweenSpawns;
    float nextSpawn;
    float explosionIntervalModifier;
    

    // Start is called before the first frame update
    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn) OrderSpawn();
    }

    void InitGame()
    {
        timeBetweenSpawns = currentGamemode.CircleSpawnInterval;
        nextSpawn = Time.time + timeBetweenSpawns;
    }

    void OrderSpawn()
    {
        circleSpawner.Spawn("Black", explosionIntervalModifier);
        nextSpawn = Time.time + timeBetweenSpawns;
    }
}
