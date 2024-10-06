﻿using UnityEngine;

public class Checkpoint : MonoBehaviourPlus
{
    [SerializeField]
    private Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.CompareTag("Player"))
        {
            Level.Checkpoint.SetCheckpoint(this);
        }
    }
}