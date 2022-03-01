using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PopUp
{
    public class PopupPresenter : MonoBehaviour
    {
        public string Message;
        
        [SerializeField] private int _alphaZero = 0;
        [SerializeField] private int _alphaFull = 1;
        [SerializeField] private int _duration = 1;
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        
        private CanvasGroup _canvasGroup;
        private PopupLogic _logic;
        private List<CanvasGroup> _uiGroups = new List<CanvasGroup>();

        private void Awake()
        {
            _logic = gameObject.GetComponent<PopupLogic>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = _alphaZero;

            SetInteractable(false);
            SetMessage(Message);
        }
        public void ActivateSelf()
        {
            _canvasGroup.DOFade(_alphaFull, _duration).SetEase(Ease.Linear);
            DeactivateUI();
            SetInteractable(true);
        }

        public void SetMessage(string text)
        {
            _textMeshPro.text = text;
        }

        public void ButtonHandler()
        {
            var sequence = DOTween.Sequence();

            sequence.Append(_canvasGroup.DOFade(_alphaZero, _duration)
                .SetEase(Ease.Linear));

            sequence.OnComplete(() => {
                ActivateUI();
                SetInteractable(false);
                _logic.NotificateListeners();
            });
        }

        private void SetInteractable(bool isIntractable)
        {
            _canvasGroup.interactable = isIntractable;
            _canvasGroup.blocksRaycasts = isIntractable;
        }

        private void DeactivateUI()
        {
            foreach (Transform go in transform.parent) {
                var group = go.gameObject.GetComponent<CanvasGroup>();
                if (group != null && group.interactable) {
                    group.interactable = false;
                    group.blocksRaycasts = false;
                    _uiGroups.Add(group);
                }
            }
        }
        
        private void ActivateUI()
        {
            foreach (var group in _uiGroups) {
                group.interactable = true;
                group.blocksRaycasts = true;
            }
            _uiGroups = new List<CanvasGroup>();
        }
    }
}
