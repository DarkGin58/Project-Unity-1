using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10.0f;

    public float speed = 65f;
    Rigidbody rb;
    public float lifeTimer = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {

    
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<Enemy>())
        {
            Debug.Log("Hit enemy");
            Enemy enemy = other.collider.GetComponent<Enemy>();
            enemy.ReduceHealth(damage);
        }
        Destroy(gameObject);
    }
}
