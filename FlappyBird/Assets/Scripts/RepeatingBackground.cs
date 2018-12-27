using UnityEngine;

namespace Assets.Scripts
{
    public class RepeatingBackground : MonoBehaviour
    {
        //private BoxCollider2D groundCollider;
        private Renderer _renderer;
        private float groundHorizontalLength;

        // Use this for initialization
        void Start ()
        {
            _renderer = GetComponent<Renderer>();
            //groundCollider = GetComponent<BoxCollider2D>();
            groundHorizontalLength = _renderer.bounds.size.x;
        }
	
        // Update is called once per frame
        void Update ()
        {
            if (transform.position.x < -groundHorizontalLength)
            {
                RepositionBackground();
            }
        }

        private void RepositionBackground()
        {
            Vector2 groundOffset = new Vector2(groundHorizontalLength * 2f, 0);
            transform.position = (Vector2) transform.position + groundOffset;
        }
    }
}
