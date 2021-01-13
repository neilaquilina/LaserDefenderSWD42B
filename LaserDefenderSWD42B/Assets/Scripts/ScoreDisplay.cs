﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    GameSession gameSession;
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        //linking scoreText and GameSession
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        
        scoreText.text = gameSession.GetScore().ToString();

    }
}
