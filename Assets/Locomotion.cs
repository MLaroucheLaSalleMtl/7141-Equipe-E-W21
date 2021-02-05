using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Locomotion : MonoBehaviour
{
    [SerializeField] CharacterController capman;
    private Vector3 move;
    private float hAxis;
    private float vAxis;
    private bool IsMoving = false;

    [SerializeField] private float gravity = 10f;
    private float speed = 10f;

    [SerializeField] private bool jump = false;
    private float jumpPower = 50f;

    [SerializeField] private GameObject arenaCenter;
    private bool isTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Water")
        {
            Debug.Log("is in contact with water");
            isTrigger = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        capman = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        hAxis = move.x * 2;
        vAxis = move.y * 2;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.performed;
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector3(hAxis / 4, 0f, vAxis / 4);
        float moveAngle = (Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg); //on calcule l'angle de muouvement et on le convertit en degré et le rendre relatif a langle de la caméra
        float lookAngle = Mathf.LerpAngle(transform.eulerAngles.y, moveAngle, 0.25f); //Progressivement me tourner vers l'angle de déplacement

        if (move.magnitude >= 0.1f) //on change d'angle seulement quand on bouge
        {
            transform.rotation = Quaternion.Euler(0, lookAngle, 0); //tourne le transform sur l'axe des y
            Vector3 forward = Vector3.forward * move.magnitude; //trouve le devant du joueur selon son orientation
            move = Quaternion.Euler(0f, moveAngle, 0f) * forward; //transposer la force avec le mouvement de l'angle
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }

        move.y -= gravity * Time.deltaTime * speed;
        capman.Move(move);


        if (jump)
        {
            move.y += jumpPower * Time.deltaTime * speed;
        }

        //if (IsMoving)
        //{
        //    //animator.SetBool("IsIdle", false);
        //    animator.SetBool("IsRunning", true); //la booléene isRunning s'active                   
        //}
        //else if (!IsMoving)
        //{
        //    animator.SetBool("IsRunning", false);
        //    //animator.SetBool("IsIdle", true);
        //}


        if (isTrigger)
        {
            capman.transform.position = arenaCenter.transform.position;            
            isTrigger = false;
        }

    }
}
