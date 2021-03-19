using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{


    public bool[] isFull;
    public GameObject[] slots;

    public static Inventory instance;
    private GameManager manager;

    private Image image;
    [SerializeField] private Sprite[] sprite;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else if(instance != null)
    //    {
    //        Destroy(this);
    //    }
    //}




    //pick up
    private Inventory inventory;

    private void Start()
    {
        //inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        manager = GameManager.instance;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //item can be added to invetory
                    inventory.isFull[i] = true;
                    break;
                }
            }
        }
    }

    private void Update()
    {
        //slots[1] = sprite[1]


        if (manager.pInvisibility > 0)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //item can be added to invetory
                    inventory.isFull[i] = true;
                    break;
                }
            }
        }
    }
}
