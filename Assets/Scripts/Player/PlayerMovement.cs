using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPlus
{
    [Header("Ссылки")]
    [SerializeField]
    private Transform modelTrasform;
    [SerializeField] 
    private Transform groundPoint;
    [SerializeField]
    private Rigidbody body;

    [Header("Настройки")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float runFactor;
    [SerializeField]
    private float notGroundFactor = 0.2f;

    [Space]

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpRayDistance = 0.3f;

    [Space]

    [SerializeField]
    private float rotateForce = 1.5f;

    [Space]

    public bool CanMove = true;
    public bool CanJump = true;

    public bool IsGround { get; private set; } = false;
    public bool IsRun { get; private set; } = false;

    public bool IsMove => moveDirection != Vector3.zero;

    private Vector3 moveDirection = Vector3.zero;

    private Vector3 CameraFoward => LevelManager.Instance.CameraCapture.transform.forward;

    public override void Initialize()
    {
        StartCoroutine(RotationAnimation());
    }

    private void Update()
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

        IsGround = Physics.Raycast(groundPoint.position, Vector3.down, jumpRayDistance, LayerMask.GetMask("Floor"));

        if (IsGround && CanJump && Input.GetKeyDown(GameManager.Instance.Config.Jump))
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        IsRun = Input.GetKey(Game.Config.Run);
    }

    private void FixedUpdate()
    {
        MovementLogic();
    }

    private void MovementLogic()
    {
        if (IsMove && CanMove)
        {
            Vector3 resultMoveVector = new Vector3(CameraFoward.x, 0, CameraFoward.z).normalized;
            float alpha = Vector3.SignedAngle(Vector3.forward, moveDirection, Vector3.up);

            float originalX = resultMoveVector.x;
            float originalZ = resultMoveVector.z;

            resultMoveVector.x = originalX * Mathf.Cos(-alpha * Mathf.Deg2Rad) - originalZ * Mathf.Sin(-alpha * Mathf.Deg2Rad);
            resultMoveVector.z = originalX * Mathf.Sin(-alpha * Mathf.Deg2Rad) + originalZ * Mathf.Cos(-alpha * Mathf.Deg2Rad);

            body.AddForce(resultMoveVector * moveSpeed * (IsRun ? runFactor : 1) * (!IsGround ? notGroundFactor : 1), ForceMode.Impulse);
        }
    }

    private IEnumerator RotationAnimation()
    {
        while (true)
        {
            if (IsMove)
            {
                float angle0 = Vector3.SignedAngle(transform.forward, moveDirection, Vector3.up);
                float angle1 = Vector3.SignedAngle(transform.forward, new Vector3(CameraFoward.x, 0, CameraFoward.z), Vector3.up);

                modelTrasform.rotation = Quaternion.Lerp(modelTrasform.rotation, Quaternion.Euler(0, angle0 + angle1, 0), rotateForce * Time.fixedDeltaTime);
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
