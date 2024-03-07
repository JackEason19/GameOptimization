using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    public int lives = 3, score, highScore;
    public TextMeshProUGUI scoreText, livesText, highScoreText;
    public GameObject gameOverScreen, pauseScreen, settingsScreen;
    //public Button quit, settings, resume, restart;
    public Transform spawn;
    PlayerController player;
    public bool paused = false;
    SFX sFX;

    void Awake()
    {
        sFX = GetComponent<SFX>();
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }
    void Start()
    {   
        Time.timeScale = 1;
        player = GetComponent<PlayerController>();
        player.transform.position = spawn.transform.position;
        score = 0;
        // Retrieve the saved high score and display it
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
        lives = 3;
        livesText.text = "Lives: " + lives;
    }
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            PauseGame();
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
        if(score > highScore)
        {
            highScoreText.text = "High Score: " + score;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver = true;
        SaveHighScore();
        gameOverScreen.SetActive(true);
    }
    void SaveHighScore()
    {
        // Check if the new score is higher than the saved high score
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            // Save the new high score
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save(); // Make sure to save changes
        }
    }
    public void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void Restart()
    {
        /*gameOverScreen.SetActive(false);
        player.gameObject.SetActive(true);
        Time.timeScale = 1;
        lives = 3;
        UpdateLives();
        score = 0;
        UpdateScoreText();
        player.transform.position = spawn.transform.position;*/

        SceneManager.LoadScene("Level1");
    }
    public void PauseGame()
    {
        if (paused == false)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            paused = true;
        }
        else if(paused == true)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            paused = false;
        }
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }
    public void GoToSettings()
    {
        settingsScreen.SetActive(true);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }
    public void ReturnTo()
    {
        if (gameOver == true)
        {
            gameOverScreen.SetActive(true);
        }
        else
        {
            pauseScreen.SetActive(true);
        }
        
        settingsScreen.SetActive(false);
    }
    public void QuitToMenu()
    {
        SaveHighScore();
        SceneManager.LoadScene("MainMenu");
        Background_Music background_Music = FindObjectOfType<Background_Music>();
        background_Music.MenuMusic();
    }
}
