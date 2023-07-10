//P A D U R A R U     V A S I L E     3 1 3 1 B


using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace Project
{
    class Line3D
    {
        private Vector3 pointA;
        private Vector3 pointB;
        private bool visibility;
        private float width;
        private Color color;
        private float BIG_SIZE = 5.0f;
        private float DEFAULT_SIZE = 5.0f;

        //constructor
        public Line3D(Randomizer _r)
        {
            pointA = _r.RandomPosition();
            pointB = _r.RandomPosition();
            visibility = true;
            width = _r.RandomWidth();
            color = _r.RandomColor();
        }

        //metoda de desenare
        public void Draw()
        {
            if (visibility)
            {
                GL.LineWidth(width);
                GL.Begin(PrimitiveType.Lines);

                GL.Color3(color);
                GL.Vertex3(pointA);
                GL.Vertex3(pointB);

                GL.End();
            }
        }

        public bool ToggleVsibility()
        {
            visibility = !visibility;
            //if (visibility)
            //    return true;
            //else
            //    return false;
            return visibility;
        }

        public void ToggleWidth()
        {
            if(width == DEFAULT_SIZE)
            {
                width = BIG_SIZE;
            }
            else
            {
                width = DEFAULT_SIZE;
            }
        }

        public void DiscoMode(Randomizer _r)
        {
            color = _r.RandomColor();
        }
    }
}
