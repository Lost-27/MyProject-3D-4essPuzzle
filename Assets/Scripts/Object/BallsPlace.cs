using DG.Tweening;
using UnityEngine;

public class BallsPlace : MonoBehaviour
{
    [SerializeField] private GameObject placeBalls;
    [SerializeField] private float duration;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            GoBalls();
        }
    }

    private void GoBalls()
    {
        placeBalls.transform.DOLocalRotate(new Vector3(transform.localEulerAngles.x + 35, transform.localEulerAngles.y,
            transform.localEulerAngles.z), duration, RotateMode.Fast).OnComplete(() =>
        {
            placeBalls.transform.DOLocalRotate(new Vector3(transform.localEulerAngles.x - 35, transform.localEulerAngles.y,
                transform.localEulerAngles.z), duration, RotateMode.Fast);
        });
    }
}
