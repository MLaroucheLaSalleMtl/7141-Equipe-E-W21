using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Behavior : MonoBehaviour
{
    private Enemy enemy;
    private Enemy enemyGO;
    [SerializeField] private Transform startPointPosition; //Référence à la base du joueur

    [SerializeField] private NavMeshAgent agent; //Mon NavMeshAgent
    [SerializeField] public List<GameObject> targetList = null; //Liste des cibles
    private float timer = 0f;

    //Patrouille de l'IA
    [SerializeField] private Transform[] patrolPoints; //Liste de points de patrouille
    private int pointIndex;

    //Attaque de l'IA

    private IState state; //Référence vers l'interface Istate

    private BaseArea baseArea;
    [SerializeField] private GameObject playerBase;

    private GameObject rangedWeapon = null;
    [SerializeField] private Equipment.typeMelee myMelee;
    [SerializeField] private Equipment.typeRange myRange;
    [SerializeField] private GameObject projectileRock = null;
    [SerializeField] private GameObject projectileSlinger = null;
    [SerializeField] private GameObject projectileBow = null;
    [SerializeField] private GameObject projectileGun = null;
    [SerializeField] private GameObject knifeObject;
    [SerializeField] private GameObject swordObject;
    [SerializeField] private GameObject spearObject;
    [SerializeField] private GameObject hammerObject;
    [SerializeField] private Animator knifeAnim;
    [SerializeField] private Animator swordAnim;
    [SerializeField] private Animator spearAnim;
    [SerializeField] private Animator hammerAnim;
    private bool timerMeleeOn = false;
    private float timerMelee = 0;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //Cache du navmesh
        pointIndex = Random.Range(0, patrolPoints.Length); //retourne un index alátoire de la liste des points de patrouille
        state = NormalState.GetState(); //référence au state pattern
        enemy = GetComponent<Enemy>();
        enemyGO = GetComponent<Enemy>();
        baseArea = playerBase.GetComponent<BaseArea>(); //Cache de cu component BaseArea

        knifeObject.GetComponent<Weapon>().SetOrigin(gameObject);
        swordObject.GetComponent<Weapon>().SetOrigin(gameObject);
        spearObject.GetComponent<Weapon>().SetOrigin(gameObject);
        hammerObject.GetComponent<Weapon>().SetOrigin(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //patrouille

        if (!this.state.CanAttackEnemy() || !this.state.GoToBase() || !this.state.DefendBase()) //Si lÍA est dans son Normal State
        {
            Invoke("OnPatrolling", 3f); //Invoque la méthode qui permet au navMesh de faire sa patrouille
        }
        else
        {
            CancelInvoke("OnPatrolling"); //Annule l'état de patrouille
        }

        foreach (GameObject target in targetList)
        {
            if (target != null)

            {
                Vector3 distanceFromEnemy = agent.transform.position - target.transform.position; //distance entre le joueur et l'IA
                if (distanceFromEnemy.magnitude < 70)
                {
                    this.state = AttackState.GetState(); //Fait appel au pattern Attack State
                    if (this.state.CanAttackEnemy()) //vérfie si l'agent peut attaquer
                    {
                        transform.LookAt(target.transform.position); //le forward de l'agent fait face à sa cible
                        agent.destination = target.transform.position; //l'agent s'arrete à sa position actuelle

                        if (distanceFromEnemy.magnitude < 40)
                        {
                            agent.destination = agent.transform.position; // l'IA s'arrete à sa position actuelle

                            transform.LookAt(target.transform.position); //L'IA s''oriente vers sa cible

                            timer += Time.deltaTime; //active le chrono
                            
                            //Activation d'un power si disponible dans l'inventaire
                            enemyGO.GetComponent<Enemy>().activeInvisibility = true; //Pouvoir d'invisibilité
                            enemyGO.GetComponent<Enemy>().activeInvincibility = true; //Pouvoir d'invincibilité
                            enemyGO.GetComponent<Enemy>().activeDoubleDamage = true; //Pouvoir Double Damage

                            if (distanceFromEnemy.magnitude < 20)
                            {
                                StrikeMeleeWeapon();
                            }
                            else if (timer > 1f)
                            {
                                SetRangedWeapon();//Testing Purpose
                                ShootRangedWeapon();

                                timer = 0f;
                            }
                        }
                    }
                }

                if (enemy.Hp <= (enemy.HpMax / 2))
                {
                    this.state = RunState.GetState(); // L'IA passe en mode fuite

                    CancelInvoke("OnPatrolling"); //Annule l'état de patrouille
                    agent.destination = startPointPosition.position; //L'IA se dirige vers sa base
                    if (enemy.Hp >= (enemy.HpMax * 0.75)) // si le joueur récupère 75% de ses points HP, il retourne à l'arène
                    {
                        this.state = NormalState.GetState();
                    }
                }

                if (baseArea.isBeingCaptured) //Si la base de l'IA est attaquée
                {
                    this.state = DefendState.GetState(); //passage au mode défense
                    CancelInvoke("OnPatrolling"); //Annule l'état de patrouille
                    Debug.Log(state);
                    if (this.state.DefendBase()) //un ennemi est dans la base de l'IA
                    {
                        agent.destination = startPointPosition.position; //L'IA se dirige vers sa base
                        Vector3 distanceFromStartPoint = agent.transform.position - startPointPosition.position;
                        if (distanceFromStartPoint.magnitude < 30) 
                        {
                            this.state = AttackState.GetState(); //si l'IA  est en mode et qu'un adversaire est à proximité, l'IA passe en mode attaque
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

        if (timerMeleeOn)
        {
            timerMelee -= Time.deltaTime;
            if (timerMelee <= 0f)
            {
                timerMelee = 0f;
                timerMeleeOn = false;
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
    public void SetMelee(Equipment.typeMelee enemyMelee)
    {
        myMelee = enemyMelee;
    }

    public void SetRange(Equipment.typeRange enemyRange)
    {
        myRange = enemyRange;
        SetRangedWeapon();
    }

    public void SetRangedWeapon()
    {
        if (myRange == Equipment.typeRange.Rock)
        {
            rangedWeapon = projectileRock;
        }
        else if (myRange == Equipment.typeRange.Slinger)
        {
            rangedWeapon = projectileSlinger;
        }
        else if (myRange == Equipment.typeRange.Bow)
        {
            rangedWeapon = projectileBow;
        }
        else if (myRange == Equipment.typeRange.Gun)
        {
            rangedWeapon = projectileGun;
        }
    }

    public void ShootRangedWeapon()
    {
        if (rangedWeapon == projectileRock)
        {
            GameObject rangedAttack = Instantiate(rangedWeapon, transform.position + (transform.forward * 3f), transform.rotation);
            rangedAttack.GetComponent<Weapon>().SetOrigin(gameObject);
            rangedAttack.GetComponent<Rigidbody>().AddForce(transform.forward * 80, ForceMode.Impulse);
        }
        else if (rangedWeapon == projectileSlinger)
        {
            GameObject rangedAttack = Instantiate(rangedWeapon, transform.position + (transform.forward * 3f), transform.rotation);
            rangedAttack.GetComponent<Weapon>().SetOrigin(gameObject);
            rangedAttack.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
        }
        else if (rangedWeapon == projectileBow)
        {
            GameObject rangedAttack = Instantiate(rangedWeapon, transform.position + (transform.forward * 5f), transform.rotation);
            rangedAttack.GetComponent<Weapon>().SetOrigin(gameObject);
            rangedAttack.GetComponent<Rigidbody>().AddForce(transform.forward * 125, ForceMode.Impulse);
        }
        else if (rangedWeapon == projectileGun)
        {
            GameObject rangedAttack = Instantiate(rangedWeapon, transform.position + (transform.forward * 3f), transform.rotation);
            rangedAttack.GetComponent<Weapon>().SetOrigin(gameObject);
            rangedAttack.GetComponent<Rigidbody>().AddForce(transform.forward * 150, ForceMode.Impulse);
        }

    }

    public void StrikeMeleeWeapon()
    {
        if (!timerMeleeOn)
        {
            timerMeleeOn = true;

            if (myMelee == Equipment.typeMelee.Knife)
            {
                timerMelee = 0.5f;
                knifeObject.SetActive(true);
                if (knifeAnim) knifeAnim.SetTrigger("Attack");
                Invoke("SetInactiveKnife", timerMelee);
            }
            else if (myMelee == Equipment.typeMelee.Sword)
            {
                timerMelee = 0.5f;
                swordObject.SetActive(true);
                if (swordAnim) swordAnim.SetTrigger("Attack");
                Invoke("SetInactiveSword", timerMelee);
            }
            else if (myMelee == Equipment.typeMelee.Spear)
            {
                timerMelee = 0.75f;
                spearObject.SetActive(true);
                if (spearAnim) spearAnim.SetTrigger("Attack");
                Invoke("SetInactiveSpear", timerMelee);
            }
            else if (myMelee == Equipment.typeMelee.Hammer)
            {
                timerMelee = 1f;
                hammerObject.SetActive(true);
                if (hammerAnim) hammerAnim.SetTrigger("Attack");
                Invoke("SetInactiveHammer", timerMelee);
            }
        }
    }

    public void SetInactiveKnife()
    {
        knifeObject.SetActive(false);
    }

    public void SetInactiveSword()
    {
        swordObject.SetActive(false);
    }

    public void SetInactiveSpear()
    {
        spearObject.SetActive(false);
    }

    public void SetInactiveHammer()
    {
        hammerObject.SetActive(false);
    }

}
