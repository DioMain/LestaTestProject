using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviourPlus
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField] 
    private Transform content;

    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float shotDelay;
    [SerializeField]
    private float bulletDestroyDelay;
    [SerializeField]
    private float bulletForce;

    private bool playerIsClose = false;
    private bool rotatedToPlayer = false;

    private Vector3 toPlayerDirection;
    private Vector3 actualDirection;

    private void Start()
    {
        StartCoroutine(RotateCoroutine());
        StartCoroutine(ShotCoroutine());
    }

    private void Update()
    {
        toPlayerDirection = (Level.Player.transform.position - transform.position).normalized;
        toPlayerDirection.y = 0;

        Debug.DrawRay(transform.position, toPlayerDirection * 6, Color.green);
        Debug.DrawRay(transform.position, content.rotation * Vector3.forward * 6, Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.CompareTag("Player"))
            playerIsClose = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody.CompareTag("Player"))
            playerIsClose = false;
    }

    private IEnumerator RotateCoroutine()
    {
        while (true)
        {
            float angle = Vector3.SignedAngle(Vector3.forward, toPlayerDirection, Vector3.up);

            content.rotation = Quaternion.Lerp(content.rotation, Quaternion.Euler(0, angle, 0), rotateSpeed * Time.deltaTime);

            actualDirection = content.rotation * Vector3.forward;

            rotatedToPlayer = Mathf.Abs(Vector3.Dot(toPlayerDirection, actualDirection)) >= 0.95;

            yield return new WaitWhile(() => !playerIsClose);
            yield return null;
        }
    }

    private IEnumerator ShotCoroutine()
    {
        while (true)
        {
            yield return new WaitWhile(() => !playerIsClose || !rotatedToPlayer);

            yield return new WaitForSeconds(shotDelay);

            GameObject bullet = Instantiate(bulletPrefab, transform.position + (actualDirection * 4), Quaternion.identity);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            rb.AddForce(actualDirection * bulletForce, ForceMode.Impulse);

            Destroy(bullet, bulletDestroyDelay);
        }
    }
}
