using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Strike : MonoBehaviour
{
    [SerializeField] private GameObject knifeObject;
    [SerializeField] private GameObject swordObject;
    [SerializeField] private GameObject spearObject;
    [SerializeField] private GameObject hammerObject;
    [SerializeField] private Animator knifeAnim;
    [SerializeField] private Animator swordAnim;
    [SerializeField] private Animator spearAnim;
    [SerializeField] private Animator hammerAnim;
    private bool isAttack = false;
    private bool timerOn = false;
    private float timer = 0;
    [SerializeField] private Equipment.typeMelee myMelee;

    // Start is called before the first frame update
    void Start()
    {
        isAttack = false;
        timerOn = false;
        timer = 0f;
        knifeObject.GetComponent<Weapon>().SetOrigin(gameObject);
        swordObject.GetComponent<Weapon>().SetOrigin(gameObject);
        spearObject.GetComponent<Weapon>().SetOrigin(gameObject);
        hammerObject.GetComponent<Weapon>().SetOrigin(gameObject);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        isAttack = context.performed;
    }

    public void SetMelee(Equipment.typeMelee playerMelee)
    {
        myMelee = playerMelee;
    }

    public void AttackKnife()
    {
        timer = 0.5f;
        knifeObject.SetActive(true);
        if (knifeAnim) knifeAnim.SetTrigger("Attack");
        Invoke("SetInactiveKnife", timer);
    }

    public void AttackSword()
    {
        timer = 0.5f;
        swordObject.SetActive(true);
        if (swordAnim) swordAnim.SetTrigger("Attack");
        Invoke("SetInactiveSword", timer);
    }

    public void AttackSpear()
    {
        timer = 0.75f;
        spearObject.SetActive(true);
        if (spearAnim) spearAnim.SetTrigger("Attack");
        Invoke("SetInactiveSpear", timer);
    }

    public void AttackHammer()
    {
        timer = 1f;
        hammerObject.SetActive(true);
        if (hammerAnim) hammerAnim.SetTrigger("Attack");
        Invoke("SetInactiveHammer", timer);
    }

    public void SetInactiveKnife()
    {
        knifeObject.SetActive(false);
    }

    public void SetInactiveSword()
    {
        swordObject.SetActive(false);
    }

    public void SetInactiveSpear()
    {
        spearObject.SetActive(false);
    }

    public void SetInactiveHammer()
    {
        hammerObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack)
        {
            isAttack = false;

            if (!timerOn)
            {
                timerOn = true;

                if (myMelee == Equipment.typeMelee.Knife)
                {
                    AttackKnife();
                }
                else if (myMelee == Equipment.typeMelee.Sword)
                {
                    AttackSword();
                }
                else if (myMelee == Equipment.typeMelee.Spear)
                {
                    AttackSpear();
                }
                else if (myMelee == Equipment.typeMelee.Hammer)
                {
                    AttackHammer();
                }
            }
        }

        if (timerOn)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f) 
            {
                timer = 0f;
                timerOn = false; 
            }
        }
    }
}