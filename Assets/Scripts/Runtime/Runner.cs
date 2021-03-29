using System;
using System.Collections.Generic;
using Enemy;
using Field;
using UnityEngine;

namespace Runtime
{
    public class Runner : MonoBehaviour
    {
        private List<IController> _controllers;
        private bool _isRunning = false;

        private void Update()
        {
            if (!_isRunning)
            {
                return;
            }

            TickControllers();
        }

        public void StartRunning()
        {
            CreateControllers();
            OnStartControllers();
            _isRunning = true;
        }

        public void StopRunning()
        {
            _isRunning = false;
            OnStopControllers();
        }

        private void CreateControllers()
        {
            _controllers = new List<IController>
            {
                new SpawnController(Game.CurrentLevel.SpawnWaves),
                new MovementCursorController(Game.Player.MovementCursor),
                new MovementController(),
                new DeathController()
            };
        }

        private void OnStartControllers()
        {
            foreach (IController controller in _controllers)
            {
                controller.OnStart();
            }
        }

        private void TickControllers()
        {
            foreach (IController controller in _controllers)
            {
                controller.Tick();
            }
        }

        private void OnStopControllers()
        {
            foreach (IController controller in _controllers)
            {
                controller.OnStop();
            }
        }
    }
}