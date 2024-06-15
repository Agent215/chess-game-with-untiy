using System;
using System.Collections.Generic;
using System.Linq;
using src.Constants;
using UnityEngine;
using UnityEngine.Diagnostics;

public class GameBoard :MonoBehaviour 
    {
        // 2d array or matrix of GamePiece
        //GameBoard
        public GameObject[,] gameBoard = new GameObject[8, 8];
        public Material squareMaterial;
        public GameObject gameSquarePrefab;
        public GameObject gamePiecePrefab;
        private Vector3 _bounds;
        public int squareSize;


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
            
            if (gameBoard[PieceLocation.Item1, PieceLocation.Item2].GetComponent<GameSquare>().getHasPiece())
            {
                gamePiece = gameBoard[PieceLocation.Item1, PieceLocation.Item2].GetComponent<GameSquare>().getGamePiece();
            }
            return gamePiece;
        }

        /// <summary>
        /// set the piece at a given location
        /// </summary>
        /// <param name="PieceLocation"></param>
        /// <param name="gamePiece"></param>
        public void SetPiece(Tuple<int, int> PieceLocation, GameObject gamePiece)
        { 
            gamePiece.transform.parent = gameBoard[PieceLocation.Item1,PieceLocation.Item2].transform;
            gameBoard[PieceLocation.Item1, PieceLocation.Item2].GetComponent<GameSquare>().setGamePiece(gamePiece.GetComponent<GamePiece>());
            gameBoard[PieceLocation.Item1, PieceLocation.Item2].GetComponent<GameSquare>().setCurrentLocation(new List<Tuple<int, int>>() { new Tuple<int, int>(PieceLocation.Item1, PieceLocation.Item2) });
            gameBoard[PieceLocation.Item1, PieceLocation.Item2].GetComponent<GameSquare>().setHasPiece(true);
            gamePiece.transform.position = gameBoard[PieceLocation.Item1, PieceLocation.Item2].GetComponent<GameSquare>().GetComponent<BoxCollider>().center ;

        }
        /// <summary>
        /// remove piece from board
        /// </summary>
        /// <param name="gamePiece"></param>
        public void RemovePiece(GamePiece gamePiece)
        {
            var w = gameBoard.GetLength(0); 
            var h = gameBoard.GetLength(1); 

         Debug.Log("looking for piece" + gamePiece.getName()); 
            for (var x = 0; x < w; ++x)
            {
                for (var y = 0; y < h; ++y)
                {
                    if (gameBoard[x, y].GetComponent<GameSquare>().getGamePiece() != null &&
                        gameBoard[x, y].GetComponent<GameSquare>().getGamePiece().Equals(gamePiece))
                    {
                        Debug.Log("removing piece " + gameBoard[x, y].GetComponent<GameSquare>().getGamePiece().getName()); 
                        //for now just set this gameObject to inactive. maybe later we can do somthing else
                        gameBoard[x, y].GetComponent<GameSquare>().getGamePiece().gameObject.SetActive(false);
                        gameBoard[x, y].GetComponent<GameSquare>().setGamePiece(null);
                        gameBoard[x,y].GetComponent<GameSquare>().setHasPiece(false);
                    }
                }
            }
        }
        
        public void RemovePieceByLocation(Tuple<int,int> location)
        {
            gameBoard[location.Item1, location.Item2].GetComponent<GameSquare>().setGamePiece(null);
            gameBoard[location.Item1, location.Item2].GetComponent<GameSquare>().setHasPiece(false);
        }
        
        /// <summary>
        /// does the location passed in contain a piece already
        /// </summary>
        /// <param name="PieceLocation"></param>
        /// <returns>Boolean</returns>
        public Boolean HasPiece(Tuple<int, int> PieceLocation)
        {
            bool occupied = gameBoard[PieceLocation.Item1, PieceLocation.Item2].GetComponent<GameSquare>().getHasPiece();

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
                //read lines in and split up words
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
                
                //create each game piece and set attributes then set position on board
                GameObject tempGamePiece = Instantiate(gamePiecePrefab, transform.position , Quaternion.identity);
                tempGamePiece.name = pieceName + " " + color + tempGamePiece.GetHashCode();
                tempGamePiece.GetComponent<GamePiece>().setColor(color);
                tempGamePiece.GetComponent<GamePiece>().setCurrentLocation(tup);
                tempGamePiece.GetComponent<GamePiece>().setName(pieceName);
                //place piece on the board
                SetPiece(tup, tempGamePiece);
                //this is the model for the piece, has no scripts attached
                GameObject pieceModel = instantiatePiece(pieceName, color);
                pieceModel.transform.parent = tempGamePiece.transform;
                pieceModel.transform.position = tempGamePiece.transform.position ;
                pieceModel.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
                //flip pieces to face correct direction
                if (color.Equals(Constants.BLACK))
                    pieceModel.transform.rotation = Quaternion.Euler(new Vector3(-90,180,0));
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

                    GameObject cell = Instantiate(gameSquarePrefab, new Vector3(0,0,0), Quaternion.identity);
                    _bounds = new Vector3((gameBoard.GetLength(0) / 2) * squareSize, 0, (gameBoard.GetLength(0) / 2) *squareSize); // + center if board pivot point is not in the center
                    Mesh mesh = new Mesh();
                    cell.AddComponent<MeshFilter>().mesh = mesh;
                    cell.transform.parent = transform;
                    cell.AddComponent<MeshRenderer>().material = squareMaterial;
                    Vector3[] vertices = new Vector3[4];
                    squareName += (i + 1).ToString();
                    cell.name = squareName;
                    //four corners of square 
                    vertices[0] = new Vector3(i * squareSize, 0, j* squareSize) - _bounds;
                    vertices[1] = new Vector3(i *squareSize, 0, (j +1)*squareSize) - _bounds;
                    vertices[2] = new Vector3((i +1)*squareSize, 0, j*squareSize) - _bounds;
                    vertices[3] = new Vector3((i+ 1) *squareSize, 0, (j+ 1)*squareSize)- _bounds;
                    //draw the square from 2 trianlges 
                    int[] tris = new int[] {0, 1, 2, 1, 3, 2};
                    mesh.vertices = vertices;
                    mesh.triangles = tris;
                    cell.AddComponent<BoxCollider>();
                    if ((j % 2 == 0 && i % 2 == 0) || (j % 2 != 0 && i % 2 != 0))
                    {
                        cell.GetComponent<GameSquare>().setColor(Constants.BLACK);
                        cell.GetComponent<GameSquare>().setSquareName(squareName);
                    }
                    else if ((j % 2 == 0 && i % 2 != 0) || (j % 2 != 0 && i % 2 == 0))
                    {
                        cell.GetComponent<GameSquare>().setColor(Constants.WHITE);
                        cell.GetComponent<GameSquare>().setSquareName(squareName);
                    }
                    cell.GetComponent<GameSquare>().setHasPiece(false);
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

                    board += gameBoard[i, j].GetComponent<GameSquare>().getsquareName();
                    board += " ";
                    if (gameBoard[i, j].GetComponent<GameSquare>().hasPiece)
                    {
                        board += gameBoard[i, j].GetComponent<GameSquare>().getGamePiece().getName();
                        board += " ";
                        // board += gameBoard[i, j].GetComponent<GameSquare>().getGamePiece().getColor();
                        board += gameBoard[i, j].GetComponent<GameSquare>().getHasPiece().ToString();
                        board += gameBoard[i, j].GetComponent<GameSquare>().getGamePiece().getCurrentLocation().ToString();
                    }
                    else
                    {
                        board += "empty ";
                        board += " ";
                        board += gameBoard[i, j].GetComponent<GameSquare>().getHasPiece().ToString();
                        board += gameBoard[i, j].GetComponent<GameSquare>().getsquareName();
                    }
                    board += " ";
                }
                board += '\n';
            }
            return board;
        }

        /// <summary>
        /// loads the piece model and instantiate at origin
        /// </summary>
        /// <param name="pieceName"></param>
        /// <param name="color"></param>
        /// <returns>The GameObject with the model</returns>
        private GameObject instantiatePiece(string pieceName,string color)
        {
            GameObject pieceModel = new GameObject();
            if (color == Constants.WHITE)
            {
                switch (pieceName)
                {
                    case Constants.PAWN:
                        pieceModel = (GameObject)Instantiate(Resources.Load("ChessPawnWhite"), new Vector3(0, 0, 0), Quaternion.identity);
                        break;
                    case Constants.ROOK:
                        pieceModel = (GameObject)Instantiate(Resources.Load("ChessRookWhite"), new Vector3(0, 0, 0), Quaternion.identity);
                        break;
                    case Constants.KNIGHT:
                        pieceModel = (GameObject)Instantiate(Resources.Load("ChessKnightWhite"), new Vector3(0, 0, 0), Quaternion.identity);
                        break;
                    case Constants.BISHOP:
                        pieceModel = (GameObject)Instantiate(Resources.Load("ChessBishopWhite"), new Vector3(0, 0, 0), Quaternion.identity);
                        break;
                    case Constants.QUEEN:
                        pieceModel = (GameObject)Instantiate(Resources.Load("ChessQueenWhite"), new Vector3(0, 0, 0), Quaternion.identity);
                        break;
                    case Constants.KING:
                        pieceModel = (GameObject)Instantiate(Resources.Load("ChessKingWhite"), new Vector3(0, 0, 0), Quaternion.identity);
                        break;
                }

            }
            else
            {
                switch (pieceName)
                {
                    case Constants.PAWN:
                        pieceModel = (GameObject)Instantiate(Resources.Load("ChessPawnBlack"), new Vector3(0, 0, 0), Quaternion.identity);
                        break;
                    case Constants.ROOK:
                        pieceModel = (GameObject)Instantiate(Resources.Load("ChessRookBlack"), new Vector3(0, 0, 0), Quaternion.identity);
                        break;
                    case Constants.KNIGHT:
                        pieceModel = (GameObject)Instantiate(Resources.Load("ChessKnightBlack"), new Vector3(0, 0, 0), Quaternion.identity);
                        break;
                    case  Constants.BISHOP:
                        pieceModel = (GameObject)Instantiate(Resources.Load("ChessBishopBlack"), new Vector3(0, 0, 0), Quaternion.identity);
                        break;
                    case  Constants.QUEEN:
                        pieceModel = (GameObject)Instantiate(Resources.Load("ChessQueenBlack"), new Vector3(0, 0, 0), Quaternion.identity);
                        break;
                    case  Constants.KING:
                        pieceModel = (GameObject)Instantiate(Resources.Load("ChessKingBlack"), new Vector3(0, 0, 0), Quaternion.identity);
                        break;
                }

            }

            return pieceModel;
        }
    }

