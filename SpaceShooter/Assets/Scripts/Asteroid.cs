using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Asteroid : MonoBehaviour
    {
        public float tumble;
        public float health;

        public void Start()
        {
            GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
        }

        public void AddDamage(float damage)
        {
            health -= damage;
        }
    }

}