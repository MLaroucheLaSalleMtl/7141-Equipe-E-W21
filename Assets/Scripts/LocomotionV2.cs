using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class LocomotionV2 : MonoBehaviour
{
    public LayerMask layerMask;

    //D�placmeent du joueur
    [SerializeField] CharacterController capman; //premi�re r�f�rence � mon joeuur
    [SerializeField] NavMeshAgent capman1; //seconde r�ference � mon joueur
    private Vector3 move; // r�f�rence au vecteur de mouvement 
    private float hAxis;
    private float vAxis;
    //private bool isMoving = false;

    //Gravit�
    [SerializeField] private float gravity = 10f;
    private float speed = 10f;

    //Saut du joueur
    private bool jump = false;
    [SerializeField] private float jumpPower = 1000f; //force du jump


    //Respawn du joueur
    [SerializeField] private GameObject arenaCenter; //r�f�rence au centre de l''ar�ne
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
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>(); //attribution des valeurs des vecteurs de mouvement de mon joueur
        hAxis = move.x * 2;
        vAxis = move.y * 2;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.performed; //jump  =true
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1)) //si clique sur bouton droit de la souris
        {
            Vector3 clickposition = -Vector3.one;
            clickposition = Camera.main.WorldToScreenPoint(Input.mousePosition + new Vector3(0, 0, 5f)); //attribution des valeurs du click de la souris dans le World space 

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //retourne l'emplacement de mon mouse click 

            RaycastHit hit; //retourne le point de collision de mon rayon

            if (Physics.Raycast(ray, out hit, 400f, layerMask)) //mon rayon, d�t�ction du bout du rayon, distance max de d�t�ction du click, layer associ� au click)
            {
                clickposition = hit.point;
                capman1.SetDestination(clickposition); //mon joueur se dirige vers la position du click
            }

            capman1.transform.LookAt(clickposition); //mon joueur s''oriente vers la position du click

            Debug.Log(clickposition);
        }

        //D�placement du joueur
        move = new Vector3(hAxis / 4, 0f, vAxis / 4);
        float moveAngle = (Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg); //on calcule l'angle de muouvement et on le convertit en degr� et le rendre relatif a langle de la cam�ra
        //float lookAngle = Mathf.LerpAngle(transform.eulerAngles.y, moveAngle, 0.25f); //Progressivement me tourner vers l'angle de d�placement

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
            if (capman.isGrounded) //mon joueur est au contact du sol
            {
                move.y = Mathf.Sqrt(jumpPower * Time.deltaTime * speed); //mon d�placment dans l'axe y
                jump = false;
            }
        }


        //Application de la gravit�
        move.y -= gravity * Time.deltaTime * speed; //mon d�placment dans l'axe y
        capman.Move(move); //application des param�tres de d�placmeent � mon joueur

        //Respawn du joueur
        if (isTrigger)
        {
            capman.transform.position = arenaCenter.transform.position; // mon joueur r�apparait � la position centrale de l'ar�ne
            isTrigger = false;
        }

    }
}
