using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Gamemode", menuName ="Gamemode")]
public class GameMode : ScriptableObject
{
    public float CircleSpawnInterval;
    public float DifficultyInterval;
    public float StartingMaxCircleTimer;
    public float StartingMinCircleTimer;
}
