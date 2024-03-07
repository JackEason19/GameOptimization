using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 randomDirection;
    PlayerController player;
    SFX sFX;
    GameManager gameManager;
    public float nextFire = .5f, currentTime = 0;
    public Transform spawnProjectilePosition;
    public GameObject pfEnemyProjectile;
    public float shootInterval = .5f;

    void Awake()
    {
        sFX = FindObjectOfType<SFX>();
        randomDirection = Random.insideUnitCircle.normalized;
        player = FindObjectOfType<PlayerController>();
        gameManager = player.GetComponent<GameManager>();
    }
    void Start()
    {
        StartCoroutine(FireWeapon());
    }
    void Update()
    {
        
        // Move the enemy ship horizontally
        transform.Translate(randomDirection * speed * Time.deltaTime, Space.World);
        transform.up = randomDirection;

        // Wrap around the screen if the enemy goes off-screen
        if (transform.position.x > Screen.width)
        {
            transform.position = new Vector3(-Screen.width, transform.position.y, transform.position.z);
        }

        
        
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        
        if (screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.y > Screen.height)
        {
            Vector3 wrappedPosition = transform.position;

            if (screenPosition.x < 0)
            {
                wrappedPosition.x = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, screenPosition.y, 0)).x;
            }
            else if (screenPosition.x > Screen.width)
            {
                wrappedPosition.x = Camera.main.ScreenToWorldPoint(new Vector3(0, screenPosition.y, 0)).x;
            }

            if (screenPosition.y < 0)
            {
                wrappedPosition.y = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, Screen.height, 0)).y;
            }
            else if (screenPosition.y > Screen.height)
            {
                wrappedPosition.y = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, 0, 0)).y;
            }

            transform.position = wrappedPosition;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Projectile"))
        {
            sFX.Crash();
            Destroy(gameObject);
            gameManager.score += 50;
            gameManager.UpdateScoreText();
        }
        else if(other.gameObject.CompareTag("Asteroid"))
        {
            sFX.Crash();
            Destroy(gameObject);
        }
        //else if (other.gameObject.CompareTag("EnemyProjectile"))
       // {
        //    Destroy(gameObject);
       // }
    }

        IEnumerator FireWeapon()
    {
        while(true)
        {
        yield return new WaitForSeconds(1f);
        Instantiate(pfEnemyProjectile, spawnProjectilePosition.position, transform.rotation);
        
        }
    }
}
