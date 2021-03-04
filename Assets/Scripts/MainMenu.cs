using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    //Panels
    [SerializeField] private GameObject pnl_MainMenu; //Mon main menu 
    [SerializeField] private GameObject pnl_Options; //menu options
    [SerializeField] private GameObject pnl_HowToPlay; //menu how to play
    [SerializeField] private GameObject pnl_ChooseColor; //séléction de couleur


    void Awake()
    {
        pnl_MainMenu.SetActive(true);
    }

    public void OnStartGame()
    {
        pnl_MainMenu.SetActive(false);
        pnl_ChooseColor.SetActive(true);
    }

    public void OnOptions()
    {
        pnl_MainMenu.SetActive(false);
        pnl_Options.SetActive(true);
    }

    public void OnReturnToMenu()
    {
        pnl_MainMenu.SetActive(true);
        pnl_Options.SetActive(false);
        pnl_HowToPlay.SetActive(false);
    }

    public void OnHowToPlay()
    {
        pnl_MainMenu.SetActive(false);
        pnl_HowToPlay.SetActive(true);
    }

    public void OnQuitGame()
    {
        Application.Quit(); //quitte l'application
    }
}
