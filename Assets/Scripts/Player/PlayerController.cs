using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Elevator>())
        {
            this.transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Elevator>())
        {
            this.transform.parent = null;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<RespawnOne>())
        {
            transform.position = new Vector3(0, 0, 2);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        if (other.gameObject.GetComponent<RespawnTwo>())
        {
            transform.position = new Vector3(-48, 0, 80);
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        
        if (other.gameObject.GetComponent<RespawnThree>())
        {
            transform.position = new Vector3(-114, 0, 80);
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        
        if (other.gameObject.GetComponent<RespawnFour>())
        {
            transform.position = new Vector3(-142, 44, 53);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        
        if (other.gameObject.GetComponent<RespawnFive>())
        {
            transform.position = new Vector3(-169, 44, 80);
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        
        if (other.gameObject.GetComponent<RespawnSex>())
        {
            transform.position = new Vector3(-142, 44, 113);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            
        }
        
        if (other.gameObject.GetComponent<RespawnSeven>())
        {
            transform.position = new Vector3(-190, 69, 180);
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        
        if (other.gameObject.GetComponent<RespawnEight>())
        {
            transform.position = new Vector3(-383, 138, 180);
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        
        if (other.gameObject.GetComponent<RespawnNine>())
        {
            transform.position = new Vector3(-216, 113, 150);
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        
        if (other.gameObject.GetComponent<RespawnTen>())
        {
            transform.position = new Vector3(-216, 113, 210);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        if (other.gameObject.GetComponent<RespawnEleven>())
        {
            transform.position = new Vector3(-383, 138, 180);
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        
        if (other.gameObject.GetComponent<RespawnTwelve>())
        {
            transform.position = new Vector3(-441, 138, 237);
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
    
}