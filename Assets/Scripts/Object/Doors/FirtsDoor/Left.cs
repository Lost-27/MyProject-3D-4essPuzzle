using UnityEngine;
using DG.Tweening;

namespace Object.FirtsDoor
{
    public class Left : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private GameObject leftDoor;
    

        public void LeftOpen()
        {
            leftDoor.transform.DOLocalRotate(new Vector3(transform.localEulerAngles.x -90, transform.localEulerAngles.y ,
                transform.localEulerAngles.z), duration, RotateMode.Fast);
        }
    }
}