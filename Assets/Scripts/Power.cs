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
                }
                else
                {
                    playerGO.GetComponent<Player>().IsInvincibilityOn(false);
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
                }
                else
                {
                    playerGO.GetComponent<Player>().SetDoubleDamageOn(1f);
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
                playerGO.GetComponent<Player>().Hp = playerGO.GetComponent<Player>().HpMax;
                isActivePower = false;
            }
        }

    }


}
