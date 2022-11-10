using System;
using DG.Tweening;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private GameObject elevator;

    private bool up = true;
    private bool down = false;
    private bool snapping = false;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (up)
            {
                UpElevator(); 
            }

            if (down)
            {
                DownElevator();  
            }
        }
    }

    private void UpElevator()
    { 
        var posTwo = elevator.transform.position + new Vector3(0, 44, 0);

      elevator.transform.DOMove(posTwo, duration, snapping).OnComplete(() =>
      { 
          up = false;
          down = true;
      });
      
    }
    
    private void DownElevator()
    {
        
        var posOne = elevator.transform.position - new Vector3(0,44,0);

        elevator.transform.DOMove(posOne, duration, snapping).OnComplete(() =>
        {
            down = false;
            up = true;
        });
        
    }
}
