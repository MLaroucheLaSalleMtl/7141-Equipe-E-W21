using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTimer : MonoBehaviour
{
    private float timer = 3f; //float pour d�signer un timer
    private bool once = false; //bool pour �tre utiliser une seule fois
    private float ballDamage = 40f; //float pour d�signer le d�g�t de la balle.

    public float BallDamage { get => ballDamage; set => ballDamage = value; } //Encapsulation

    // Start is called before the first frame update
    void Start()
    {
        once = false; //R�initialis� le bool once � false
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }*/

    private void DestroyBall() //M�thode pour d�truire la balle
    {
        Destroy(gameObject); //D�truire le gameObject de ce script
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy && !once) //Condition If pour si le gameObject de ce script est actif dans la sc�ne et que le bool once est false
        {
            once = true; //Bool once est mis � true
            Invoke("DestroyBall", timer); //Invoquer la m�thode DestroyBall() apr�s le timer pour d�truire la balle.
        }
    }
}
