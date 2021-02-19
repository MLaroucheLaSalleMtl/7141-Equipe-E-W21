using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Characters : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float hpMax;
    [SerializeField] private float damage;
    [SerializeField] private float defense;

    public Characters()
    {
        this.Hp = 200f;
        this.HpMax = 200f;
        this.Damage = 50f;
        this.Defense = 15f;
    }

    public Characters(float hp, float damage, float defense)
    {
        this.Hp = hp;
        this.HpMax = hpMax;
        this.Damage = damage;
        this.Defense = defense;
    }

    public float Hp { get => hp; set => hp = value; }
    public float HpMax { get => hpMax; set => hpMax = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Defense { get => defense; set => defense = value; }

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
