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
    [SerializeField] private GameObject[] characters = new GameObject[8]; //joueurs + IA
    [SerializeField] private GameObject[] mapPoints = new GameObject[8]; //GameObject qui représente la position des personnages dans le mini-map
    [SerializeField] private Material[] materialsColour = new Material[8]; //Couleur des personnages

    [SerializeField] private CharacterController player;

    
    //panelpause
    [SerializeField] private GameObject pnlPauseMenu;
    [SerializeField] private GameObject pnlGameVictory;
    [SerializeField] private GameObject pnlGameOver;

    public static bool isPaused = false; //est-ce que je suis en pause
    private int opponentGone = 0;

    private float timer = 0f;

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

    // Start is called before the first frame update
    void Start()
    {
        SetColour();
    }

    public void SetColour()
    {
        characters[0].GetComponent<MeshRenderer>().material = materialsColour[PlayerPrefs.GetInt("Player", 0)];
        mapPoints[0].GetComponent<MeshRenderer>().material = materialsColour[PlayerPrefs.GetInt("Player", 0)];
        for (int i = 1; i < characters.Length; i++)
        {
            characters[i].GetComponent<MeshRenderer>().material = materialsColour[PlayerPrefs.GetInt("Enemy" + i, 0)];
            mapPoints[i].GetComponent<MeshRenderer>().material = materialsColour[PlayerPrefs.GetInt("Enemy" + i, 0)];
        }
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

    public void VerifyOpponentPresent()
    {
        opponentGone = 0;

        for(int i = 1; i < characters.Length; i++)
        {
            if (characters[i] == null)
            {
                opponentGone++;
            }
        }

        if(opponentGone >= 7)
        {
            GameVictory();
        }
    }

    public void GameVictory()
    {
        Time.timeScale = 0f;
        pnlGameVictory.SetActive(true);
        player.GetComponent<CharacterController>().enabled = false;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        pnlGameOver.SetActive(true);
        player.GetComponent<CharacterController>().enabled = false;
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

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
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
