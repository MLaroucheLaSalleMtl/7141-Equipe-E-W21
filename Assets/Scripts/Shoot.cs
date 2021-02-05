using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float firePower = 20f;
    private bool isFiring = false;



    public void OnFire(InputAction.CallbackContext context)
    {
        isFiring = context.performed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            isFiring = false;
            GameObject fireBall = Instantiate(projectile, transform.position + (transform.forward * 2f), transform.rotation);
            fireBall.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse);
        }
    }
}
