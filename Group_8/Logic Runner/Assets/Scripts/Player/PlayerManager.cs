using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isGameStarted;
    public GameObject startingtext;

    public static float score;
    public TextMeshProUGUI scoreText;
    public float PointIncrePerSec;

    public static int numberOfCoins;
    public TextMeshProUGUI CoinsText;

    public static bool isGamePaused;
    public Animator animator;
    


    void Start()
    {
        FindObjectOfType<AudiioManager>().StopSound("GameOver");
        FindObjectOfType<AudiioManager>().PauseSound("Menu");
        FindObjectOfType<AudiioManager>().PlaySound("MainTheme");
        score = 0f;
        PointIncrePerSec = 3f;
        gameOver = isGameStarted = isGamePaused = false;


        Time.timeScale = 1;
        numberOfCoins = 0;
        animator.SetBool("IsPaused", false);
    }

    // Update is called once per frame
    void Update()
    {
        CoinsText.text = "Coins: " + PlayerPrefs.GetInt("TotalCoins", 0).ToString();
        if(isGameStarted)
        {
            score = score + PointIncrePerSec*Time.deltaTime;
            scoreText.text = "Score: "+ (int)score;
        }
        
        

        if(gameOver)
        {   
            
            animator.SetBool("IsGameOver", true);
            FindObjectOfType<AudiioManager>().PlaySound("Collide");
            FindObjectOfType<AudiioManager>().StopSound("MainTheme");
            FindObjectOfType<AudiioManager>().PlaySound("GameOver");
            if ((int)score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", (int)score);
            }

            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            Destroy(gameObject);
        }
        
        //CoinsText.text = "Coins: "+ numberOfCoins;

        if (Input.GetKeyDown(KeyCode.Mouse0)  && !isGameStarted)
        {
            isGameStarted = true;
            Destroy(startingtext);
        }
    }

    
}
