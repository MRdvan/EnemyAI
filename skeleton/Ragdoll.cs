using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class Ragdoll
{
    public static void DoRagdoll(Rigidbody[] rigidbodies,NavMeshAgent agent,Animator animator,bool state)
    {
        foreach (Rigidbody bodies in rigidbodies)
        {
            bodies.isKinematic = !state;
        }
        agent.enabled = !state;
        animator.enabled = !state;
    }
    public static IEnumerator ApplyForce(Rigidbody body, Transform pos, Vector3 forceDir)// adding impact force after turn into ragdoll
    {
        yield return new WaitForSeconds(0.1f);
        forceDir.y = 1;
        body.AddForce(forceDir, ForceMode.VelocityChange);
    }

}
