using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private Animator animator;
    public GameObject weapon;
    private BoxCollider weaponCollider;

    private void Start()
    {
        animator= GetComponent<Animator>();
        weaponCollider= GetComponent<BoxCollider>();
        weaponCollider.enabled= false;
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        weaponCollider.enabled = true;
        Invoke(nameof(ResetAttack), 1);
    }

    void ResetAttack()
    {
        weaponCollider.enabled = false;
    }
}
