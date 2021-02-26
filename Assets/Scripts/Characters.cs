using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Characters : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float hpMax;
    private float damage;
    private float damageBase;
    private float defense;
    private float defenseBase;

    public Characters()
    {
        this.hp = 200f;
        this.hpMax = 200f;
        this.damage = 30f;
        this.damageBase = 30f;
        this.defense = 15f;
        this.defense = 15f;
    }

    public Characters(float hp, float damage, float defense)
    {
        this.hp = hp;
        this.hpMax = hp;
        this.damage = damage;
        this.damageBase = damage;
        this.defense = defense;
        this.defenseBase = defense;
    }

    public float Hp { get => hp; set => hp = value; }
    public float HpMax { get => hpMax; set => hpMax = value; }
    public float Damage { get => damage; set => damage = value; }
    public float DamageBase { get => damageBase; set => damageBase = value; }
    public float Defense { get => defense; set => defense = value; }
    public float DefenseBase { get => defenseBase; set => defenseBase = value; }

    public abstract void ReceiveDamage();

    public abstract void IsDead();

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
