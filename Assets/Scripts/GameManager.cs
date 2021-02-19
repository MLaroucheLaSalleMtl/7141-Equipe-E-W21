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
    float timer = 0f;
    //panelpause
    [SerializeField] private GameObject pnlPauseMenu;
    public static bool isPaused = false; //est-ce que je suis en pause

    [SerializeField] private CharacterController player;

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


        pnlPauseMenu.SetActive(false);
    }

    public void ResumeGame()
    {
        pnlPauseMenu.SetActive(false);
        Time.timeScale = 1f; //remet le temps de jeu à son cours normal
        isPaused = false;
        player.GetComponent<CharacterController>().enabled = true;
    }

    public void PauseGame()
    {
        pnlPauseMenu.SetActive(true);
        Time.timeScale = 0f; //freeze le temps dans le jeu
        isPaused = true;
        player.GetComponent<CharacterController>().enabled = false;
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }


    public void OnDeployment()
    {
        //timer += Time.deltaTime;
        ////Characters deployment on arena
        //if(timer < 3f)
        //{
        for (int i = 0; i < pointStart.Length; i++)
        {
            characters[i].transform.position = pointStart[i].transform.position;
        }
        //}
        //else { return; }
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //Characters deployment on arena
        if (timer < 3f)
        {
            OnDeployment();
        }
        else
        {
            CancelInvoke("OnDeployment");
        }
        Debug.Log(timer);

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); //résume le jeu
            }
            else
            {
                PauseGame(); //mets le jeu en pause
            }
        }

    }
}
