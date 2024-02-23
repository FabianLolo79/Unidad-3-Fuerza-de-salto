using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject ObstaclePrefab;

    private Vector3 spawnPosition = new Vector3(20, 0, 0);
    private float starDelay = 2f;
    private float repeatRate = 2f;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", starDelay, repeatRate); //instancia obstáculos por tiempo
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // generador de obstáculos 
    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(ObstaclePrefab, spawnPosition, ObstaclePrefab.transform.rotation);
        }
    }
}
