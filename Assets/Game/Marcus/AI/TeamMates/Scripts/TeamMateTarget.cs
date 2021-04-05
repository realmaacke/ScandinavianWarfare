using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMateTarget : MonoBehaviour
{
    public TeamMatesAI DeathFunction;
    public float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Death();
            health = 100f;
        }
    }

    void Death()  // DeathFunction
    {
        DeathFunction.GetComponent<EnemyAi>().Die();
    }
}
