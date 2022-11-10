using System;
using UnityEngine;

public interface IObjectTweener //**
{
    void MoveTo(Transform transform, Vector3 targetPosition, Action competedCallback);
}