using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VACMovement : MonoBehaviour
{
    public Rigidbody vacm;
    public Transform mainCam;
    public Transform vacBase;
    public float speed = 100f;
    public float baseRotation = 10f;

    private int[] userInput = new int[4];
    private string keys = "wasd";
    private Vector3 moveVector;
    private Quaternion baseQuaternion = Quaternion.Euler(0, 0, 0);

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
            vacm.AddForce(-vacm.velocity, ForceMode.VelocityChange);
            vacm.AddForce(moveVector, ForceMode.VelocityChange);
        }     
    }

    void LateUpdate()
    {   
        if(moveVector != Vector3.zero)
        {
            baseQuaternion = Quaternion.LookRotation(vacm.velocity.normalized);
            vacBase.rotation = Quaternion.Slerp(vacBase.rotation, baseQuaternion, baseRotation * Time.deltaTime);
        }
    }
}
