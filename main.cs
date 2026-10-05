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

        static double calculateX(double i, double j, double k) {
            return j * Math.Sin(A) * Math.Sin(B) * Math.Cos(C) - 
                   k * Math.Cos(A) * Math.Sin(B) * Math.Cos(C) + 
                   j * Math.Cos(A) * Math.Sin(C) +
                   k * Math.Sin(A) * Math.Sin(C) + 
                   i * Math.Cos(B) * Math.Cos(C);
        }

        static double calculateY(double i, double j, double k) {
            return j * Math.Cos(A) * Math.Cos(C) + 
                   k * Math.Sin(A) * Math.Cos(C) - 
                   j * Math.Sin(A) * Math.Sin(B) * Math.Sin(C) + 
                   k * Math.Cos(A) * Math.Sin(B) * Math.Sin(C) -
                   i * Math.Cos(B) * Math.Sin(C);
        }

        static double calculateZ(double i, double j, double k) {
            return k * Math.Cos(A) * Math.Cos(B) - 
                   j * Math.Sin(A) * Math.Cos(B) + 
                   i * Math.Sin(B);
        }

        static void calculateForSurface(double cubeX, double cubeY, double cubeZ, char ch) {
            x = calculateX(cubeX, cubeY, cubeZ);
            y = calculateY(cubeX, cubeY, cubeZ);
            z = calculateZ(cubeX, cubeY, cubeZ) + distanceFromCamera;

            ooz = 1 / z; // "One over z"

            screenX = (int)(gridWidth / 2 + horizontalOffset + FOVScaleFactor * ooz * x * 2);
            screenY = (int)(gridHeight / 2 + FOVScaleFactor * ooz * y);

            bufferIndex = screenX + gridWidth * screenY;
            
            if (bufferIndex >= 0 && bufferIndex < gridWidth * gridHeight) {
                if (ooz > zBuffer[bufferIndex]) {
                    zBuffer[bufferIndex] = ooz;
                    buffer[bufferIndex] = ch;
                }
            }
        }   
    }
}