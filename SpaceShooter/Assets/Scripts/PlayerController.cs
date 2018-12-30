using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }

    [Serializable]
    public class Gun
    {
        public string name;
        public Transform transform;
        public bool isActive;

        public Gun(string _name, bool _isActive, Transform _transform)
        {
            name = _name;
            isActive = _isActive;
            transform = _transform;
        }
    }

    enum FiringMode
    {
        Single,
        Double,
        Triple
    }

    public class PlayerController : MonoBehaviour
    {
        private GameController gameController;
        private float fireRate;
        private float nextFire;
        private float speed;
        private float tilt;
        private float damage;
        private float damageLimit;
        private Dictionary<FiringMode, float> firingModeSteps;
        private Dictionary<float, float> firingRateSteps;
        private FiringMode firingMode;

        public Boundary boundary;
        public GameObject shot;

        public Gun[] guns;

        void Start()
        {
            fireRate = 0.3f;
            speed = 10;
            tilt = 4;
            damageLimit = 100;
            firingMode = FiringMode.Single;

            firingModeSteps = new Dictionary<FiringMode, float>
            {
                {FiringMode.Single, 0},
                {FiringMode.Double, 250},
                {FiringMode.Triple, 750}
            };

            firingRateSteps = new Dictionary<float, float>
            {
                {0.3f, 0},
                {0.25f, 500},
                {0.20f, 1000}
            };

            // TODO: use singleton pattern to reference game controller
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

        void Update()
        {
            SetFiringMode();

            SetFiringRate();

            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                foreach (var gun in guns.Where(x => x.isActive))
                {
                    Instantiate(shot, gun.transform.position, gun.transform.rotation);
                }

                GetComponent<AudioSource>().Play();
            }
        }

        void FixedUpdate()
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");

            var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            GetComponent<Rigidbody>().velocity = movement * speed;

            GetComponent<Rigidbody>().position = new Vector3(
                Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            );

            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * (-tilt));
        }

        private void SetFiringRate()
        {
            if (gameController.score >= firingRateSteps[0.3f])
            {
                fireRate = 0.3f;
            }

            if (gameController.score >= firingRateSteps[0.25f])
            {
                fireRate = 0.2f;
            }

            if (gameController.score >= firingRateSteps[0.2f])
            {
                fireRate = 0.1f;
            }
        }

        private void SetFiringMode()
        {
            if (gameController.score >= firingModeSteps[FiringMode.Single])
            {
                firingMode = FiringMode.Single;
                guns.First(x => x.name == "Main").isActive = true;
                guns.First(x => x.name == "Port").isActive = false;
                guns.First(x => x.name == "Starboard").isActive = false;
            }

            if (gameController.score >= firingModeSteps[FiringMode.Double])
            {
                firingMode = FiringMode.Double;
                guns.First(x => x.name == "Main").isActive = false;
                guns.First(x => x.name == "Port").isActive = true;
                guns.First(x => x.name == "Starboard").isActive = true;
            }

            if (gameController.score >= firingModeSteps[FiringMode.Triple])
            {
                firingMode = FiringMode.Triple;

                guns.First(x => x.name == "Main").isActive = true;
                guns.First(x => x.name == "Port").isActive = true;
                guns.First(x => x.name == "Starboard").isActive = true;
            }
        }

    }

}
