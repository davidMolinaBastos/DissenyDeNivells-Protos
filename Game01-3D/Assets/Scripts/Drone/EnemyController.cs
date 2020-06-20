using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent NavMeshAgent;
    enum TState{ Patrol, GoTo, Atack, Die}
    private TState state = TState.Patrol;

    public Transform[] patrolPoints;
    int patrolPosID;

    public Transform player;
    public GameObject drop;
    [Header("Stats")]
    public float DistToAtack = 50;
    public float DistToDetect = 150;
    public float attackCooldown = 5;
    public float timeToDie = 1;
    public float damage = 50;
    public float MaxHp = 100;
    public int dropValue; 

    float HP = 100f;
    float time = 0;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        time += Time.deltaTime;
        CheckState();
        UpdateState();
    }

    private void CheckState()
    {
        switch (state)
        {
            case TState.Patrol:
                if (Vector3.Distance(player.position, transform.position) < DistToDetect && time > 0.5f)
                    ChangeState(TState.GoTo);
                break;
            case TState.GoTo:
                if (Vector3.Distance(player.position, transform.position) < DistToAtack && time > 0.5f)
                    ChangeState(TState.Atack);
                else if (Vector3.Distance(player.position, transform.position) < DistToDetect && time > 0.5f)
                    ChangeState(TState.Patrol);
                break;
            case TState.Atack:
                if (Vector3.Distance(player.position, transform.position) > DistToAtack && time > 0.5f)
                    ChangeState(TState.GoTo);
                break;
        }
    }
    private void ChangeState(TState newState)
    {
        switch (state)
        {
            case TState.Patrol:
                time = 0;
                NavMeshAgent.isStopped = true;
                break;
            case TState.GoTo:
                time = 0;
                NavMeshAgent.isStopped = true;
                break;
            case TState.Atack:
                time = 0;
                break;
            case TState.Die:
                time = 0;
                HP = MaxHp;
                GetComponent<MeshRenderer>().material.color = Color.white;
                break;
        }
        switch (newState)
        {
            case TState.Patrol:
                NavMeshAgent.isStopped = false;
                patrolPosID = GetClosestPatrolPosId();
                NavMeshAgent.SetDestination(patrolPoints[patrolPosID].position);
                break;
            case TState.GoTo:
                NavMeshAgent.isStopped = false;
                NextChasePos();
                break;
            case TState.Atack:
                player.GetComponent<PlayerController>().Damage((int)damage);
                break;
        }
        state = newState;
    }
    private void UpdateState()
    {
        switch (state)
        {
            case TState.Patrol:
                if (!NavMeshAgent.hasPath && NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
                    MoveToNextPatrolPosition();
                break;
            case TState.GoTo:
                if (!NavMeshAgent.hasPath && NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
                    MoveToNextPatrolPosition();
                break;
            case TState.Atack:
                if (time >= attackCooldown)
                {
                    player.GetComponent<PlayerController>().Damage((int)damage);
                    time = 0;
                }
                transform.LookAt(Vector3.Lerp(transform.forward, player.position, Time.deltaTime));
                break;
            case TState.Die:
                if (time <= timeToDie)
                    GetComponent<MeshRenderer>().material.color = Color.Lerp(GetComponent<MeshRenderer>().material.color, new Color(0, 0, 0, 0), Time.deltaTime);
                else
                    Die();
                break;
        }
    }

    void MoveToNextPatrolPosition()
    {
        ++patrolPosID;
        if (patrolPosID >= patrolPoints.Length)
            patrolPosID = 0;
        NavMeshAgent.SetDestination(patrolPoints[patrolPosID].position);
    }
    void NextChasePos()
    {
        NavMeshAgent.isStopped = false;
        Vector3 dest = player.position - transform.position;
        float dist = dest.magnitude;
        dest /= dist;
        dest = transform.position + dest * (dist - DistToAtack);
        NavMeshAgent.SetDestination(dest);
    }
    int GetClosestPatrolPosId()
    {
        int id = patrolPosID;
        for (int i = 0; i < patrolPoints.Length; i++)
            if (Vector3.Distance(transform.position, patrolPoints[id].position) > Vector3.Distance(patrolPoints[i].position, transform.position))
                id = i;
        return id;
    }
    

    public void Damage(int damage)
    {
        print(HP + "Damage:" + damage);
        HP -= damage;
        print(HP);
        if (HP <= 0)
            ChangeState(TState.Die);
    }
    void Die()
    {
        if (drop != null)
        {
            GameObject Drop = Instantiate(drop);
            if(Drop.GetComponent<KeyScript>() != null)
                Drop.GetComponent<KeyScript>().keyID = dropValue;
            else if(Drop.GetComponent<AmmoItem>() != null)
            {
                Drop.GetComponent<AmmoItem>().value = dropValue;
                Drop.GetComponent<AmmoItem>().weapon1 = Random.Range(0, 100) > 50;
            }
        }

        gameObject.SetActive(false);
    }
}
