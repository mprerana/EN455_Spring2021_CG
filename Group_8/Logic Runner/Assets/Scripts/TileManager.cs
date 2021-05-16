using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int NumberOfTiles = 3;
    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;
    public GameObject quizPannel;
    public static bool QuizPanel = false;
    public Animator animator;
    public static bool DoorA = false;
    public static bool DoorB = false;
    public static bool DoorC = false;
    public static bool speeds = true;


    void Start()
    {
        for (int i = 0; i < NumberOfTiles; i++)
        {
            if (i==0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(1,tilePrefabs.Length-1));
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(zSpawn%180 == 0 && playerTransform.position.z  > zSpawn + 30 -(NumberOfTiles*tileLength))
        {
            SpawnTile(8);
            DeleteTile();
            StartCoroutine(EnableQuizPannel());
            
        }else
        {
            if(playerTransform.position.z  > zSpawn + 30 -(NumberOfTiles*tileLength))
        {
            SpawnTile(Random.Range(1,tilePrefabs.Length-1));
            DeleteTile();
        }
        }

    
    }


    public void SpawnTile(int tileIndex)
    {
        GameObject Go = Instantiate(tilePrefabs[tileIndex], transform.forward*zSpawn, transform.rotation);
        activeTiles.Add(Go);
        zSpawn += tileLength;
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
        PlayerManager.score += 5;
    }
    private IEnumerator EnableQuizPannel()
    {   
        speeds = false;
        yield return new WaitForSeconds(200/111);
        quizPannel.SetActive(true);
        QuizPanel = true;  
        yield return new WaitForSeconds(460/111);
        quizPannel.SetActive(false);
        //yield return new WaitForSeconds(100/111);

        animator.SetTrigger("Jump");
        yield return new WaitForSeconds(1.2f);
        speeds = true;
        
    }

    
}
