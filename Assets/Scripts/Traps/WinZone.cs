﻿using UnityEngine;

public class WinZone : MonoBehaviourPlus
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.CompareTag("Player"))
        {
            Level.Timer.StopWatch();
            Game.Win.Win();
        }
    }
}