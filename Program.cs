//P A D U R A R U     V A S I L E     3 1 3 1 B


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Project
{

    class Program
    {
        static void Main(string[] args)
        {
            using (SimpleWindow w = new SimpleWindow())
            {
                w.Run(30.0);
            }

        }

    }
}
