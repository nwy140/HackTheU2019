using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionColCast : MonoBehaviour
{
    public GameObject collidedObj;

    public GameObject objToSpawn;

    public GameObject gunMesh;
    public float range;


    // This object is a marker for the last known location that the AI saw
    public GameObject lastKnownLocationObj;
    // Logic
    // Detect any object that touches cone collider

    /*
     * shot raycast towards that object, if walls are blocking the raycast, raycast would not work
     * raycast will work only if if hits that collidedObject
     * Character will only attack the collided object if raycast worked
     *
     */

    float nextTime ;
    public bool bIsCallEveryFame = true;
    public float fireRate = 0.5f;

    private GameObject characterMesh;
    private void Awake()
    {

        objToSpawn.GetComponentInChildren<MechExtraCharSkillRangeAtkRayCast3D>().range = range;
        characterMesh = transform.root.GetComponentInChildren<VisCharAnim>().gameObject;

    }

    private void FixedUpdate() {
        if (collidedObj ) {
            //nextTime = Time.time + fireRate; // increase time between bullets for weapon delay

            characterMesh.transform.LookAt(collidedObj .transform);
            

        //rotate weapon holder to face target
        //characterMesh.transform.rotation = Quaternion.Slerp(characterMesh.transform.rotation, collidedObj.transform.rotation, Time.deltaTime);
            print("Shooting"+collidedObj);
        }
        //{
        //    if (collidedObj) {
        //        OnDetectPerception(collidedObj.transform);
        //    }

    }


    private void OnDetectPerception( Transform point) {
        
        GameObject currentHit;
        //ray is raycast type, upon creation it agregates hit event if occcured, inits with orientation and position
        //objToSpawn is a yellow rectangle to show ray for debuging 
        GameObject ray = Instantiate(objToSpawn, characterMesh.transform.position, Quaternion.identity);
        //have ray look at point given by loop  
        ray.transform.LookAt(point);

        //Debug.Log(ray.name);
        //The object that was hit by the ray is set to currentHit
        currentHit = ray.GetComponentInChildren<MechExtraCharSkillRangeAtkRayCast3D>().targetObj;

        // AI Response
        if(currentHit)
        {
            // AI only attacks when raycast detects an enemy
            if (currentHit.tag == "Enemy") {
                lastKnownLocationObj.transform.position = currentHit.transform.position;
                fire_weapon(lastKnownLocationObj.transform);
            }
        }
    }
    void fire_weapon(Transform hit)
    {
        //characterMesh.LookAt(hit.transform);
        //Shoots a yellow raycast at target
        gunMesh.GetComponentInChildren<MechExtraCharSkillRangeAtkSpwnObj>().useWeapon();
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && other.gameObject != characterMesh.transform.parent.gameObject)
        {
            collidedObj = other.gameObject;
            OnDetectPerception(collidedObj.transform);
        }
    }   
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Enemy" && other.gameObject != characterMesh.parent.gameObject)
    //    {
    //        collidedObj = other.gameObject;
    //    }
    //}


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy") {
            collidedObj = null;
        }
    } 
}
