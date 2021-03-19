using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Inventaire
    public int pInvisibility = 0;
    public int pInvincibility = 0;
    public int pDoubleDamage = 0;
    public int pDoubleSpeed = 0;
    public int pDoubleScore = 0;
    public int pInstantHealing = 0;
    [SerializeField] private GameObject okIcon1 = null;
    [SerializeField] private GameObject okIcon2 = null;
    [SerializeField] private GameObject okIcon3 = null;
    [SerializeField] private GameObject okIcon4 = null;
    [SerializeField] private GameObject okIcon5 = null;
    [SerializeField] private GameObject okIcon6 = null;
    [SerializeField] private GameObject nokIcon1 = null;
    [SerializeField] private GameObject nokIcon2 = null;
    [SerializeField] private GameObject nokIcon3 = null;
    [SerializeField] private GameObject nokIcon4 = null;
    [SerializeField] private GameObject nokIcon5 = null;
    [SerializeField] private GameObject nokIcon6 = null;
    //Booléene pour les pouvoirs(Input system)
    public bool isUsingInvisibility = false;
    public bool isUsingInvincibility = false;
    public bool isUsingDoubleDamage = false;
    public bool isUsingDoubleSpeed = false;
    public bool isUsingDoubleScore = false;
    public bool isUsingInstantHealing = false;

    private Power power; //Référence aus Scipt Power
    [SerializeField] private GameObject powerGO; //Référence aus Scipt Power
    public static GameManager instance = null; //mon Gamemanager

    [SerializeField] private GameObject[] pointStart = null; //point de départ du joueur
    [SerializeField] private GameObject[] characters = new GameObject[8]; //joueurs + IA
    [SerializeField] private GameObject[] mapPoints = new GameObject[8]; //GameObject qui représente la position des personnages dans le mini-map
    [SerializeField] private Material[] materialsColour = new Material[8]; //Couleur des personnages
    [SerializeField] private Text[] charactersID = new Text[8]; //Text des noms des personnages Besoin de couleur et non material

    [SerializeField] private CharacterController player; //mon character controller
    [SerializeField] private Characters character = null; //mon joueur


    //panelpause
    [SerializeField] private GameObject pnlPauseMenu; //Menu pause
    [SerializeField] private GameObject pnlGameVictory; //mon écran de victoire
    [SerializeField] private GameObject pnlGameOver; // mon écran de gameOver

    public static bool isPaused = false; //est-ce que je suis en pause
    private int opponentGone = 0; //int pour désigner le nombre de opponent détruit

    private float timer = 0f; //float qui désigne le timer

    //Score
    private float score;
    public float finalScore; //mon score final basé sur le temps passé dans le jeu
    private float hpScore; //mon hpscore
    private float multiplier = 1.25f; // bonus de score
    [SerializeField] private Text timeScoreTxtGO; //Mon time score pour l'écran Game Over
    [SerializeField] private Text hpScoreTxtGO; //Mon Hp score pour l'écran Game Over
    [SerializeField] private Text totalScoreTxtGO; //Mon total score pour l'écran Game Over
    [SerializeField] private Text timeScoreTxtGV; //Mon time score pour l'écran Game Victory
    [SerializeField] private Text hpScoreTxtGV; //Mon Hp score pour l'écran Game Victory
    [SerializeField] private Text totalScoreTxtGV; //Mon total score pour l'écran Game Victory
    private bool isGameOver = false; //ai-je perdu ?
    private bool hasWon = false; //ai-je gagné ?

    //Chrono
    [SerializeField] private Text chrono; //Mon chrono text
    private float chronoTime = 0f;
    private float timeElapsed; //variable pour calculer le temps passé dans le jeu


    void Awake()
    {
        if (instance == null) //si aucune isntance
        {
            instance = this; // créer une nouvelle instance
        }
        else if (instance != null) //si j'ai déja une instance
        {
            Destroy(this); //détruire l'instance actuelle
        }


        pnlPauseMenu.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        power = GetComponent<Power>();
        SetColour(); //Aller à la méthode SetColour pour mettre le couleur au Player et Opponent
    }
    public void OnInvisbility(InputAction.CallbackContext context)
    {
        isUsingInvisibility = context.performed;
    }

    public void OnInvincibility(InputAction.CallbackContext context)
    {
        isUsingInvincibility = context.performed;
    }

    public void OnDoubleDamage(InputAction.CallbackContext context)
    {
        isUsingDoubleDamage = context.performed;
    }

    public void OnDoubleSpeed(InputAction.CallbackContext context)
    {
        isUsingDoubleSpeed = context.performed;
    }

    public void OnDoubleScore(InputAction.CallbackContext context)
    {
        isUsingDoubleScore = context.performed;
    }

    public void OnInstantHealing(InputAction.CallbackContext context)
    {
        isUsingInstantHealing = context.performed;
    }

    public void SetColour() //Méthode pour mettre la couleur
    {
        characters[0].GetComponent<MeshRenderer>().material = materialsColour[PlayerPrefs.GetInt("Player", 0)]; //Donner le material du joueur au couleur dans PlayerPrefs de Player
        mapPoints[0].GetComponent<MeshRenderer>().material = materialsColour[PlayerPrefs.GetInt("Player", 0)]; //Donner le material de l'icône Mini-Map du joueur au couleur dans PlayerPrefs de Player
        charactersID[0].color = materialsColour[PlayerPrefs.GetInt("Player", 0)].color; //Donner la couleur du Text du joueur au couleur dans PlayerPrefs de Player
        for (int i = 1; i < characters.Length; i++) //Boucle For pour traverser le array characters à partir de 1
        {
            characters[i].GetComponent<MeshRenderer>().material = materialsColour[PlayerPrefs.GetInt("Enemy" + i, 0)]; //Donner le material de l'ennemi au couleur dans PlayerPrefs de Enemy
            mapPoints[i].GetComponent<MeshRenderer>().material = materialsColour[PlayerPrefs.GetInt("Enemy" + i, 0)]; //Donner le material de l'icône Mini-Map de l'ennemi au couleur dans PlayerPrefs de Enemy
            charactersID[i].color = materialsColour[PlayerPrefs.GetInt("Enemy" + i, 0)].color; //Donner la couleur du Text de l'enemmi au couleur dans PlayerPrefs de Enemy
        }
    }
    
    public void OnDeployment() //Méthode pour déployer les characters
    {
        for (int i = 0; i < pointStart.Length; i++) //Boucle For pour traverser le array pointStart
        {
            characters[i].transform.position = pointStart[i].transform.position; //Déploie chaque joueur à un start point dans l'arène 
        }
    }

    public void VerifyOpponentPresent() //Méthode pour vérifier la présence des opponents dans la scène
    {
        opponentGone = 0; //initialise le int à 0

        for (int i = 1; i < characters.Length; i++) //Boucle For pour traverser le array characters
        {
            if (characters[i] == null) //Condition If pour si le personnage à cette élément est détruit
            {
                opponentGone++; //Incrémente le int
            }
        }

        if (opponentGone >= 7) //Condition If pour si le int est à 7
        {
            GameVictory(); //Aller à la méthode GameVictory() pour déclarer la victoire du joueur
        }
    }

    public void GameVictory() //Méthode pour la victoire du joueur
    {
        hasWon = true; 
        Time.timeScale = 0f;
        pnlGameVictory.SetActive(true);
        player.GetComponent<CharacterController>().enabled = false;
    }

    public void GameOver() //Méthode pour la défaite du joueur
    {
        isGameOver = true;
        Time.timeScale = 0f;
        pnlGameOver.SetActive(true);
        player.GetComponent<CharacterController>().enabled = false;
    }

    public void ResumeGame() //Méthode pour résumer le jeu
    {
        pnlPauseMenu.SetActive(false);
        Time.timeScale = 1f; //remet le temps de jeu à son cours normal
        isPaused = false;
        player.GetComponent<CharacterController>().enabled = true;
    }

    public void PauseGame() //Méthode pour mettre le jeu en pause
    {
        pnlPauseMenu.SetActive(true);
        Time.timeScale = 0f; //freeze le temps dans le jeu
        isPaused = true;
        player.GetComponent<CharacterController>().enabled = false;
    }

    public void ReturnToMainMenu() //Méthode pour retourner au menu principal
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
            CancelInvoke("OnDeployment"); //le déploiment est annulé après 3 secondes pour permettre au joueurs de patrouiller dans l'arène
        }

        //Vérifier les Opponents dans la scène.
        VerifyOpponentPresent();

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

        //TimeScore
        score = timer * multiplier; //mon score dnas le temps
        hpScore = character.GetComponent<Characters>().Hp * multiplier; //mes points HP * bonus
        finalScore = score + hpScore; //score final
        //Debug.Log(score);
        if (isGameOver || hasWon)
        {
            if (character.GetComponent<Characters>().Hp < 0)
            {
                //Affichage du score
                hpScore = 0;
                timeScoreTxtGO.text = "Time Score: " + finalScore.ToString("F4");
                hpScoreTxtGO.text = "HP Score: " + hpScore.ToString("F4");
                totalScoreTxtGO.text = "Total Score: " + finalScore.ToString("F4");
                timeScoreTxtGV.text = "Time Score: " + finalScore.ToString("F4");
                hpScoreTxtGV.text = "HP Score: " + hpScore.ToString("F4");
                totalScoreTxtGV.text = "Total Score: " + finalScore.ToString("F4");
                if(powerGO.GetComponent<Power>().isDoubleScore) //A revoir
                {
                    finalScore = finalScore * 2;
                    totalScoreTxtGV.text = "Total Score: " + finalScore.ToString("F4");
                }
            }
            else
            {
                //Affichage du score
                timeScoreTxtGO.text = "Time Score: " + finalScore.ToString("F4");
                hpScoreTxtGO.text = "HP Score: " + hpScore.ToString("F4");
                totalScoreTxtGO.text = "Total Score: " + finalScore.ToString("F4");
                timeScoreTxtGV.text = "Time Score: " + finalScore.ToString("F4");
                hpScoreTxtGV.text = "HP Score: " + hpScore.ToString("F4");
                totalScoreTxtGV.text = "Total Score: " + finalScore.ToString("F4");
            }
        }


        //Chrono
        chronoTime += Time.deltaTime;
        int minutes = (int) chronoTime / 60; // minutes écoulés
        int seconds = (int) chronoTime - (minutes * 60); //secondes écoulées
        chrono.text = "Time : " + minutes.ToString("D2") + ":" + seconds.ToString("D2"); //convertit le temps passé en string pour l'afficher sur le canvas txt_timer

        
        //Debug.Log(pInvisibility);
        //Inventaire
        //invisbility
        if(pInvisibility > 0) 
        { 
            okIcon1.SetActive(true);
            nokIcon1.SetActive(false);
        } 
        else 
        {
            okIcon1.SetActive(false);
            nokIcon1.SetActive(true);
        }

        //invincibility
        if (pInvincibility > 0)
        {
            okIcon2.SetActive(true);
            nokIcon2.SetActive(false);
        }
        else
        {
            okIcon2.SetActive(false);
            nokIcon2.SetActive(true);
        }

        //instant healing
        if (pInstantHealing > 0)
        {
            okIcon3.SetActive(true);
            nokIcon3.SetActive(false);
        }
        else
        {
            okIcon3.SetActive(false);
            nokIcon3.SetActive(true);
        }

        //double speed
        if (pDoubleSpeed > 0)
        {
            okIcon4.SetActive(true);
            nokIcon4.SetActive(false);
        }
        else
        {
            okIcon4.SetActive(false);
            nokIcon4.SetActive(true);
        }

        //double damage
        if (pDoubleDamage > 0)
        {
            okIcon5.SetActive(true);
            nokIcon5.SetActive(false);
        }
        else
        {
            okIcon5.SetActive(false);
            nokIcon5.SetActive(true);
        }

        //double score
        if (pDoubleScore > 0)
        {
            okIcon6.SetActive(true);
            nokIcon6.SetActive(false);
        }
        else
        {
            okIcon6.SetActive(false);
            nokIcon6.SetActive(true);
        }
    }
}
