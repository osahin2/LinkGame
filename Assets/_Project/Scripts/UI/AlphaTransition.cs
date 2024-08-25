using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class AlphaTransition : MonoBehaviour
    {
        [SerializeField] private Image _alphaImage;
        [SerializeField] private float _transitionTime;

        public void Transition(float fadeAmount, Action onFade = null, float delay = 0f)
        {
            _alphaImage.DOFade(fadeAmount, _transitionTime)
                .SetDelay(delay)
                .OnComplete(() =>
                {
                    onFade?.Invoke();
                });
        }
    }
}
