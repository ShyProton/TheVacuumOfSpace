using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VACMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float push = 2.0f;
    public float gravity = 2.0f;

    private float gravitation;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if(controller.isGrounded)
            {
                VACImate.inputDir = moveDirection;
                gravitation = 0;
            }
            
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        gravitation -= gravity * Time.deltaTime;
        controller.Move(new Vector3(0f, gravitation, 0f));
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        
        if(body == null || body.isKinematic) { return; }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.AddForceAtPosition(pushDir * push, hit.point);
    }

    /*
    public Rigidbody vacm;
    public Transform mainCam;

    public float speed = 6f;
    public float baseRotation = 20f;

    private Vector3 moveVector;
    private Vector3 modifiedVelocity;

    private int[] userInput = new int[4];
    private string keys = "wasd";
    

    void Start()
    {
        Debug.Log("V.A.C.M. INITIALIZING");
    }

    void Update()
    {
        for(int i = 0; i < 4; i++)
        {
            userInput[i] = Input.GetKey(keys[i].ToString()) ? 1 : 0;
        }

        modifiedVelocity = new Vector3(vacm.velocity.x, 0, vacm.velocity.z);

        VACImate.velocity = moveVector == Vector3.zero ? VACImate.velocity : modifiedVelocity;
    }

    void FixedUpdate()
    {
        float fwdbck = userInput[0] - userInput[2];
        float rgtlft = userInput[3] - userInput[1];

        float diagLim = fwdbck != 0 && rgtlft != 0 ? (float)Math.Sin(45 * Math.PI / 180) : 1;

        moveVector = (mainCam.right * rgtlft + mainCam.forward * fwdbck) * diagLim * speed;
        moveVector.y = 0.0f;

        if(moveVector != Vector3.zero)
        {
            vacm.AddForce(-modifiedVelocity, ForceMode.VelocityChange);
            vacm.AddForce(moveVector, ForceMode.VelocityChange);
        }
    }
    */
}
