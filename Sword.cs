using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damage = 10; // Verilecek hasar miktar�
    public string enemyTag = "Enemy"; // Enemy objelerinin etiketi

    private void OnTriggerEnter(Collider other)
    {
        // �arp��an objenin etiketi kontrol edilir
        if (other.CompareTag(enemyTag))
        {
            // Enemy objesinde bir Health bile�eni aran�r
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Hasar verilir
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}