using BoardSolvers;
using Grid;
using Level;
using Link;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace App
{
    public class Gameplay
    {
        public event Action OnLevelFail;
        public event Action OnLevelWin;

        private readonly GameBoard _gameBoard;
        private readonly BoardSolver _boardSolver;
        private readonly ILevel _level;
        private readonly ILinkController _linkController;
        private readonly LevelConditions _levelConditions;

        public Gameplay(GameBoard gameBoard, BoardSolver boardSolver, IGameContext gameContext)
        {
            _gameBoard = gameBoard;
            _boardSolver = boardSolver;
            _level = gameContext.Locator.Get<ILevel>();
            _linkController = new LinkController(gameContext);
            _levelConditions = new LevelConditions(_level);
        }
        public void Init()
        {
            var levelData = _level.GetLevelData();
            _gameBoard.Init(levelData.GridWidth, levelData.GridHeight);
            _linkController.Init();
            _boardSolver.Init();
            _levelConditions.Init();

            AddEvents();
        }
        public void DeInit()
        {
            RemoveEvents();

            _boardSolver.DeInit();
            _linkController.DeInit();
            _gameBoard.DeInit();
        }
        private void OnItemsLinkedHandler(IEnumerable<IGridSlot> linkedSlots)
        {
            _boardSolver.FillBoard(linkedSlots);
            var scoredAmount = linkedSlots.Count() * 3;
            _levelConditions.SetConditions(scoredAmount);
        }
        private void OnMoveReachedZeroHandler()
        {
            if (_levelConditions.IsScoreCompleted)
            {
                return;
            }
            OnLevelFail?.Invoke();
        }
        private void OnScoreReachedZeroHandler()
        {
            OnLevelWin?.Invoke();
        }
        private void AddEvents()
        {
            _linkController.OnItemsLinked += OnItemsLinkedHandler;
            _levelConditions.OnMoveReachedZero += OnMoveReachedZeroHandler;
            _levelConditions.OnScoreReachedZero += OnScoreReachedZeroHandler;
        }
        private void RemoveEvents()
        {
            _linkController.OnItemsLinked -= OnItemsLinkedHandler;
            _levelConditions.OnMoveReachedZero -= OnMoveReachedZeroHandler;
            _levelConditions.OnScoreReachedZero -= OnScoreReachedZeroHandler;
        }
    }
}
