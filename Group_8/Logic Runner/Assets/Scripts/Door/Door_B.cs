using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_B : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && TileManager.DoorB)
        {
            FindObjectOfType<AudiioManager>().PlaySound("Break");
            animator.SetTrigger("DoorB");
            TileManager.DoorB = false;
            PlayerManager.score += 100;
        }
    }
}
