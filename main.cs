using System;
using System.Threading;

namespace SpinningCubes {
    class Program {
        static double A, B, C;

        static double cubeWidth = 20;
        static int width = 160, height = 44;
        static double[] zBuffer = new double[160 * 44];
        static char[] buffer = new char[160 * 44];
        static char backgroundASCIICode = '.';
        static int distanceFromCam = 100;
        static double horizontalOffset;
        static double K1 = 40;
        static double incrementSpeed = 0.6;

        static double x, y, z;
        static double ooz;
        static int xp, yp;
        static int idx;
    }
}
