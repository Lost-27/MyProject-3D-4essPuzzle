using System;
using DG.Tweening;
using UnityEngine;

public class BridgeMove : MonoBehaviour
{
    [SerializeField] private float duration;
    // [SerializeField] private GameObject Bridge;
    //
    // private void Start()
    // {
    //     BridgeRoatate();
    // }
    //
    // private void BridgeRoatate()
    // {
    //     Bridge.transform.DORotate(new Vector3(0, -360, 0), duration, RotateMode.LocalAxisAdd).SetLoops(-1)
    //         .SetEase(Ease.Linear);
    // }
    
    Rigidbody m_Rigidbody;
    Vector3 m_EulerAngleVelocity;
    
    void Start()
    {
        m_EulerAngleVelocity = new Vector3(0, duration, 0);
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
    }
}
