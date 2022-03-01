using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PopUp
{
    public class PopupSystemFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _simplePopup;
        [SerializeField] private GameObject _notificationPopup;

        private List<GameObject> _popupShower = new List<GameObject>() ;

        public void CreateTopPriorityPopupOf(PopupType type)
        {
            var singlePopup = InstantiateAndSetPrefab(type, 0, "Top Priority Popup");
            _popupShower.Add(singlePopup);
            SortPopupShower();
            ShowTopPriorityPopup();
        }

        public void CreatePopupShower(List<(PopupType type, int priority)> typesAndPriorities)
        {
            foreach (var typesAndPriority in typesAndPriorities) {
                var message = "Priority = " + typesAndPriority.priority + "\n" + typesAndPriority.type;
                var popup = InstantiateAndSetPrefab(typesAndPriority.type, typesAndPriority.priority, message);
                _popupShower.Add(popup);
            }
            SortPopupShower();
            ShowTopPriorityPopup();
        }

        private GameObject GetPrefabBy(PopupType type) 
            => type == PopupType.Simple ? _simplePopup : _notificationPopup;

        private GameObject InstantiateAndSetPrefab(PopupType type, int priority, string message)
        {
            var popup = Instantiate(GetPrefabBy(type), transform);
            popup.GetComponent<PopupLogic>().SetUpPopup(priority, message);
            return popup;
        }

        private void SortPopupShower()
            => _popupShower = _popupShower.OrderBy(popup => popup.GetComponent<PopupLogic>().Priority).ToList();

        private void ShowTopPriorityPopup()
        {
            if (_popupShower.Count == 0) {
                return;
            }
            var logic = _popupShower[0].GetComponent<PopupLogic>();
            logic.AddListener(ShowTopPriorityPopup);
            logic.ShowPopup();
            RemoveShowedFromList();
        }

        private void RemoveShowedFromList() => _popupShower.RemoveAt(0);
        
    }
}
