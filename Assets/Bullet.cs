using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float fireSpeed = 2f;
    private Rigidbody rb;
    [SerializeField] private float despawnTimer = 3f;
    [SerializeField] private GameObject playerInstance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 _playerPos = GameObject.Find("VRCamera").transform.position;
        // set our laser on its merry way. no need to update transform manually
        rb.velocity = ((transform.position - _playerPos)+ new Vector3(0, 0, -1)) * fireSpeed;

        // freeze the rotation so it doesnt go spinning after a collision
        rb.freezeRotation = true;
    }

    // we want to store the laser's velocity every frame
    // so we can use this data during collisions to reflect
    private Vector3 oldVelocity;
    void FixedUpdate()
    {
        // because we want the velocity after physics, we put this in fixed update
        oldVelocity = rb.velocity;

        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    // when a collision happens
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "lightsaber")
        {

            // get the point of contact
            ContactPoint contact = collision.contacts[0];

            // reflect our old velocity off the contact point's normal vector
            Vector3 reflectedVelocity = Vector3.Reflect(oldVelocity, contact.normal);

            // assign the reflected velocity back to the rigidbody
            rb.velocity = reflectedVelocity;
            Quaternion rotation = Quaternion.FromToRotation(oldVelocity, reflectedVelocity);
            transform.rotation = rotation * transform.rotation;

        }
        else
        {
            //Destroy(gameObject);
        }
    }

}
