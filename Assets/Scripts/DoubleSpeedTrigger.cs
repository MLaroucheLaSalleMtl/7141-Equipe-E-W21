using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSpeedTrigger : Power
{
    private GameObject prefabDoubleSpeed;
    private Power power;

    // Start is called before the first frame update
    void Start()
    {
        prefabDoubleSpeed = GetComponent<GameObject>();
        power = GetComponent<Power>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            power.GetComponent<Power>().isDoubleSpeed = true;
            power.GetComponent<Power>().isActivePower = true;
            Debug.Log("Oncollision");
            Debug.Log(power.isDoubleSpeed);
            //Destroy(gameObject);
        }
    }
}
