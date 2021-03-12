using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityTrigger : Power
{
    private GameObject prefabInvincibility;
    private Power power;

    // Start is called before the first frame update
    void Start()
    {
        prefabInvincibility = GetComponent<GameObject>();
        power = GetComponent<Power>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            power.GetComponent<Power>().isInvincible = true;
            power.GetComponent<Power>().isActivePower = true;
            Debug.Log("Oncollision");
            Debug.Log(power.isInvincible);
            //Destroy(gameObject);
        }
    }
}
