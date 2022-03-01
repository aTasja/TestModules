using System;
using System.Collections.Generic;
using PopUp;
using UnityEngine;
using UnityEngine.Playables;

namespace Tests
{
    public class PopupFactoryTest: MonoBehaviour
    {
        public PopupSystemFactory popupSystemFactory;

        private void Start()
        {
            var popupTypesByPriority = new List<(PopupType type, int priority)>
            {
                (PopupType.Notification, 5),
                (PopupType.Notification, 4),
                (PopupType.Simple, 1),
                (PopupType.Notification, 3),
                (PopupType.Simple, 2)
            };
            
            popupSystemFactory.CreatePopupShower(popupTypesByPriority);
            popupSystemFactory.CreateTopPriorityPopupOf(PopupType.Notification);
        }

        public void ButtonHandler()
        {
            Debug.Log("Button Pressed");
        }
    }
}
