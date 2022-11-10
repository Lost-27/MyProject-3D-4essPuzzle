using DG.Tweening;
using UnityEngine;

public class EnemyPointMovement : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Transform _point;
    

    private Sequence Seq;
    
    private bool snapping = false;


    private int i = 1;
    private void Start()
    {
        StartMove();
    }

    private void StartMove()
    {
        var localPosition = transform.localPosition;
        var posOne = localPosition + _point.localPosition;
        var posTwo = localPosition;
        
        i *= -1;
        if (i < 1)
        {
            Seq = DOTween.Sequence();
            Seq.Append(transform.DOLocalMove(posOne, _duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOLocalMove(posTwo, _duration, snapping).SetEase(Ease.Linear));
            Seq.SetEase(Ease.Linear);
            Seq.SetLoops(-1); 
        }
        else
        {
            Seq.Kill();
        }
        
    }
}
