﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public GameObject[] hazards;
        public Vector3 spawnValues;
        public int hazardCount;
        public float spawnWait;
        public float startWait;
        public float waveWait;
        public Text scoreText;
        public Text restartText;
        public Text gameOverText;
        public int score;
        public float playerHealth;

        private bool gameOver;
        private bool restart;

        void Start()
        {
            score = 0;
            playerHealth = 100;
            gameOver = false;
            restart = false;
            restartText.text = string.Empty;
            gameOverText.text = string.Empty;
            UpdateScore();
            StartCoroutine(SpawnWaves());
        }

        IEnumerator SpawnWaves()
        {
            yield return new WaitForSeconds(startWait);

            while (!gameOver)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    var hazard = hazards[Random.Range(0, hazards.Length)];
                    var spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y,
                        spawnValues.z);
                    var spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }

                yield return new WaitForSeconds(waveWait);

                if (gameOver)
                {
                    restartText.text = "Press 'R' to restart";
                    restart = true;
                }
            }
        }

        void Update()
        {
            if (restart)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            
        }

        void UpdateScore()
        {
            scoreText.text = "Score: " + score;
        }

        public void AddScore(int newScoreValue)
        {
            score += newScoreValue;
            UpdateScore();
        }

        public void AddDamage(float newDamageValue)
        {
            playerHealth -= newDamageValue;
        }

        public void GameOver()
        {
            gameOverText.text = "Game Over!";
            gameOver = true;
        }
    }
}
