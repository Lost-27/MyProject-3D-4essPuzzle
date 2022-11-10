using System;
using DG.Tweening;
using UnityEngine;

namespace Fence
{
    public class Fence : MonoBehaviour
    {
        private FenceLeft _fenceLeft;
        private FenceRight _fenceRight;
        [SerializeField] private GameObject Left;
        [SerializeField] private GameObject Right;

        private void Start()
        {
            _fenceLeft = Left.GetComponent<FenceLeft>();
            _fenceRight = Right.GetComponent<FenceRight>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                _fenceLeft.FenceLeftOpen();
                _fenceRight.FenceRightOpen();
            }
        }
        
        
    }
}