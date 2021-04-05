using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ShootingSystem : MonoBehaviour
{
    public Camera playercam;
    public ParticleSystem Muzzleflash;

    public float damage = 10f;
    public float range = 100f;


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

    }

    void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(playercam.transform.position, playercam.transform.forward, out hit, range) && hit.transform.tag == "Enemy")
        {
            Debug.Log(hit.transform.name);

            hit.transform.GetComponent<EnemyAi>();

            EnemyAi DealDamage = hit.transform.GetComponent<EnemyAi>();
            if (DealDamage != null)
            {
                DealDamage.TakeDamage(10);
            }
        }
        Muzzleflash.Play();
    }
}
