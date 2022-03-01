using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WorldEnities

{
    public class BaseWorldFactory : MonoBehaviour
    {
        [SerializeField] private Tilemap _baseTilemap; 
        private List<int[]> _baseLayerStructureSpecification; // int[x,y,BaseType]
        private Dictionary<BaseType, BasePresenter> _structureRenderDictionary;
        
        public void Initiate(Tilemap baseTilemap, 
                                    List<int[]> baseLayerStructureSpecification, 
                                    Dictionary<BaseType, BasePresenter> structureRenderDictionary)
        {
            _baseTilemap = baseTilemap;
            _baseLayerStructureSpecification = baseLayerStructureSpecification;
            _structureRenderDictionary = structureRenderDictionary;
        }
        
        public void CreateWorld()
        {
            foreach(var buildStructure in _baseLayerStructureSpecification) {
                var buildPosition = new Vector2Int(buildStructure[0], buildStructure[1]);
                var buildType = (BaseType)buildStructure[2];
                CreateSpecifiedStructureByType(buildType, buildPosition);
            }
        }

        private void CreateSpecifiedStructureByType(BaseType type, Vector2Int position)
        {
            var structurePrefab = _structureRenderDictionary[type].PrefabAddressable;

            structurePrefab.InstantiateAsync().Completed += (go) => {
                var structureInstantiated = go.Result.transform;
                structureInstantiated.SetParent(_baseTilemap.transform);
                structureInstantiated.GetComponent<BasePresenter>().
                    SetOn(position);
            };
        }
    }
}
