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

    GameObject hit;
    Transform characterMesh;
    float timeCount;

    public bool bIsCallEveryFame = false;
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
        if (bIsCallEveryFame && Time.time >nextTime)
        {
            nextTime = Time.time + fireRate; // increase time between bullets for weapon delay

            OnEnable();

        }
       

    }
    void OnEnable()
    {

        foreach (Transform point in objTargets) {

            if (point) {
                //ray is raycast type, upon creation it agregates hit event if occcured, inits with orientation and position
                //objToSpawn is a yellow rectangle to show ray for debuging 
                GameObject ray = Instantiate(objToSpawn,transform.position,Quaternion.identiy;
                //have ray look at point given by loop  
                ray.transform.LookAt(point);
                //Debug.Log(ray.name);
                // AI Response
                // only if obj has animation e.g Soldier Animator
                hit = ray.GetComponentInChildren<MechExtraCharSkillRangeAtkRayCast3D>().targetObj;      
                if (hit)
                {
                    //only qualified ray collisions are reactive 
                    if (hit.tag == "Enemy")
                    {
                        fire_weapon(hit.transform);
                    }
                }

            }
        }

    }

    void fire_weapon(Transform hit)
    {
        //rotate weapon holder to face target
        characterMesh.rotation = Quaternion.Slerp(characterMesh.rotation, hit.transform.rotation, Time.deltaTime);
        //Shoots a yellow raycast at target
        gunMesh.GetComponentInChildren<MechExtraCharSkillRangeAtkSpwnObj>().useWeapon();
    }
}
