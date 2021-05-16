using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColorB : MonoBehaviour
{
    private Renderer rend;

    [SerializeField]

    private Color ColorToTurnTo1 = Color.white;
    private Color ColorToTurnTo2 = Color.red;
    private Color ColorToTurnTo3 = Color.blue;
    private Color ColorToTurnTo4 = Color.yellow;
    private Color ColorToTurnTo5 = Color.green;
   

    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update() 
    {
        if(PlayerManager.isGameStarted)
        {
            rend.material.color = ColorToTurnTo1;
            StartCoroutine(Coloring());
            rend.material.color = ColorToTurnTo2;
            StartCoroutine(Coloring());
            rend.material.color = ColorToTurnTo3;
            StartCoroutine(Coloring());
            rend.material.color = ColorToTurnTo4;
            StartCoroutine(Coloring());
            rend.material.color = ColorToTurnTo5;
            StartCoroutine(Coloring());
        }    
    }
    private IEnumerator Coloring()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
