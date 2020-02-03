using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VACMovement : MonoBehaviour
{
    public Rigidbody vacm;
    public Transform camera;
    public float speed = 100f;

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
    }

    void FixedUpdate()
    {
        float fwdbck = userInput[0] - userInput[2];
        float rgtlft = userInput[3] - userInput[1];

        float diagLim = fwdbck != 0 && rgtlft != 0 ? (float)Math.Sin(45 * Math.PI / 180) : 1;

        Vector3 move = camera.right * rgtlft + camera.forward * fwdbck;

        move *= diagLim;
        move.y = 0.0f;

        if(fwdbck != 0 || rgtlft != 0)
        {
            vacm.AddForce(move, ForceMode.VelocityChange);
        }
        
    }
}
