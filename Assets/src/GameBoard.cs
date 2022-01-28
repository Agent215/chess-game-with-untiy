using System;
using src.Constants;
using UnityEngine;

    public class GameBoard : MonoBehaviour, IGameBoard
    {
        // 2d array or matrix of GamePiece
        //GameBoard
       public GameSquare[,] gameBoard = new GameSquare[8, 8];


        public GameBoard GetGameBoard()
        {
            return this;
        }
        public void SetGameBoard(GameBoard gameBoard)
        {
            this.gameBoard = gameBoard.gameBoard;
        }

        /// <summary>
        /// returns the piece at a given location
        /// </summary>
        /// <param name="PieceLocation"></param>
        /// <returns>GamePiece</returns>
        public GamePiece GetPiece(Tuple<int, int> PieceLocation)
        {
            GamePiece gamePiece = new GamePiece();
            
            if (gameBoard[PieceLocation.Item1, PieceLocation.Item2].getHasPiece())
            {
                gamePiece = gameBoard[PieceLocation.Item1, PieceLocation.Item2].getGamePiece();

            }
            return gamePiece;
        }

        /// <summary>
        /// set the piece at a given location
        /// </summary>
        /// <param name="PieceLocation"></param>
        /// <param name="gamePiece"></param>
        public void SetPiece(Tuple<int, int> PieceLocation, GamePiece gamePiece)
        {
            gameBoard[PieceLocation.Item1, PieceLocation.Item2].setGamePiece(gamePiece);
            gameBoard[PieceLocation.Item1, PieceLocation.Item2].setHasPiece(true);
        }
        /// <summary>
        /// remove piece from board
        /// </summary>
        /// <param name="gamePiece"></param>
        public void RemovePiece(GamePiece gamePiece)
        {
            var w = gameBoard.GetLength(0); 
            var h = gameBoard.GetLength(1); 

            for (var x = 0; x < w; ++x)
            {
                for (var y = 0; y < h; ++y)
                {
                    if (gameBoard[x, y].Equals(gamePiece))
                    {
                        gameBoard[x, y].setGamePiece(null);
                        gameBoard[x,y].setHasPiece(false);
                    }
                        
                }
            }

        }
        
        public void RemovePieceByLocation(Tuple<int,int> location)
        {
            gameBoard[location.Item1, location.Item2].setGamePiece(null);
            gameBoard[location.Item1, location.Item2].setHasPiece(false);

        }
        
        /// <summary>
        /// does the location passed in contain a piece already
        /// </summary>
        /// <param name="PieceLocation"></param>
        /// <returns>Boolean</returns>
        public Boolean HasPiece(Tuple<int, int> PieceLocation)
        {
            bool occupied = gameBoard[PieceLocation.Item1, PieceLocation.Item2] != null;

            return occupied;
        }

        /// <summary>
        /// Set the board with pieces based of off config file
        /// </summary>
        /// <param name="filename"></param>
        public void Initialize(String filename)
        {
            InitializeSquares();
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\brahm\OneDrive\Documents\UnityStuff\My project\Assets\src\Config\defaultGame.txt");

            foreach (string line in lines)
            {
                string[] words = line.Split(' ');
                Console.WriteLine(line);
                Debug.Log(words[0]);
                string pieceName = (string)words[0];
                string color = (string)words[1];
                string x = (string)words[2];
                string y = (string)words[3];

                string s = "setting piece " + pieceName + " at " + x + " " + y;
                Debug.Log(s);

                Tuple<int, int> tup = new Tuple<int, int>(int.Parse(x), int.Parse(y));
                GamePiece tempGamePiece = new GamePiece();
                tempGamePiece.setColor(color);
                tempGamePiece.setCurrentLocation(tup);
                tempGamePiece.setName(pieceName);
                SetPiece(tup, tempGamePiece);
            }
            Debug.Log(ToString());
        }

        /// <summary>
        /// set squares as empty with color and name
        /// </summary>
        public void InitializeSquares()
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    string squareName = "";
                    switch (j)
                    {
                        case 7:
                            squareName += Constants.A;
                            break;
                        case 6:
                            squareName += Constants.B;
                            break;
                        case 5:
                            squareName += Constants.C;
                            break;
                        case 4:
                            squareName += Constants.D;
                            break;
                        case 3:
                            squareName += Constants.E;
                            break;
                        case 2:
                            squareName += Constants.F;
                            break;
                        case 1:
                            squareName += Constants.G;
                            break;
                        case 0:
                            squareName += Constants.H;
                            break;
                    }

                    GameSquare cell = new GameSquare();
                    squareName += (i + 1).ToString();
                    if ((j % 2 == 0 && i % 2 == 0) || (j % 2 != 0 && i % 2 != 0))
                    {
                        cell.setColor(Constants.BLACK);
                        cell.setSquareName(squareName);
                    }
                    else if ((j % 2 == 0 && i % 2 != 0) || (j % 2 != 0 && i % 2 == 0))
                    {
                        cell.setColor(Constants.WHITE);
                        cell.setSquareName(squareName);
                    }
                    cell.setHasPiece(false);
                    gameBoard[i, j] = cell;
                }
            }

        }

        /// <summary>
        /// print the game board as array
        /// </summary>
        /// <returns>string</returns>
        override
            public string ToString()
        {
            string board = "";
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {

                    board += gameBoard[i, j].getsquareName();
                    board += " ";
                    if (gameBoard[i, j].hasPiece)
                    {
                        board += gameBoard[i, j].getGamePiece().getName();
                        board += " ";
                        board += gameBoard[i, j].getGamePiece().getColor();
                    }
                    else
                    {
                        board += "empty";
                    }
                    board += " ";
                }
                board += '\n';
            }
            return board;
        }
        
    }

