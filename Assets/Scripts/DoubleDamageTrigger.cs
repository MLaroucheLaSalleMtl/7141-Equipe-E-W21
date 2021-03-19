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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            manager.pDoubleDamage++;
            Debug.Log("Double Damage acquired");
            Destroy(gameObject);
        }
    }
}
