## Description
I wanted to test and practice my skills in understanding matricies and certain code logic. I also decided to learn a new programming language, c# alongside this.

The code logic was not designed by me.
The inspiration for this project: https://github.com/tarantino07/cube.c

## Code changes
Changed float variables to double which truncate values. This was done as the System.Math functions for Sin and Cos can only accept and return doubles, so making them in to double will remove the need to "cast" them into float each time.

Instead of using the ANSI escape code `\x1b[2J`, create a `char[]` and call `Console.Write()` once per frame while resetting the cursor using `Console.SetCursorPosition(0,0)`

Used `Array.Fill()` to clear buffers for handling memory safely

### Additions

Made the cubes different colors for contrast

## Running the program
Ensure you increase the size of your terminal enough to see the cube.

Then run:

``` Terminal
dotnet run
```