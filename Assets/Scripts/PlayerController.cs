using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform ringCenter;
    public GameManager gameManager;
    SFX sFX;
    public float radius = 5f;
    public float speed = 10f;

    public float angle = 0f;
    public Transform spawnProjectilePosition;
    public GameObject pfProjectile;
    Rigidbody2D rb;
    public float nextFire = 0f, currentTime = 0;
    //private Camera mainCamera;
    //private float slowdownDistance = 1f; // Distance from the camera's bounds to start slowing down
    //private float slowdownFactor = 0.5f;
    //public float dampingDistance = 2f;

    

    void Start()
    {
        sFX = GetComponent<SFX>();
        gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        
        MovePlayer();
 
        FireWeapon();
        
    }

    void Update()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        // Check if the player is off-screen
        if (screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.y > Screen.height)
        {
            // Wrap the player's position to the opposite side of the screen
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

    public void MovePlayer()
    {
        rb.velocity = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        if (Input.GetKey("q"))
        {
            transform.Rotate(Vector3.forward, speed);
        }
        if (Input.GetKey("e"))
        {
            transform.Rotate(Vector3.forward, -speed);
        }

        //Vector3 newPosition = ringCenter.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);

        //transform.position = newPosition;

        //Vector3 directionToCenter = (ringCenter.position - transform.position).normalized;

        //float angleToCenter = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg + 270;

        //transform.rotation = Quaternion.Euler(0f, 0f, angleToCenter);

        //transform.position = ringCenter.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
        
    }

    void FireWeapon()
    {
        currentTime += Time.deltaTime;
        if (Input.GetButton("Fire1") && currentTime > nextFire)
        {
            nextFire += currentTime;
        Instantiate(pfProjectile, spawnProjectilePosition.position, Quaternion.identity);
        nextFire -= currentTime;
        currentTime = 0f;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyProjectile") || other.gameObject.CompareTag("Asteroid"))
        {
            sFX.Crash();
            gameManager.lives -= 1;
            gameManager.UpdateLives();
        }
        if(gameManager.lives <= 0)
        {
            sFX.GameOverSound();
            gameManager.GameOver();
        }
    }
}
