const int BoardSize = 9;

while (true)
{
    PlayGame();
    Console.Write("Play again? (y/n): ");
    var answer = Console.ReadLine();
    if (!string.Equals(answer?.Trim(), "y", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }
}

static void PlayGame()
{
    var board = Enumerable.Range(1, BoardSize).Select(number => (char)('0' + number)).ToArray();
    var currentPlayer = 'X';
    var moves = 0;

    Console.WriteLine("Tic-Tac-Toe");
    Console.WriteLine("Choose a square by entering its number.");

    while (true)
    {
        DisplayBoard(board);
        Console.Write($"Player {currentPlayer}, choose a square (1-9): ");
        var input = Console.ReadLine();
        if (input is null)
        {
            return;
        }

        if (!int.TryParse(input, out var square) || square is < 1 or > BoardSize)
        {
            Console.WriteLine("Please enter a number from 1 to 9.");
            continue;
        }

        var index = square - 1;
        if (board[index] is 'X' or 'O')
        {
            Console.WriteLine("That square is already taken. Choose another.");
            continue;
        }

        board[index] = currentPlayer;
        moves++;

        if (HasWon(board, currentPlayer))
        {
            DisplayBoard(board);
            Console.WriteLine($"Player {currentPlayer} wins!");
            return;
        }

        if (moves == BoardSize)
        {
            DisplayBoard(board);
            Console.WriteLine("It's a draw!");
            return;
        }

        currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
    }
}

static void DisplayBoard(char[] board)
{
    for (var row = 0; row < 3; row++)
    {
        Console.WriteLine($" {board[row * 3]} | {board[row * 3 + 1]} | {board[row * 3 + 2]}");
        if (row < 2)
        {
            Console.WriteLine("---+---+---");
        }
    }
}

static bool HasWon(char[] board, char player)
{
    int[][] winningLines =
    [
        [0, 1, 2], [3, 4, 5], [6, 7, 8],
        [0, 3, 6], [1, 4, 7], [2, 5, 8],
        [0, 4, 8], [2, 4, 6]
    ];

    return winningLines.Any(line => line.All(index => board[index] == player));
}
