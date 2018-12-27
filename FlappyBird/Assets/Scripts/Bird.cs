using UnityEngine;

namespace Assets.Scripts
{
    public class Bird : MonoBehaviour
    {
        public float upForce = 200f;

        private bool isDead;
        private Rigidbody2D rb2D;
        private Animator anim;

        // Use this for initialization
        void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!isDead)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    anim.SetTrigger("Flap");
                    rb2D.velocity = Vector2.zero;
                    rb2D.AddForce(new Vector2(0, upForce));

                }
            }
        }

        void OnCollisionEnter2D()
        {
            rb2D.velocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("Die");
            GameControl.instance.BirdDied();
        }
    }
}
