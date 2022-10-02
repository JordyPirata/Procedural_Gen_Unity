using UnityEngine;

namespace MapGeneration
{
    public class MapDisplay : MonoBehaviour
    {
        public Renderer textureRender;
        public MeshFilter meshFilter;
        public MeshRenderer meshRenderer;

        public void DrawTexture(Texture2D texture)
        {
            textureRender.sharedMaterial.mainTexture = texture;
            textureRender.transform.localScale = new Vector3(texture.width / 5, 1, texture.height / 5);
        }
        public void DrawMesh(MeshData meshData, Texture2D texture)
        {
            meshFilter.sharedMesh = meshData.CreateMesh();
            meshRenderer.sharedMaterial.mainTexture = texture; 
        }
    }
}