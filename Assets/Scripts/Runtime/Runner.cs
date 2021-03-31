using System;
using System.Collections.Generic;
using Enemy;
using Enemy.Movement;
using Enemy.Spawn;
using Field;
using Turret;
using Turret.Spawn;
using Turret.Weapon;
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
                new SpawnController(Game.Player.Grid, Game.CurrentLevel.SpawnWaves),
                new GridRaycastController(Game.Player.GridHolder),
                new TurretChooseController(Game.Player.TurretMarket),
                new TurretSpawnController(Game.Player.Grid, Game.Player.TurretMarket),
                new TurretShootController(),
                new MovementController(),
                new DeathController(Game.Player.Grid)
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