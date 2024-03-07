using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = .5f;
    private Vector3 randomDirection;
    PlayerController player;
    GameManager gameManager;
    void Awake()
    {
        // Generate a random direction when the enemy is created
        randomDirection = Random.insideUnitCircle.normalized;
        player = FindObjectOfType<PlayerController>();
        gameManager = player.GetComponent<GameManager>();
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
}
