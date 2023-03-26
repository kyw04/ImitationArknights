using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public GameObject attackRange;
    public float hp;
    public float damage;
    public float attackTime;
    public float speed;

    private NavMeshAgent agent;
    private Vector3 destination;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
    }

    private void Update()
    {
        if (Vector3.Distance(destination, target.position) > 1.0f)
        {
            destination = target.position;
            agent.destination = destination;
        }
    }
}
