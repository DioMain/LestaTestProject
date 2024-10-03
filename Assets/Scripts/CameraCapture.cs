using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCapture : MonoBehaviour, IInitialize
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float offset;
    [SerializeField]
    private float height;

    [SerializeField, Range(0, 360)]
    private float alphaAngle;
    [SerializeField, Range(-89f, 89f)]
    private float betaAngle;

    [SerializeField]
    private float sensitivity = 1f;

    public Vector3 LookDireciton {  get; private set; } = Vector3.zero;

    public void Initialize()
    {
        StartCoroutine(CameraMoveCoroutine());
    }


    private void Update()
    {
        Vector3 lookDirection = transform.forward;

        lookDirection.x = Mathf.Cos(alphaAngle * Mathf.Deg2Rad) * Mathf.Cos(betaAngle * Mathf.Deg2Rad);
        lookDirection.z =  Mathf.Sin(alphaAngle * Mathf.Deg2Rad) * Mathf.Cos(betaAngle * Mathf.Deg2Rad);
        lookDirection.y =  Mathf.Sin(betaAngle * Mathf.Deg2Rad);

        transform.position = target.position + new Vector3(lookDirection.x * offset, lookDirection.y * offset, lookDirection.z * offset) + new Vector3(0, height, 0);

        LookDireciton = lookDirection;

        transform.LookAt(target);

        alphaAngle -= Input.GetAxis("Mouse X") * sensitivity;
        betaAngle -= Input.GetAxis("Mouse Y") * sensitivity;

        betaAngle = Mathf.Clamp(betaAngle, -89f, 89f);
    }

    private IEnumerator CameraMoveCoroutine()
    {
        while (true)
        {


            yield return new WaitForFixedUpdate();
        }
    }
}
