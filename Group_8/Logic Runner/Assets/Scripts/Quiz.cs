using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    public Sprite[] QuizPrefebs;
    public GameObject Img;
    private int Question;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TileManager.QuizPanel == true)
        {
            Question = Random.Range(0,QuizPrefebs.Length);
            // Sprite QuizPrefeb = QuizPrefebs[Question];
            ChangeImg();
            TileManager.QuizPanel = false;
        }
        
    }
    private void ChangeImg()
    {
        Img.GetComponent<Image>().sprite = QuizPrefebs[Question];
        QuizQuestion();
    }


    private void QuizQuestion()
    {        
        if(Question%3 == 0)
        {
            TileManager.DoorA = true;
        }else if(Question%3 == 1)
        {
            TileManager.DoorB = true;
        }
        else if(Question%3 == 2)
        {
            TileManager.DoorC = true;
        }
    
    }
}
