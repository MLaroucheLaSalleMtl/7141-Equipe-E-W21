using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseArea : MonoBehaviour
{
    [SerializeField] private GameObject character = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private bool baseActive = true;

    [SerializeField] private bool isPresentCharacter = false;
    private bool isPresentPlayer = false;
    private bool isPresentCapture = false;

    [SerializeField] private GameObject panelBaseCapture = null;
    [SerializeField] private Image imageFillCaptureProgress = null;
    [SerializeField] private float floatCaptureProgress = 0;

    private float timeRegen = 0;
    private bool once = false;
    public bool isBeingCaptured = false;
    [SerializeField] private float baseRegen = 10f;
    [SerializeField] private float multiplierDamage = 1.2f;
    [SerializeField] private float multiplierDefense = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        baseActive = true;
        isBeingCaptured = false;
        panelBaseCapture.SetActive(false);
        imageFillCaptureProgress.fillAmount = 0;
        floatCaptureProgress = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character") 
        {
            if (baseActive)
            {
                if (other.gameObject == character)
                {
                    isPresentCharacter = true;
                    Debug.Log("Occupier Enter");
                }
                if (other.gameObject != character && other.gameObject == player && character != player)
                {
                    panelBaseCapture.SetActive(true);
                    isPresentPlayer = true;
                }
                if (other.gameObject != character && other.gameObject != player)
                {
                    isPresentCapture = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            if (other.gameObject == character)
            {
                isPresentCharacter = false;
                Debug.Log("Occupier Left");
            }
            if (other.gameObject != character && other.gameObject == player && character != player)
            {
                panelBaseCapture.SetActive(false);
                isPresentPlayer = false;
            }
            if (other.gameObject != character && other.gameObject != player)
            {
                isPresentCapture = false;
            }
        }
    }

    public void BaseRegen()
    {
        if (character != player)
        {
            if (character.GetComponent<Enemy>().Hp < character.GetComponent<Enemy>().HpMax)
            {
                character.GetComponent<Enemy>().Hp += baseRegen;

                if (character.GetComponent<Enemy>().Hp > character.GetComponent<Enemy>().HpMax)
                {
                    character.GetComponent<Enemy>().Hp = character.GetComponent<Enemy>().HpMax;
                }
            }
        }
        else if (character == player)
        {
            if (character.GetComponent<Player>().Hp < character.GetComponent<Player>().HpMax)
            {
                character.GetComponent<Player>().Hp += baseRegen;

                if (character.GetComponent<Player>().Hp > character.GetComponent<Player>().HpMax)
                {
                    character.GetComponent<Player>().Hp = character.GetComponent<Player>().HpMax;
                }
            }
        }
    }

    public void BaseDamageBenefit()
    {
        if (character != player)
        {
            character.GetComponent<Enemy>().Damage *= multiplierDamage;
        }
        else if (character == player)
        {
            character.GetComponent<Player>().Damage *= multiplierDamage;
        }
    }

    public void BaseDefenseBenefit()
    {
        if (character != player)
        {
            character.GetComponent<Enemy>().Defense *= multiplierDefense;
        }
        else if (character == player)
        {
            character.GetComponent<Player>().Defense *= multiplierDefense;
        }
    }

    public void BaseDamageReturn()
    {
        if (character != player)
        {
            character.GetComponent<Enemy>().Damage = character.GetComponent<Enemy>().DamageBase;
        }
        else if (character == player)
        {
            character.GetComponent<Player>().Damage = character.GetComponent<Player>().DamageBase;
        }
    }

    public void BaseDefenseReturn()
    {
        if (character != player)
        {
            character.GetComponent<Enemy>().Defense = character.GetComponent<Enemy>().DefenseBase;
        }
        else if (character == player)
        {
            character.GetComponent<Player>().Defense = character.GetComponent<Player>().DefenseBase;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (baseActive && isPresentCharacter)
        {
            if (character != null)
            {
                timeRegen += Time.deltaTime;
                if (timeRegen >= 0.5f)
                {
                    timeRegen = 0f;
                    BaseRegen();
                }
            }
        }

        if (baseActive && imageFillCaptureProgress && isPresentPlayer && !isPresentCharacter)
        {
            imageFillCaptureProgress.fillAmount += Time.deltaTime * 0.025f;
            isBeingCaptured = true;
        } 
        else
        {
            isBeingCaptured = false;
        }

        if (baseActive && isPresentCapture && !isPresentCharacter)
        {
            floatCaptureProgress += Time.deltaTime * 0.1f;
        }

        if (baseActive && (imageFillCaptureProgress.fillAmount == 1f || floatCaptureProgress > 1f))
        {
            baseActive = false;
        }

        if (baseActive)
        {
            if (character != null)
            {
                if (!once)
                {
                    BaseDamageBenefit();
                    BaseDefenseBenefit();
                    once = true;
                }
            }
        } 
    }
}
