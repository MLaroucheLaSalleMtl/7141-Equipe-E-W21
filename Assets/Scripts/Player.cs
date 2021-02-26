using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Characters
{
    public Player() { }
    public Player(float hp, float damage, float defense) : base(hp, damage, defense) { }

    private Enemy enemy;

    private GameManager manager;

    [SerializeField] private GameObject enemyGO = null;
    [SerializeField] private Image healthBar = null;

    private float dmgReceived = 0;

    public override void ReceiveDamage()
    {
        dmgReceived = enemy.Damage - this.Defense;
        //Debug.Log("Player Defense " + this.Defense);
        //Debug.Log("Enemy Damage " + enemy.Damage);
        //Debug.Log("Player Damage Received " + dmgReceived);
        this.Hp -= dmgReceived;
        healthBar.fillAmount = this.Hp/200f;
        //Debug.Log(this.Hp);
        //Debug.Log(healthBar.fillAmount);
        if(this.Hp <= 0f)
        {
            IsDead();
        }
    }

    public override void IsDead()
    {
        manager.GameOver();
    }

    public bool ReturnDead()
    {
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
            ReceiveDamage();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        enemy = enemyGO.GetComponent<Enemy>();
        healthBar.fillAmount = 1f;
        //Debug.Log("Player Defense " + this.Defense);
        //Debug.Log("Enemy Damage " + enemy.Damage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
