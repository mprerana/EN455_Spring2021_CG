using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifeduration = 2f;
    public int damage = 5;

    private float lifetimer;

    private bool shotByPlayer;
    public bool ShotByPlayer
    {
        get
        {
            return shotByPlayer;
        }

        set
        {
            shotByPlayer = value;
        }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        lifetimer = lifeduration;
        
    }

    // Update is called once per frame
    void Update()
    {
        //make the bullet move
        transform.position += transform.forward * speed * Time.deltaTime;

        //check if the bullet should disappear
        lifetimer -= Time.deltaTime;
        
        if(lifetimer <= 0f)
        {
            gameObject.SetActive(false);
        }
        
    }
}
