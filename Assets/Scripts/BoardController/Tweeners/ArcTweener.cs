using System;
using DG.Tweening;
using UnityEngine;

public class ArcTweener : MonoBehaviour, IObjectTweener
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight;

    public void MoveTo(Transform transform, Vector3 targetPosition, Action competedCallback = null)
    {
        float distance = Vector3.Distance(targetPosition, transform.localPosition);
        transform.DOLocalJump(targetPosition, jumpHeight, 1, distance / movementSpeed);
    }
}