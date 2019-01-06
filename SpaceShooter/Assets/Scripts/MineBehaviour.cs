using System.Collections;
using Assets.Scripts;
using UnityEngine;


public enum MineType
{
    Dumb,
    Seeker
}

public class MineBehaviour : MonoBehaviour
{
    public MineType mineType;
    public float seekerSensorDistance; // The distance the mine can detect the player
    public float smoothing;
    public Boundary boundary;

    private Transform playerTransform;
    private float currentSpeed;
    private Vector3 targetManeuver;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        rigidBody.velocity = transform.forward * -2;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        currentSpeed = rigidBody.velocity.z;
        //rigidBody.velocity = transform.forward * 0;

        if (mineType == MineType.Seeker)
        {
            StartCoroutine(Seek());
        }  
    }

    IEnumerator Seek()
    {
        

        while (true)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            var canSeePlayer = distance < 6;

            if (canSeePlayer && playerTransform != null)
            {
                rigidBody.velocity = transform.forward * 0;

                rigidBody.isKinematic = true;
                targetManeuver = new Vector3(playerTransform.position.x, 0, playerTransform.position.z);

                //rigidBody.isKinematic = false;
                yield return new WaitForSeconds(5f);
            }
            //else
            //{
            rigidBody.isKinematic = false;
            rigidBody.velocity = transform.forward * -2;
            targetManeuver = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(0);
            //}
            
        }
    }

    void FixedUpdate()
    {
        if (mineType != MineType.Seeker)
        {
            return;
        }

        float newManeuverX = Mathf.MoveTowards(rigidBody.velocity.x, targetManeuver.x, Time.deltaTime * smoothing);
        float newManeuverZ = Mathf.MoveTowards(rigidBody.velocity.z, targetManeuver.z, Time.deltaTime * smoothing);
        rigidBody.velocity = new Vector3(newManeuverX, newManeuverZ, currentSpeed);
        rigidBody.position = new Vector3(
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );
    }
}
