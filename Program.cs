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
        
        static void Main() {
            Console.CursorVisible = false;
            Console.Clear();

            char[] consoleBuffer = new char[gridWidth * 2 * gridHeight];
            
            while (true) {
                Array.Fill(buffer, backgroundASCIICode);
                Array.Fill(zBuffer, 0);

                // First Cube
                cubeWidth = 20;
                horizontalOffset = -2 * cubeWidth;
                for (double cubeX = -cubeWidth; cubeX < cubeWidth; cubeX += incrementSpeed) {
                    for (double cubeY = -cubeWidth; cubeY < cubeWidth; cubeY += incrementSpeed) {
                        calculateForSurface(cubeX, cubeY, -cubeWidth, '@');
                        calculateForSurface(cubeWidth, cubeY, cubeX, '$');
                        calculateForSurface(-cubeWidth, cubeY, -cubeX, '~');
                        calculateForSurface(-cubeX, cubeY, cubeWidth, '#');
                        calculateForSurface(cubeX, -cubeWidth, -cubeY, ';');
                        calculateForSurface(cubeX, cubeWidth, cubeY, '+');
                    }
                }
                // Second Cube
                cubeWidth = 10;
                horizontalOffset = 1 * cubeWidth;
                for (double cubeX = -cubeWidth; cubeX < cubeWidth; cubeX += incrementSpeed) {
                    for (double cubeY = -cubeWidth; cubeY < cubeWidth; cubeY += incrementSpeed) {
                        calculateForSurface(cubeX, cubeY, -cubeWidth, '@');
                        calculateForSurface(cubeWidth, cubeY, cubeX, '$');
                        calculateForSurface(-cubeWidth, cubeY, -cubeX, '~');
                        calculateForSurface(-cubeX, cubeY, cubeWidth, '#');
                        calculateForSurface(cubeX, -cubeWidth, -cubeY, ';');
                        calculateForSurface(cubeX, cubeWidth, cubeY, '+');
                    }
                }

                // Third Cube
                cubeWidth = 5;
                horizontalOffset = 8 * cubeWidth;
                for (double cubeX = -cubeWidth; cubeX < cubeWidth; cubeX += incrementSpeed) {
                    for (double cubeY = -cubeWidth; cubeY < cubeWidth; cubeY += incrementSpeed) {
                        calculateForSurface(cubeX, cubeY, -cubeWidth, '@');
                        calculateForSurface(cubeWidth, cubeY, cubeX, '$');
                        calculateForSurface(-cubeWidth, cubeY, -cubeX, '~');
                        calculateForSurface(-cubeX, cubeY, cubeWidth, '#');
                        calculateForSurface(cubeX, -cubeWidth, -cubeY, ';');
                        calculateForSurface(cubeX, cubeWidth, cubeY, '+');
                    }
                }

                // Render Frame
                int consoleIndex = 0;
                for (int iterateY = 0; iterateY < gridHeight; iterateY++) {
                    for (int iterateX = 0; iterateX < gridWidth; iterateX++) {
                        consoleBuffer[consoleIndex++] = buffer[iterateX + iterateY * gridWidth];
                    }
                    consoleBuffer[consoleIndex++] = '\n';
                }
                Console.SetCursorPosition(0, 0);
                Console.Write(consoleBuffer);

                A += 0.05;
                B += 0.05;
                C += 0.01;

                Thread.Sleep(16);

            }
        }
    }
}