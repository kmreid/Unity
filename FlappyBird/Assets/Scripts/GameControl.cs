using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameControl : MonoBehaviour
    {
        public static GameControl instance;

        public float scrollSpeed = -1.5f;
        public float skyScrollSpeed = -0.75f;
        public GameObject gameOverText;
        public Text scoreText;
        public bool gameOver;

        private int score = 0;

        // Use this for initialization
        void Awake ()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
	
        // Update is called once per frame
        void Update ()
        {
            if (gameOver && Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public void BirdScored()
        {
            if (gameOver)
            {
                return;
            }

            score++;
            scoreText.text = string.Format("Score: {0}", score);
        }

        public void BirdDied()
        {
            gameOverText.SetActive(true);
            gameOver = true;
        }
    }
}
