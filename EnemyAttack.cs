using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isAttacking;
    public Transform player;
    public float attackRange;
    public Animator anim;
    public float damage;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(player.transform.position, transform.position) < attackRange) && isAttacking == false)
        {
            anim.SetTrigger("attack");
            isAttacking = true;
            Invoke("attack", 1);
        }
    }

    public void attack()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < attackRange)
        {
            //player.gameObject.GetComponent<PlayerController>().takeDamage(10);
            Debug.LogWarning("Damage Player");
            player.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
        }
        isAttacking = false;
    }
}
