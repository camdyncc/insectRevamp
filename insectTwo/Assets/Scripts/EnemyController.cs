using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{

    //private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyState enemy_State;

    public float walk_Speed = 0.5f;
    public float run_Speed = 4f;

    public float chase_Distance = 30f;
    private float current_Chase_Distance;
    public float attack_Distance = 1.8f;
    public float chase_After_Attack_Distance = 2f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    private float patrol_Timer;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    private Transform target;
    public GameObject attack_Point;

    // private EnemyAudio enemy_Audio;





    void Awake()
    {
        // enemy_Anim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag(Tags.PLAYER_TAG ).transform;





    }



    // Start is called before the first frame update
    void Start()
    {
        enemy_State = EnemyState.PATROL;
        SetNewRandomDestination();
        patrol_Timer = patrol_For_This_Time;

        //attack as soon as enemy reaches player
        attack_Timer = wait_Before_Attack;

        //memorize value of chase distance so we can put it back
        current_Chase_Distance = chase_Distance;

        



    }

    // Update is called once per frame
    void Update()
    {
        System.Diagnostics.Debug.WriteLine("testing plZ");
        if (enemy_State == EnemyState.PATROL)
        {
            Patrol();
        }
        if (enemy_State == EnemyState.CHASE)
        {
            Chase();
            // navAgent.speed = run_Speed;
        }

        if (enemy_State == EnemyState.ATTACK)
        {
            //make enemy enter attack state//
            //targets the player

            //stops targeting once player distance is > then the

            Attack();
        }




    }


    void Patrol()
    {
        //allows nav agent to move
        navAgent.isStopped = false;
        navAgent.speed = walk_Speed;
        SetNewRandomDestination();
        System.Diagnostics.Debug.WriteLine("testing");

        patrol_Timer += Time.deltaTime;

        if (patrol_Timer > patrol_For_This_Time)
        {
            SetNewRandomDestination();

            patrol_Timer = 0f;
        }

        //if speed greater than 0
        if (navAgent.velocity.sqrMagnitude > 0)
        {
            // enemy_Anim.Walk(true);
        }
        else
        {
            // enemy_Anim.Walk(false);
        }

        //gets distance between enemy and player (transform.postion = enemy, target.position = player)
        if (Vector3.Distance(transform.position, target.position) <= chase_Distance)
        {
            //enemy_Anim.Walk(false);
            enemy_State = EnemyState.CHASE;
            

            //play audio of enemy spotting player
        }

    }// patrol
    void Chase()
    {
        //enable agent to move again
        navAgent.isStopped = false;
        //sets agent to run speed and run animation
        navAgent.speed = run_Speed;


        // sets new desitnation of agent to the position of the player 
        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            //enemy_Anim.Run(true);
        }
        else
        {
            // enemy_Anim.Run(false);
        }

        //if distance between agent and player is less than the attack distance, agent will attack
        if (Vector3.Distance(transform.position, target.position) <= attack_Distance)
        {
            //stop walk/run animations
            //  enemy_Anim.Run(false);
            //  enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;


            //resets chase distance to previous value incase player runs away from the agent
            // if we do not reset the chase distance, agent will still think player is in attacking distance 
            // since the attack distance condition was previously true
            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }




        }
        else if (Vector3.Distance(transform.position, target.position) > chase_Distance)
        {
            //if player runs away from enemy to the point that the distance between the player and the agent is
            //now greater than the chase distance, the agent will stop running due to chase_Distance now being false

            // enemy_Anim.Run(false);
            enemy_State = EnemyState.PATROL;

            //reset patrol timer so new patrol destination can be determined asap
            patrol_Timer = patrol_For_This_Time;
            //reset chase distance value to previouse value
            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }

        }

    }

    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_Timer += Time.deltaTime;
        if (attack_Timer > wait_Before_Attack)
        {
            // enemy_Anim.Attack();

            // resets attack timer so cannibal doesnt attack forever
            attack_Timer = 0f;

            //play attack sound



        }

        // test if player runs away
        if (Vector3.Distance(transform.position, target.position) > attack_Distance + chase_After_Attack_Distance)
        {
            enemy_State = EnemyState.CHASE;
        }


    } //attack

    void SetNewRandomDestination()
    {
        //gets value between 20 and 60 units to determine next patrol destiantion
        //this runs once the total time patrolled is greather than the time limit for the current patrol
        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;

        //if the random postition chosen for patrol is outside of navMesh, a new position will be chosen
        //-1 includes all layers
        // new safe position is saved in navHit
        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        //since new safe position is saved in navHit, we will set desitnation to the position of navHit
        navAgent.SetDestination(navHit.position);

    }


    void Turn_On_AttackPoint()
    {
        // attack_Point.SetActive(true);
    }

    void Turn_Off_AttackPoint()
    {
        // if (attack_Point.activeInHierarchy)
        //{
        //   attack_Point.SetActive(false);
        //  }
    }

    public EnemyState Enemy_State
    {
        get; set;
    }
}