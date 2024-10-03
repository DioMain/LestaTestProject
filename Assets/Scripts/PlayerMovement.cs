using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IInitialize
{
    [SerializeField]
    private Transform modelTrasform;
    [SerializeField] 
    private Transform floorPoint;

    [SerializeField]
    private Rigidbody body;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float runFactor;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpRayDistance = 0.3f;

    [SerializeField]
    private float rotateForce = 1.5f;

    private bool isGround = false;
    public bool IsGround => isGround;

    private bool isRun = false;
    public bool IsRun => isRun;

    public bool IsMove => moveDirection != Vector3.zero;

    private Vector3 moveDirection = Vector3.zero;

    private Vector3 CameraFoward => LevelManager.Instance.CameraCapture.transform.forward;

    public void Initialize()
    {
        StartCoroutine(RotationAnimation());
    }

    private void Update()
    {
        moveDirection = Vector3.zero;

        if (Input.GetKey(GameManager.Instance.Config.MoveUp))
            moveDirection += Vector3.forward;

        if (Input.GetKey(GameManager.Instance.Config.MoveLeft))
            moveDirection += Vector3.left;

        if (Input.GetKey(GameManager.Instance.Config.MoveDown))
            moveDirection -= Vector3.forward;

        if (Input.GetKey(GameManager.Instance.Config.MoveRight))
            moveDirection += Vector3.right;

        moveDirection = moveDirection.normalized;

        isGround = Physics.Raycast(floorPoint.position, Vector3.down, jumpRayDistance, LayerMask.GetMask("Floor"));

        if (isGround && Input.GetKeyDown(GameManager.Instance.Config.Jump))
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        isRun = Input.GetKey(GameManager.Instance.Config.Run);
    }

    private void FixedUpdate()
    {
        if (IsMove)
        {
            Vector3 resultMoveVector = new Vector3(CameraFoward.x, 0, CameraFoward.z).normalized;
            float alpha = Vector3.SignedAngle(Vector3.forward, moveDirection, Vector3.up);

            float originalX = resultMoveVector.x;
            float originalZ = resultMoveVector.z;

            resultMoveVector.x = originalX * Mathf.Cos(-alpha * Mathf.Deg2Rad) - originalZ * Mathf.Sin(-alpha * Mathf.Deg2Rad);
            resultMoveVector.z = originalX * Mathf.Sin(-alpha * Mathf.Deg2Rad) + originalZ * Mathf.Cos(-alpha * Mathf.Deg2Rad);

            transform.position += resultMoveVector * moveSpeed * (IsRun ? runFactor : 1) * Time.fixedDeltaTime;
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
