using UnityEngine;

public class VACBeam : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject firePoint;
    public CameraCollision camControls;
    public Transform camFollow;

    private GameObject spawnedLaser;

    private float fadeStep = 0.1f;
    private float fadeMin = 0.2f;

    void Start()
    {
        spawnedLaser = Instantiate(laserPrefab, firePoint.transform) as GameObject;
        SetLaser(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetLaser(true);
        }

        if (Input.GetMouseButton(0))
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
        CameraCollision.maxDist = setting ? 6 : 5;
        
    }

    void UpdateLaser()
    {
        if (firePoint != null)
        {
            /*
            foreach (Renderer child in this.GetComponentsInChildren<Renderer>())
            {
                Color childColor = child.material.color;
                childColor.a = 0.5f
                child.material.shader = Shader.Find("Universal Render Pipeline/Lit");
                child.material.SetColor("_BaseColor", childColor);
            }
            */
            spawnedLaser.transform.position = firePoint.transform.position;
        }
    }
}
