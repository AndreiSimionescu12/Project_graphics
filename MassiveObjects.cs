//P A D U R A R U     V A S I L E     3 1 3 1 B


using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Project
{
    public class MassiveObjects
    {
        //LABORATOR 4 - FISIER CUB
        //private const String FILENAME = "assets/Cub.txt";

        //LABORATOR 3 - FISIER TRIUNGHI
        private const String FILENAME = "assets/Triangle.txt";
        private const int FACTOR_SCALARE_IMPORT = 5;

        private List<Vector3> coordsList;

        private bool visibility;
        private Color meshColor;
        private float R, G, B;
        private bool hasError;
        private const float MaxValueColor = 1.0f;
        private const float MinValueColor = 0.0f;
        Randomizer rando = new Randomizer();

        public MassiveObjects(Color col)
        {
            try
            {
                coordsList = LoadFromObjFile(FILENAME);

                if (coordsList.Count == 0)
                {
                    Console.WriteLine("Crearea obiectului a esuat: obiect negasit/coordonate lipsa!");
                    return;
                }
                visibility = true;
                meshColor = col;
                hasError = false;
                Console.WriteLine("Obiect 3D încarcat - " + coordsList.Count.ToString() + " vertexuri disponibile!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: assets file <" + FILENAME + "> is missing!!!");
                hasError = true;
            }
        }

        public void ToggleVisibility()
        {
            if (hasError == false)
            {
                visibility = !visibility;
            }
        }

        public void Draw()
        {
            if (hasError == false && visibility == true)
            {
                GL.Color3(meshColor);
                GL.Begin(PrimitiveType.Triangles);
                foreach (var vert in coordsList)
                {
                    GL.Vertex3(vert);
                }
                GL.End();
            }
        }
        public void ChangeColorShade(float r, float g, float b)
        {
            if (R < MaxValueColor)
                R = r + R;
            if (G < MaxValueColor)
                G = g + G;
            if (B < MaxValueColor)
                B = b + B;
        }
        public void DrawTriangleWithColorList(List<Color> colorsList)
        {
            if (hasError == false && visibility == true)
            {
                int contor = 0;
                GL.Begin(PrimitiveType.Triangles);
                //foreach (var vert in coordsList)
                for (int i = 3; i < 6; i++)
                {
                    GL.Color3(colorsList[contor]);
                    GL.Vertex3(coordsList[i]);
                    contor++;

                }
                GL.End();
            }
        }
        public void DrawTriangle()
        {
            if (hasError == false && visibility == true)
            {
                GL.Color3(MinValueColor + R, MinValueColor + G, MinValueColor + B);
                GL.Begin(PrimitiveType.Triangles);
                for (int i = 0; i < 3; i++)
                {
                    GL.Vertex3(coordsList[i]);
                }
                GL.End();
            }
        }

        public void DrawQuad()
        {
            if (hasError == false && visibility == true)
            {
                int contor = 0;
                GL.Color3(meshColor);
                GL.Begin(PrimitiveType.Quads);
                foreach (var vert in coordsList)
                {
                    if (contor == 20)
                    {
                        GL.Color3(MinValueColor + R, MinValueColor + G, MinValueColor + B);
                    }
                    GL.Vertex3(vert);
                    contor++;
                }
                GL.End();
            }
        }

        public void DrawQuadWithColorList(List<Color> colorsList)
        {
            if (hasError == false && visibility == true)
            {
                int contor = 0;
                GL.Color3(meshColor);
                GL.Begin(PrimitiveType.Quads);

                foreach (var vert in coordsList)
                {
                    if (contor == 3) //reseteaza nr de culori ca sa deseneze si ptr restul
                        contor = 0;
                    GL.Color3(colorsList[contor]);
                    GL.Vertex3(vert);
                    contor++;
                }
                GL.End();
            }
        }

        private List<Vector3> LoadFromObjFile(string fname)
        {
            List<Vector3> vlc3 = new List<Vector3>();
            var lines = File.ReadLines(fname);
            Console.WriteLine("======= LISTA VERTEXURI ======");
            foreach (var line in lines)
            {
                string[] block = line.Trim().Split(' ');
                float xval = float.Parse(block[0].Trim()) * FACTOR_SCALARE_IMPORT;
                float yval = float.Parse(block[1].Trim()) * FACTOR_SCALARE_IMPORT;
                float zval = float.Parse(block[2].Trim()) * FACTOR_SCALARE_IMPORT;
                Console.WriteLine("{0},{1},{2}", block[0], block[1], block[2]);
                vlc3.Add(new Vector3((int)xval, (int)yval, (int)zval));

            }
            return vlc3;
        }
    }
}
