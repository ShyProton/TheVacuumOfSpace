using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VACHole : MonoBehaviour
{
    public Transform blackHole;
    public LayerMask layerMask;
    public Color32 highlight;

    public float holeStrength = 2000f;
    public float castRadius = 3f;
    public float castDistance = 10f;

    private Vector3 castOrigin;
    private Vector3 castDirection;
    private GameObject affectedObject;

    private bool mouseClicked;
    private float objectDistance;

    void Update()
    {
        mouseClicked = Input.GetMouseButton(0);
    }

    void FixedUpdate()
    {   
        if(affectedObject)
        {
            affectedObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
        
        if (mouseClicked)
        {
            RaycastHit hit;

            castOrigin = blackHole.position;
            castDirection = blackHole.forward;

            if (Physics.SphereCast(castOrigin, castRadius, castDirection, out hit, castDistance, layerMask, QueryTriggerInteraction.UseGlobal))
            {
                affectedObject = hit.transform.gameObject;
                objectDistance = hit.distance;

                Debug.Log(objectDistance);
                if (affectedObject.CompareTag("VACHole"))
                {
                    Vector3 forceDirection = blackHole.position - affectedObject.GetComponent<Transform>().position;
                    affectedObject.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * holeStrength * Time.deltaTime);
                    affectedObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    affectedObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", highlight);
                }
            }
            else
            {
                objectDistance = castDistance;
                affectedObject = null;
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
