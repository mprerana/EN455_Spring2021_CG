using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate( 0,150*Time.deltaTime,0);
    }


    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins", 0) + 1);
            //PlayerPrefs.DeleteKey("TotalCoins");
            FindObjectOfType<AudiioManager>().PlaySound("pickupcoins");
            PlayerManager.numberOfCoins += 1;
            //Debug.Log("Coins: " + PlayerManager.numberOfCoins);
            PlayerManager.score += 2;
            Destroy(gameObject);
        }
        
    }
}
