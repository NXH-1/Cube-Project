using System;
using System.Threading;

namespace SpinningCubes {
    class Program {
        static double A, B, C; // Angles of rotation

        static double cubeWidth = 20;
        static int gridWidth = 160, gridHeight = 44;
        static double[] zBuffer = new double[160 * 44];
        static char[] buffer = new char[160 * 44];
        static char backgroundASCIICode = '.';
        static int distanceFromCamera = 100;
        static double horizontalOffset;
        static double FOVScaleFactor = 40;
        static double incrementSpeed = 0.6;

        static double x, y, z; // Rotated 3D coordinates
        static double ooz;
        static int screenX, screenY;
        static int bufferIndex;
    }
}
