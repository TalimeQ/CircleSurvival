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
    AudioSource popAudioSource;

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

    private void OnDestroy()
    {
        BaseCircle.OnCircleRemoved -= OnCircleRemoved;
        BaseCircle.OnPlayerFail -= OnPlayerFailed;
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
        BonusSpawn();
        popAudioSource.Play();
    }

    void OnPlayerFailed(BaseCircle circle)
    {
        FinalizeGame();
    }

    void FinalizeGame()
    {
        GameRunning = false;
        ObjectPooler.SharedInstance.OnGameFinished();
        bool isHighscore = checkScore();
        uiController.OnGameFinished(isHighscore);
    }

    void BonusSpawn()
    {
        float BonusSpawnChance = Random.Range(0, 100);
        if(currentGamemode.BonusSpawnChance >= BonusSpawnChance)
        {
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
        IncreaseDifficulty();
    }

    void IncreaseDifficulty()
    {
        timeBetweenSpawns -= currentGamemode.CircleSpawnIntervalChange;
        // Cap minimal spawn time so the game appears to be possible to win
        if (timeBetweenSpawns < currentGamemode.MinimalSpawnInterval) timeBetweenSpawns = currentGamemode.MinimalSpawnInterval;
        explosionIntervalModifier += currentGamemode.DifficultyIntervalChange;
    }
}
