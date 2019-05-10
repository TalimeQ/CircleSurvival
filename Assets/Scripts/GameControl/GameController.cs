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

    Dictionary<string,int> objectAmount = new Dictionary<string, int>();

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
        if (Time.time > nextSpawn && GameRunning) OrderSpawn();
    }

    void InitGame()
    {
        timeBetweenSpawns = currentGamemode.CircleSpawnInterval;
        foreach(GameObject circleToSpawn in spawnedCircles)
        {
            objectAmount.Add(circleToSpawn.tag, 0);
        }
        nextSpawn = Time.time + timeBetweenSpawns;
        GameRunning = true;
    }

    void OrderSpawn()
    {
        float randomChance = Random.Range(0.0f, 100.0f);
        // TODO make it gamemode based :)
        if(randomChance <= 10.0f) circleSpawner.Spawn("Black", explosionIntervalModifier);
        else circleSpawner.Spawn("Exploding", explosionIntervalModifier);


        nextSpawn = Time.time + timeBetweenSpawns;
    }

    void OnCircleRemoved(BaseCircle circle)
    {
        objectAmount[circle.gameObject.tag]--;
        print("Removal notified!");
    }

    void OnPlayerFailed(BaseCircle circle)
    {
        GameRunning = false;
        ObjectPooler.SharedInstance.OnGameFinished();
    }

}
