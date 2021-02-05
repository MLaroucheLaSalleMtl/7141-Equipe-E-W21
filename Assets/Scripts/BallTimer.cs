using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTimer : MonoBehaviour
{
    private float timer = 5f;
    private bool once = false;

    // Start is called before the first frame update
    void Start()
    {
        once = false;
    }

    private void DestroyBall()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy && !once)
        {
            once = true;
            Invoke("DestroyBall", timer);
        }
    }
}
