using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityTrigger : MonoBehaviour
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
            manager.pInvincibility++;
            Debug.Log("Invincibility acquired");
            Destroy(gameObject);
        }
        else if (collision.gameObject.name != "CapMan")
        {
            collision.gameObject.GetComponent<Enemy>().eInvincibility++;
        }
    }
}
