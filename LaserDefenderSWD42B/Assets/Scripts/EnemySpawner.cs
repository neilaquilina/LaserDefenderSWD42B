using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //a list of Waves
    [SerializeField] List<WaveConfig> waveConfigsList;

    //always start from position 0
    int startingWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        //set the currentWave to 0 (1st Wave)
        var currentWave = waveConfigsList[startingWave];

        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //call a coroutine to spawn Enemies in waveToSpawn
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveToSpawn)
    {
        //spawn enemies depending on the numberOfEnemies in waveToSpawn
        for (int enemyCount = 1; enemyCount <= waveToSpawn.GetNumberOfEnemies(); enemyCount++)
        {
            //spawn enemy from waveToSpawn 
            //at the position specified by the waypoint(0) of waveToSpawn
            Instantiate(
                waveToSpawn.GetEnemyPrefab(),
                waveToSpawn.GetWaypoints()[0].transform.position,
                Quaternion.identity);

            yield return new WaitForSeconds(waveToSpawn.GetTimeBetweenSpawns());

        }
        
    }
        
}
