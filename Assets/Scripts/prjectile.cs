using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class prjectile : MonoBehaviour
{
    private Rigidbody2D bulletRigidbody;
    public PlayerController player;
    public float speed = 25;
    SFX sFX;
    void Awake()
    {
        sFX = FindObjectOfType<SFX>();
        sFX.Shoot();
        bulletRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        Vector3 ShootDirection = player.transform.up;
        bulletRigidbody.velocity = ShootDirection * speed;
        //Vector3 directionToCenter = (player.ringCenter.position - transform.position).normalized;

        float angleToCenter = Mathf.Atan2(ShootDirection.y, ShootDirection.x) * Mathf.Rad2Deg + 270;
        transform.rotation = Quaternion.Euler(0f, 0f, angleToCenter);
        Invoke("DeleteObject", 2f);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Enemy")|| other.gameObject.CompareTag("Asteroid"))
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
