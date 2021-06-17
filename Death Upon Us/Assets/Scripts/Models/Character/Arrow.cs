using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rigidbody;
    private float lifeTimer = 2f;
    private float timer;
    private bool hitSomething = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
