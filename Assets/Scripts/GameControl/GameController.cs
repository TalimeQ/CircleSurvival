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
    
    List<string> objectTags;

    float timeBetweenSpawns;
    float nextSpawn;
    float explosionIntervalModifier;


    // Start is called before the first frame update
    void Start()
    {
        objectTags = new List<string>();
        BaseCircle.OnCircleRemoved += OnCircleRemoved;
        BaseCircle.OnPlayerFail += OnPlayerFailed;
        InitGame();
    }

    
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
        circleSpawner.Spawn("Exploding", explosionIntervalModifier);
        nextSpawn = Time.time + timeBetweenSpawns;
    }

    void OnCircleRemoved(BaseCircle circle)
    {
        print("Removal notified!");
    }

    void OnPlayerFailed(BaseCircle circle)
    {
        print("Player failed!");
    }
}
