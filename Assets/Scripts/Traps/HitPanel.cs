using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPanel : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private MeshRenderer meshRenderer;

    private bool playerIsTouch = false;

    private Coroutine damageCoroutine = null;
    private bool isDamaging => damageCoroutine != null;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsTouch = true;

            if (!isDamaging)
                damageCoroutine = StartCoroutine(DamageCoroutine());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.rigidbody.tag == "Player")
            playerIsTouch = false;
    }

    private IEnumerator DamageCoroutine()
    {
        meshRenderer.material.color = new Color(0.75f, 0.5f, 0);

        yield return new WaitForSeconds(1f);

        meshRenderer.material.color = Color.red;

        if (playerIsTouch)
            GameManager.Instance.Life.Damage(damage);

        yield return new WaitForSeconds(0.15f);

        meshRenderer.material.color = new Color(0.75f, 0.7f, 0.7f);

        yield return new WaitForSeconds(5f);

        damageCoroutine = null;
    }
}
