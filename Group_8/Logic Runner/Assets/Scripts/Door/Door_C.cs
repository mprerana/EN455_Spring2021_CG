using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_C : MonoBehaviour
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
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && TileManager.DoorC)
        {
            FindObjectOfType<AudiioManager>().PlaySound("Break");
            animator.SetTrigger("DoorC");
            TileManager.DoorC = false;
            PlayerManager.score += 100;
        }
    }
}
