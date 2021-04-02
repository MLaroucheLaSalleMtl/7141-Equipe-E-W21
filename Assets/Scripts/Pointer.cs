using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pointer : MonoBehaviour
{

    private Rigidbody pointer;

    private Vector2 look;
    private float hLook;
    private float vLook;

    //[SerializeField] private GameObject capman;

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>(); //attribution des valeurs des vecteurs de mouvement de mon joueur
        hLook = look.x;
        vLook = look.y;
    }

    void Start()
    {
        pointer = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        look = new Vector3(hLook, 0f, vLook);
        pointer.MovePosition(look);
        //capman.GetComponent<LocomotionV2>().transform.LookAt(pointer.transform.position);
    }

}
