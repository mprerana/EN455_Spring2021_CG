using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{

    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI CoinscoreText;

    void Start()
    {
        FindObjectOfType<AudiioManager>().PauseSound("MainTheme");
        FindObjectOfType<AudiioManager>().PlaySound("Menu");
        Time.timeScale = 1;
    }    
    private void Update()
    {
        highScoreText.text = "" + PlayerPrefs.GetInt("HighScore", 0);
        CoinscoreText.text = "Coins: " + PlayerPrefs.GetInt("TotalCoins", 0).ToString();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
