using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Behavior : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent; //Mon NavMeshAgent
    [SerializeField] public List<GameObject> targetList = null; //Liste des cibles
    private float timer = 0f;

    //Patrouille de l'IA
    [SerializeField] private Transform[] patrolPoints; //Liste de points de patrouille
    private int pointIndex;

    //Attaque de l'IA
    [SerializeField] private GameObject projectile; //mon projectile
    [SerializeField] private float firePower = 80f;

    private IState state; //Référence vers l'interface Istate

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //Cache du navmesh
        pointIndex = Random.Range(0, patrolPoints.Length); //retourne un index alátoire de la liste des points de patrouille
        state = NormalState.GetState(); //référence au state pattern
    }

    // Update is called once per frame
    void Update()
    {
        //patrouille
        Invoke("OnPatrolling", 3f); //Invoque la méthode qui permet au navMesh de faire sa patrouille

        if (this.state.CanAttackEnemy()) //vérifie l'état du pattern actuel
        {
            CancelInvoke("OnPatrolling"); //Annule l'état de patrouille
        }
        else { OnPatrolling(); }

        foreach (GameObject target in targetList)
        {
            if (target != null)

            {
                Vector3 distanceFromEnemy = agent.transform.position - target.transform.position; //distance entre le joueur et l'IA

                if (distanceFromEnemy.magnitude < 70)
                {
                    this.state = AttackState.GetState(); //Fait appel au pattern Attack State
                    transform.LookAt(target.transform.position); //le forward de l'agent fait face à sa cible
                    agent.destination = target.transform.position; //l'agent s'arrete à sa position actuelle

                    if (distanceFromEnemy.magnitude < 40)
                    {
                        agent.destination = agent.transform.position; // l'IA s'arrete à sa position actuelle

                        transform.LookAt(target.transform.position);

                        timer += Time.deltaTime;
                        if (timer > 1f)
                        {
                            GameObject fireBall = Instantiate(projectile, transform.position + (transform.forward * 3f), transform.rotation); //Fait une instanciation du projectile
                            fireBall.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse); //Donne une force et une direction au projectile instacié
                            timer = 0f;
                        }
                    }
                }
                else
                {
                    this.state = NormalState.GetState(); //fait appel au pattern Normal State
                }
            }
        }
    }

    private void OnPatrolling()
    {
        if (pointIndex < patrolPoints.Length) //vérfie la position de l'index par rapport à la taille de la liste des points de patrouille
        {
            agent.destination = Vector3.MoveTowards(transform.position, patrolPoints[pointIndex].position, 500f * Time.deltaTime); //Dirige l'agent vers le point indéxé de la liste de patrouille

            if (Vector3.Distance(agent.transform.position, patrolPoints[pointIndex].position) < 10f) //vérifie la distance en tre le joueur et sa destination
            {
                pointIndex++; //incrémente l'index pour permettre au joueur d'aller à un nouveau point de patrouille
                agent.SetDestination(patrolPoints[pointIndex].position);
            }
            if (pointIndex >= patrolPoints.Length - 1)
            {
                pointIndex = pointIndex = Random.Range(0, patrolPoints.Length); //réintialise le tableau une fois la taille final tdu tableau atteinte
            }
        }
    }
}
