using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //a list of points of type Transform
    [SerializeField] List<Transform> waypoints;

    [SerializeField] WaveConfig waveConfig;

    //shows the next waypoint
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        
        //set the starting position of the Enemy ship to the position of the 1st waypoint
        //transform.position = waypoints[waypointIndex].transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove(); 
    }

    public void SetWaveConfig(WaveConfig waveConfigToSet)
    {
        waveConfig = waveConfigToSet;
    }


    private void EnemyMove()
    {
        //     0,1,2            2
        if(waypointIndex <= waypoints.Count-1)
        {
            //save the next waypoint as my target position
            var targetPosition = waypoints[waypointIndex].transform.position;

            targetPosition.z = 0f;
            //set the enemy movement per frame
            var enemyMovement = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;

            //move from the current position, to the target position, at the enemyMovement speed
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemyMovement);

            //if target waypoint is reached, update to the next waypoint
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }

        }
        //if enemy reaches the last waypoint
        else
        {
            Destroy(gameObject);
        }

    }

}
