using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyByContact : MonoBehaviour
    {
        public GameObject explosion;
        public GameObject playerExplosion;
        public int scoreValue;
        private GameController gameController;

        void Start()
        {
            var gameControllerObject = GameObject.FindWithTag("GameController");
            if (gameControllerObject != null)
            {
                gameController = gameControllerObject.GetComponent<GameController>();
            }

            if (gameController == null)
            {
                Debug.Log("Cannot find 'GameController' script");
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
            {
                return;
            }

            if (other.CompareTag("Player"))
            {
                gameController.AddDamage(10f);

                Destroy(gameObject);

                if (gameController.playerHealth <= 0)
                {
                    if (playerExplosion != null)
                    {
                        Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                    }

                    Destroy(other.gameObject);           
                    gameController.GameOver();
                }
            }
            else
            {
                if (GetComponent<Asteroid>() != null)
                {
                    var asteroid = GetComponent<Asteroid>();
                    asteroid.AddDamage(10f);

                    Destroy(other.gameObject); // Destroy the bolt

                    if (asteroid.health <= 0)
                    {
                        // If the asteroid's health is low enough, destroy it
                        if (explosion != null)
                        {
                            Instantiate(explosion, transform.position, transform.rotation);
                        }                   
                        Destroy(gameObject);
                        gameController.AddScore(scoreValue);
                    }
                }
                else // TODO: enemy health
                {
                    if (explosion != null)
                    {
                        Instantiate(explosion, transform.position, transform.rotation);
                    }

                    Destroy(other.gameObject);
                    Destroy(gameObject);
                    gameController.AddScore(scoreValue);
                }

                
            }
        }
    }

}