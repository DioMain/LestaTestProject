using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBar : MonoBehaviour, IInitialize
{
    [SerializeField]
    private RectTransform bar;

    private Vector2 defaultSize;

    public void Initialize()
    {
        defaultSize = bar.sizeDelta;
    }

    public void SetValue(float value)
    {
        bar.sizeDelta = new Vector2(defaultSize.x * value, defaultSize.y);   
    }
}
