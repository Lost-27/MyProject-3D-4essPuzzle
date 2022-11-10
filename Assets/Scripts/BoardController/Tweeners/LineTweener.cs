using System;
using DG.Tweening;
using UnityEngine;

public class LineTweener : MonoBehaviour, IObjectTweener
{
    [SerializeField] private float movementSpeed;

    public void MoveTo(Transform transform, Vector3 targetPosition, Action competedCallback = null)
    {
        float distance = Vector3.Distance(targetPosition, transform.localPosition);
        transform.DOLocalMove(targetPosition, distance / movementSpeed).OnComplete(() => competedCallback?.Invoke());
    }
}