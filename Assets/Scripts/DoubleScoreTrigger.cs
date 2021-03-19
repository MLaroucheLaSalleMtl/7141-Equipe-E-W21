using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreTrigger : MonoBehaviour
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
            manager.pDoubleScore++;
            Debug.Log("Double Score acquired");
            Destroy(gameObject);
        }
    }

}
