using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDeployment : MonoBehaviour
{
    [SerializeField] private GameObject[] powerRank1 = null;
    [SerializeField] private GameObject[] powerRank2 = null;
    [SerializeField] private GameObject[] powerRank3 = null;
    int randomPower = 0;
    private bool instantiateOnce1 = false;
    private bool instantiateOnce2 = false;
    private bool instantiateOnce3 = false;

    [SerializeField] private Transform[] spawnPositions = null;
    int randomPositionSpawn = 0;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetRandomPosition()
    {
        do
        {
            randomPositionSpawn = Random.Range(0, spawnPositions.Length);
        } while (spawnPositions[randomPositionSpawn].gameObject.activeInHierarchy);
        spawnPositions[randomPositionSpawn].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 5f && timer < 20f)
        {
            if (!instantiateOnce1)
            {
                SetRandomPosition();
                randomPower = Random.Range(0, powerRank1.Length);
                GameObject power1 = Instantiate(powerRank1[randomPower], spawnPositions[randomPositionSpawn].position, spawnPositions[randomPositionSpawn].rotation, spawnPositions[randomPositionSpawn]);
                instantiateOnce1 = true;


            }
        } 
        
        else if(timer >= 20f && timer < 50f)
        {
            if (!instantiateOnce2)
            {
                SetRandomPosition();
                randomPower = Random.Range(0, powerRank2.Length);
                GameObject power2 = Instantiate(powerRank2[randomPower], spawnPositions[randomPositionSpawn].position, spawnPositions[randomPositionSpawn].rotation, spawnPositions[randomPositionSpawn]);
                instantiateOnce2 = true;
            }
        } 
        
        else if(timer >= 50f && timer < 80f)
        {
            if (!instantiateOnce3)
            {
                SetRandomPosition();
                randomPower = Random.Range(0, powerRank3.Length);
                GameObject power3 = Instantiate(powerRank3[randomPower], spawnPositions[randomPositionSpawn].position, spawnPositions[randomPositionSpawn].rotation, spawnPositions[randomPositionSpawn]);
                instantiateOnce3 = true;
            }
        }


    }
}
