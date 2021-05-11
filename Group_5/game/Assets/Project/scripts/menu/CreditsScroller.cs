using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroller : MonoBehaviour
{
    private float resetTimer = 30f;
    

    // Update is called once per frame
    void Update()
    {
        resetTimer -= Time.deltaTime;
        if (resetTimer <= 0)
        {
            SceneManager.LoadScene("Menu");
        }

    }
}
