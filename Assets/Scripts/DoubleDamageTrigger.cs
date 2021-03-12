using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDamageTrigger : Power
{
    private GameObject prefabDoubleDamage;
    private Power power;

    // Start is called before the first frame update
    void Start()
    {
        prefabDoubleDamage = GetComponent<GameObject>();
        power = GetComponent<Power>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            power.GetComponent<Power>().isDoubleDamage = true;
            power.GetComponent<Power>().isActivePower = true;
            Debug.Log("Oncollision");
            Debug.Log(power.isDoubleDamage);
            //Destroy(gameObject);
        }
    }
}
