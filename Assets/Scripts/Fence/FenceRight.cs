using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FenceRight : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private GameObject fenceRight;
    
    
    public void FenceRightOpen()
    {
        fenceRight.transform.DOLocalRotate(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y,
            transform.localEulerAngles.z + 90), duration, RotateMode.Fast);
    }
}
