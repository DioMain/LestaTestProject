using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float runFactor;

    [SerializeField]
    private float jumpForce;

    private Vector3 moveVector = Vector3.zero;

    private void Update()
    {
        moveVector = Vector3.zero;

        if (Input.GetKey(GameManager.Instance.Config.MoveUp))
            moveVector += Vector3.forward;

        if (Input.GetKey(GameManager.Instance.Config.MoveLeft))
            moveVector += Vector3.left;

        if (Input.GetKey(GameManager.Instance.Config.MoveDown))
            moveVector -= Vector3.forward;

        if (Input.GetKey(GameManager.Instance.Config.MoveRight))
            moveVector += Vector3.right;

        moveVector = moveVector.normalized;
    }

    private void FixedUpdate()
    {
        transform.position += moveVector * moveSpeed * Time.fixedDeltaTime;
    }
}
