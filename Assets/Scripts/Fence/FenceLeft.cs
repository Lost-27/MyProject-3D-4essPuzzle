using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FenceLeft : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private GameObject fenceLeft;
    

    public void FenceLeftOpen()
    {
        fenceLeft.transform.DOLocalRotate(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - 90,
            transform.localEulerAngles.z), duration, RotateMode.Fast);
    }
}
