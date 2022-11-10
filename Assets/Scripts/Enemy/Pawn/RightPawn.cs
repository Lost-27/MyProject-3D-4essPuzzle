using DG.Tweening;
using UnityEngine;

public class RightPawn : MonoBehaviour
{
    [SerializeField] private float duration;
    // [SerializeField] private GameObject rightPawn;
    
    private Sequence Seq;
    
    private bool snapping = false;


    private int i = 1;
    private void Start()
    {
        StartMove();
    }

    private void StartMove()
    {
        var posOne = transform.position + new Vector3(0,0,-5.05f);
        var posTwo = transform.position;
        
        i *= -1;
        if (i < 1)
        {
            Seq = DOTween.Sequence();
            Seq.Append(transform.DOMove(posOne, duration, snapping).SetEase(Ease.Linear));
            Seq.Append(transform.DOMove(posTwo, duration, snapping).SetEase(Ease.Linear));
            Seq.SetEase(Ease.Linear);
            Seq.SetLoops(-1); 
        }
        else
        {
            Seq.Kill();
        }
        
    }
}
