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
     
    [SerializeField]
    List<GameObject> spawnedCircles;

    public static float timePassed = 0.0f;

    float timeBetweenSpawns;
    float nextSpawn;
    float explosionIntervalModifier;

    bool GameRunning = false;

   
    void Start()
    {
        BaseCircle.OnCircleRemoved += OnCircleRemoved;
        BaseCircle.OnPlayerFail += OnPlayerFailed;
        InitGame();
    }

    void Update()
    {
        if (GameRunning)
        {
            timePassed += Time.deltaTime;
            if (Time.time > nextSpawn) OrderSpawn();
        }
        
    }

    void InitGame()
    {
        timeBetweenSpawns = currentGamemode.CircleSpawnInterval;
        nextSpawn = Time.time + timeBetweenSpawns;
        GameRunning = true;
    }

    void OrderSpawn()
    {
        float randomChance = Random.Range(0.0f, 100.0f);
        if(randomChance <= currentGamemode.blackCircleChance) circleSpawner.Spawn("Black", explosionIntervalModifier);
        else circleSpawner.Spawn("Exploding", explosionIntervalModifier);
        nextSpawn = Time.time + timeBetweenSpawns;
    }

    void OnCircleRemoved(BaseCircle circle)
    {
        // Left for some juicy scoring system for later
    }

    void OnPlayerFailed(BaseCircle circle)
    {
        GameRunning = false;
        ObjectPooler.SharedInstance.OnGameFinished();
    }

}
