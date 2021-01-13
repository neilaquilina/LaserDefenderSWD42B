using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;

    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        //if there is more than 1 GameSession, destroy the last one
        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            //do not destroy the GameSession when changing scenes
            DontDestroyOnLoad(gameObject);
            
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    //reset GameSession
    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
