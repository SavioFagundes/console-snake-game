namespace SnakeGame // EN: Declares the SnakeGame namespace. | PT: Declara o namespace SnakeGame.
{
    public class Coord // EN: Declares the Coord class. | PT: Declara a classe Coord.
    {
        private int _x; // EN: Private field for X coordinate. | PT: Campo privado para a coordenada X.
        private int _y; // EN: Private field for Y coordinate. | PT: Campo privado para a coordenada Y.
        public int X { get { return _x; } } // EN: Public getter for X. | PT: Getter público para X.
        public int Y { get { return _y; } } // EN: Public getter for Y. | PT: Getter público para Y.

        public Coord(int x, int y) // EN: Constructor with x and y parameters. | PT: Construtor com parâmetros x e y.
        {
            _x = x; // EN: Sets the X coordinate. | PT: Define a coordenada X.
            _y = y; // EN: Sets the Y coordinate. | PT: Define a coordenada Y.
        }

        public override bool Equals(object? obj) // EN: Overrides Equals for comparison. | PT: Sobrescreve Equals para comparação.
        {
            if ((obj == null) || !GetType().Equals(obj.GetType())) // EN: Checks if obj is null or not the same type. | PT: Verifica se obj é nulo ou de tipo diferente.
                return false; // EN: Returns false if not comparable. | PT: Retorna falso se não for comparável.
            Coord other = (Coord)obj; // EN: Casts obj to Coord. | PT: Faz cast de obj para Coord.
            return _x == other._x && _y == other._y; // EN: Returns true if both coordinates match. | PT: Retorna verdadeiro se ambas as coordenadas forem iguais.
        }

        public void ApplyMovementDirection(Direction direction) // EN: Moves the coordinate in the given direction. | PT: Move a coordenada na direção dada.
        {
            switch (direction) // EN: Checks the direction. | PT: Verifica a direção.
            {
                case Direction.Left: // EN: If moving left. | PT: Se mover para a esquerda.
                    _x--; // EN: Decrease X. | PT: Diminui X.
                    break;
                case Direction.Right: // EN: If moving right. | PT: Se mover para a direita.
                    _x++; // EN: Increase X. | PT: Aumenta X.
                    break;
                case Direction.Up: // EN: If moving up. | PT: Se mover para cima.
                    _y--; // EN: Decrease Y. | PT: Diminui Y.
                    break;
                case Direction.Down: // EN: If moving down. | PT: Se mover para baixo.
                    _y++; // EN: Increase Y. | PT: Aumenta Y.
                    break;
            }
        }
    }
}
