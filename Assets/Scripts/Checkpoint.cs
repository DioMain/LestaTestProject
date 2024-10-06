using UnityEngine;

public class Checkpoint : MonoBehaviourPlus
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Level.Checkpoint.CurrentCheckpoint != this)
        {
            Level.Checkpoint.SetCheckpoint(this);
        }
    }
}