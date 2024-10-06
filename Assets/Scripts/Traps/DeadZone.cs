using UnityEngine;

public class DeadZone : MonoBehaviourPlus
{
    [SerializeField]
    private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.CompareTag("Player"))
        {
            Game.Life.Damage(damage);

            Level.Checkpoint.SpawnPlayer();
        }
    }
}
