using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    //Panels
    [SerializeField] private GameObject pnl_MainMenu;
    [SerializeField] private GameObject pnl_Options;
    [SerializeField] private GameObject pnl_ChooseColor;


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
    }

    public void OnQuitGame()
    {
        Application.Quit();
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
