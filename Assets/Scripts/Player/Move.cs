using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
     public DynamicJoystick stick;

     [SerializeField] private float _speedRotate;
     [SerializeField] private float _speedTranslate;
     [SerializeField] private Rigidbody rb;
     
     private void Update()
     {
          var vertical = stick.Vertical * _speedTranslate * Time.deltaTime;
          transform.Translate(0, 0, vertical);
          
          Vector3 Rotate = new Vector3(0, stick.Horizontal, 0);
          Quaternion deltaRotation = Quaternion.Euler(Rotate * Time.deltaTime * _speedRotate);
          rb.MoveRotation(rb.rotation * deltaRotation);
          
     }

}

