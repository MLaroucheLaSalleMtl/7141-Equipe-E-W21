using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Behavior : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] private Transform startPointPosition; //R�f�rence � la base du joueur

    [SerializeField] private NavMeshAgent agent; //Mon NavMeshAgent
    [SerializeField] public List<GameObject> targetList = null; //Liste des cibles
    private float timer = 0f;

    //Patrouille de l'IA
    [SerializeField] private Transform[] patrolPoints; //Liste de points de patrouille
    private int pointIndex;

    //Attaque de l'IA
    [SerializeField] private GameObject projectile; //mon projectile
    [SerializeField] private float firePower = 80f;

    private IState state; //R�f�rence vers l'interface Istate

    private BaseArea baseArea;
    [SerializeField] private GameObject playerBase;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //Cache du navmesh
        pointIndex = Random.Range(0, patrolPoints.Length); //retourne un index al�toire de la liste des points de patrouille
        state = NormalState.GetState(); //r�f�rence au state pattern
        enemy = GetComponent<Enemy>();
        baseArea = playerBase.GetComponent<BaseArea>(); //Cache de cu component BaseArea
    }

    // Update is called once per frame
    void Update()
    {
        //patrouille

        if (!this.state.CanAttackEnemy() || !this.state.GoToBase() || !this.state.DefendBase()) //Si l�A est dans son Normal State
        {
            Invoke("OnPatrolling", 3f); //Invoque la m�thode qui permet au navMesh de faire sa patrouille
        }
        else
        {
            CancelInvoke("OnPatrolling"); //Annule l'�tat de patrouille
        }

        foreach (GameObject target in targetList)
        {
            if (target != null)

            {
                Vector3 distanceFromEnemy = agent.transform.position - target.transform.position; //distance entre le joueur et l'IA
                if (distanceFromEnemy.magnitude < 70)
                {
                    this.state = AttackState.GetState(); //Fait appel au pattern Attack State
                    if (this.state.CanAttackEnemy()) //v�rfie si l'agent peut attaquer
                    {
                        transform.LookAt(target.transform.position); //le forward de l'agent fait face � sa cible
                        agent.destination = target.transform.position; //l'agent s'arrete � sa position actuelle

                        if (distanceFromEnemy.magnitude < 40)
                        {
                            agent.destination = agent.transform.position; // l'IA s'arrete � sa position actuelle

                            transform.LookAt(target.transform.position); //L'IA s''oriente vers sa cible

                            timer += Time.deltaTime; //active le chrono
                            if (timer > 1f)
                            {
                                GameObject fireBall = Instantiate(projectile, transform.position + (transform.forward * 3f), transform.rotation); //Fait une instanciation du projectile
                                fireBall.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse); //Donne une force et une direction au projectile instaci�
                                timer = 0f;
                            }
                        }
                    }

                }

                if (enemy.Hp <= (enemy.HpMax / 2))
                {
                    this.state = RunState.GetState(); // L'IA passe en mode fuite

                    CancelInvoke("OnPatrolling"); //Annule l'�tat de patrouille
                    agent.destination = startPointPosition.position; //L'IA se dirige vers sa base
                    if (enemy.Hp >= (enemy.HpMax * 0.75)) // si le joueur r�cup�re 75% de ses points HP, il retourne � l'ar�ne
                    {
                        this.state = NormalState.GetState();
                    }
                }

                if (baseArea.isBeingCaptured) //Si la base de l'IA est attaqu�e
                {
                    this.state = DefendState.GetState(); //passage au mode d�fense
                    CancelInvoke("OnPatrolling"); //Annule l'�tat de patrouille
                    Debug.Log(state);
                    if (this.state.DefendBase()) //un ennemi est dans la base de l'IA
                    {
                        agent.destination = startPointPosition.position; //L'IA se dirige vers sa base
                        Vector3 distanceFromStartPoint = agent.transform.position - startPointPosition.position;
                        if (distanceFromStartPoint.magnitude < 30) 
                        {
                            this.state = AttackState.GetState(); //si l'IA  est en mode et qu'un adversaire est � proximit�, l'IA passe en mode attaque
                        }
                    }
                }
                else if (!baseArea.isBeingCaptured)
                {
                    this.state = NormalState.GetState(); //fait appel au pattern Normal State
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
        if (pointIndex < patrolPoints.Length) //v�rfie la position de l'index par rapport � la taille de la liste des points de patrouille
        {
            agent.destination = Vector3.MoveTowards(transform.position, patrolPoints[pointIndex].position, 500f * Time.deltaTime); //Dirige l'agent vers le point ind�x� de la liste de patrouille

            if (Vector3.Distance(agent.transform.position, patrolPoints[pointIndex].position) < 10f) //v�rifie la distance en tre le joueur et sa destination
            {
                pointIndex++; //incr�mente l'index pour permettre au joueur d'aller � un nouveau point de patrouille
                agent.SetDestination(patrolPoints[pointIndex].position);
            }
            if (pointIndex >= patrolPoints.Length - 1)
            {
                pointIndex = pointIndex = Random.Range(0, patrolPoints.Length); //r�intialise le tableau une fois la taille final tdu tableau atteinte
            }
        }
    }
}
