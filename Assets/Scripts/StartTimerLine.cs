using UnityEngine;

public class StartTimerLine : MonoBehaviourPlus
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.CompareTag("Player"))
            Level.Timer.StartWatch();
    }
}