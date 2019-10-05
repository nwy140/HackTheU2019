using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachObjects : MonoBehaviour
{
    private bool attachedToObject;
    Ray ray;
    RaycastHit hit;
    public GameObject boxBoundary; // collider to stop drone from going to low that objects fall through the ground


    private void OnTriggerEnter(Collider other)
    {
        //must collide trigger with same object raycasted by mouse and mouse must be pressed
        if (other.tag == "Attach" && attachedToObject == false)
        {

            if (other.GetComponent<Rigidbody>() != null)
            {
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ
                // | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY
                ;
            }
            other.transform.parent = transform;
            attachedToObject = true;
            boxBoundary.SetActive(attachedToObject);

        }
    }

    public void DetachControls(float fire1AxisValue, float jumpAxisValue)
    {

        if (fire1AxisValue > 0)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.name);
            }
            else
            {
                // if raycast hit nothing
                hit = new RaycastHit();
            }
        }
        if (hit.collider != null)
        {
            if (hit.transform.gameObject.tag == "Attach")
            {
                DetachObjects();
            }
        }

        if (jumpAxisValue > 0)
        {
            DetachObjects();
        }


    }

    void DetachObjects()
    {
        if (transform.childCount > 0)
        {
            transform.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            transform.DetachChildren();
        }
        // when no children

    }
    void Update()
    {
        if (attachedToObject && transform.childCount > 0)
        {
            transform.GetChild(0).transform.localPosition = Vector3.zero;

            //Press space or when mouse click attached object
            if (Vector3.Distance(transform.position, transform.GetChild(0).transform.position) > 7f)
            {
                DetachObjects();
            }

        }
        if (transform.childCount == 0  )
        {
            attachedToObject = false;
            boxBoundary.SetActive(attachedToObject);
        } 
    }
}


/*
        
        if(fire1AxisValue>0)
            RaycastFromMouse();
        else //reset hit when no click
            hit = new RaycastHit();
        
        //Press space or when mouse click attached object
        if(jumpAxisValue > 0 || hit.transform.gameObject.tag == "Attach" || Vector3.Distance(transform.position, transform.GetChild(0).transform.position) >7f ) {
            transform.DetachChildren();
        transform.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
*/