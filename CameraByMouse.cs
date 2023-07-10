//P A D U R A R U     V A S I L E     3 1 3 1 B

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Project
{
    class CameraByMouse
    {
        private Vector3 eye;
        private Vector3 target;
        private Vector3 up_vector;

        private const int MOVEMENT_UNIT = 1;

        public CameraByMouse()
        {
            eye = new Vector3(200, 175, 25);
            target = new Vector3(0, 25, 0);
            up_vector = new Vector3(0, 1, 0);
        }

        public CameraByMouse(int _eyeX, int _eyeY, int _eyeZ, int _targetX, int _targetY, int _targetZ, int _upX, int _upY, int _upZ)
        {
            eye = new Vector3(_eyeX, _eyeY, _eyeZ);
            target = new Vector3(_targetX, _targetY, _targetZ);
            up_vector = new Vector3(_upX, _upY, _upZ);
        }

        public CameraByMouse(Vector3 _eye, Vector3 _target, Vector3 _up)
        {
            eye = _eye;
            target = _target;
            up_vector = _up;
        }

        public void SetCameraMouse()
        {
            Matrix4 camera = Matrix4.LookAt(eye, target, up_vector);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref camera);
        }
    }
}
