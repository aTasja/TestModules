using UnityEngine;

namespace Message
{
    public class MessageNumberFactory : MessageTextFactory
    {

        [SerializeField] private Color _positiveNumberColor;
        [SerializeField] private Color _zeroNumberColor;
        [SerializeField] private Color _negativeNumberColor;
        
        public void ShowNumber(int number, Vector3 position)
        {
            PaintNumber(number);
            ShowText(number.ToString(), position);
        }
        
        private void PaintNumber(int number)
        {
            if (number == 0) 
            {
                SetColor(_zeroNumberColor);
            }
            else if (number>0) {
                SetColor(_positiveNumberColor);
            }
            else {
                SetColor(_negativeNumberColor);
            }
        }
    
        private void SetColor(Color color)
            => _messageContainer.color = color;
    }
}
