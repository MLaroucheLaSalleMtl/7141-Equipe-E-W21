using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    
    private GameObject rangedWeapon = null;
    [SerializeField] private Equipment.typeRange myRange;
    [SerializeField] private GameObject projectileRock = null;
    [SerializeField] private GameObject projectileSlinger = null;
    [SerializeField] private GameObject projectileBow = null;
    [SerializeField] private GameObject projectileGun = null;

    [SerializeField] private GameObject projectile; // mon projectile
    private bool isFiring = false; //est-ce que je tire ?
    private float timer = 0f;

    [SerializeField] private AudioClip audioRock;
    [SerializeField] private AudioClip audioSlinger;
    [SerializeField] private AudioClip audioBow;
    [SerializeField] private AudioClip audioGun;

    public void OnFire(InputAction.CallbackContext context)
    {
        isFiring = context.performed; //isFiring = true
    }

    public void SetRange(Equipment.typeRange playerRange)
    {
        myRange = playerRange;
        SetRangedWeapon();
    }

    public void SetRangedWeapon()
    {
        if (myRange == Equipment.typeRange.Rock)
        {
            rangedWeapon = projectileRock;
        }
        else if (myRange == Equipment.typeRange.Slinger)
        {
            rangedWeapon = projectileSlinger;
        }
        else if (myRange == Equipment.typeRange.Bow)
        {
            rangedWeapon = projectileBow;
        }
        else if (myRange == Equipment.typeRange.Gun)
        {
            rangedWeapon = projectileGun;
        }
    }

    public void ShootRangedWeapon()
    {
        if(rangedWeapon == projectileRock) 
        {
             GameObject rangedAttack = Instantiate(rangedWeapon, transform.position + (transform.forward * 3f), transform.rotation);
             rangedAttack.GetComponent<Weapon>().SetOrigin(gameObject);
             rangedAttack.GetComponent<Rigidbody>().AddForce(transform.forward * 80, ForceMode.Impulse);
             rangedAttack.GetComponent<AudioSource>().PlayOneShot(audioRock);
        } 
        else if(rangedWeapon == projectileSlinger) 
        {
             GameObject rangedAttack = Instantiate(rangedWeapon, transform.position + (transform.forward * 3f), transform.rotation);
             rangedAttack.GetComponent<Weapon>().SetOrigin(gameObject);
             rangedAttack.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
             rangedAttack.GetComponent<AudioSource>().PlayOneShot(audioSlinger);
        }
        else if(rangedWeapon == projectileBow) 
        {
             GameObject rangedAttack = Instantiate(rangedWeapon, transform.position + (transform.forward * 5f), transform.rotation);
             rangedAttack.GetComponent<Weapon>().SetOrigin(gameObject);
             rangedAttack.GetComponent<Rigidbody>().AddForce(transform.forward * 125, ForceMode.Impulse);
             rangedAttack.GetComponent<AudioSource>().PlayOneShot(audioBow);
        }
        else if(rangedWeapon == projectileGun) 
        {
             GameObject rangedAttack = Instantiate(rangedWeapon, transform.position + (transform.forward * 3f), transform.rotation);
             rangedAttack.GetComponent<Weapon>().SetOrigin(gameObject);
             rangedAttack.GetComponent<Rigidbody>().AddForce(transform.forward * 150, ForceMode.Impulse);
             rangedAttack.GetComponent<AudioSource>().PlayOneShot(audioGun);
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        SetRangedWeapon();

        timer += Time.deltaTime; //active le chrono
        if (isFiring)
        {
            if (timer > 0.5f)
            {
                ShootRangedWeapon();
                
                timer = 0f;
                isFiring = false;
            }
        }
    }
}
