using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainPanel;
    public GameObject difficultyPanel;

    [Header("UI Elements")]
    public Text highScoreText;
    public Text titleText;

    void Start()
    {
        mainPanel.SetActive(true);

        difficultyPanel.SetActive(false);

        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (highScoreText != null)
        {
            highScoreText.text = "Best Score: " + savedHighScore;
        }
    }

    public void OnPlayPressed()
    {
        mainPanel.SetActive(false);

        difficultyPanel.SetActive(true);
    }

    public void OnBackPressed()
    {
        difficultyPanel.SetActive(false);

        mainPanel.SetActive(true);
    }

    public void OnEasyPressed()
    {
        DifficultyManager.difficulty = Difficulty.Easy;

        SceneManager.LoadScene("_Scene_0");
    }

    public void OnMediumPressed()
    {
        DifficultyManager.difficulty = Difficulty.Medium;

        SceneManager.LoadScene("_Scene_0");
    }

    public void OnHardPressed()
    {
        DifficultyManager.difficulty = Difficulty.Hard;

        SceneManager.LoadScene("_Scene_0");
    }

    public void OnQuitPressed()
    {
        Application.Quit();

        #if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

        #endif
    }
}