using BoardSolvers;
using Grid;
using UnityEngine;
namespace App
{
    public class Gameplay : MonoBehaviour
    {
        private GameBoard _gameBoard;
        private BoardSolver _boardSolver;
        public void Construct(GameBoard gameBoard, BoardSolver boardSolver)
        {
            _gameBoard = gameBoard;
            _boardSolver = boardSolver;
        }
        public void Init()
        {
            _gameBoard.Init();
            _boardSolver.Init();
        }
        public void DeInit()
        {
            _gameBoard.DeInit();
            _boardSolver.DeInit();
        }
    }
}
