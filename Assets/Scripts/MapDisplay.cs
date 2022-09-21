using UnityEngine;

namespace MapGeneration
{
    public class MapDisplay : MonoBehaviour
    {
        public Renderer textureRender;

        public void DrawNoiseMap(float[,] noise)
        {
            int width = noiseMap.GetLength(0);
            int heigth = noiseMap.GetLength(1);
        }
    }
}
