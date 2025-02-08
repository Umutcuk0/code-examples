// Base State Class (MonoBehaviour'dan t�retilmi�)
using UnityEngine;

public class State : MonoBehaviour
{
    public virtual void EnterState(PlayerController player) { }
    public virtual void UpdateState(PlayerController player) { }
    public virtual void ExitState(PlayerController player) { }
}

// StateMachine Class
public class StateMachine
{
    private State currentState;

    public void Initialize(State startingState, PlayerController player)
    {
        currentState = startingState;
        currentState.EnterState(player);
    }

    public void ChangeState(State newState, PlayerController player)
    {
        currentState.ExitState(player);
        currentState = newState;
        currentState.EnterState(player);
    }

    public void Update(PlayerController player)
    {
        currentState.UpdateState(player);
    }
}

// Idle State (MonoBehaviour'dan t�retilmi�)
public class IdleState : State
{
    public override void EnterState(PlayerController player)
    {
        Debug.Log("Entered Idle State");
        player.animator.SetBool("isRunning", false);  // Ko�ma animasyonu durduruluyor
        player.animator.SetBool("isIdle", true);  // Idle animasyonu ba�lat�l�yor
    }

    public override void UpdateState(PlayerController player)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.stateMachine.ChangeState(player.jumpState, player);  // Z�plama durumu
        }
        else if (Input.GetKey(KeyCode.W))  // 'W' tu�una bas�ld���nda ko�ma animasyonu ba�lar
        {
            player.stateMachine.ChangeState(player.runState, player);
        }
        else if (Input.GetMouseButtonDown(0))  // Mouse sol t�klama ile sald�r� animasyonu ba�lar
        {
            player.stateMachine.ChangeState(player.attackState, player);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            player.stateMachine.ChangeState(player.heavyAttackState, player);   
        }
    }

    public override void ExitState(PlayerController player)
    {
        Debug.Log("Exited Idle State");
        player.animator.SetBool("isIdle", false);  // Idle animasyonu durduruluyor
    }
}

// Jump State (MonoBehaviour'dan t�retilmi�)
public class JumpState : State
{
    public override void EnterState(PlayerController player)
    {
        Debug.Log("Entered Jump State");
        player.animator.SetTrigger("Jump");  // Z�plama animasyonu ba�lat�l�yor
    }

    public override void UpdateState(PlayerController player)
    {
        if (player.IsGrounded())  // Yere indi�inde Idle durumuna ge�iyor
        {
            player.stateMachine.ChangeState(player.idleState, player);
        }
    }

    public override void ExitState(PlayerController player)
    {
        Debug.Log("Exited Jump State");
    }
}

// Run State (MonoBehaviour'dan t�retilmi�)
public class RunState : State
{
    public override void EnterState(PlayerController player)
    {
        Debug.Log("Entered Run State");
        player.animator.SetBool("isRunning", true);  // Ko�ma animasyonu ba�lat�l�yor
        player.animator.SetBool("isIdle", false);  // Idle animasyonu durduruluyor
    }

    public override void UpdateState(PlayerController player)
    {
        if (!Input.GetKey(KeyCode.W))  // 'W' tu�u b�rak�ld���nda Idle durumuna ge�iyor
        {
            player.stateMachine.ChangeState(player.idleState, player);
        }
    }

    public override void ExitState(PlayerController player)
    {
        Debug.Log("Exited Run State");
        player.animator.SetBool("isRunning", false);  // Ko�ma animasyonu durduruluyor
    }
}

// Attack State (MonoBehaviour'dan t�retilmi�)
public class AttackState : State
{
    public override void EnterState(PlayerController player)
    {
        Debug.Log("Entered Attack State");
        player.animator.SetTrigger("Attack");  // Sald�r� animasyonu ba�lat�l�yor
    }

    public override void UpdateState(PlayerController player)
    {
        if (!player.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))  // Sald�r� animasyonu bitti�inde Idle'a ge�iyor
        {
            player.stateMachine.ChangeState(player.idleState, player);
        }
    }

    public override void ExitState(PlayerController player)
    {
        Debug.Log("Exited Attack State");
    }
}

public class HeavyAttackState : State
{
    public override void EnterState(PlayerController player)
    {
        Debug.Log("Entered HeavyAttack State");
        player.animator.SetTrigger("HeavyAttack");  // Sald�r� animasyonu ba�lat�l�yor
    }

    public override void UpdateState(PlayerController player)
    {
        if (!player.animator.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack"))  // Sald�r� animasyonu bitti�inde Idle'a ge�iyor
        {
            player.stateMachine.ChangeState(player.idleState, player);
        }
    }

    public override void ExitState(PlayerController player)
    {
        Debug.Log("Exited HeavyAttack State");
    }
}

// PlayerController (MonoBehaviour s�n�f�)

