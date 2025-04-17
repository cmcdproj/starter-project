using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{


    CharacterController Controller;
    //RigidBody rb;

    public float Speed; //This is how fast the player goes
    public float JumpVelocity; //This is the speed the player jumps
    public float GravSpeed; //This is how quickly the player falls down
    public float checkDist; //This is how far below them their feet are
    public float camDist;
    public Transform Cam;


    private bool grounded;

    private float velocity;
    private float terminalVelocity = 5;

    // Start is called before the first frame update
    void Start()
    {

        Controller = GetComponent<CharacterController>();
        //rb = GetComponent<RigidBody>();

    }
    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, checkDist, layerMask))
        {
            grounded = true;
            if (velocity < -0.5)
                velocity = 0;
        }
        else
        {
            grounded = false;
        }
        GameObject go = GameObject.Find("Main Camera");
        CameraMove cr = go.GetComponent<CameraMove>();
        if (Physics.Raycast(transform.position, Vector3.Normalize(Cam.position - transform.position), out hit, camDist, layerMask))
        {
            //Debug.Log(hit.distance);
            
            cr.distance = hit.distance;
        } else
        {
            cr.distance = camDist;

        }

    }
    // Update is called once per frame
    void Update()
    {

        float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            velocity = JumpVelocity;
        else if (velocity > -terminalVelocity) {
            if (!Input.GetKey(KeyCode.Space))
                velocity -= GravSpeed * Time.deltaTime;
            velocity -= GravSpeed * Time.deltaTime;
        }
        Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
        Movement.y = velocity;


        Controller.Move(Movement);

        if (Movement.magnitude != 0f)
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Cam.GetComponent<CameraMove>().sensivity * Time.deltaTime);


            Quaternion CamRotation = Cam.rotation;
            CamRotation.x = 0f;
            CamRotation.z = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);

        }
    }

}