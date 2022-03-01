using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WorldEnities
{
    public class BasePresenter : MonoBehaviour
    {
        public BaseType TypeOf;
        public AssetReference PrefabAddressable;

        [SerializeField]private Vector2 PositionXY { get; set; }
        public void SetOn(Vector2 position)
        {
            PositionXY = new Vector2(position.x, position.y);
            gameObject.transform.position = new Vector3(position.x, position.y, 0);
        }
		
    }
}
