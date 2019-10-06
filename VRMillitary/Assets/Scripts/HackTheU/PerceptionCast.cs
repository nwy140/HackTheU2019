using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///<summary> 
///     This class performs a raycast that detects any Character and apply damage to them, and then push them back in recoil
///         
///     Explanation:

/// 		
///     Usage:

/// 		
///     Integration:

///
///     Implement Later:
///     - Add Pushback force 
/// 
/// </summary>
/// 
public class PerceptionCast : MonoBehaviour
{
    public float range = 10f;
    public float damage = 5f;
    public float pushBackForce;

    public GameObject objToSpawn;
    public GameObject gunMesh;
    //Ray shootRay;
    //RaycastHit shootHit; //Anything that's hit by the raycast
    //int shootableMask;
    //LineRenderer gunLine;

    float nextTime;
    public float fireRate = 0.5f;
    public Vector3 rotOffset = new Vector3(0, 0, 0);
    public List<Transform> objTargets;

    GameObject currentHit;
    Transform characterMesh;
    float timeCount;

    public bool bIsCallEveryFame = false;
    public List<GameObject> detectedRayHits;

    //AI Logic


    /*
     *
     * If detect an enemy using raycast,
     * rotates towards that enemy and shoot at the enemy
     *
     * 
     */
    private void Awake()
    {
        objToSpawn.GetComponentInChildren<MechExtraCharSkillRangeAtkRayCast3D>().range = range;
         characterMesh = transform.root.GetComponentInChildren<VisCharAnim>().transform;

    }
    private void Update()
    {

        // called every  (default .5 seconds) // set in fireRate
        if (bIsCallEveryFame && Time.time >nextTime)
        {
            nextTime = Time.time + fireRate; // increase time between bullets for weapon delay

            OnDetectPerception();

        }
       

		if (detectedRayHits.Count> 0)
		{
			foreach (var hit in detectedRayHits)
            {
					fire_weapon(hit);
			}
            characterMesh.rotation = Quaternion.Slerp(characterMesh.rotation, detectedRayHits[0].transform.rotation, Time.deltaTime);
            print("Rotating to " + detectedRayHits + "At" + detectedRayHits[0].transform.position);
        }
    }
	void OnDetectPerception()
	{
        bool bisDetectedSomething;
        //detectedRayHits.Clear();
        foreach (Transform point in objTargets)
		{

			if (point)
			{
				//ray is raycast type, upon creation it agregates hit event if occcured, inits with orientation and position
				//objToSpawn is a yellow rectangle to show ray for debuging 
				GameObject ray = Instantiate(objToSpawn, transform.position, Quaternion.identity);
				//have ray look at point given by loop  
				ray.transform.LookAt(point);
				//Debug.Log(ray.name);
				// AI Response
				// only if obj has animation e.g Soldier Animator
				currentHit = ray.GetComponentInChildren<MechExtraCharSkillRangeAtkRayCast3D>().targetObj;

                if (currentHit) {
                    if (currentHit.tag == "Enemy" ) {
                        bisDetectedSomething = true;
                        // add currentHit to array if  array doesn't contain currentHit yet
                        if (!detectedRayHits.Contains (currentHit)) {
                            detectedRayHits.Add(currentHit);
                        } 
                    }

                }

			}

		}
        // if nothing was detected // clear Array
	}

		void fire_weapon(GameObject hit)
		{
        //rotate weapon holder to face target
            characterMesh.rotation = Quaternion.Slerp(characterMesh.rotation, hit.transform.rotation, Time.deltaTime);
        //characterMesh.LookAt(hit.transform);
        //Shoots a yellow raycast at target
            gunMesh.GetComponentInChildren<MechExtraCharSkillRangeAtkSpwnObj>().useWeapon();
	}


}
