using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Mover : MonoBehaviour
    {
        public float speed;

        void Start()
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
    }

}
