using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseArenaScreen : MonoBehaviour
{

    //Selection de l'ar�ne
    [SerializeField] private GameObject pnlChooseArena; //panel choose arena
    private GameObject btn_Forest; //bouton arene forest
    private GameObject btn_Ruins; //bouton arene ruins

    private void Awake()
    {
        pnlChooseArena.SetActive(true);
    }

    public void OnForestClick()
    {
        pnlChooseArena.SetActive(false);
        SceneManager.LoadScene(1); //Charge la sc�ne ind�x�e
    }

    public void OnRuinsClick()
    {
        pnlChooseArena.SetActive(false);
        SceneManager.LoadScene(2); //Charge la sc�ne ind�x�e
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
