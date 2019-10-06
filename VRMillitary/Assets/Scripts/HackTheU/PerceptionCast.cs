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
    public Vector3 rotOffset = new Vector3(0, 30f, 0);
    public List<Transform> objTargets;

    GameObject hit;
    Transform characterMesh;


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
        if (hit ) {
            characterMesh.transform.LookAt(hit.transform);
        }

    }
    void OnEnable()
    {

        foreach (Transform point in objTargets) {

            if (point) {
                GameObject obj= Instantiate(objToSpawn,transform.position,point.rotation);
                obj.transform.LookAt(point);

                // set 
                Debug.Log(obj.name);
                // AI Response
                // only if obj has animation e.g Soldier Animator

                hit = obj.GetComponentInChildren<MechExtraCharSkillRangeAtkRayCast3D>().targetObj;
      
                if (hit)
                {
                    if (hit.tag == "Enemy")
                    {
                        AIResponse(hit.transform);
                    }
                    else {
                    }
                }

            }
        }

    }

    void AIResponse(Transform hit)
    {
        gunMesh.GetComponentInChildren<MechExtraCharSkillRangeAtkSpwnObj>().useWeapon();
        hit = null;
    }
    /*
     *
     * If detect 
     *
     * 
     */
    


}
/*
             shootableMask = LayerMask.GetMask("PropCol");
            gunLine = GetComponent<LineRenderer>();

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;
            gunLine.SetPosition(0, transform.position);

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                //hit an enemy goes here
                gunLine.SetPosition(1, shootHit.point); // draw line from position of fired all the way to hit point
                Debug.Log(gameObject.name + " 2DRaycasthit: " + shootHit.collider.name);

                GameObject targetObj = shootHit.collider.gameObject;
                MechCharStatHP targetMechCharStatHP = targetObj.GetComponent<MechCharStatHP>();
                if (targetMechCharStatHP)
                    targetMechCharStatHP.ApplyDamage(damage);

                MechExtraCharSkillPhysicsShortcuts.LaunchObjBy2Transforms(shootHit.collider.transform, transform, pushBackForce);


            }
            else gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            //        shootHit.collider.GetComponent<MechCharStatHP>().ApplyDamage(damage);

 */
