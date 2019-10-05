using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{

    public Vector3 idleForce;
    public float speedMultiplier = 1; // multiplier for movement force

    private float horizontalSpeed = 3f;
    private float verticalSpeed = 3f;
    public float horizontalRot =3f;

    Rigidbody myRB;
    

    // Start is called before the first frame update
    void Awake()
    {
        myRB = GetComponent<Rigidbody>();
        horizontalSpeed *= speedMultiplier;
        verticalSpeed *= speedMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        // Lock Z axis
        transform.position = new Vector3(transform.position.x,transform.position.y,0f);
        myRB.AddForce(idleForce); // force added to Drone at all times
        myRB.AddForce(-idleForce); // inertia force added to Drone at all times


        //clamp rotation
        // if(transform.rotation.z <=-30 || transform.rotation.z >=30 || movingHorizontal == 0){
        //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * horizontalRot);
        // }

    }

    public void MoveHorizontal(float AxisValue){
        myRB.AddForce(new Vector3(horizontalSpeed * AxisValue, 0f, 0f));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -30* AxisValue), Time.deltaTime * horizontalRot);
    }

    public void MoveVertical(float AxisValue){
        myRB.AddForce(new Vector3(0f, verticalSpeed * AxisValue, 0f));
    }
    




}
