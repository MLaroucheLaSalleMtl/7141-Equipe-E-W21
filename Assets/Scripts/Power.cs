using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Power : MonoBehaviour
{
    public enum PowerType { Invisibility, Invincibility, DoubleSpeed, DoubleDamage, DoubleScore, InstantHealing  }

    private PowerType actualPower;
    private GameManager manager;
    [SerializeField] private CharacterController capman;
    [SerializeField] private NavMeshAgent agentCapman;
    [SerializeField] private Renderer myRenderer;
    private Characters character;
    private Player player;
    [SerializeField] private GameObject playerGO;
    [SerializeField] private GameObject ballGO;
    private BallTimer ball;

    private float playerDefense = 0;
    private float ballDmg = 0;

    public List<Power> powerList;

    public bool isActivePower = false;
    public bool isInvisible = false;
    public bool isInvincible = false;
    public bool isDoubleSpeed = false;
    public bool isDoubleDamage = false;
    public bool isDoubleScore = false;
    public bool isInstantHealing = false;

    private float timer = 10f;

    void Start()
    {
        capman = GetComponent<CharacterController>();
        myRenderer = GetComponent<Renderer>();
        agentCapman = GetComponent<NavMeshAgent>();
        manager = GameManager.instance;
        powerList.Capacity = 2;
        //playerDefense = GetComponent<Player>();
    }

    void Invisibility()
    {
        isInvisible = true;
        actualPower = PowerType.Invisibility;
    }

    void Invincibility()
    {
        isInvincible = true;
        actualPower = PowerType.Invincibility;
    }

    void DoubleSpeed()
    {
        isDoubleSpeed = true;
        actualPower = PowerType.DoubleSpeed;
    }

    void DoubleDamage()
    {
        isDoubleDamage = true;
        actualPower = PowerType.DoubleDamage;
    }

    void DoubleScore()
    {
        isDoubleScore = true;
        actualPower = PowerType.DoubleScore;
    }

    void InstantHealing()
    {
        isInstantHealing = true;
        actualPower = PowerType.InstantHealing;
    }

    void Update()
    {
        //Pourvoir d'invisibilité
        if (isInvisible)
        {
            isActivePower = true;
            if (isActivePower)
            {
                timer -= Time.deltaTime;
                myRenderer.enabled = false;
                if (timer <= 0f)
                {
                    myRenderer.enabled = true;
                    isActivePower = false;
                    isInvisible = false;
                    timer = 10f;
                }
            }

        }

        //Pouvoir d'invincibilité
        if (isInvincible)
        {
            isActivePower = true;
            if (isActivePower)
            {
                timer -= Time.deltaTime;
                if (timer > 0f)
                {
                    playerGO.GetComponent<Player>().IsInvincibilityOn(true);
                    /*
                    playerDefense = playerGO.GetComponent<Player>().Defense;
                    //character.GetComponent<Characters>().Defense = ball.GetComponent<BallTimer>().BallDamage;
                    playerGO.GetComponent<Player>().Defense = 50;
                    //player.GetComponent<Player>().Defense = player.GetComponent<Player>().Defense * 2;
                    */
                }
                else
                {
                    playerGO.GetComponent<Player>().IsInvincibilityOn(false);
                    
                    //playerGO.GetComponent<Player>().Defense = playerDefense;
                    //character.GetComponent<Characters>().Defense = character.GetComponent<Characters>().Defense;
                    //player.GetComponent<Player>().Defense = player.GetComponent<Player>().Defense;
                    isActivePower = false;
                    isInvincible = false;

                }
            }
        }

        //Pouvoir DoubleSpeed
        if (isDoubleSpeed)
        {
            isActivePower = true;
            if (isActivePower)
            {
                timer -= Time.deltaTime;
                if (timer > 0f)
                {
                    float newSpeed = agentCapman.speed * 1.5f;
                    agentCapman.speed = newSpeed;
                }
                else
                {
                    agentCapman.speed = agentCapman.speed;
                    isActivePower = false;
                }
            }
        }

        //Pouvoir DoubleDamage
        if (isDoubleDamage)
        {
            isActivePower = true;
            if (isActivePower)
            {
                timer = 5.0f;
                timer -= Time.deltaTime;
                if (timer > 0f)
                {
                    playerGO.GetComponent<Player>().SetDoubleDamageOn(2f);
                    //ballDmg = ballGO.GetComponent<BallTimer>().BallDamage;
                    //ball.GetComponent<BallTimer>().BallDamage = ball.GetComponent<BallTimer>().BallDamage * 2;
                    //ballGO.GetComponent<BallTimer>().BallDamage = ballGO.GetComponent<BallTimer>().BallDamage * 2;
                }
                else
                {
                    playerGO.GetComponent<Player>().SetDoubleDamageOn(1f);
                    //ball.GetComponent<BallTimer>().BallDamage = ball.GetComponent<BallTimer>().BallDamage;
                    //ballGO.GetComponent<BallTimer>().BallDamage = ballDmg;
                    isActivePower = false;
                }
            }
        }

        //Pouvoir DoubleScore
        if (isDoubleScore)
        {
            isActivePower = true;
            if (isActivePower)
            {
                manager.finalScore = manager.finalScore * 2;
            }
        }

        //Pouvoir Instant Healing
        if (isInstantHealing)
        {
            isActivePower = true;
            if (isActivePower)
            {
                //character.GetComponent<Characters>().Hp = character.GetComponent<Characters>().HpMax;
                playerGO.GetComponent<Player>().Hp = playerGO.GetComponent<Player>().HpMax;
                isActivePower = false;
            }
        }

    }


}
