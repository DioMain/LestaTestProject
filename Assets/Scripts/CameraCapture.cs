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

        transform.position = target.position + new Vector3(lookDirection.x * offset, lookDirection.y * offset, lookDirection.z * offset);

        transform.LookAt(target);
    }

    private IEnumerator CameraMoveCoroutine()
    {
        while (true)
        {


            yield return new WaitForFixedUpdate();
        }
    }
}
