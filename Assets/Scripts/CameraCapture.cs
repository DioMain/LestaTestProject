using UnityEngine;

public class CameraCapture : MonoBehaviour
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

    public bool CanCapture = true;

    public Vector3 LookDireciton {  get; private set; } = Vector3.zero;

    private void Update()
    {
        if (CanCapture)
        {
            Vector3 lookDirection = transform.forward;

            lookDirection.x = Mathf.Cos(alphaAngle * Mathf.Deg2Rad) * Mathf.Cos(betaAngle * Mathf.Deg2Rad);
            lookDirection.z = Mathf.Sin(alphaAngle * Mathf.Deg2Rad) * Mathf.Cos(betaAngle * Mathf.Deg2Rad);
            lookDirection.y = Mathf.Sin(betaAngle * Mathf.Deg2Rad);

            LookDireciton = lookDirection;

            transform.position = target.position + new Vector3(LookDireciton.x * offset, LookDireciton.y * offset, LookDireciton.z * offset) + new Vector3(0, height, 0);
            transform.LookAt(target);

            alphaAngle -= Input.GetAxis("Mouse X") * sensitivity;
            betaAngle -= Input.GetAxis("Mouse Y") * sensitivity;

            betaAngle = Mathf.Clamp(betaAngle, -89f, 89f);
        }
    }
}
