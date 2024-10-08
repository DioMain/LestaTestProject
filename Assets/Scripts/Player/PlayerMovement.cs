using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPlus
{
    [Header("������")]
    [SerializeField]
    private Transform modelTrasform;
    [SerializeField] 
    private Transform groundPoint;
    [SerializeField]
    private Rigidbody body;

    [Header("���������")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float runFactor;
    [SerializeField]
    private float notGroundFactor = 0.2f;
    [SerializeField, Range(0f, 1f)]
    private float coyoteTime = 0.1f;

    [Space]

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpRayDistance = 0.3f;
    [SerializeField]
    private float jumpRayRadius = 0.25f;

    [Space]

    [SerializeField]
    private float rotateForce = 1.5f;

    [Space]

    public bool CanMove = true;
    public bool CanJump = true;
    public bool CanRotate = true;

    public bool IsGround { get; private set; } = false;
    public bool IsRun { get; private set; } = false;

    private bool isAllowJump = false;

    public bool IsMove => moveDirection != Vector3.zero;

    private Vector3 moveDirection = Vector3.zero;
    private RaycastHit groundHit;

    private Vector3 CameraFoward => LevelManager.Instance.CameraCapture.transform.forward;

    public override void Initialize()
    {
        StartCoroutine(RotationAnimation());
        StartCoroutine(CoyoteTime());
    }

    private void Update()
    {
        MovementLogic();

        JumpLogic();
    }

    private void FixedUpdate()
    {
        MovementLogicFixed();
    }

    private void MovementLogic()
    {
        moveDirection = Vector3.zero;

        if (Input.GetKey(Game.Config.MoveUp))
            moveDirection += Vector3.forward;

        if (Input.GetKey(Game.Config.MoveLeft))
            moveDirection += Vector3.left;

        if (Input.GetKey(Game.Config.MoveDown))
            moveDirection -= Vector3.forward;

        if (Input.GetKey(Game.Config.MoveRight))
            moveDirection += Vector3.right;

        moveDirection = moveDirection.normalized;

        IsRun = Input.GetKey(Game.Config.Run);
    }

    private void MovementLogicFixed()
    {
        if (IsMove && CanMove)
        {
            Vector3 resultMoveVector = new Vector3(CameraFoward.x, 0, CameraFoward.z).normalized;
            float alpha = Vector3.SignedAngle(Vector3.forward, moveDirection, Vector3.up);

            float originalX = resultMoveVector.x;
            float originalZ = resultMoveVector.z;

            resultMoveVector.x = originalX * Mathf.Cos(-alpha * Mathf.Deg2Rad) - originalZ * Mathf.Sin(-alpha * Mathf.Deg2Rad);
            resultMoveVector.z = originalX * Mathf.Sin(-alpha * Mathf.Deg2Rad) + originalZ * Mathf.Cos(-alpha * Mathf.Deg2Rad);

            if (groundHit.normal != Vector3.zero)
                resultMoveVector = Vector3.ProjectOnPlane(resultMoveVector, groundHit.normal).normalized;

            body.AddForce(resultMoveVector * moveSpeed * (IsRun ? runFactor : 1) * (!IsGround ? notGroundFactor : 1), ForceMode.Impulse);
        }
    }

    private void JumpLogic()
    {
        IsGround = Physics.SphereCast(groundPoint.position, jumpRayRadius, Vector3.down, out groundHit, jumpRayDistance, LayerMask.GetMask("Floor"), QueryTriggerInteraction.UseGlobal);

        if (CanJump && isAllowJump && Input.GetKeyDown(GameManager.Instance.Config.Jump))
        {
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isAllowJump = false;
        }
    }

    private IEnumerator RotationAnimation()
    {
        while (true)
        {
            if (IsMove && CanRotate)
            {
                float angle0 = Vector3.SignedAngle(transform.forward, moveDirection, Vector3.up);
                float angle1 = Vector3.SignedAngle(transform.forward, new Vector3(CameraFoward.x, 0, CameraFoward.z), Vector3.up);

                modelTrasform.rotation = Quaternion.Lerp(modelTrasform.rotation, Quaternion.Euler(0, angle0 + angle1, 0), rotateForce * Time.fixedDeltaTime);
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator CoyoteTime()
    {
        while (true)
        {
            yield return new WaitWhile(() => !IsGround);

            isAllowJump = true;

            yield return new WaitWhile(() => IsGround);
            yield return new WaitForSeconds(coyoteTime);

            isAllowJump = false;
        }
    }
}
