//P A D U R A R U     V A S I L E     3 1 3 1 B



using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Randomizer
    {
        //ctrl F1 pt help de la microsoft
        private Random r;

        public Randomizer()
        {
            r = new Random();
        }

        public Color RandomColor()
        {
            int genR = r.Next(0, 255);
            int genG = r.Next(0, 255);
            int genB = r.Next(0, 255);

            Color col = Color.FromArgb(genR, genG, genB);

            return col;
        }

        public List<Color> RandomColors()
        {
            List<Color> colorsList = new List<Color>();
            colorsList.Add(RandomColor());
            colorsList.Add(RandomColor());
            colorsList.Add(RandomColor());

            return colorsList;
        }

        public Vector3 RandomPosition()
        {
            int first = r.Next(0, 10);
            int third = r.Next(0, 10);
            int last = r.Next(0, 10);

            Vector3 position = new Vector3(first, third, last);

            return position;
        }

        public float RandomWidth()
        {
            int width_number = r.Next(1, 50);

            float width = width_number;

            return width;
        }
    }
}
