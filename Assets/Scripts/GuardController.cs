using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class GuardController : MonoBehaviour
{
    public float lookRadius = 2.5f;

    public List<Transform> targets = new List<Transform>();
    public int target;

    Transform player;
    NavMeshAgent agent;

    void Start()
    {
        target = 0;

        player = GameController.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target == targets.Count)
            target = 0;

        if (GameController.playerFound == true)
        {
            //targets.Clear();
            PlayerController.playerCanMove = false;
            agent.isStopped = true;
            GetComponent<GuardController>().enabled = false;
            //targets.Add(player);
        }

        // Get the distance to the target
        float distance = Vector3.Distance(targets[target].position, transform.position);
        float playerDistance = Vector3.Distance(player.position, transform.position);

        // This is an optional radius that can be used if the target gets within a certain distance
        if (playerDistance <= lookRadius)
        {
            agent.SetDestination(player.position);
            FaceTarget();
        }
        else
            agent.SetDestination(targets[target].position);     // Move towards the target
        
        if (distance <= agent.stoppingDistance)
            target++;
        
    }

    // Face the target
    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Define the look radius with a red wire
    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, lookRadius);
    //}

}
