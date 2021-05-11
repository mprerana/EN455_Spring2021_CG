using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamecontroller : MonoBehaviour
{
    [Header("Game")]
    public Player player;
    public GameObject EnemyContainer;
    public GameObject textDisplay;


    [Header("UI")]
    public Text healthText;
    public Text ammoText;
    public Text enemyText;
    public Text infoText;
    public Text Timetext;

    public int secondsLeft = 60;
    public bool takingAway = false;
    private bool gameOver = false;
    private bool GameOver = false;

    private float resetTimer = 3f;

    void Start()
    {
        infoText.gameObject.SetActive(false);
        Timetext.gameObject.SetActive(false);
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
    }
    
 
    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + player.Health;
        ammoText.text = "Vaccine: " + player.Ammo;

        int aliveEnemies = 0;
        foreach(Enemy enemy in EnemyContainer.GetComponentsInChildren<Enemy>())
        {
            if (enemy.Killed == false)
            {
                aliveEnemies++;
            }
        }
        enemyText.text = "Virus: " + aliveEnemies;

        if(aliveEnemies == 0)
        {
            gameOver = true;
            infoText.gameObject.SetActive(true);
            infoText.text = "You Protected The City ! \n Mission Accomplished";
        }

        if (player.Killed == true)
        {
            GameOver = true;
            infoText.gameObject.SetActive(true);
            infoText.text = "You Have been Infected :( ! \n Mission Failed";

        }
        

        if (gameOver == true)
        {
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }

        if (GameOver == true)
        {
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
        if (takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }


        if (secondsLeft == 00)
        {

            Timetext.gameObject.SetActive(true);
            Timetext.text = "Time's up";
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }

        }
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if (secondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;

        }
        else
        {
            textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        }

        takingAway = false;

    }
}
