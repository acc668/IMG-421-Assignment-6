using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager S;

    [Header("UI Elements - Drag in from Inspector")]
    public Text scoreText;
    public Text highScoreText;
    public Text shieldText;
    public Text livesText;
    public Text difficultyText;
    public GameObject gameOverPanel;
    public Text gameOverScoreText;

    [Header("Set Dynamically")]
    public int score = 0;
    public int highScore = 0;
    public int lives;

    private DifficultySettings settings;

    void Awake()
    {
        S = this;
        
        settings = DifficultyManager.GetSettings();

        lives = settings.playerStartLives;

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Start()
    {
        if (Hero.S != null)
        {
            Hero.S.shieldLevel = settings.playerStartShield;
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (difficultyText != null)
        {
            difficultyText.text = DifficultyManager.difficulty.ToString();
        }

        UpdateHUD();
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UpdateHUD();
    }

    public void PlayerHit()
    {
        lives--;

        UpdateHUD();

        if (lives <= 0)
        {
            TriggerGameOver();
        }
    }

    public void UpdateHUD()
    {
        if (scoreText != null) 
        {
            scoreText.text = "Score: " + score;
        }

        if (highScoreText != null) 
        {
            highScoreText.text = "Best: " + highScore;
        }

        if (shieldText != null) 
        {
            shieldText.text = "Shield: " + GetShieldStars();
        }

        if (livesText != null) 
        {
            livesText.text = "Lives: " + GetLivesDisplay();
        }
    }

    string GetShieldStars()
    {
        if (Hero.S == null) 
        {
            return "";
        }

        int level = (int)Hero.S.shieldLevel;

        string stars = "";

        for (int i = 0; i < level; i++) 
        {
            stars += "★";
        }

        return stars.Length > 0 ? stars : "NONE";
    }

    string GetLivesDisplay()
    {
        string display = "";

        for (int i = 0; i < lives; i++) 
        {
            display += "♥ ";
        }

        return display.Trim();
    }

    void TriggerGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            if (gameOverScoreText != null)
            {
                gameOverScoreText.text = "Final Score: " + score;
            }
        }

        if (Main.S != null)
        {
            Main.S.CancelInvoke();
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("_Scene_0");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

        #endif
    }
}