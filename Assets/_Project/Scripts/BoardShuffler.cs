using DG.Tweening;
using Grid;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
namespace BoardSolvers
{
    public class BoardShuffler
    {
        IGameBoard _gameBoard;
        public BoardShuffler(IGameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }
        public void Shuffle(Action onShuffled = null)
        {
            var seq = DOTween.Sequence();
            for (int i = 0; i < _gameBoard.GridSlots1D.Count - 1; i++)
            {
                var pos = Random.Range(i, _gameBoard.GridSlots1D.Count);
                var shuffleItem = _gameBoard.GridSlots1D[i].Item;
                var targetItem = _gameBoard.GridSlots1D[pos].Item;

                _gameBoard.GridSlots1D[i].Clear();
                _gameBoard.GridSlots1D[i].SetItem(targetItem);
                _gameBoard.GridSlots1D[pos].Clear();
                _gameBoard.GridSlots1D[pos].SetItem(shuffleItem);
            }
            for (int i = 0; i < _gameBoard.GridSlots1D.Count; i++)
            {
                var slot = _gameBoard.GridSlots1D[i];
                seq.Join(slot.Item.Transform.DOMove(_gameBoard.GridToWorldCenter(slot.GridPosition), 1f)
                    .SetEase(Ease.InBack))
                    .OnComplete(() =>
                    {
                        onShuffled?.Invoke();
                    });
            }
        }
    }
}
