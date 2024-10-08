using System.Collections;
using UnityEngine;

public class MovedObject : MonoBehaviour
{
    [SerializeField]
    private Rigidbody content;

    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private Transform[] movePoints;

    private int currentPointIndex = 0;

    private void Start()
    {
        content.position = movePoints[0].position;

        StartCoroutine(ChangePointCoroutine());
    }

    private void FixedUpdate()
    {
        content.MovePosition(content.position + (movePoints[currentPointIndex].position - content.position).normalized * speed * Time.fixedDeltaTime);
    }

    private IEnumerator ChangePointCoroutine()
    {
        while (true)
        {
            currentPointIndex = currentPointIndex >= movePoints.Length - 1 ? 0 : currentPointIndex + 1;

            yield return new WaitForSeconds((movePoints[currentPointIndex].position - content.position).magnitude / speed);
        }
    }
}
