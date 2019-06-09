using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public int LocalKillCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            LocalKillCount = LocalKillCount + 1;

            other.GetComponent<EnemyHealth>().TakeDamage(1);            
        }
    }
}
