using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public HealthController controller;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            HealthController Health;
            Health = controller;

            Debug.Log("Hit");

            Health.GetComponent<HealthController>().Damage(10);

            

            Destroy(gameObject);
        }
    }
}
