using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters
{
    public Enemy(float hp, float damage, float defense) : base(hp, damage, defense) { }

    private Player player;

    [SerializeField] private GameObject playerGO = null;

    public override void ReceiveDamage()
    {
        this.Hp -= (player.Damage - this.Defense);

        if(this.Hp <= 0f)
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
