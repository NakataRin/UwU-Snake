using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class UwU
{
    // Game settings: grid width, height, and initial score
    static int width = 40;
    static int height = 20;
    static int score = 0;

    // Snake and food positions
    static int snakeX, snakeY, foodX, foodY;

    // Snake's movement velocity (change in X and Y per tick)
    static int velocityX = 0, velocityY = 0;

    // List to keep track of the snake's body positions
    static List<Tuple<int, int>> snakeBody = new List<Tuple<int, int>>();

    // Boolean flag to check if the game is over
    static bool gameOver = false;

    static void Main()
    {
        // Hide the cursor to make the game display cleaner
        Console.CursorVisible = false;

        // Initialize game settings and start the game
        StartGame();

        // Main game loop that runs until the game is over
        while (!gameOver)
        {
            Draw();    // Render the game screen
            Input();   // Process user input
            Logic();   // Update the game logic based on input and game state
            Thread.Sleep(100); // Control game speed by delaying the next loop iteration
        }

        // Display "Game Over" message and the final score
        Console.SetCursorPosition(width / 2 - 5, height / 2);
        Console.WriteLine("Game Ovuwu! >.<");
        Console.SetCursorPosition(width / 2 - 6, height / 2 + 1);
        Console.WriteLine($"UwU Scowe: {score}");
        Console.ReadKey(); // Wait for the user to press a key before closing the game
    }

    static void StartGame()
    {
        // Initialize snake position at the center of the grid
        snakeX = width / 2;
        snakeY = height / 2;

        // Clear any existing body parts and add the initial position
        snakeBody.Clear();
        snakeBody.Add(new Tuple<int, int>(snakeX, snakeY));

        // Generate the first food item on the grid
        GenerateFood();
    }

    static void Draw()
    {
        Console.Clear(); // Clear the console for redrawing

        // Draw the top boundary of the game area
        for (int i = 0; i < width + 2; i++)
            Console.Write("~");
        Console.WriteLine();

        // Draw the game area (snake, food, and empty space)
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (j == 0) Console.Write("~"); // Left boundary

                // Draw snake head, food, snake body, or empty space
                if (i == snakeY && j == snakeX) Console.Write("O.o"); // Snake head
                else if (i == foodY && j == foodX) Console.Write("UwU"); // Food
                else if (snakeBody.Any(b => b.Item1 == j && b.Item2 == i)) Console.Write("o.o"); // Snake body
                else Console.Write(" "); // Empty space

                if (j == width - 1) Console.Write("~"); // Right boundary
            }
            Console.WriteLine();
        }

        // Draw the bottom boundary of the game area
        for (int i = 0; i < width + 2; i++)
            Console.Write("~");
        Console.WriteLine();

        // Display the current score
        Console.WriteLine($"Scowe: {score} >w<");
    }

    static void Input()
    {
        // Check if a key is pressed
        if (Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey(true).Key; // Get the pressed key without displaying it
            switch (key)
            {
                // Update the snake's direction based on the key pressed (WASD keys)
                case ConsoleKey.W:
                    if (velocityY != 1) // Prevent moving in the opposite direction
                    {
                        velocityX = 0;
                        velocityY = -1;
                    }
                    break;
                case ConsoleKey.S:
                    if (velocityY != -1)
                    {
                        velocityX = 0;
                        velocityY = 1;
                    }
                    break;
                case ConsoleKey.A:
                    if (velocityX != 1)
                    {
                        velocityX = -1;
                        velocityY = 0;
                    }
                    break;
                case ConsoleKey.D:
                    if (velocityX != -1)
                    {
                        velocityX = 1;
                        velocityY = 0;
                    }
                    break;
            }
        }
    }

    static void Logic()
    {
        // Move the snake by updating its position based on the current velocity
        snakeX += velocityX;
        snakeY += velocityY;

        // Check if the snake has hit the wall boundaries
        if (snakeX < 0 || snakeX >= width || snakeY < 0 || snakeY >= height)
        {
            gameOver = true; // End the game if the snake hits the wall
        }

        // Check if the snake has found the food
        if (snakeX == foodX && snakeY == foodY)
        {
            score++; // Increase the score
            GenerateFood(); // Generate new food
            snakeBody.Add(new Tuple<int, int>(snakeX, snakeY)); // Add new segment to the snake's body
        }

        // Move the snake's body (shift positions from the head down)
        for (int i = snakeBody.Count - 1; i > 0; i--)
        {
            snakeBody[i] = snakeBody[i - 1];
        }

        // Update the position of the snake's head
        if (snakeBody.Count > 0)
            snakeBody[0] = new Tuple<int, int>(snakeX, snakeY);

        // Check if the snake has collided with itself
        if (snakeBody.Skip(1).Any(b => b.Item1 == snakeX && b.Item2 == snakeY))
        {
            gameOver = true; // End the game if the snake collides with its own body
        }
    }

    static void GenerateFood()
    {
        // Randomly generate new coordinates for the food within the grid
        Random rand = new Random();
        foodX = rand.Next(0, width);
        foodY = rand.Next(0, height);
    }
}
