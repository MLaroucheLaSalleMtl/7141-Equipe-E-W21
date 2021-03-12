using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantHealingTrigger : Power
{
    private GameObject prefabInstantHealing;
    private Power power;

    // Start is called before the first frame update
    void Start()
    {
        prefabInstantHealing = GetComponent<GameObject>();
        power = GetComponent<Power>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            power.GetComponent<Power>().isInstantHealing = true;
            power.GetComponent<Power>().isActivePower = true;
            Debug.Log("Oncollision");
            Debug.Log(power.isInstantHealing);
            //Destroy(gameObject);
        }
    }
}
