using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehavior : MonoBehaviour {

	/**
	  * Below is the behavior for monsters chasing a standing target. Usually player.
	  **/

	public static void FollowStandingTargetStart (NavMeshAgent agent, GameObject target) 
	{
		agent.destination = target.transform.position;
	}

	/**
	  * Below is the behavior for monsters chasing a moving target. 
	  * Nothing uses this yet, but it could make a cute chain of mobs <3
	  **/

	public static void FollowMovingTargetStart (NavMeshAgent agent, Transform target, Vector3 dest) 
	{
		// Cache agent component and destination
		agent.destination = target.position;
		dest = agent.destination;
	}

	public static void FollowMovingTargetUpdate (NavMeshAgent agent, Transform target, Vector3 dest) 
	{
		// Update destination if the target moves one unit
		if (Vector3.Distance (dest, target.position) > 1.0f) {
			dest = target.position;
			agent.destination = dest;
		}
	}

	/**
	  * 
	  * 
	  **/

	// TODO: finish this bunny related thing
	// LayerMask mask = LayerMask.NameToLayer("SnowTerrain");
	public static void WanderingStart (NavMeshAgent agent, 
									   GameObject self,
									   LayerMask mask,
									   float wanderRadius, 
									   float destChangeRate, 
									   ref float nextDestChange) 
	{
		if (Time.time >= nextDestChange) {
			nextDestChange += destChangeRate;
     		Vector3 dest = RandomNavSphere(self.transform.position, wanderRadius, mask.value);
	     	agent.SetDestination(dest);
		}
	}

	// Extracted from https://forum.unity3d.com/threads/solved-random-wander-ai-using-navmesh.327950/
	public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }
	

	
}
