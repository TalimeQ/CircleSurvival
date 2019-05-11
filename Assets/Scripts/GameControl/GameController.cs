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
    InGameUi uiController;
     
    [SerializeField]
    List<GameObject> spawnedCircles;

    public static float timePassed = 0.0f;

    float timeBetweenSpawns;
    float nextSpawn;
    float explosionIntervalModifier;
    AudioSource popAudioSource;

    bool GameRunning = false;

   
    void Start()
    {
        BaseCircle.OnCircleRemoved += OnCircleRemoved;
        BaseCircle.OnPlayerFail += OnPlayerFailed;
        popAudioSource = GetComponent<AudioSource>();
        InitGame();
    }

    private void Awake()
    {
        InitGame();
    }

    void Update()
    {
        if (GameRunning)
        {
            timePassed += Time.deltaTime;
            if (Time.time > nextSpawn)
            {
                OrderSpawn();
                ManageTime();
            }
        }
        
    }

    void InitGame()
    {
        timeBetweenSpawns = currentGamemode.CircleSpawnInterval;
        timePassed = 0.0f;
        nextSpawn = Time.time + timeBetweenSpawns;
        GameRunning = true;
    }

    void OrderSpawn()
    {
        float randomChance = Random.Range(0.0f, 100.0f);
        if(randomChance <= currentGamemode.blackCircleChance) circleSpawner.Spawn("Black", explosionIntervalModifier);
        else circleSpawner.Spawn("Exploding", explosionIntervalModifier);
        
    }

    void OnCircleRemoved(BaseCircle circle)
    {
        DoubleTap();
        popAudioSource.PlayOneShot(popAudioSource.clip);
    }

    void OnPlayerFailed(BaseCircle circle)
    {
        FinalizeGame();
    }

    private void FinalizeGame()
    {
        GameRunning = false;
        ObjectPooler.SharedInstance.OnGameFinished();
        bool isHighscore = checkScore();
        uiController.OnGameFinished(isHighscore);
    }

    void DoubleTap()
    {
        float chanceForTap = Random.Range(0, 100);
        if(currentGamemode.DoubleTapChance >= chanceForTap)
        {
            print("Double tap!");
            OrderSpawn();
        }
    }

    bool checkScore()
    {
        float oldHS = PlayerPrefs.GetFloat("Highscore", 0.0f);
        if (oldHS < timePassed)
        {
            PlayerPrefs.SetFloat("Highscore", timePassed);
            return true;
        }
        else return false;
    }

    void ManageTime()
    {
        nextSpawn = Time.time + timeBetweenSpawns;
        timeBetweenSpawns -= currentGamemode.CircleSpawnIntervalChange;
        // We meed to cap maximal spawn time
        if (timeBetweenSpawns < currentGamemode.MinimalSpawnInterval) timeBetweenSpawns = currentGamemode.MinimalSpawnInterval;
        explosionIntervalModifier += currentGamemode.DifficultyIntervalChange;

    }

}
