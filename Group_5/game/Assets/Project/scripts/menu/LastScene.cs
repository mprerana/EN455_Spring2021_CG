using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LastScene : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Level1");

    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
