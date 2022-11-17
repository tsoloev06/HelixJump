using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public ParticleSystem PaintSlash;
    public ParticleSystem PlatformDestruction;
    public ParticleSystem Confetti;
    public static bool gameOver;
    public static bool levelWin;

    public GameObject gameOverPannel;
    public GameObject levelWinPannel;

    public static int CurrentLevelIndex;
    public static int noOfPassingRings;


    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public static int numberOfPassedRings;
    public static int score = 0;

    public Slider ProgressBar;

    public void Awake()
    {
        CurrentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }


    private void Start()
    {
        PaintSlash.Play();
        Time.timeScale = 1f;
        noOfPassingRings = 0;
        numberOfPassedRings = 0;
        highScoreText.text = "Best Score\n" + PlayerPrefs.GetInt("HighScore", 0);
        gameOver = false;
        levelWin = false;
        
    }
    private void Update() {
        currentLevelText.text = CurrentLevelIndex.ToString();
        nextLevelText.text = (CurrentLevelIndex + 1).ToString();

        //update our slider
        int progress = noOfPassingRings * 100 / FindObjectOfType<HelixManager>().noOfRings;
        ProgressBar.value = progress;

        scoreText.text = score.ToString();
        if (gameOver)
        {
            Time.timeScale = 0;
            PlatformDestruction.Play();
            gameOverPannel.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                if(score> PlayerPrefs.GetInt("HighScore", 0))
                {
                    PlayerPrefs.SetInt("HighScore", score);
                }
                SceneManager.LoadScene(0);
                score = 0; 
            }
        }

        if (levelWin) {
            Confetti.Play();
            levelWinPannel.SetActive(true);
            if (Input.GetMouseButtonDown(0)) {
                PlayerPrefs.SetInt("CurrentLevelIndex", CurrentLevelIndex + 1);
                SceneManager.LoadScene(0);
            }
        }
    }
}
