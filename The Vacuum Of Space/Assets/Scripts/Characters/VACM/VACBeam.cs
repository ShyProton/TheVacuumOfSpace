using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VACBeam : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject firePoint;

    private GameObject spawnedLaser;
    private Color vaColor;
    private float alphaFadeSpeed;

    void Start()
    {
        spawnedLaser = Instantiate(laserPrefab, firePoint.transform) as GameObject;
        vaColor = this.GetComponent<MeshRenderer>().material.color;
        SetLaser(false);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SetLaser(true);
        }

        if(Input.GetMouseButton(0))
        {
            UpdateLaser();
        } 
        else
        {
            SetLaser(false);
        }
    }

    void SetLaser(bool setting)
    {
        spawnedLaser.SetActive(setting);

        if(setting)
        {
            
        }
    }

    void UpdateLaser()
    {
        if(firePoint != null)
        {
            vaColor.a = 0.3f;
            this.GetComponent<MeshRenderer>().material.color = vaColor;
            spawnedLaser.transform.position = firePoint.transform.position;
        }
    }
}
