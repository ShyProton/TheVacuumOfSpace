using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VACHole : MonoBehaviour
{
    public Animator vacimation;
    public Transform blackHole;
    public Transform capsuleBase;
    public Transform vacmHead;
    public Transform mainCam;
    public float capsuleRotation = 20f;
    public float holeStrength = 2000f;
    public float castRadius = 3f;
    public float castDistance = 10f;
    public LayerMask layerMask;

    private bool mouseClicked;
    private Vector3 castOrigin;
    private Vector3 castDirection;
    private float objectDistance;
    public GameObject affectedObject;

    void Update()
    {
        mouseClicked = Input.GetMouseButton(0);

        if (mouseClicked)
        {
            vacimation.SetTrigger("MouseClicked");
            Quaternion capQuat = Quaternion.Slerp(capsuleBase.rotation, mainCam.rotation, capsuleRotation * Time.deltaTime);
            Quaternion headQuat = Quaternion.Slerp(vacmHead.rotation, mainCam.rotation, VACMovement.headRotation * Time.deltaTime);
            capsuleBase.rotation = Quaternion.Euler(0, capQuat.eulerAngles.y, 0);
            vacmHead.rotation = Quaternion.Euler(0, headQuat.eulerAngles.y, 0);
        } else
        {
            vacimation.ResetTrigger("MouseClicked");
        }
    }

    void FixedUpdate()
    {
        if(mouseClicked)
        {
            RaycastHit hit;

            castOrigin = blackHole.position;
            castDirection = blackHole.forward;

            if (Physics.SphereCast(castOrigin, castRadius, castDirection, out hit, castDistance, layerMask, QueryTriggerInteraction.UseGlobal))
            {
                affectedObject = hit.transform.gameObject;
                objectDistance = hit.distance;
            } else
            {
                objectDistance = castDistance;
                affectedObject = null;
            }

            if(affectedObject.tag == "VACHole")
            {
                Vector3 forceDirection = blackHole.position - affectedObject.GetComponent<Transform>().position;
                affectedObject.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * holeStrength * Time.deltaTime);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(castOrigin, castOrigin + castDirection * objectDistance);
        Gizmos.DrawWireSphere(castOrigin + castDirection * objectDistance, castRadius);
    }
}
