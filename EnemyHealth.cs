using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;

    // Hasar alma fonksiyonu
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log("Enemy Health: " + health);

        // Sa�l�k s�f�r�n alt�na d��erse objeyi yok et
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
