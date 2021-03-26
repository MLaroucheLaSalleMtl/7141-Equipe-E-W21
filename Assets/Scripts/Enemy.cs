using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : Characters
{

    public Enemy() { } //Constructeur par défaut
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

    private GameManager manager;
    [SerializeField] private Renderer myRenderer;
    [SerializeField] private NavMeshAgent agent;
    private float timer = 10f;

    //Variables d'incrémentation de pouvoirs
    public int eInvisibility = 0;
    public int eInvincibility = 0;
    public int eDoubleDamage = 0;
    public int eDoubleSpeed = 0;
    public int eInstantHealing = 0;
    
    //Activation des pouvoirs
    public bool activeInvisibility = false;
    public bool activeInvincibility = false;
    public bool activeDoubleDamage = false;


    public override void ReceiveDamage()
    {
        //dmgReceived = collision.gameObject.GetComponent<BallTimer>().BallDamage * baseAreaBenefit.BaseDamageBenefit() - this.Defense * baseAreaBenefit.BaseDefenseBenefit(); //Équation qui enlève le damage par la défense 


        if (damageOrigin == player) 
        {
            if (dmgType == "Melee")
            {
                dmgReceived = damageOrigin.GetComponent<Equipment>().DamageMelee(damageOrigin.GetComponent<Player>().MyMelee) * damageOrigin.GetComponent<Player>().GetPlayerBaseAreaBenefit().BaseDamageBenefit() * damageOrigin.GetComponent<Player>().SendDoubleDamageOn() - gameObject.GetComponent<Equipment>().DefenseArmor(MyArmor) * baseAreaBenefit.BaseDefenseBenefit();
            }
            else if (dmgType == "Range")
            {
                dmgReceived = damageOrigin.GetComponent<Equipment>().DamageRange(damageOrigin.GetComponent<Player>().MyRange) * damageOrigin.GetComponent<Player>().GetPlayerBaseAreaBenefit().BaseDamageBenefit() * damageOrigin.GetComponent<Player>().SendDoubleDamageOn() - gameObject.GetComponent<Equipment>().DefenseArmor(MyArmor) * baseAreaBenefit.BaseDefenseBenefit();
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

        if (this.Hp <= 0f) //Condition If pour si la valeur Hp est inférieure ou égale à 0
        { 
            SetHealthBar(); //Aller à la méthode SetHealthBar() pour ajuster la barre de vie avant de mourir
            IsDead(); //Aller à la méthode IsDead() pour aller mourir
        }
    }

    public override void IsDead() //Méthode pour si le opponent est mort
    {
        Destroy(gameObject); //Détruire le gameObject de ce script
    }

    public bool ReturnDead() //Méthode pour retourner un bool
    {
        return false; //Retourne false
    }

    private void OnCollisionEnter(Collision collision) //Méthode pour si il y a une collision.
    {
        if(collision.gameObject.tag == "Damage") //Condition If pour si le tag du gameObject de la collision est un "Damage"
        {
            //ReceiveDamage(collision); //Aller à la méthode ReceiveDamage() avec la collision
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
        gameObject.GetComponent<AI_Behavior>().SetMelee(MyMelee); //Need to call it when changing melee
        gameObject.GetComponent<AI_Behavior>().SetRange(MyRange); //Need to call it when changing range
        manager = GameManager.instance; //Référence au GameMnager
    }

    public void SetHealthBar() //Méthode pour ajuster la barre de vie
    {
        SetFillHealthBar(); //Aller à la méthode SetFillHealthBar() pour ajuster la valeur de la barre de vie
        SetColorHealthBar(); //Aller à la méthode SetColorHealthBar() pour changer la couleur de la barre de vie selon la valeur.
        SetTextHealthBar(); //Aller à la méthode SetTextHealthBar() pour changer la valeur affiché de la barre de vie.
    }

    public void SetFillHealthBar() //Méthode pour bouger la barre de vie
    {
        sliderHealthBar.value = this.Hp; //Donner la valeur du slider la vie du joueur.
    }

    public void SetColorHealthBar() //Méthode pour changer la couleur de la barre de vie selon la valeur.
    {
        if (this.Hp >= 0.6 * this.HpMax) //Condition If pour si la vie du joueur est supérieure ou égale à 60% de sa vie maximum.
        {
            fillHealthBar.color = Color.green; //Changer la couleur du fillHealthBar par Vert
        }
        else if (this.Hp >= 0.3 * this.HpMax && this.Hp < 0.6 * this.HpMax) //Condition If pour si la vie du joueur est supérieure ou égale à 30% et inférieur à 60% de sa vie maximum.
        {
            fillHealthBar.color = Color.yellow; //Changer la couleur du fillHealthBar par Jaune
        }
        else if (this.Hp >= 0 * this.HpMax && this.Hp < 0.3 * this.HpMax) //Condition If pour si la vie du joueur est supérieure ou égale à 0% et inférieur à 30% de sa vie maximum.
        {
            fillHealthBar.color = Color.red; //Changer la couleur du fillHealthBar par Rouge
        }
    }

    public void SetTextHealthBar() //Méthode pour changer la valeur affiché de la barre de vie.
    {
        if (this.Hp >= 100) //Condition If pour si la valeur de Hp est supérieure ou égale à 100.
        {
            txtHealthBar.text = Convert.ToInt32(this.Hp).ToString("D3"); //Afficher la valeur de Hp à 3 chiffres
        }
        else if (this.Hp < 100) //Condition If pour si la valeur de Hp est inférieure à 100.
        {
            txtHealthBar.text = Convert.ToInt32(this.Hp).ToString("D2"); //Afficher la valeur de Hp à 2 chiffres
        } 
        if(this.Hp < 0) //Condition If pour si la valeur de Hp est inférieure ou égale à 0.
        {
            txtHealthBar.text = Convert.ToInt32(this.Hp * 0).ToString("D1"); //Afficher 0
        }
    }

    public BaseArea GetEnemyBaseAreaBenefit()
    {
        return baseAreaBenefit;
    }

    private void InstantHealing()
    {
        if (this.eInstantHealing > 0)
        {
            timer -= Time.deltaTime;
            this.Hp = this.HpMax;
            eInstantHealing--;
        }
    }

    private void DoubleDamage()
    {
        if (this.eDoubleDamage > 0)
        {
            //A FAIRE
        }
    }

    private void DoubleSpeed()
    {
        if (this.eDoubleSpeed > 0)
        {
            timer -= Time.deltaTime;
            agent.speed = 35f;
            eDoubleSpeed--;
            if (timer <= 0f)
            {
                agent.speed = 25f;
                timer = 10f;
            }
        }
    }

    private void Invincibility()
    {
        if (this.eInvincibility > 0)
        {
            timer -= Time.deltaTime;
            float enemyDefense = this.Defense;
            this.Defense = 50f;
            eInvincibility--;
            if (timer <= 0f)
            {
                this.Defense = enemyDefense;
                timer = 10f;
            }
        }
    }

    private void Invisibility()
    {
        if (this.eInvisibility > 0)
        {
            timer -= Time.deltaTime;
            myRenderer.enabled = false;
            eInvisibility--;
            if (timer <= 0f)
            {
                myRenderer.enabled = true;
                timer = 10f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetHealthBar(); //Aller à la méthode SetHealthBar() pour ajuster la barre de vie du joueur.

        if (activeInvisibility)
        {
            Invisibility();
        }


        if (activeInvisibility)
        {
            Invincibility();
        }


        DoubleSpeed();


        if (activeInvisibility)
        {
            DoubleDamage();
        }

        InstantHealing();

    }

}
