using DG.Tweening;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private GameObject bishop;

    private Sequence Seq;
    
    private Vector3 posOne = new Vector3(10.65f, 1.565f, 16f);
    private Vector3 posTwo = new Vector3(9.8f, 1.565f, 14f);
    private Vector3 posThree = new Vector3(10.65f, 1.565f, 12f);
    private Vector3 posFour = new Vector3(9.8f, 1.565f, 10f);

    private bool snapping = false;
    
    
    private int i = 1;
    private void Start()
    {
        StartMove();
    }

    private void StartMove()
    {
        i *= -1;
        if (i < 1)
        {
            Seq = DOTween.Sequence();
            Seq.Append(bishop.transform.DOMove(posOne, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(bishop.transform.DOMove(posTwo, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(bishop.transform.DOMove(posThree, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(bishop.transform.DOMove(posFour, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(bishop.transform.DOMove(posThree, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(bishop.transform.DOMove(posTwo, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(bishop.transform.DOMove(posOne, duration, snapping).SetEase(Ease.Linear));
            Seq.SetEase(Ease.Linear);
            Seq.SetLoops(-1); 
        }
        else
        {
            Seq.Kill();
        }
        
    }
}
