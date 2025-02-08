using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damage = 10; // Verilecek hasar miktarý
    public string enemyTag = "Enemy"; // Enemy objelerinin etiketi

    private void OnTriggerEnter(Collider other)
    {
        // Çarpýþan objenin etiketi kontrol edilir
        if (other.CompareTag(enemyTag))
        {
            // Enemy objesinde bir Health bileþeni aranýr
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Hasar verilir
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}