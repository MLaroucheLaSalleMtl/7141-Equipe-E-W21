using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSpeedTrigger : MonoBehaviour
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
                manager.pDoubleSpeed++;
                Debug.Log("Double Speed acquired");
                Destroy(gameObject);
            }
            else if (collision.gameObject.name != "CapMan")
            {
                collision.gameObject.GetComponent<Enemy>().eDoubleSpeed++;
            }
        }
    }
}
