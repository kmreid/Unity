using System;
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
        public GameObject healthBar;
        private RectTransform healthRectTransform;
        private float healthRectWidth;
        public int score;
        public float playerHealth;

        private bool gameOver;
        private bool restart;

        void Start()
        {
            score = 0;
            playerHealth = 100;

            healthRectTransform = healthBar.GetComponent<RectTransform>();
            healthRectWidth = healthRectTransform.rect.width;
            
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
            UpdateScore(); // TODO: use Observer pattern
        }

        void UpdateHealth()
        {
            healthRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, healthRectWidth * (playerHealth / 100f));
        }

        public void AddDamage(float newDamageValue)
        {
            playerHealth -= newDamageValue;
            UpdateHealth(); // TODO: use Observer pattern
        }

        public void GameOver()
        {
            gameOverText.text = "Game Over!";
            gameOver = true;
        }
    }
}
