using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class Events : MonoBehaviour
{
    public GameObject gamePausedPanel;
    public Button pauseButton; 
    public Animator animator;

    private void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;

        if (PlayerManager.gameOver)
            pauseButton.interactable = false;

        if (PlayerManager.gameOver)
            return;


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PlayerManager.isGamePaused)
            {
                ResumeGame();
                gamePausedPanel.SetActive(false);
            }
            else
            {
                PauseGame();
                FindObjectOfType<AudiioManager>().PauseSound("MainTheme");
                FindObjectOfType<AudiioManager>().PlaySound("Menu");
                gamePausedPanel.SetActive(true);
            }

        }
    }


     public void ReplayGame()
     {
         SceneManager.LoadScene("Level");
     }
     public void QuitGame()
     {
         Application.Quit();
     }
     public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void PauseGame()
    {
        if (!PlayerManager.isGamePaused && !PlayerManager.gameOver)
        {
            Time.timeScale = 0;
            PlayerManager.isGamePaused = true;
            animator.SetBool("IsPaused", true);
        }
    }
    public void ResumeGame()
    {
        if (PlayerManager.isGamePaused)
        {
            animator.SetBool("IsPaused", false);
            FindObjectOfType<AudiioManager>().StopSound("Menu");
            Time.timeScale = 1;
            PlayerManager.isGamePaused = false;
            FindObjectOfType<AudiioManager>().PlaySound("MainTheme");
        }
    }
}
