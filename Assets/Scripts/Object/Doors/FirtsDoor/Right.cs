using UnityEngine;
using DG.Tweening;

namespace Object.FirtsDoor
{
    public class Right : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private GameObject rightDoor;
    

        public void RightOpen()
        {
            rightDoor.transform.DOLocalRotate(new Vector3(transform.localEulerAngles.x +90, transform.localEulerAngles.y ,
                transform.localEulerAngles.z), duration, RotateMode.Fast);
        }
    }
}