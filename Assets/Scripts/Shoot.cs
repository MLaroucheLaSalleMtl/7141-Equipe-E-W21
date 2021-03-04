using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject projectile; // mon projectile
    [SerializeField] private float firePower = 80f; //ma puissance de feu
    private bool isFiring = false; //est-ce que je tire ?
    private float timer = 0f;



    public void OnFire(InputAction.CallbackContext context)
    {
        isFiring = context.performed; //isFiring = true
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; //active le chrono
        if (isFiring)
        {
            if (timer > 1f)
            {
                GameObject fireBall = Instantiate(projectile, transform.position + (transform.forward * 3f), transform.rotation); //instanciation de mon projectile, son emplacement et sa direction
                fireBall.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse); //rajout de la force a mon projectile(rigidbody)
                timer = 0f;
                isFiring = false;
            }
        }
    }
}
