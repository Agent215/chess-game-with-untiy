using System;
using System.Collections;
using System.Collections.Generic;
using src.Constants;
using UnityEngine;

public class PieceEventHandlers : MonoBehaviour
{
  
    /// <summary>
    /// can move in straight line horizontally and vertically until they hit opposing piece
    /// </summary>
    /// <param name="newLocation"></param>
    /// <param name="gameBoard"></param>
    /// <param name="color"></param>
    /// <param name="currentlocation"></param>
    /// <param name="validMoves"></param>
    public static void RookEventHandler(Tuple<int, int> newLocation, GameBoard gameBoard, string color, Tuple<int, int> currentlocation, List<Tuple<int, int>> validMoves)
    {
        //TODO this is stupid
        bool breakLoop = false;

        int pathLength =0;
        string moveDirection = "";
        //if moving on x axis
        if (newLocation.Item2 == currentlocation.Item2)
        {
            moveDirection = "x";
            Debug.Log("moving in the x direction");
            pathLength = Math.Abs(newLocation.Item1 - currentlocation.Item1);
        }
        //if moving on y axis
        if (newLocation.Item1== currentlocation.Item1)
        {
            moveDirection = "y";
            Debug.Log("moving in the y direction");
            pathLength = Math.Abs(newLocation.Item2 - currentlocation.Item2);
        }

        // if move is valid 
        if (currentlocation.Item1 == newLocation.Item1 || currentlocation.Item2 == newLocation.Item2)
        {
            for (int  i = 1; i <= pathLength; i++)
            {
                Debug.Log("entering loop");
                if (!breakLoop)
                {Debug.Log("still not valid");
                    
                    if (moveDirection.Equals("y"))
                    {
                        Tuple<int, int> move;
                        if (newLocation.Item2 - currentlocation.Item2 > 0)
                        {
                            move = new Tuple<int, int>(currentlocation.Item1, currentlocation.Item2 +i);
                        }
                        else
                        {
                             move = new Tuple<int, int>(currentlocation.Item1, currentlocation.Item2-i );
                        }
                        if (!gameBoard.HasPiece(move))
                        {
                            Debug.Log("valid move for rook");
                            validMoves.Add(move);
                        }
                        // if the piece is the same color as you then that spot is not valid
                        else if (gameBoard.GetPiece(newLocation).getColor().Equals(color))
                        {
                            Debug.Log("invalid move piece on same team occupies square");
                        }
                        else// this is the opponents piece
                        {
                            breakLoop = true;
                            Debug.Log("valid move for rook");
                            validMoves.Add(move);
                        }
                    }
                    if (moveDirection.Equals("x"))
                    {
                        Tuple<int, int> move;
                        if (newLocation.Item1 - currentlocation.Item1 > 0)
                        {
                            move = new Tuple<int, int>(currentlocation.Item1 +i, currentlocation.Item2);
                        }
                        else
                        {
                            move = new Tuple<int, int>(currentlocation.Item1 -i, currentlocation.Item2 );
                        }
                        if (!gameBoard.HasPiece(move))
                        {
                            Debug.Log("valid move for rook");
                            validMoves.Add(move);
                        }
                        // if the piece is the same color as you then that spot is not valid
                        else if (gameBoard.GetPiece(newLocation).getColor().Equals(color))
                        {
                            Debug.Log("invalid move piece on same team occupies square");
                            breakLoop = true;
                        }
                        else// this is the opponents piece
                        {
                            Debug.Log("valid move for rook");
                            validMoves.Add(move);
                            breakLoop = true;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Move is invalid");
        }
       
        //else move is invalid 

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="newLocation"></param>
    /// <param name="gameBoard"></param>
    /// <param name="color"></param>
    /// <param name="currentlocation"></param>
    /// <param name="validMoves"></param>
    public static void PawnEventHandler(Tuple<int, int> newLocation, GameBoard gameBoard, string color, Tuple<int, int> currentlocation,
        List<Tuple<int, int>> validMoves)
    {
        if (color == Constants.WHITE)
        {
            //if pawn is on starting position it can move up +2 spaces
            if (currentlocation.Item1 == 1)
            {
                Tuple<int, int> move1 = new Tuple<int, int>(currentlocation.Item1 + 1, currentlocation.Item2);
                Tuple<int, int> move2 = new Tuple<int, int>(currentlocation.Item1 + 2, currentlocation.Item2);
                validMoves.Add(move1);
                validMoves.Add(move2);
            }
            else if (currentlocation.Item1 != 7) // we are not at the end of the board
            {
                //pawn can move +1 up if there is nothing in that spot
                if (!gameBoard.HasPiece(newLocation))
                {
                    Tuple<int, int> move1 = new Tuple<int, int>(currentlocation.Item1 +1, currentlocation.Item2);
                    validMoves.Add(move1);
                }
                else if (gameBoard.HasPiece(newLocation) && gameBoard.GetPiece(newLocation).getColor() != Constants.WHITE)
                {
                    //if a pawn is diagonally adjacent to an opposing piece then they can move +1 up and +1 left or right
                    Tuple<int, int> move1 = new Tuple<int, int>(currentlocation.Item1  +1, currentlocation.Item2 + 1);
                    Tuple<int, int> move2 = new Tuple<int, int>(currentlocation.Item1 + 1, currentlocation.Item2 - 1);
                    //TODO
                    // if an opponents pawn has passed next to this pawn then current pawn get move +1 up and left or right behind that pawn to capture it
                }
            }
        }
        else if (color == Constants.BLACK)
        {
            //if pawn is on starting position it can move up +2 spaces
            if (currentlocation.Item1 == 6)
            {
                Tuple<int, int> move1 = new Tuple<int, int>(currentlocation.Item1 - 1, currentlocation.Item2);
                Tuple<int, int> move2 = new Tuple<int, int>(currentlocation.Item1 - 2, currentlocation.Item2);
                validMoves.Add(move1);
                validMoves.Add(move2);
            }
            else if (currentlocation.Item1 != 7) // we are not at the end of the board
            {
                //pawn can move +1 up if there is nothing in that spot
                if (!gameBoard.HasPiece(newLocation))
                {
                    Tuple<int, int> move1 = new Tuple<int, int>(currentlocation.Item1 - 1, currentlocation.Item2);
                    validMoves.Add(move1);
                }
                else if (gameBoard.HasPiece(newLocation) && gameBoard.GetPiece(newLocation).getColor() != Constants.BLACK)
                {
                    //if a pawn is diagonally adjacent to an opposing piece then they can move +1 up and +1 left or right
                    Tuple<int, int> move1 = new Tuple<int, int>(currentlocation.Item1 - 1, currentlocation.Item2 + 1);
                    Tuple<int, int> move2 = new Tuple<int, int>(currentlocation.Item1 - 1, currentlocation.Item2 - 1);
                    //TODO 
                    // if an opponents pawn has passed next to this pawn then current pawn get move +1 up and left or right behind that pawn to capture it
                }
            }
        }
    }

    /// <summary>
    ///  can move in 8 directions if all degrees of freedom are open
    //+1 up and +2 left or +2 up and +1 left
    //+1 up and +2 right or +2 up and +1 right
    //-1 down and +2 right or -2 down and +1 right
    //-1 down and +2 left or -2 down and +1 left
    /// </summary>
    /// <param name="newLocation"></param>
    /// <param name="gameBoard"></param>
    /// <param name="color"></param>
    /// <param name="currentlocation"></param>
    /// <param name="validMoves"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void KnightEventHandler(Tuple<int, int> newLocation, GameBoard gameBoard, string color, Tuple<int, int> currentlocation, List<Tuple<int, int>> validMoves)
    {
        
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newLocation"></param>
    /// <param name="gameBoard"></param>
    /// <param name="color"></param>
    /// <param name="currentlocation"></param>
    /// <param name="validMoves"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void BishopEventHandler(Tuple<int, int> newLocation, GameBoard gameBoard, string color, Tuple<int, int> currentlocation, List<Tuple<int, int>> validMoves)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// can move in any direction +1
    /// </summary>
    /// <param name="newLocation"></param>
    /// <param name="gameBoard"></param>
    /// <param name="color"></param>
    /// <param name="currentlocation"></param>
    /// <param name="validMoves"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void KingEventHandler(Tuple<int, int> newLocation, GameBoard gameBoard, string color, Tuple<int, int> currentlocation, List<Tuple<int, int>> validMoves)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// can move in any direction until they hit an opponents piece
    /// </summary>
    /// <param name="newLocation"></param>
    /// <param name="gameBoard"></param>
    /// <param name="color"></param>
    /// <param name="currentlocation"></param>
    /// <param name="validMoves"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void QueenEventHandler(Tuple<int, int> newLocation, GameBoard gameBoard, string color, Tuple<int, int> currentlocation, List<Tuple<int, int>> validMoves)
    {
        throw new NotImplementedException();
    }
}
