using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Characters : MonoBehaviour
{
    [SerializeField] private float hp; //float pour d�signer le hp 
    [SerializeField] private float hpMax; //float pour d�signer le hpmax
    private float hpBase; //float pour d�signer la base de hp
    private float defense; //float pour d�signer la d�fense
    private float defenseBase; //float pour d�signer la base de d�fense

    public Characters() //Constructeur par d�faut
    {
        this.hp = 200f;
        this.hpMax = 200f;
        this.hpBase = 200f;
        this.defense = 15f;
        this.defense = 15f;
    }

    public Characters(float hp, float defense)  
    {
        this.hp = hp;
        this.hpMax = hp;
        this.hpBase = hp;
        this.defense = defense;
        this.defenseBase = defense;
    }

    //Encapsulation
    public float Hp { get => hp; set => hp = value; }
    public float HpMax { get => hpMax; set => hpMax = value; }
    public float HpBase { get => hpBase; set => hpBase = value; }
    public float Defense { get => defense; set => defense = value; }
    public float DefenseBase { get => defenseBase; set => defenseBase = value; }

    public abstract void ReceiveDamage(Collision collision); //M�thode Abstract ReceiveDamage

    public abstract void IsDead(); //M�thode Abstract IsDead()

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
