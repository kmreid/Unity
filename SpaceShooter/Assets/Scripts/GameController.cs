using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public GameObject hazard;
        public Vector3 spawnValues;
        public int hazardCount;
        public float spawnWait;
        public float startWait;
        public float waveWait;
        public Text scoreText;
        public Text restartText;
        public Text gameOverText;

        private int score;
        private bool gameOver;
        private bool restart;


        void Start()
        {
            score = 0;
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

        public void GameOver()
        {
            gameOverText.text = "Game Over!";
            gameOver = true;
        }
    }
}
