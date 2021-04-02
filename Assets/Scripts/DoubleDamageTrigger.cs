using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDamageTrigger : MonoBehaviour
{
    private GameManager manager; //en faire pour chaque pouvoir

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            if (collision.gameObject.name == "CapMan")
            {
                manager.pDoubleDamage++;
                Debug.Log("Double Damage acquired");
                Destroy(gameObject);
            }
            else if (collision.gameObject.name != "CapMan")
            {
                collision.gameObject.GetComponent<Enemy>().eDoubleDamage++;
            }
        }
    }
}
