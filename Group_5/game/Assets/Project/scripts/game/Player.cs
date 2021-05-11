using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visuals")]
    public Camera PlayerCamera;

    [Header("Gameplay")]
    public int initialHealth = 100;
    public int initialAmmo = 12;
    public float knockbackForce = 10;
    public float hurtDuration = 0.5f;

    private int health;
    public int Health
    {
        get
        {
            return health;
        }
    }
    
    private int ammo;

    public int Ammo
    {
        get
        {
            return ammo;
        }
    }

    private bool killed;
    public bool Killed
    {
        get
        {
            return killed;
        }
    }

    private bool isHurt;
     // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
        ammo = initialAmmo;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ammo > 0 && Killed == false)
            {
                ammo--;
                GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet(true);
                bulletObject.transform.position = PlayerCamera.transform.position + PlayerCamera.transform.forward;
                bulletObject.transform.forward = PlayerCamera.transform.forward;

            }


        }
        
    }

    //check for collision
    
    void OnTriggerEnter(Collider otherCollider)
    {
        
        if (otherCollider.GetComponent<Ammocrate>() != null)
        {
            //collect ammo crate
            Ammocrate ammocrate = otherCollider.GetComponent<Ammocrate>();
            ammo += ammocrate.ammo;

            Destroy(ammocrate.gameObject);
        }
        else if (otherCollider.GetComponent<HealthCrate>() != null)
        {

            //collect health crate
            HealthCrate healthCrate = otherCollider.GetComponent<HealthCrate>();
            health += healthCrate.health;

            Destroy(healthCrate.gameObject);
        }

        if (isHurt == false)
        {
            //gameObject hazard = null;
            if (otherCollider.GetComponent<Enemy>() != null)
            {
                //Touching enemies
                Enemy enemy = otherCollider.GetComponent<Enemy>();
                if(enemy.Killed == false)
                {
                    //hazard = enemy.gameObject;
                    health -= enemy.damage;
                    isHurt = true;
                    Vector3 hurtDirection = (transform.position - enemy.transform.position).normalized;
                    Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                    GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce);
                    StartCoroutine(HurtRoutine());


                }



            }
            else if(otherCollider.GetComponent<Bullet>() != null)
            {
                Bullet bullet = otherCollider.GetComponent<Bullet>();
                if(bullet.ShotByPlayer == false)
                {
                    //hazard = bullet.gameObject; 
                    health -= bullet.damage;
                    isHurt = true;

                    bullet.gameObject.SetActive(false);
                    Vector3 hurtDirection = (transform.position - bullet.transform.position).normalized;
                    Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                    GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce);
                    StartCoroutine(HurtRoutine());



                }
            }
            
            /*if (hazard != null)
            {
                isHurt = true;
                Vector3 hurtDirection = (transform.position - hazard.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce);
                StartCoroutine(HurtRoutine());

            }*/

            if (health <= 0)
            {
                if(killed == false)
                {
                    killed = true;
                    OnKill();
                }
            }

        }
           


    }
    
    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(hurtDuration);

        isHurt = false;
    }

    private void OnKill()
    {
        GetComponent<FirstPersonController>().enabled = false;

    }
}
