using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{

    //Axis
    public float horizontalAxis;
    public float verticalAxis;

    //Mouse Axis
    public float fireAxis_1;
    //Other Axis
    public float jumpAxis;


    private DroneMovement droneMovement;
    private AttachObjects attachObjects;
    // Start is called before the first frame update
    void Awake()
    {
        droneMovement = GetComponent<DroneMovement>();
        attachObjects = GetComponentInChildren<AttachObjects>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
        fireAxis_1 = Input.GetAxis("Fire1");
        jumpAxis = Input.GetAxis("Jump");

        droneMovement.MoveHorizontal(horizontalAxis);
        droneMovement.MoveVertical(verticalAxis);
        
        // attachObjects.RaycastFromMouse(fireAxis_1);
        attachObjects.DetachControls(fireAxis_1,jumpAxis);    


    }
}
