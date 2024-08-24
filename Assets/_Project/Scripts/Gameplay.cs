using BoardSolvers;
using Grid;
using Item.Data;
using Level;
using System;
using UnityEngine;
namespace App
{
    public class Gameplay : MonoBehaviour
    {
        private GameBoard _gameBoard;
        private BoardSolver _boardSolver;
        private ILevel _level;
        public void Construct(GameBoard gameBoard, BoardSolver boardSolver, ILevel level)
        {
            _gameBoard = gameBoard;
            _boardSolver = boardSolver;
            _level = level;
        }
        public void Init()
        {
            var levelData = _level.GetLevelData();
            _gameBoard.Init(levelData.GridWidth, levelData.GridHeight);
            _boardSolver.Init();
        }
        public void DeInit()
        {
            _gameBoard.DeInit();
            _boardSolver.DeInit();
        }
    }
}
