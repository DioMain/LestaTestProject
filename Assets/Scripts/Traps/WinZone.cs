using UnityEngine;

public class WinZone : MonoBehaviourPlus
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.CompareTag("Player"))
        {
            Game.Win.Win();
        }
    }
}