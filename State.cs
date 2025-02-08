// Base State Class (MonoBehaviour'dan türetilmiþ)
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

// Idle State (MonoBehaviour'dan türetilmiþ)
public class IdleState : State
{
    public override void EnterState(PlayerController player)
    {
        Debug.Log("Entered Idle State");
        player.animator.SetBool("isRunning", false);  // Koþma animasyonu durduruluyor
        player.animator.SetBool("isIdle", true);  // Idle animasyonu baþlatýlýyor
    }

    public override void UpdateState(PlayerController player)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.stateMachine.ChangeState(player.jumpState, player);  // Zýplama durumu
        }
        else if (Input.GetKey(KeyCode.W))  // 'W' tuþuna basýldýðýnda koþma animasyonu baþlar
        {
            player.stateMachine.ChangeState(player.runState, player);
        }
        else if (Input.GetMouseButtonDown(0))  // Mouse sol týklama ile saldýrý animasyonu baþlar
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

// Jump State (MonoBehaviour'dan türetilmiþ)
public class JumpState : State
{
    public override void EnterState(PlayerController player)
    {
        Debug.Log("Entered Jump State");
        player.animator.SetTrigger("Jump");  // Zýplama animasyonu baþlatýlýyor
    }

    public override void UpdateState(PlayerController player)
    {
        if (player.IsGrounded())  // Yere indiðinde Idle durumuna geçiyor
        {
            player.stateMachine.ChangeState(player.idleState, player);
        }
    }

    public override void ExitState(PlayerController player)
    {
        Debug.Log("Exited Jump State");
    }
}

// Run State (MonoBehaviour'dan türetilmiþ)
public class RunState : State
{
    public override void EnterState(PlayerController player)
    {
        Debug.Log("Entered Run State");
        player.animator.SetBool("isRunning", true);  // Koþma animasyonu baþlatýlýyor
        player.animator.SetBool("isIdle", false);  // Idle animasyonu durduruluyor
    }

    public override void UpdateState(PlayerController player)
    {
        if (!Input.GetKey(KeyCode.W))  // 'W' tuþu býrakýldýðýnda Idle durumuna geçiyor
        {
            player.stateMachine.ChangeState(player.idleState, player);
        }
    }

    public override void ExitState(PlayerController player)
    {
        Debug.Log("Exited Run State");
        player.animator.SetBool("isRunning", false);  // Koþma animasyonu durduruluyor
    }
}

// Attack State (MonoBehaviour'dan türetilmiþ)
public class AttackState : State
{
    public override void EnterState(PlayerController player)
    {
        Debug.Log("Entered Attack State");
        player.animator.SetTrigger("Attack");  // Saldýrý animasyonu baþlatýlýyor
    }

    public override void UpdateState(PlayerController player)
    {
        if (!player.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))  // Saldýrý animasyonu bittiðinde Idle'a geçiyor
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
        player.animator.SetTrigger("HeavyAttack");  // Saldýrý animasyonu baþlatýlýyor
    }

    public override void UpdateState(PlayerController player)
    {
        if (!player.animator.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack"))  // Saldýrý animasyonu bittiðinde Idle'a geçiyor
        {
            player.stateMachine.ChangeState(player.idleState, player);
        }
    }

    public override void ExitState(PlayerController player)
    {
        Debug.Log("Exited HeavyAttack State");
    }
}

// PlayerController (MonoBehaviour sýnýfý)

