using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class LocomotionV2 : MonoBehaviour
{
    public LayerMask layerMask;

    //Déplacmeent du joueur
    [SerializeField] CharacterController capman;
    [SerializeField] NavMeshAgent capman1;
    private Vector3 move;
    private float hAxis;
    private float vAxis;
    //private bool isMoving = false;

    //Gravité
    [SerializeField] private float gravity = 10f;
    private float speed = 10f;

    //Saut du joueur
    private bool jump = false;
    [SerializeField] private float jumpPower = 1000f;


    //Respawn du joueur
    [SerializeField] private GameObject arenaCenter;
    private bool isTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            Debug.Log("is in contact with water");
            isTrigger = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        capman = GetComponent<CharacterController>();
        capman1 = GetComponent<NavMeshAgent>();
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

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 clickposition = -Vector3.one;
            clickposition = Camera.main.WorldToScreenPoint(Input.mousePosition + new Vector3(0, 0, 5f));

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 400f, layerMask))
            {
                clickposition = hit.point;
                capman1.SetDestination(clickposition);
            }

            capman1.transform.LookAt(clickposition);

            Debug.Log(clickposition);
        }

        //Déplacement du joueur
        move = new Vector3(hAxis / 4, 0f, vAxis / 4);
        float moveAngle = (Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg); //on calcule l'angle de muouvement et on le convertit en degré et le rendre relatif a langle de la caméra
        //float lookAngle = Mathf.LerpAngle(transform.eulerAngles.y, moveAngle, 0.25f); //Progressivement me tourner vers l'angle de déplacement

        if (move.magnitude >= 0.1f) //on change d'angle seulement quand on bouge
        {
            //transform.rotation = Quaternion.Euler(0, lookAngle, 0); //tourne le transform sur l'axe des y
            Vector3 forward = Vector3.forward * move.magnitude; //trouve le devant du joueur selon son orientation
            move = Quaternion.Euler(0f, moveAngle, 0f) * forward; //transposer la force avec le mouvement de l'angle
            //isMoving = true;
        }
        else
        {
            //isMoving = false;
        }


        //Saut du joueur
        if (jump)
        {
            if (capman.isGrounded)
            {
                move.y = Mathf.Sqrt(jumpPower * Time.deltaTime * speed);
                jump = false;
            }
        }


        //Application de la gravité
        move.y -= gravity * Time.deltaTime * speed;
        capman.Move(move);


        //Respawn du joueur
        if (isTrigger)
        {
            capman.transform.position = arenaCenter.transform.position;
            isTrigger = false;
        }

    }
}
