using UnityEngine;


namespace MapGeneration
{
    public class LevelGeneration : MonoBehaviour
    {
        [SerializeField]
        private int mapWidthInTitles, mapDepthInTiles;
        [SerializeField]
        private GameObject tilePrefab;

        private void Start()
        {
            GenerateMap();
        }

        void GenerateMap()
        {
            Vector3 titleSize = tilePrefab.GetComponent<MeshRenderer>().bounds.size;
            int tileWidth = (int)titleSize.x;
            int tileDepth = (int)titleSize.z;

        }
    }
}
