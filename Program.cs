using SnakeGame; // EN: Imports the SnakeGame namespace. | PT: Importa o namespace SnakeGame.

Coord gridDimensions = new Coord(50, 20); // EN: Sets the grid size to 50x20. | PT: Define o tamanho do campo como 50x20.
Coord snakePos = new Coord(10, 10); // EN: Sets the initial snake position. | PT: Define a posição inicial da cobra.
Random random = new Random(); // EN: Creates a random number generator. | PT: Cria um gerador de números aleatórios.
Coord applePos = new Coord(random.Next(1, gridDimensions.X - 1), random.Next(1, gridDimensions.Y - 1)); // EN: Places the apple at a random position inside the grid. | PT: Coloca a maçã em uma posição aleatória dentro do campo.
int frameDelayMilli = 100; // EN: Sets the delay between frames (speed). | PT: Define o atraso entre os frames (velocidade).
Direction movementDirection = Direction.Right; // EN: Sets the initial movement direction to right. | PT: Define a direção inicial do movimento para a direita.
List<Coord> snakePosHistory = new List<Coord>(); // EN: Stores the positions of the snake's body. | PT: Armazena as posições do corpo da cobra.
int tailLength = 3; // EN: Initial tail length. | PT: Comprimento inicial da cauda.

bool gameOver = false; // EN: Game over flag. | PT: Flag de fim de jogo.

while (!gameOver) // EN: Main game loop, runs until game over. | PT: Loop principal do jogo, executa até o fim do jogo.
{
    Console.Clear(); // EN: Clears the console for redrawing. | PT: Limpa o console para redesenhar.

    // EN: Draws the field. | PT: Desenha o campo.
    for (int y = 0; y < gridDimensions.Y; y++) // EN: Loops through each row. | PT: Percorre cada linha.
    {
        for (int x = 0; x < gridDimensions.X; x++) // EN: Loops through each column. | PT: Percorre cada coluna.
        {
            Coord currentCoord = new Coord(x, y); // EN: Current cell coordinate. | PT: Coordenada da célula atual.

            if (snakePos.Equals(currentCoord)) // EN: If it's the snake's head. | PT: Se for a cabeça da cobra.
            {
                Console.Write("O"); // EN: Draws the snake's head. | PT: Desenha a cabeça da cobra.
            }
            else if (snakePosHistory.Contains(currentCoord)) // EN: If it's part of the snake's body. | PT: Se for parte do corpo da cobra.
            {
                Console.Write("o"); // EN: Draws the snake's body. | PT: Desenha o corpo da cobra.
            }
            else if (applePos.Equals(currentCoord)) // EN: If it's the apple's position. | PT: Se for a posição da maçã.
            {
                Console.Write("@"); // EN: Draws the apple. | PT: Desenha a maçã.
            }
            else if (x == 0 || x == gridDimensions.X - 1 || y == 0 || y == gridDimensions.Y - 1) // EN: If it's a wall. | PT: Se for uma parede.
            {
                Console.Write("#"); // EN: Draws the wall. | PT: Desenha a parede.
            }
            else
            {
                Console.Write(" "); // EN: Draws empty space. | PT: Desenha espaço vazio.
            }
        }
        Console.WriteLine(); // EN: Moves to the next line after each row. | PT: Vai para a próxima linha após cada linha.
    }

    // EN: Checks collision with the wall. | PT: Verifica colisão com a parede.
    if (snakePos.X == 0 || snakePos.X == gridDimensions.X - 1 ||
        snakePos.Y == 0 || snakePos.Y == gridDimensions.Y - 1)
    {
        gameOver = true; // EN: Ends the game if hit the wall. | PT: Encerra o jogo se bater na parede.
        break;
    }

    // EN: Checks collision with the snake's own body. | PT: Verifica colisão com o próprio corpo da cobra.
    if (snakePosHistory.Contains(new Coord(snakePos.X, snakePos.Y)))
    {
        gameOver = true; // EN: Ends the game if hit itself. | PT: Encerra o jogo se bater em si mesma.
        break;
    }

    // EN: Checks if the snake ate the apple. | PT: Verifica se a cobra comeu a maçã.
    if (snakePos.Equals(applePos))
    {
        tailLength++; // EN: Increases the tail length. | PT: Aumenta o comprimento da cauda.
        // EN: Generates a new apple in a free position. | PT: Gera uma nova maçã em uma posição livre.
        do
        {
            applePos = new Coord(random.Next(1, gridDimensions.X - 1), random.Next(1, gridDimensions.Y - 1));
        } while (snakePosHistory.Contains(applePos) || snakePos.Equals(applePos));
    }

    // EN: Updates the snake's body history. | PT: Atualiza o histórico do corpo da cobra.
    snakePosHistory.Add(new Coord(snakePos.X, snakePos.Y)); // EN: Adds the current head position to the history. | PT: Adiciona a posição atual da cabeça ao histórico.
    while (snakePosHistory.Count > tailLength) // EN: Removes oldest positions to keep the tail length. | PT: Remove as posições mais antigas para manter o comprimento da cauda.
    {
        snakePosHistory.RemoveAt(0);
    }

    // EN: Non-blocking key reading for movement. | PT: Leitura de teclas não bloqueante para movimento.
    DateTime time = DateTime.Now; // EN: Stores the current time. | PT: Armazena o tempo atual.
    while ((DateTime.Now - time).TotalMilliseconds < frameDelayMilli) // EN: Waits for the frame delay. | PT: Espera pelo atraso do frame.
    {
        if (Console.KeyAvailable) // EN: If a key was pressed. | PT: Se uma tecla foi pressionada.
        {
            ConsoleKey key = Console.ReadKey(true).Key; // EN: Reads the pressed key. | PT: Lê a tecla pressionada.
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (movementDirection != Direction.Right) // EN: Prevents reversing direction. | PT: Impede reversão de direção.
                        movementDirection = Direction.Left; // EN: Moves left. | PT: Move para a esquerda.
                    break;
                case ConsoleKey.RightArrow:
                    if (movementDirection != Direction.Left)
                        movementDirection = Direction.Right; // EN: Moves right. | PT: Move para a direita.
                    break;
                case ConsoleKey.UpArrow:
                    if (movementDirection != Direction.Down)
                        movementDirection = Direction.Up; // EN: Moves up. | PT: Move para cima.
                    break;
                case ConsoleKey.DownArrow:
                    if (movementDirection != Direction.Up)
                        movementDirection = Direction.Down; // EN: Moves down. | PT: Move para baixo.
                    break;
            }
        }
    }

    // EN: Moves the snake in the current direction. | PT: Move a cobra na direção atual.
    snakePos = new Coord(snakePos.X, snakePos.Y); // EN: Creates a new position for the head. | PT: Cria uma nova posição para a cabeça.
    snakePos.ApplyMovementDirection(movementDirection); // EN: Applies the movement direction. | PT: Aplica a direção do movimento.
}

Console.Clear(); // EN: Clears the screen at the end. | PT: Limpa a tela ao final.
Console.WriteLine("Game Over!"); // EN: Prints game over message. | PT: Exibe mensagem de fim de jogo.
Console.WriteLine($"Pontuação: {tailLength - 3}"); // EN: Shows the score. | PT: Mostra a pontuação.
Console.WriteLine("Pressione qualquer tecla para sair..."); // EN: Waits for a key to exit. | PT: Aguarda uma tecla para sair.
Console.ReadKey();
