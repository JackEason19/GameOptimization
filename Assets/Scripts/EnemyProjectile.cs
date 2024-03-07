using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody2D bulletRigidbody;
    public float speed = 25;
    SFX sFX;

    
    void Awake()
    {
        sFX = FindObjectOfType<SFX>();
        sFX.Shoot();
        Vector3 ShootDirection = transform.up;
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = ShootDirection * speed;
        Invoke("DeleteObject", 2f);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Asteroid"))
        {
            sFX.LaserHit();
        DeleteObject();
        }
    }
    void DeleteObject()
    {
        Destroy(gameObject);
    }
}
