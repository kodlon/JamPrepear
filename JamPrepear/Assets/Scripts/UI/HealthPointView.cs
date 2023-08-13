using System;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;

namespace UI
{
    public class HealthPointView : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private List<RectTransform> _healthPointList;
        [SerializeField] private RectTransform _gameOverPanel;

        private void Start()
        {
            _playerController.OnHealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _playerController.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int health)
        {
            if (_healthPointList.Count == 0)
                return;
            
            
            Destroy(_healthPointList[_healthPointList.Count - 1].gameObject);
            _healthPointList.RemoveAt(_healthPointList.Count - 1);
            
            if (_healthPointList.Count == 0)
                _gameOverPanel.gameObject.SetActive(true);
        }
    }
}