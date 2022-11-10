using UnityEngine;
using DG.Tweening;

namespace ChessPuzzle.Game.Enemy.Bishop
{
    public class BishopMoveTwo : MonoBehaviour
    {
        [SerializeField] private float duration;

        private Sequence Seq;

        private bool snapping = false;
    
        private Vector3 posOne = new Vector3(-175f, 69f, 178f);
        private Vector3 posTwo = new Vector3(-170f, 69f, 183f);
        private Vector3 posThree = new Vector3(-166f, 69f, 178f);

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
}