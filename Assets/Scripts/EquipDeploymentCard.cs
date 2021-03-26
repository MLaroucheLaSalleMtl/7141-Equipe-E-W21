using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipDeploymentCard : MonoBehaviour
{
    //Melee : Knife = 30, Sword = 40, Spear = 40, Hammer = 40
    //Range : Rock = 30, Slinger = 35, Bow = 40, Gun = 50
    //Armor : Cloth = 15, Light = 20, Medium = 25, Heavy = 30

    [SerializeField] private GameObject player = null;

    [SerializeField] private GameObject panelDeployment = null;
    [SerializeField] private Text textDeployment = null;

    [SerializeField] private int numEquip = 0;
    private int numWave = 0;
    private float timerWave = 0f;
    private bool onceEquip = false;
    private bool onceWave1 = false;
    private bool onceWave2 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PrepareEquipmentCard()
    {
        onceEquip = true;
        switch (numWave)
        {
            case 0:
                numEquip = UnityEngine.Random.Range(0, 2);
                break;
            case 1:
                numEquip = UnityEngine.Random.Range(2, 7);
                break;
            case 2:
                numEquip = UnityEngine.Random.Range(4, 9);
                break;
            default:
                numEquip = UnityEngine.Random.Range(0, 9);
                break;
        }
    }

    public void DisplayEquipmentCard()
    {
        panelDeployment.SetActive(true);
        switch (numEquip)
        {
            case 0:
                textDeployment.text = "Slinger Range Card";
                break;
            case 1:
                textDeployment.text = "Light Armor Card";
                break;
            case 2:
                textDeployment.text = "Bow Range Card";
                break;
            case 3:
                textDeployment.text = "Medium Armor Card";
                break;
            case 4:
                textDeployment.text = "Sword Melee Card";
                break;
            case 5:
                textDeployment.text = "Spear Melee Card";
                break;
            case 6:
                textDeployment.text = "Hammer Melee Card";
                break;
            case 7:
                textDeployment.text = "Gun Range Card";
                break;
            case 8:
                textDeployment.text = "Heavy Armor Card";
                break;
            default:
                textDeployment.text = "Empty Card";
                break;

        }
    }

    public void AttachEquipmentPlayer(Collision collision)
    {
        switch (numEquip)
        {
            case 0:
                collision.gameObject.GetComponent<Player>().MyRange = Equipment.typeRange.Slinger;
                break;
            case 1:
                collision.gameObject.GetComponent<Player>().MyArmor = Equipment.typeArmor.Light;
                break;
            case 2:
                collision.gameObject.GetComponent<Player>().MyRange = Equipment.typeRange.Bow;
                break;
            case 3:
                collision.gameObject.GetComponent<Player>().MyArmor = Equipment.typeArmor.Medium;
                break;
            case 4:
                collision.gameObject.GetComponent<Player>().MyMelee = Equipment.typeMelee.Sword;
                break;
            case 5:
                collision.gameObject.GetComponent<Player>().MyMelee = Equipment.typeMelee.Spear;
                break;
            case 6:
                collision.gameObject.GetComponent<Player>().MyMelee = Equipment.typeMelee.Hammer;
                break;
            case 7:
                collision.gameObject.GetComponent<Player>().MyRange = Equipment.typeRange.Gun;
                break;
            case 8:
                collision.gameObject.GetComponent<Player>().MyArmor = Equipment.typeArmor.Heavy;
                break;
            default:
                break;
        }

        SetInactiveEquipmentCard();
    }
    public void AttachEquipmentEnemy(Collision collision)
    {
        switch (numEquip)
        {
            case 0:
                collision.gameObject.GetComponent<Enemy>().MyRange = Equipment.typeRange.Slinger;
                break;
            case 1:
                collision.gameObject.GetComponent<Enemy>().MyArmor = Equipment.typeArmor.Light;
                break;
            case 2:
                collision.gameObject.GetComponent<Enemy>().MyRange = Equipment.typeRange.Bow;
                break;
            case 3:
                collision.gameObject.GetComponent<Enemy>().MyArmor = Equipment.typeArmor.Medium;
                break;
            case 4:
                collision.gameObject.GetComponent<Enemy>().MyMelee = Equipment.typeMelee.Sword;
                break;
            case 5:
                collision.gameObject.GetComponent<Enemy>().MyMelee = Equipment.typeMelee.Spear;
                break;
            case 6:
                collision.gameObject.GetComponent<Enemy>().MyMelee = Equipment.typeMelee.Hammer;
                break;
            case 7:
                collision.gameObject.GetComponent<Enemy>().MyRange = Equipment.typeRange.Gun;
                break;
            case 8:
                collision.gameObject.GetComponent<Enemy>().MyArmor = Equipment.typeArmor.Heavy;
                break;
            default:
                break;
        }

        SetInactiveEquipmentCard();
    }

    public void SetInactiveEquipmentCard()
    {
        panelDeployment.SetActive(false);
        gameObject.SetActive(false);
        onceEquip = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            if (collision.gameObject == player)
            {
                Debug.Log("Collision On");
                AttachEquipmentPlayer(collision);
            }
            else
            {
                AttachEquipmentEnemy(collision);
            }

            SetInactiveEquipmentCard();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character" && other.gameObject == player)
        {
            Debug.Log("Trigger On");
            DisplayEquipmentCard();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Character" && other.gameObject == player)
        {
            panelDeployment.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timerWave += Time.deltaTime;

        if (timerWave >= 60f && !onceWave1)
        {
            onceWave1 = true;
            numWave++;
        }

        if (timerWave >= 120f && !onceWave2)
        {
            onceWave2 = true;
            numWave++;
        }

        if (gameObject.activeInHierarchy && !onceEquip)
            PrepareEquipmentCard();
    }
}
