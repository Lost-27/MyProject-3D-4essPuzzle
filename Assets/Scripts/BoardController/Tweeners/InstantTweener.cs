using System;
using UnityEngine;

public class InstantTweener : MonoBehaviour, IObjectTweener
{
    public void MoveTo(Transform transform, Vector3 targetPosition, Action competedCallback = null)
    {
        transform.position = targetPosition;
    }
}