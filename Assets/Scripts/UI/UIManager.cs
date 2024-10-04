using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IInitialize
{
    [SerializeField]
    private TextMeshProUGUI velocityOutput;
    [SerializeField]
    private Rigidbody playerBody;

    public void Initialize()
    {
        
    }

    private void Update()
    {
        velocityOutput.text = $"X: {playerBody.velocity.x}, Y: {playerBody.velocity.y}, Z: {playerBody.velocity.z}";
    }
}
