using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using WorldEnities;
using Random = UnityEngine.Random;

namespace Tests
{
    public class BaseWorldFactoryTest : MonoBehaviour
    {
        public Tilemap InitialMap;
        public Tilemap BaseMap;
        public BasePresenter BasePresenterResidential;
        
        private List<int[]> _baseLayerStructureSpecification; // int[x,y,BaseType]
        private Dictionary<BaseType, BasePresenter> _structureRenderDictionary;
        private int _testBuildingsQuantity = 3;

        // Start is called before the first frame update
        void Start()
        {
            _baseLayerStructureSpecification = new List<int[]>();
            FillList(_baseLayerStructureSpecification);

            _structureRenderDictionary = new Dictionary<BaseType, BasePresenter>();
            _structureRenderDictionary.Add(BaseType.Residential, BasePresenterResidential);

            var factory = GetComponent<BaseWorldFactory>();
            factory.Initiate(BaseMap, _baseLayerStructureSpecification, _structureRenderDictionary);
            factory.CreateWorld();
        }

        private void FillList(List<int[]> baseList)
        {
            for (var i = 0; i < _testBuildingsQuantity; i++) {
                Vector3Int pos = GetRandomPosition();
                baseList.Add(new int[] {pos.x, pos.y, (int) BaseType.Residential});
            }
        }
        private Vector3Int GetRandomPosition()
        {
            var availablePositions = GetAvailablePositions(InitialMap);
            return availablePositions[Random.Range(0, availablePositions.Count)];
        }

        private List<Vector3Int> GetAvailablePositions(Tilemap map)
        {
            var result = new List<Vector3Int>();

            foreach (var position in map.cellBounds.allPositionsWithin) {
                if (map.HasTile(position)) {
                    var worldPos = map.CellToWorld(position);
                    result.Add(new Vector3Int((int) worldPos.x, (int) worldPos.y, 0));
                }
            }
            return result;
        }
    }
}
