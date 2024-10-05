using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField]
    private float force;
    [SerializeField]
    private float periodicTime;
    [SerializeField] 
    private Vector3 direction;
    [SerializeField]
    private Vector3[] directions;
    [SerializeField]
    private bool isChangeDirection = false;

    [SerializeField]
    private ParticleSystem particle;

    private List<Rigidbody> bodys = new();

    private void Start()
    {
        if (isChangeDirection)
        {
            StartCoroutine(DirectionChangeCoroutine());
        }
        else
        {
            ApplyDireciton(direction);
        }
    }

    private void FixedUpdate()
    {
        foreach (var body in bodys)
            body.AddForce(direction * force, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!bodys.Contains(other.attachedRigidbody))
            bodys.Add(other.attachedRigidbody);
    }

    private void OnTriggerExit(Collider other)
    {
        if (bodys.Contains(other.attachedRigidbody))
            bodys.Remove(other.attachedRigidbody);
    }

    private void ApplyDireciton(Vector3 direction)
    {
        particle.transform.rotation = Quaternion.LookRotation(direction);
        this.direction = direction;
    }

    private IEnumerator DirectionChangeCoroutine()
    {
        int curIndex = 0;

        while (true)
        {
            ApplyDireciton(directions[curIndex]);

            curIndex = curIndex >= directions.Length - 1 ? 0 : curIndex + 1;

            yield return new WaitForSeconds(periodicTime);
        }
    }
}
