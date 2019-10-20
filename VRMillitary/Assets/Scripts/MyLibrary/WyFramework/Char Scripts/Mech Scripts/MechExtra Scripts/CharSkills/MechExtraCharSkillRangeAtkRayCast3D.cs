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
public class MechExtraCharSkillRangeAtkRayCast3D : MonoBehaviour
{
    public float range = 10f;
    public float damage = 5f;
    public float pushBackForce;
    public GameObject targetObj;

    public GameObject shooter;

    Ray shootRay;
    RaycastHit shootHit; //Anything that's hit by the raycast
    int shootableMask;
    public LineRenderer gunLine;

    public bool bIsCallEveryFame = false;
    private void Update()
    {
        if (bIsCallEveryFame) {
            Start();
        }
    }

	void Awake() {
		shootableMask = LayerMask.GetMask("PropCol");
		if (gunLine == null)
		{
			gunLine = GetComponent<LineRenderer>();
		}

	}
    // Start is called before the first frame update
    void Start()
    {


		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;
		gunLine.SetPosition(0, transform.position);
		if (Physics.Raycast(shootRay, out shootHit,range , shootableMask)){
            //hit an enemy goes here
            gunLine.SetPosition(1,shootHit.point); // draw line from position of fired all the way to hit point
            Debug.Log(gameObject.name + " 2DRaycasthit: " + shootHit.collider.name);
            
            targetObj = shootHit.collider.gameObject;
            if (targetObj.tag == "Enemy" || targetObj.tag == "Enemy_2" && targetObj!=shooter) {
                MechCharStatHP targetMechCharStatHP = targetObj.GetComponent<MechCharStatHP>();
                if (targetMechCharStatHP) {
	                targetMechCharStatHP.ApplyDamage(damage);
                    print("Applying dmg: " + damage + " to " + targetObj.name);
                }
            }
            MechExtraCharSkillPhysicsShortcuts.LaunchObjBy2Transforms(shootHit.collider.transform,transform, pushBackForce);
            
            
        } else gunLine.SetPosition(1,shootRay.origin + shootRay.direction * range);
//        shootHit.collider.GetComponent<MechCharStatHP>().ApplyDamage(damage);


    }   


}
