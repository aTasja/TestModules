using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Message
{
    public class MessageTextFactory : MonoBehaviour
    {
        [SerializeField] public Vector3 Offset;
        [SerializeField] public float Duration;

        [SerializeField] internal Ease MoveEase = Ease.Linear;
        [SerializeField] internal Ease FadeToFullEase = Ease.Linear;
        [SerializeField] internal Ease FadeToZeroEase = Ease.Linear;
        [SerializeField] internal TextMeshPro _messageContainer;
        
        private float _alphaZero = 0f;
        private float _alphaFull = 1f;

        public void ShowText(string message, Vector3 at)
        {
            transform.position = at - Offset;
            _messageContainer.text = message;
    
            _messageContainer.DOFade(_alphaFull, Duration/2)
                .SetEase(FadeToFullEase);
            
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(at + Offset, Duration/2)
                .SetEase(MoveEase));
            
            sequence.OnComplete(() => {
                _messageContainer.DOFade(_alphaZero, Duration / 2)
                    .SetEase(FadeToZeroEase);
            });
        }
    }
}
