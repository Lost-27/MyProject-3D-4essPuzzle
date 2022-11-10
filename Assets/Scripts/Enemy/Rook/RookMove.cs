using DG.Tweening;
using UnityEngine;

public class RookMove : MonoBehaviour
{
    [SerializeField] private float duration;

    private Sequence Seq;

    private bool snapping = false;
    
    private Vector3 posOne = new Vector3(-327.5f, 138f, 182.5f);
    private Vector3 posTwo = new Vector3(-327.5f, 138f, 177.5f);
    private Vector3 posThree = new Vector3(-331.5f, 138f, 177.5f);
    private Vector3 posFour = new Vector3(-331.5f, 138f, 182.5f);
    private Vector3 posFive = new Vector3(-335.5f, 138f, 182.5f);
    private Vector3 posSix = new Vector3(-335.5f, 138f, 177.5f);
    private Vector3 posSeven = new Vector3(-339.5f, 138f, 177.5f);
    private Vector3 posEight = new Vector3(-339.5f, 138f, 182.5f);

    private int i = 1;

    private void Start()
    {
        MoveBishopTwo();
    }

    private void MoveBishopTwo()
    {
        i *= -1;
        if (i < 1)
        {
            Seq = DOTween.Sequence();
            
            Seq.Append(transform.DOMove(posOne, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOMove(posTwo, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOMove(posThree, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOMove(posFour, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOMove(posFive, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOMove(posSix, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOMove(posSeven, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOMove(posEight, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOMove(posSeven, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOMove(posSix, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOMove(posFive, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOMove(posFour, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOMove(posThree, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOMove(posTwo, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOMove(posOne, duration, snapping).SetEase(Ease.Linear));

            Seq.SetEase(Ease.Linear);
            Seq.SetLoops(-1);
        }
        else
        {
            Seq.Kill();
        }
    }
}
