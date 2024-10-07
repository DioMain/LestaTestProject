using System.Collections;
using UnityEngine;

public class RotatedObject : MonoBehaviour
{
    [SerializeField]
    private Rigidbody content;

    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private bool constantRotation = false;

    [SerializeField]
    private Vector3[] angles;
    
    private int current = 0;

    private void Start()
    {
        content.transform.rotation = Quaternion.Euler(angles[0]);

        StartCoroutine(RotateCoroutine());

        if (!constantRotation)
            StartCoroutine(ChangeRotationCoroutine());
    }

    IEnumerator RotateCoroutine()
    {
        while (true)
        {
            if (constantRotation)
                content.MoveRotation(Quaternion.RotateTowards(content.rotation, content.rotation * Quaternion.Euler(angles[0]), speed * Time.fixedDeltaTime));
            else
                content.MoveRotation(Quaternion.RotateTowards(content.rotation, Quaternion.Euler(angles[current]), speed * Time.fixedDeltaTime));



            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator ChangeRotationCoroutine()
    {
        while (true)
        {
            current = (current + 1) % angles.Length;

            yield return new WaitForSeconds((angles[current] - angles[MathfPlus.Mod(current - 1, angles.Length)]).magnitude / speed);
        }
    }
}