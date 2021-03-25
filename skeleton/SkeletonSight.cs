using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] internal GameObject sightLight;
    private Transform archer;
    private Vector3 dirToPlayer;
    private float sightRange;
    private float sightAngle;

    private void Start()
    {
        sightRange = sightLight.GetComponent<Light>().range;
        sightAngle = sightLight.GetComponent<Light>().spotAngle;
        archer = FindObjectOfType<archer>().transform;
    }

    private void Update()
    {
        if (Vector3.Distance(archer.position,transform.position) < sightRange)//in the sight range
        {
            dirToPlayer = archer.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dirToPlayer,out hit, sightRange))
            {
                if (hit.collider.gameObject.CompareTag("archer"))
                {
                    if (Vector3.Angle(dirToPlayer, transform.forward) < sightAngle / 2)//in the sight angle and DETECTED!!
                    {
                        GetComponent<skeletonState>().Chase();
                        sightLight.SetActive(false);
                    }
                }
            }
        }
    }

}
