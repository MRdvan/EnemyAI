using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton : MonoBehaviour
{
    [SerializeField] internal skeletonState state;
    
    internal Rigidbody[] rigidbodies;
    [SerializeField] private float health;
    [SerializeField] private GameObject sightLight;
    public float Health { get { return health; } set{ health = value; } }


    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        health = 100;
    }

    public void TakeDamage(float damage,Rigidbody body, Transform pos,Vector3 impactDir)
    {
        Debug.Log("hitted:" + body.transform.name);
        health -= damage;
        sightLight.SetActive(false);
        state.Chase();
        if (health <= 0)
        {
            Die(body,pos,impactDir);
        }
    }

    public void Die(Rigidbody body, Transform pos,Vector3 impactDir)
    {
        //inc kill counter for player

        if (QuestManager.Instance.quest !=null && QuestManager.Instance.workingEvent.Type == GoalType.Kill)
        {
            QuestManager.Instance.UpdateEventGoalProgress(gameObject.tag, GoalType.Kill);
        }
        Ragdoll.DoRagdoll(rigidbodies, state._agent, state._animator, true);
        StartCoroutine(Ragdoll.ApplyForce(body, pos, impactDir));

    }

}
