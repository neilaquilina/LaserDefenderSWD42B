using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 200;
    [SerializeField] int scoreValue;

    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;

    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float enemyLaserSpeed = 10f;

    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionDuration;

    [SerializeField] AudioClip enemyDeathSound;
    [SerializeField] [Range(0, 1)] float enemyDeathSoundVolume = 0.75f;

    [SerializeField] AudioClip enemyShootSound;
    [SerializeField] [Range(0, 1)] float enemyShootSoundVolume = 0.2f;

    //reduce Enemy health everytime an enemy collides with a gameObject
    //which has a DamageDealer component
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        DamageDealer dmg = otherObject.gameObject.GetComponent<DamageDealer>();

        //if the object does not have a DamageDealer class end the method
        if (!dmg && otherObject.gameObject.tag == "player") //if dmg does not exist
        {
            Die();
            return;
        }
        else if (!dmg)
        {
            return;
        }

        ProcessHit(dmg);
    }

    private void ProcessHit(DamageDealer dmg)
    {
        health -= dmg.GetDamage();
        //destroy Player Laser
        dmg.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    //when Enemy dies
    private void Die()
    {
        //add scoreValue to GameSession score
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        //create an Explosion Particle
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        //destroy explosion after explosionDuration
        Destroy(explosion, explosionDuration);
        //play enemyDeathSound at Camera position, at enemyDeathSoundVolume
        AudioSource.PlayClipAtPoint(enemyDeathSound, Camera.main.transform.position, enemyDeathSoundVolume);
        

    }

    // Start is called before the first frame update
    void Start()
    {
        //give a random time to shotCounter
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    //count down shotCounter to 0 and shoot
    private void CountDownAndShoot()
    {
        //every frame reduce the amount of time of shotCounter
        shotCounter -= Time.deltaTime;

        if(shotCounter <= 0f)
        {
            EnemyFire();
            //reset shotCounter after every fire
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    //spawn an enemy Laser from the Enemy's position
    private void EnemyFire()
    {
        GameObject enemyLaser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity);

        //give a velocity to the laser in y-axis negative
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);

        //play enemyShootSound at Camera position, with enemyShootSoundVolume
        AudioSource.PlayClipAtPoint(enemyShootSound, Camera.main.transform.position, enemyShootSoundVolume);
    }
}
