using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    //the enemy
    [SerializeField] GameObject enemyPrefab;
    //the path on which to move
    [SerializeField] GameObject pathPrefab;
    //time between each spawn
    [SerializeField] float timeBetweenSpawns = 0.5f;
    //random time difference between spawns
    [SerializeField] float spawnRandomFactor = 0.3f;
    //number of enemies in Wave
    [SerializeField] int numberOfEnemies = 5;
    //enemy Movement speed
    [SerializeField] float enemyMoveSpeed = 2f;

    //encapsulation methods
    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    
    public List<Transform> GetWaypoints()
    {
        //each wave can have different waypoints
        var waveWaypoints = new List<Transform>();

        //go into PathPrefab, and for each child add it to list: waveWaypoints
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }
    
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public float GetSpawnRandomFactor() { return spawnRandomFactor;  }

    public int GetNumberOfEnemies() { return numberOfEnemies; }

    public float GetEnemyMoveSpeed() { return enemyMoveSpeed;  }

    

}
