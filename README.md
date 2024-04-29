# Chess Game with AI Integration

This is a Chess game developed in C# using Windows Forms. The game features a graphical user interface (GUI) and allows you to play against another human player or an AI opponent powered by OpenAI's GPT-4 language model.

## Features

- Play against a human opponent or an AI opponent using GPT-4.
- Interactive GUI with a chessboard and piece movements.
- Promotion of pawns to other pieces (Queen, Rook, Bishop, or Knight) upon reaching the opposite end of the board.
- Detection of check, checkmate, and stalemate scenarios.
- Victory screen upon winning the game.
- Customizable maximum number of attempts for the AI to make a valid move.

## Requirements

- .NET Framework (version compatible with the provided source code)
- OpenAI API key (for playing against the AI opponent)

## Getting Started

1. Clone the repository or download the source code.
2. Open the solution file in Visual Studio or your preferred C# IDE.
3. Set your OpenAI API key in the appropriate location within the code.
4. Build and run the application.
5. Choose whether to play against a human opponent or the AI.
6. Enjoy the game!

## Usage

1. **Playing Against a Human Opponent**:
   - Click on a piece to highlight its available moves.
   - Click on a highlighted square to move the selected piece to that position.
   - Take turns with your opponent to make moves.
   - The game will detect check, checkmate, and stalemate scenarios and display appropriate messages.
   - If a pawn reaches the opposite end of the board, you will be prompted to promote the pawn to another piece (Queen, Rook, Bishop, or Knight).

2. **Playing Against the AI Opponent**:
   - The AI will make moves when it's the black player's turn.
   - If a pawn reaches the opposite end of the board, the AI will automatically promote the pawn to the best possible piece.
   - The AI will analyze the board and make the best possible move based on its training by OpenAI's GPT-4 language model.
   - You can adjust the maximum number of attempts the AI makes to find a valid move by modifying the `MaxAttempts` property in the code.

## Contributing

Contributions to this project are welcome. If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

## Acknowledgements

- [OpenAI](https://www.openai.com/) for their powerful language models and API.
- [Newtonsoft.Json](https://www.newtonsoft.com/json) for JSON serialization and deserialization.
