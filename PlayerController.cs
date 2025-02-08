using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;

    public float moveSpeed = 5f;

    public StateMachine stateMachine;

    public IdleState idleState;
    public JumpState jumpState;
    public RunState runState;
    public AttackState attackState;
    public HeavyAttackState heavyAttackState;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        stateMachine = new StateMachine();

        idleState = gameObject.AddComponent<IdleState>();  // MonoBehaviour nesnelerini ekleyin
        jumpState = gameObject.AddComponent<JumpState>();
        runState = gameObject.AddComponent<RunState>();
        attackState = gameObject.AddComponent<AttackState>();
        heavyAttackState = gameObject.AddComponent<HeavyAttackState>();

        stateMachine.Initialize(idleState, this);
    }

    void Update()
    {
        stateMachine.Update(this);

        // Hareket kontrol�
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ);

        if (move.magnitude > 0.1f)  // E�er hareket varsa, ko�ma animasyonu aktif edilir
        {
            rb.MovePosition(transform.position + move * moveSpeed * Time.deltaTime);
        }
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}