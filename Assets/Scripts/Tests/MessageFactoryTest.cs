using UnityEngine;
using Message;
using UnityEngine.UIElements;

namespace Tests
{
    public class MessageFactoryTest : MonoBehaviour
    {
        public string Text;
        public Vector3 TextPosition;
        public MessageTextFactory MessageTextFactory;
        
        public int Number;
        public Vector3 NumberPosition;
        public MessageNumberFactory MessageNumberFuctory;
        
        private void Start()
        {
            MessageTextFactory.GetComponent<MessageTextFactory>().ShowText(Text, TextPosition);
            MessageNumberFuctory.GetComponent<MessageNumberFactory>().ShowNumber(Number, NumberPosition);
        }
    }
}
