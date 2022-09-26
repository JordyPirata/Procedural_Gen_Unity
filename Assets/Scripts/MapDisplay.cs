using UnityEngine;

namespace MapGeneration
{
    public class MapDisplay : MonoBehaviour
    {
        public Renderer textureRender;

        public void DrawNoiseMap(float[,] noise)
        {
            int width = noise.GetLength(0);
            int heigth = noise.GetLength(1);

            Texture2D texture = new Texture2D(width, heigth);
            Color[] colorMap = new Color[width * heigth];
            for (int y = 0; y < heigth; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    colorMap[y * heigth + x] = Color.Lerp(Color.black, Color.white, noise[x, y]);
                }
            }
            texture.SetPixels(colorMap);
            texture.Apply();

            textureRender.sharedMaterial.mainTexture = texture;
            textureRender.transform.localScale = new Vector3(width,1, heigth);
        }
    }
}
