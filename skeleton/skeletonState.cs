using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class skeletonState : MonoBehaviour
{
    [SerializeField] private skeleton skeleton;
    [SerializeField] public Transform[] points;
    [SerializeField] public int destinationPoint;
    internal NavMeshAgent _agent;
    internal Animator _animator;
    private archer _archer;
    float distance;
    public float attackRate = 3f;
    public float nextAttack = 2.9f;
    public int damage;

    // Start is called before the first frame update


    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _archer = FindObjectOfType<archer>();
        destinationPoint = 0;
        _agent.autoBraking = false;
        _animator.SetBool("wander", true);
        //Ragdoll.DoRagdoll(skeleton.rigidbodies, _agent, _animator, false);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, _archer.transform.position);
    }

    internal void GoToNextPoint()
    {
        //if (points.Length == 0)
        //    return;
         destinationPoint = (destinationPoint + 1) % points.Length;
        _agent.SetDestination(points[destinationPoint].position);
       
    }

    internal void Hit()
    {
        _archer.stats.TakeDamage(damage);    
    }

    internal void Attack()
    {
        _animator.SetFloat("dist", distance);
        if (nextAttack > attackRate)
        {
            _animator.SetTrigger("attack");
            nextAttack = 0;
        }
        else
        {
            nextAttack += Time.deltaTime;
        }
    }

    internal  void Chase()
    {

        _animator.SetFloat("dist", distance);
        _animator.SetBool("wander", false);
        _animator.SetBool("chase", true);
        if (_agent.enabled == true)
        {
            _agent.autoBraking = true;
            _agent.stoppingDistance = 8;
            _agent.SetDestination(_archer.transform.position);
        }
       
        //if (distance > 50)
        //{
        //    _animator.SetBool("chase", false);
        //}
    }
   


}
