﻿using UnityEngine;

namespace MapGeneration
{
    public static class MeshGenerator
    {
        public static MeshData GenerateTerrainMesh(float[,] heigthMap, float heigthMultiplier, AnimationCurve curve,int levelOfDetail)
        {
            int width = heigthMap.GetLength(0);
            int heigth = heigthMap.GetLength(1);
            float topLeftX = (width - 1) / -2f;
            float topLeftZ = (heigth - 1) / 2f;

            int meshSimplifyIncrement = (levelOfDetail == 0) ? 1 : levelOfDetail*2;
            int verticesPerLine = (width - 1) / meshSimplifyIncrement + 1;

            MeshData meshData = new MeshData(verticesPerLine, verticesPerLine);
            int vertexIndex = 0;

            for (int y = 0; y < heigth; y += meshSimplifyIncrement)
            {
                for (int x = 0; x < width; x += meshSimplifyIncrement)
                {
                    meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, curve.Evaluate(heigthMap[x,y]) * heigthMultiplier, topLeftZ - y);
                    meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)heigth);

                    if (x < width -1 && y < heigth -1)
                    {
                        meshData.AddTriangle(vertexIndex,vertexIndex + verticesPerLine + 1,vertexIndex + verticesPerLine);
                        meshData.AddTriangle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                    }
                    vertexIndex++;
                }
            }
            return meshData;
        }
    }
    public class MeshData
    {
        public Vector3[] vertices;
        public int[] triangles;
        public Vector2[] uvs;
        int triangleIndex;
        public MeshData(int meshWidth, int meshHeight)
        {
            vertices = new Vector3[meshWidth * meshHeight];
            uvs = new Vector2[meshWidth * meshHeight];   
            triangles = new int[(meshWidth-1)*(meshHeight-1)*6];
        }
        public void AddTriangle(int a, int b, int c)
        {
            triangles[triangleIndex] = a;
            triangles[triangleIndex + 1] = b;
            triangles[triangleIndex + 2] = c;
            triangleIndex += 3;
        }

        public Mesh CreateMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}
