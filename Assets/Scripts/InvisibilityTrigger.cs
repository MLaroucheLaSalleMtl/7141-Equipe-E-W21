using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityTrigger : Power
{
    private GameObject prefabInvisibility;
    private Power power;

    // Start is called before the first frame update
    void Start()
    {
        prefabInvisibility = GetComponent<GameObject>();
        power = GetComponent<Power>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            power.GetComponent<Power>().isInvisible = true;
            power.GetComponent<Power>().isActivePower = true;
            Debug.Log("Oncollision");
            Debug.Log(power.isInvisible);
            //Destroy(gameObject);
        }
    }
}
