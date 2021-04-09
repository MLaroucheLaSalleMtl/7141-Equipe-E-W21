using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class LocomotionV2 : MonoBehaviour
{
    private GameManager manager;
    private float timer = 0f;
    private bool useTimer = false;

    public LayerMask layerMask;

    //Déplacmeent du joueur
    [SerializeField] CharacterController capman; //première référence à mon joeuur
    [SerializeField] NavMeshAgent capman1; //seconde réference à mon joueur
    private Vector3 move; // référence au vecteur de mouvement 
    private float hAxis;
    private float vAxis;

    //Look(Rotation du joueur)
    private Vector3 look;
    private float hLook;
    private float vLook;


    //Gravité
    [SerializeField] private float gravity = 10f;
    private float speed = 10f;

    //Saut du joueur
    private bool jump = false;
    [SerializeField] private float jumpPower = 1000f; //force du jump


    //Respawn du joueur
    [SerializeField] private GameObject arenaCenter; //référence au centre de l''arène
    private bool isTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water") //si mon joueur es au contact de l'eau
        {
            Debug.Log("is in contact with water");
            isTrigger = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        capman = GetComponent<CharacterController>(); //Cache du character controller
        capman1 = GetComponent<NavMeshAgent>(); //cache du navmesh agent
        manager = GameManager.instance;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>(); //attribution des valeurs des vecteurs de mouvement de mon joueur
        hAxis = move.x;
        vAxis = move.y;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.performed; //jump  =true
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>(); //attribution des valeurs des vecteurs de mouvement de mon joueur
        hLook = look.x;
        vLook = look.y;
    }

    void FaceMousePosition()
    {
        Plane playerplane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hit;

        if (playerplane.Raycast(ray, out hit))
        {
            Vector3 targetPoint = ray.GetPoint(hit);
            Quaternion targetRot = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, speed * Time.deltaTime);
        }
    }


    private void isDoubleSpeed()
    {
        if(useTimer)
        {
            timer -= Time.deltaTime;
        }

        //Double Speed
        if (manager.isUsingDoubleSpeed || (manager.powerIsUsed && manager.index == 3))
        {
            if (manager.pDoubleSpeed > 0)
            {
                useTimer = true;
                timer = 10f;
                manager.pDoubleSpeed--;
            }
            manager.isUsingDoubleSpeed = false;
        }

        if(timer > 0f)
        {
            move = new Vector3(hAxis, 0f, vAxis);
            capman.Move(move);
        }
        else
        {
            useTimer = false;
            timer = 0f;
            move = new Vector3(hAxis / 2, 0f, vAxis / 2);
            capman.Move(move);
        }
    }

    // Update is called once per frame
    void Update()
    {

        FaceMousePosition();

        isDoubleSpeed();

        look = new Vector3(hLook, 0f, vLook);
        float lookAngle = (Mathf.Atan2(look.x, look.z) * Mathf.Rad2Deg); //Progressivement me tourner vers l'angle de déplacement

        if (look.magnitude > 0.9f)
        {
            transform.rotation = Quaternion.Euler(0, lookAngle, 0);
        }

        float moveAngle = (Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg); //on calcule l'angle de muouvement et on le convertit en degré et le rendre relatif a langle de la caméra
        //float lookingAngle = Mathf.LerpAngle(transform.eulerAngles.y, moveAngle, 0.25f); //Progressivement me tourner vers l'angle de déplacement

        if (move.magnitude >= 0.1f) //on change d'angle seulement quand on bouge
        {
            //transform.rotation = Quaternion.Euler(0, lookAngle, 0); //tourne le transform sur l'axe des y
            Vector3 forward = Vector3.forward * move.magnitude; //trouve le devant du joueur selon son orientation
            move = Quaternion.Euler(0f, moveAngle, 0f) * forward; //transposer la force avec le mouvement de l'angle
            //move = Quaternion.Euler(0f, lookAngle, 0f) * forward; //transposer la force avec le mouvement de l'angle
            //isMoving = true;

        }
        else
        {
            //isMoving = false;
        }


        //Saut du joueur
        if (jump)
        {
            if (capman.isGrounded) //mon joueur est au contact du sol
            {
                move.y = Mathf.Sqrt(jumpPower * Time.deltaTime * speed); //mon déplacment dans l'axe y
                jump = false;
            }
        }


        //Application de la gravité
        move.y -= gravity * Time.deltaTime * speed; //mon déplacment dans l'axe y
        capman.Move(move); //application des paramètres de déplacmeent à mon joueur

        //Respawn du joueur
        if (isTrigger)
        {
            capman.transform.position = arenaCenter.transform.position; // mon joueur réapparait à la position centrale de l'arène
            isTrigger = false;
        }

    }


}
