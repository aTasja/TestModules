using System;
using System.Collections.Generic;
using UnityEngine;

namespace PopUp
{
    public class PopupLogic : MonoBehaviour
    {
        public PopupType Type;
        public int Priority;

        private PopupPresenter _presenter;
        private List<Action> _listeners = new List<Action>();

        private void Awake()
        {
            _presenter = gameObject.GetComponent<PopupPresenter>();
        }

        public void SetUpPopup(int priority, string message)
        {
            Priority = priority;
            _presenter.SetMessage(message);
        }
        
        public void ShowPopup()
        {
            _presenter.ActivateSelf();
        }

        public void AddListener(Action listener)
        {
            if(Priority == 0) return;
            _listeners.Add(listener);
        }

        public void NotificateListeners()
        {
            if (_listeners.Count > 0) {
                foreach (var listener in _listeners) {
                    listener();
                }
            }
            _listeners = new List<Action>();
        }
    }
}
