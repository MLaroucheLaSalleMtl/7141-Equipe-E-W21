using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTimer : MonoBehaviour
{
    private float timer = 3f; //float pour désigner un timer
    private bool once = false; //bool pour être utiliser une seule fois
    private float ballDamage = 40f; //float pour désigner le dégât de la balle.

    public float BallDamage { get => ballDamage; set => ballDamage = value; } //Encapsulation

    // Start is called before the first frame update
    void Start()
    {
        once = false; //Réinitialisé le bool once à false
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }*/

    private void DestroyBall() //Méthode pour détruire la balle
    {
        Destroy(gameObject); //Détruire le gameObject de ce script
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy && !once) //Condition If pour si le gameObject de ce script est actif dans la scène et que le bool once est false
        {
            once = true; //Bool once est mis à true
            Invoke("DestroyBall", timer); //Invoquer la méthode DestroyBall() après le timer pour détruire la balle.
        }
    }
}
