using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_A : MonoBehaviour
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
        if(other.tag == "Player" && TileManager.DoorA)
        {
            FindObjectOfType<AudiioManager>().PlaySound("Break");
            animator.SetTrigger("DoorA");
            TileManager.DoorA = false;
            PlayerManager.score += 100;
        }
    }
}
