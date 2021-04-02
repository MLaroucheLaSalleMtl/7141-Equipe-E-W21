using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvisibilityTrigger : MonoBehaviour
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
                manager.pInvisibility++;
                Debug.Log("Invisibility acquired");
                Destroy(gameObject);
            }
            else if (collision.gameObject.name != "CapMan")
            {
                collision.gameObject.GetComponent<Enemy>().eInvisibility++;
            }
        }
    }
}
