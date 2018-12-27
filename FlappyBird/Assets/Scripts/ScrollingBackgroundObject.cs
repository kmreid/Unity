using UnityEngine;

namespace Assets.Scripts
{
    public class ScrollingBackgroundObject : MonoBehaviour
    {
        private Rigidbody2D rb2d;

        // Use this for initialization
        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            rb2d.velocity = new Vector2(GameControl.instance.skyScrollSpeed, 0);
        }

        // Update is called once per frame
        void Update()
        {
            if (GameControl.instance.gameOver)
            {
                rb2d.velocity = Vector2.zero;
            }
        }
    }
}
