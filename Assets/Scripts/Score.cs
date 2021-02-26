using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    private float score;
    private float finalScore;
    private float multiplier = 1.25f;
    private float timer = 0f;
    private Text scoreTxt;

    [SerializeField] private GameObject pnlScore = null;

    void Awake()
    {
        pnlScore.SetActive(false);
    }

    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {

        timer = Time.deltaTime;

        while(Time.timeScale == 1.0f)
        {
            foreach(GameObject player in characters)
            {
                if(player != null)
                {
                    score = timer * multiplier;
                }
                else
                {
                    finalScore = score;
                    scoreTxt.text = finalScore.ToString();
                }
            }
        }

        foreach (GameObject player in characters)
        {
            if(characters.Length ==  characters.Length - 6)
            {
                Time.timeScale = 0f;
                pnlScore.SetActive(true);
            }
        }
    }
}
