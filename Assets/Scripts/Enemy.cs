using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters
{
    public Enemy() { }
    public Enemy(float hp, float damage, float defense) : base(hp, damage, defense) { }

    private Player player;

    [SerializeField] private GameObject playerGO = null;

    private float dmgReceived = 0;

    public override void ReceiveDamage()
    {
        dmgReceived = player.Damage - this.Defense;
        this.Hp -= dmgReceived;
        //Debug.Log("Enemy Defense " + this.Defense);
        //Debug.Log("Player Damage " + player.Damage);
        //Debug.Log("Opponent Damage Received " + dmgReceived);

        if (this.Hp <= 0f)
        {
            IsDead();
        }
    }

    public override void IsDead()
    {
        Destroy(gameObject);
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
        player = playerGO.GetComponent<Player>();
        //Debug.Log("Enemy Defense " + this.Defense);
        //Debug.Log("Player Damage " + player.Damage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
