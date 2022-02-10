using System;
using UnityEngine;

namespace src.util
{
    public static class Utils
    {
        public static Tuple<int, int> getLocationFromName(string chessSquareName, GameBoard gameBoard)
        {
            for (int i = 0; i < gameBoard.gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.gameBoard.GetLength(1); j++)
                {
                    if (gameBoard.gameBoard[i, j].GetComponent<GameSquare>().getsquareName().Equals(chessSquareName))
                    {
                        Tuple<int, int> move1 = new Tuple<int, int>(i, j);
                        Debug.Log("chessSquareName "+ chessSquareName +" is at location ["+ i + " " + j+"]" );
                        return move1;
                    }
                }
            }
            return null;
        }
    }
}