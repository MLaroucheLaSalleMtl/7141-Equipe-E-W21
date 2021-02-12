using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Behavior : MonoBehaviour
{

    [SerializeField] private NavMeshAgent opponent;
    [SerializeField] private GameObject target;

    //Patrouille de l'IA
    [SerializeField] private Transform[] patrolPoints;
    //private int index = 0;
    private int randomPoint;

    //Attaque de l'IA
    [SerializeField] private GameObject projectile;
    [SerializeField] private float firePower = 20f;


    // Start is called before the first frame update
    void Start()
    {
        opponent = GetComponent<NavMeshAgent>();
       
    }

    // Update is called once per frame
    void Update()
    {
        randomPoint = Random.Range(0, patrolPoints.Length);
        opponent.destination = Vector3.MoveTowards(transform.position, patrolPoints[randomPoint].position, 250f * Time.deltaTime);

        if (Vector3.Distance(opponent.transform.position, patrolPoints[randomPoint].position) < 1f)
        {
            opponent.destination = Vector3.MoveTowards(transform.position, patrolPoints[randomPoint].position, 250f * Time.deltaTime);
        }

        Vector3 distanceFromPLayer = opponent.transform.position - target.transform.position; //distance entre le joueur et l'IA

        if (distanceFromPLayer.magnitude < 50)
        {
            transform.LookAt(target.transform.position);
            opponent.destination = target.transform.position;

            if (distanceFromPLayer.magnitude < 40)
            {
                opponent.destination = opponent.transform.position; // l'IA s'arrete à sa position actuelle

                transform.LookAt(target.transform.position);

                GameObject fireBall = Instantiate(projectile, transform.position + (transform.forward * 2f), transform.rotation);
                //for (int i = 10; i > 1; i--)
                //{
                    fireBall.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse);
                //}
            }
        }
    }
}
