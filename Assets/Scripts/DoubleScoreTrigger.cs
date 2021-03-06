using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreTrigger : Power
{
    private GameObject prefabDoubleScore;
    private Power power;

    // Start is called before the first frame update
    void Start()
    {
        prefabDoubleScore = GetComponent<GameObject>();
        power = GetComponent<Power>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            power.GetComponent<Power>().isDoubleScore = true;
            power.GetComponent<Power>().isActivePower = true;
            Debug.Log("Oncollision");
            Debug.Log(power.isDoubleScore);
            //Destroy(gameObject);
        }
    }
}
