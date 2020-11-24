using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //a list of Waves
    [SerializeField] List<WaveConfig> waveConfigsList;

    [SerializeField] bool looping = false;

    //always start from position 0
    int startingWave = 0;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        //set the currentWave to 0 (1st Wave)
        var currentWave = waveConfigsList[startingWave];
        //loop all waves until looping == true
        do
        {
            //start the coroutine that spawns all enemies in wave
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping); //while (looping == true)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    //coroutine to spawn all waves
    private IEnumerator SpawnAllWaves()
    {
        foreach(WaveConfig currentWave in waveConfigsList)
        {
            //the coroutine waits for all enemies to spawn before yielding
            //and going to the next wave in List
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    //call a coroutine to spawn Enemies in waveToSpawn
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveToSpawn)
    {
        //spawn enemies depending on the numberOfEnemies in waveToSpawn
        for (int enemyCount = 1; enemyCount <= waveToSpawn.GetNumberOfEnemies(); enemyCount++)
        {
            //spawn enemy from waveToSpawn 
            //at the position specified by the waypoint(0) of waveToSpawn
            var newEnemy = Instantiate(
                            waveToSpawn.GetEnemyPrefab(),
                            waveToSpawn.GetWaypoints()[0].transform.position,
                            Quaternion.identity);
            //the wave will be selected from newEnemy and the Enemy applied to it
            //the wave will be selected from script and not from Unity
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveToSpawn);

            yield return new WaitForSeconds(waveToSpawn.GetTimeBetweenSpawns());

        }
        
    }
        
}
