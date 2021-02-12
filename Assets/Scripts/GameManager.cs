using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null; //mon Gamemanager

    [SerializeField] private GameObject[] pointStart = null; //point de départ du joueur
    [SerializeField] private GameObject[] characters = null; //joueurs + IA


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }

        OnDeployment();
    }

    public void OnDeployment()
    {
        //Characters deployment on arena
        for (int i = 0; i < pointStart.Length; i++)
        {
            //int randomDisplay = random.Next(0, 7);

            characters[i].transform.position = pointStart[i].transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}
