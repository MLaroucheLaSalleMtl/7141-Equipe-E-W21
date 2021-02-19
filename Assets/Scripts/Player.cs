using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Characters
{
    public Player(float hp, float damage, float defense) : base(hp, damage, defense) { }

    private Enemy enemy;

    private GameManager manager;

    [SerializeField] private GameObject enemyGO = null;
    [SerializeField] private Image healthBar = null;

    public override void ReceiveDamage()
    {
        this.Hp -= (enemy.Damage - this.Defense);
        healthBar.fillAmount = this.Hp/200f;

        if(this.Hp <= 0f)
        {
            IsDead();
        }
    }

    public override void IsDead()
    {
        //manager.GameOver();
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
        healthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
