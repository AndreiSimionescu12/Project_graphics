//P A D U R A R U     V A S I L E     3 1 3 1 B



using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace Project
{


    class SimpleWindow : GameWindow
    {
        private KeyboardState previousKeyboard;
        private MouseState previousClick;

        private Randomizer rando;
        private Color DEFAULT_BKG_COLOR = Color.BlueViolet;
        private Line3D firstLine;
        private List<Line3D> lista_linii = new List<Line3D>();
        private List<Color> colorsList;

        private readonly Axes ax;
        private readonly Grid grid;
        private readonly Camera3DIsometric cam;
        private bool displayMarker;
        private ulong updatesCounter;
        private ulong framesCounter;
        private MassiveObjects objy;
        private MassiveObjects triangle;
        private MassiveObjects quad;

        public SimpleWindow() : base(860, 610, new GraphicsMode(32, 24, 0, 8))//max 8 //modul anti aliasing)
        {
            VSync = VSyncMode.On;
            rando = new Randomizer();
            colorsList = rando.RandomColors();

            ax = new Axes();
            grid = new Grid();
            cam = new Camera3DIsometric();
            objy = new MassiveObjects(Color.Red);
            firstLine = new Line3D(rando);

            triangle = new MassiveObjects(Color.Red);
            quad = new MassiveObjects(Color.Red);

            //la pornirea aplicatiei se apeleaza:
            DisplayHelp();
            //----------------lab 5
            displayMarker = false;
            updatesCounter = 0;
            framesCounter = 0;
            //----------------

            Line3D x = new Line3D(rando);
            Line3D y = new Line3D(rando);
            Line3D z = new Line3D(rando);
            lista_linii.Add(x);
            lista_linii.Add(y);
            lista_linii.Add(z);

            Console.WriteLine("== LISTA INITIALA DE CULORI RANDOM TRIUNGHI ==");
            for (int color = 0; color < colorsList.Count; color++)
            {
                Console.WriteLine(colorsList[color]);
            }

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less); //prioritarea ordinii afisarii elementelor

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            //proiectia cum vad eu camera 3d

            //set bg
            GL.ClearColor(DEFAULT_BKG_COLOR);

            //set view port
            GL.Viewport(0, 0, this.Width, this.Height);

            //set mediu 3d
            //proiectie de perspectiiva                                                                 //mereu 1 //distanta fata de camera
            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 950);
            //tipul de matrice Matrixmode
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            //set camera, set the eye     pozitia ochiului, unde se uita ochiul, 
            Matrix4 eye = Matrix4.LookAt(30, 30, 30, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref eye);

            cam.SetCamera();

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            //---lab5
            updatesCounter++;

            if (displayMarker)
            {
                TimeStampIt("update", updatesCounter.ToString());
            }

            // LOGIC CODE
            KeyboardState currentKeyboard = Keyboard.GetState();
            MouseState currentClick = Mouse.GetState();

            //----------lab 3-------------
            // camera control Mouse by click
            if (currentClick[MouseButton.Left])
            {
                cam.MoveRight();
            }
            if (currentClick[MouseButton.Right])
            {
                cam.MoveLeft();
            }
            //-----------------------------

            // camera control (isometric mode)
            if (currentKeyboard[Key.W])
            {
                cam.MoveForward();
            }
            if (currentKeyboard[Key.S])
            {
                cam.MoveBackward();
            }
            if (currentKeyboard[Key.A])
            {
                cam.MoveLeft();
            }
            if (currentKeyboard[Key.D])
            {
                cam.MoveRight();
            }
            if (currentKeyboard[Key.Q])
            {
                cam.MoveUp();
            }
            if (currentKeyboard[Key.E])
            {
                cam.MoveDown();
            }
            //---------

            //logic code

            if (currentKeyboard[Key.Escape])
            {
                Exit();
            }

            //creare meniu help
            if (currentKeyboard[Key.H] && !previousKeyboard[Key.H])
            {
                DisplayHelp();
            }
            //end

            //resetare bg color la default
            if (currentKeyboard[Key.R] && !previousKeyboard[Key.R])
            {
                GL.ClearColor(DEFAULT_BKG_COLOR);
            }
            //end

            //random bg color
            if (currentKeyboard[Key.B] && !previousKeyboard[Key.B])
            {
                GL.ClearColor(rando.RandomColor());
            }
            //end

            //discomode
            if (currentKeyboard[Key.X] && !previousKeyboard[Key.X])
            {
                firstLine.DiscoMode(rando);
            }
            //end

            //linie show
            if (currentKeyboard[Key.V] && !previousKeyboard[Key.V])
            {
                firstLine.ToggleVsibility();
                //Console.WriteLine("toggle visibility");
            }
            //end

            //grosime linie
            if (currentKeyboard[Key.G] && !previousKeyboard[Key.G])
            {
                firstLine.ToggleWidth();
            }
            //end

            // LAB 3[triunghi] -> cerinta 8     |||      LAB 4[cub] -> cerinta 1:
            if (currentKeyboard[Key.F1] && !previousKeyboard[Key.F1])
            {
                triangle.ChangeColorShade(0.04f, 0f, 0f);
                //quad.ChangeColorShade(0.04f, 0f, 0f);
            }
            if (currentKeyboard[Key.F2] && !previousKeyboard[Key.F2])
            {
                triangle.ChangeColorShade(0f, 0.04f, 0f);
                //quad.ChangeColorShade(0.04f, 0f, 0f);
            }
            if (currentKeyboard[Key.F3] && !previousKeyboard[Key.F3])
            {
                triangle.ChangeColorShade(0f, 0f, 0.04f);
                //quad.ChangeColorShade(0.04f, 0f, 0f);
            }

            // LAB 3 -> cerinta 9     |||      LAB 4[cub] -> cerinta 2 & 3: 
            if (currentKeyboard[Key.F4] && !previousKeyboard[Key.F4])
            {
                colorsList = rando.RandomColors();
                Console.WriteLine("== CULOARE RANDOM TRIUNGHI -> LISTA CULORI RandomColor==");
                for (int color = 0; color < colorsList.Count; color++)
                {
                    Console.WriteLine(colorsList[color]);
                }
            }

            // LAB 4 -> cerinta 1:
            if (currentKeyboard[Key.F5] && !previousKeyboard[Key.F5])
            {
                quad.ChangeColorShade(0f, 0.04f, 0f);
            }


            previousKeyboard = currentKeyboard;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            //foreach (Line3D lines in lista_linii)
            //{
            //    lines.Draw();
            //}
            //DisplayLinesList();

            //----------LAB 5
            //grid.Draw();
            //ax.Draw();
            //objy.Draw();
            //---------------

            // LAB 3 -> cerinta 9:
            triangle.DrawTriangleWithColorList(colorsList);

            // LAB 3 -> cerinta 8:
            triangle.DrawTriangle();

            // LAB 4 - > CUB
            //quad.DrawQuad();
            //quad.DrawQuadWithColorList(colorsList);

            //DisplayTriangle();

            SwapBuffers();
        }

        private void DisplayHelp()
        {
            Console.WriteLine("\n       MENIU");
            Console.WriteLine("B - random bg color");
            Console.WriteLine("G - random grosime linie");
            Console.WriteLine("R - resetare bg color");
            Console.WriteLine("H - meniu");
            Console.WriteLine("ESC - Exit");
            Console.WriteLine(" ");
            Console.WriteLine(" L A B O R A T O R - 3 ");
            Console.WriteLine("Click stanga , Click dreapta [cerinta 8] mutare camera stanga-dreapta ");
            Console.WriteLine("F1 , F2 , F3 - [cerinta 8] schimbare culoare triunghiSTANGA (pe nuanta)");
            Console.WriteLine("F4 -[cerinta 9] culoare random triunghiDREAPTA");
        }

        public void DisplayTriangle()
        {
            GL.Color3(Color.Red);
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex3(-0.15f, -0.15f, 0.0f);
            GL.Vertex3(0.15f, -0.15f, 0.0f);
            GL.Vertex3(0.0f, 0.15f, 0.0f);
            GL.End();
        }

        private void DisplayLinesList()
        {
            //if (firstLine.ToggleVsibility().Equals(true))
            //{
            foreach (Line3D lines in lista_linii)
            {
                lines.Draw();
            }
            //}
        }

        private void TimeStampIt(String source, String counter)
        {
            String dt = DateTime.Now.ToString("hh:mm:ss.ffff");
            Console.WriteLine("     TSTAMP from <" + source + "> on iteration <" + counter + ">: " + dt);
        }



    }
}
