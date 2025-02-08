using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;

    // Hasar alma fonksiyonu
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log("Enemy Health: " + health);

        // Saðlýk sýfýrýn altýna düþerse objeyi yok et
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy defeated!");
        Destroy(gameObject);
    }
}
