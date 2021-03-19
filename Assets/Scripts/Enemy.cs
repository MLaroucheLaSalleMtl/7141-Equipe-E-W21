using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Characters
{
    public Enemy() { } //Constructeur par d�faut
    public Enemy(float hp, float defense) : base(hp, defense) { } //Constructeur avec les valeurs du script Characters

    private BaseArea baseAreaBenefit; //Script BaseArea
    [SerializeField] private GameObject player = null;
    private GameObject damageOrigin = null;
    private string dmgType = " ";

    [SerializeField] private GameObject baseArea = null; //GameObject pour associer le baseArea au opponent
    [SerializeField] private Slider sliderHealthBar = null; //Silder pour ajuster la valeur de la barre de vie.
    [SerializeField] private Image fillHealthBar = null; //Image pour ajuster le UI de la barre de vie.
    [SerializeField] private Text txtHealthBar = null; //Text pour afficher la valeur de la barre de vie

    private float dmgReceived = 0; //float pour garder la valeur de damage recu.

    public override void ReceiveDamage()
    {
        //dmgReceived = collision.gameObject.GetComponent<BallTimer>().BallDamage * baseAreaBenefit.BaseDamageBenefit() - this.Defense * baseAreaBenefit.BaseDefenseBenefit(); //�quation qui enl�ve le damage par la d�fense 


        if (damageOrigin == player) 
        {
            if (dmgType == "Melee")
            {
                dmgReceived = damageOrigin.GetComponent<Equipment>().DamageMelee(damageOrigin.GetComponent<Player>().MyMelee) * damageOrigin.GetComponent<Player>().GetPlayerBaseAreaBenefit().BaseDamageBenefit() - gameObject.GetComponent<Equipment>().DefenseArmor(MyArmor) * baseAreaBenefit.BaseDefenseBenefit();
            }
            else if (dmgType == "Range")
            {
                dmgReceived = damageOrigin.GetComponent<Equipment>().DamageRange(damageOrigin.GetComponent<Player>().MyRange) * damageOrigin.GetComponent<Player>().GetPlayerBaseAreaBenefit().BaseDamageBenefit() - gameObject.GetComponent<Equipment>().DefenseArmor(MyArmor) * baseAreaBenefit.BaseDefenseBenefit();
            }
        }
        else 
        {
            if (dmgType == "Melee")
            {
                dmgReceived = damageOrigin.GetComponent<Equipment>().DamageMelee(damageOrigin.GetComponent<Enemy>().MyMelee) * damageOrigin.GetComponent<Enemy>().GetEnemyBaseAreaBenefit().BaseDamageBenefit() - gameObject.GetComponent<Equipment>().DefenseArmor(MyArmor) * baseAreaBenefit.BaseDefenseBenefit();
            }
            else if (dmgType == "Range")
            {
                dmgReceived = damageOrigin.GetComponent<Equipment>().DamageRange(damageOrigin.GetComponent<Enemy>().MyRange) * damageOrigin.GetComponent<Enemy>().GetEnemyBaseAreaBenefit().BaseDamageBenefit() - gameObject.GetComponent<Equipment>().DefenseArmor(MyArmor) * baseAreaBenefit.BaseDefenseBenefit();
            }
        }

        this.Hp -= dmgReceived; //Soustrait le Hp par le dmgReceived.

        if (this.Hp <= 0f) //Condition If pour si la valeur Hp est inf�rieure ou �gale � 0
        { 
            SetHealthBar(); //Aller � la m�thode SetHealthBar() pour ajuster la barre de vie avant de mourir
            IsDead(); //Aller � la m�thode IsDead() pour aller mourir
        }
    }

    public override void IsDead() //M�thode pour si le opponent est mort
    {
        Destroy(gameObject); //D�truire le gameObject de ce script
    }

    public bool ReturnDead() //M�thode pour retourner un bool
    {
        return false; //Retourne false
    }

    private void OnCollisionEnter(Collision collision) //M�thode pour si il y a une collision.
    {
        if(collision.gameObject.tag == "Damage") //Condition If pour si le tag du gameObject de la collision est un "Damage"
        {
            //ReceiveDamage(collision); //Aller � la m�thode ReceiveDamage() avec la collision
        }
        if (collision.gameObject.tag == "Melee")
        {
            damageOrigin = collision.gameObject.GetComponent<Weapon>().GetOrigin(); 
            dmgType = "Melee";
            ReceiveDamage();
        }
        if (collision.gameObject.tag == "Range")
        {
            damageOrigin = collision.gameObject.GetComponent<Weapon>().GetOrigin();
            dmgType = "Range";
            ReceiveDamage();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        baseAreaBenefit = baseArea.GetComponent<BaseArea>(); //Cache le baseAreaBenefit
    }

    public void SetHealthBar() //M�thode pour ajuster la barre de vie
    {
        SetFillHealthBar(); //Aller � la m�thode SetFillHealthBar() pour ajuster la valeur de la barre de vie
        SetColorHealthBar(); //Aller � la m�thode SetColorHealthBar() pour changer la couleur de la barre de vie selon la valeur.
        SetTextHealthBar(); //Aller � la m�thode SetTextHealthBar() pour changer la valeur affich� de la barre de vie.
    }

    public void SetFillHealthBar() //M�thode pour bouger la barre de vie
    {
        sliderHealthBar.value = this.Hp; //Donner la valeur du slider la vie du joueur.
    }

    public void SetColorHealthBar() //M�thode pour changer la couleur de la barre de vie selon la valeur.
    {
        if (this.Hp >= 0.6 * this.HpMax) //Condition If pour si la vie du joueur est sup�rieure ou �gale � 60% de sa vie maximum.
        {
            fillHealthBar.color = Color.green; //Changer la couleur du fillHealthBar par Vert
        }
        else if (this.Hp >= 0.3 * this.HpMax && this.Hp < 0.6 * this.HpMax) //Condition If pour si la vie du joueur est sup�rieure ou �gale � 30% et inf�rieur � 60% de sa vie maximum.
        {
            fillHealthBar.color = Color.yellow; //Changer la couleur du fillHealthBar par Jaune
        }
        else if (this.Hp >= 0 * this.HpMax && this.Hp < 0.3 * this.HpMax) //Condition If pour si la vie du joueur est sup�rieure ou �gale � 0% et inf�rieur � 30% de sa vie maximum.
        {
            fillHealthBar.color = Color.red; //Changer la couleur du fillHealthBar par Rouge
        }
    }

    public void SetTextHealthBar() //M�thode pour changer la valeur affich� de la barre de vie.
    {
        if (this.Hp >= 100) //Condition If pour si la valeur de Hp est sup�rieure ou �gale � 100.
        {
            txtHealthBar.text = Convert.ToInt32(this.Hp).ToString("D3"); //Afficher la valeur de Hp � 3 chiffres
        }
        else if (this.Hp < 100) //Condition If pour si la valeur de Hp est inf�rieure � 100.
        {
            txtHealthBar.text = Convert.ToInt32(this.Hp).ToString("D2"); //Afficher la valeur de Hp � 2 chiffres
        } 
        if(this.Hp < 0) //Condition If pour si la valeur de Hp est inf�rieure ou �gale � 0.
        {
            txtHealthBar.text = Convert.ToInt32(this.Hp * 0).ToString("D1"); //Afficher 0
        }
    }

    public BaseArea GetEnemyBaseAreaBenefit()
    {
        return baseAreaBenefit;
    }

    // Update is called once per frame
    void Update()
    {
        SetHealthBar(); //Aller � la m�thode SetHealthBar() pour ajuster la barre de vie du joueur.
    }
}
