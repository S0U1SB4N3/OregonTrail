﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using TrailCommon;

namespace TrailGame
{
    public abstract class SimulationApp : ISimulation
    {
        private const int FrameTimesSize = 60;
        private const float TimeScale = 1.0f;
        private double _currentTickTime;
        private Queue<float> _frameTimes;
        private float _frameTimesSum;
        private double _previousTickTime;
        private Randomizer _random;
        private Stopwatch _timePerTick;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:TrailGame.SimulationApp" /> class.
        /// </summary>
        protected SimulationApp()
        {
            _timePerTick = new Stopwatch();
            _frameTimes = new Queue<float>();
            _random = new Randomizer((int) DateTime.Now.Ticks & 0x0000FFF);
            TotalTicks = 0;
            TickPhase = "*";
        }

        public static SimulationApp Instance { get; private set; }

        public string TickPhase { get; private set; }

        public double DeltaTime { get; private set; }

        public float TickDelta { get; private set; }

        public abstract void ChooseProfession();

        public abstract void BuyInitialItems();

        public abstract void ChooseNames();

        public void StartGame()
        {
            NewgameEvent?.Invoke();
        }

        public Randomizer Random
        {
            get { return _random; }
        }

        public uint TotalTicks { get; private set; }

        public int FPS
        {
            get { return (int) (FrameTimesSize/_frameTimesSum*TimeScale); }
        }

        public void SetMode(IGameMode mode)
        {
            ModeChangedEvent?.Invoke(mode.ModeType);
        }

        public event NewGame NewgameEvent;
        public event EndGame EndgameEvent;
        public event ModeChanged ModeChangedEvent;
        public event TickSim TickEvent;

        public void Tick()
        {
            // We do not tick if there is no instance associated with it.
            if (Instance == null)
                throw new InvalidOperationException("Attempted to tick game initializer when instance is null!");

            // Start a new stopwatch timer.
            _timePerTick.Reset();
            _timePerTick.Start();

            // Increase the tick count.
            TotalTicks++;

            TickPhase = TickVisualizer(TickPhase);

            // Fire tick event for any subscribers to see and overrides for inheriting classes.
            TickEvent?.Invoke(TotalTicks);
            OnTick();

            // Stop the timer, and save the elapsed ticks for the operation.
            _timePerTick.Stop();
            CalculateFPS();
        }

        private void CalculateFPS()
        {
            _currentTickTime = _timePerTick.Elapsed.TotalMilliseconds;

            // Calculate amount of time past since the previous frame's time.
            DeltaTime = _currentTickTime - _previousTickTime;

            // Make this frame's time be the previous frame's time (for next loop's calculation).
            _previousTickTime = _currentTickTime;

            // This makes 1 second equal to 1.0f, and half a second 0.5f, and so on.
            TickDelta = (float) DeltaTime/1000.0f;

            if (DeltaTime > 0.0f)
            {
                // While looping here allows the frameTimesSize member to be changed dynamically
                while (_frameTimes.Count >= FrameTimesSize)
                {
                    _frameTimesSum -= _frameTimes.Dequeue();
                }
                while (_frameTimes.Count < FrameTimesSize)
                {
                    _frameTimesSum += (float) DeltaTime;
                    _frameTimes.Enqueue((float) DeltaTime);
                }
            }
        }

        /// <summary>
        ///     Used for showing player that simulation is ticking on main view.
        /// </summary>
        private static string TickVisualizer(string currentPhase)
        {
            switch (currentPhase)
            {
                case @"*":
                    return @"|";
                case @"|":
                    return @"/";
                case @"/":
                    return @"-";
                case @"-":
                    return @"\";
                case @"\":
                    return @"|";
                default:
                    return "*";
            }
        }

        public static void Create(SimulationApp gameInstance)
        {
            // Complain if the simulation app has already been created.
            if (Instance != null)
                throw new InvalidOperationException(
                    "Cannot create new simulation app when instance is not null, please call destroy before creating new simulation app instance.");

            Instance = gameInstance;
        }

        protected static void Destroy()
        {
            // Complain if destroy was awakened for no reason.
            if (Instance == null)
                throw new InvalidOperationException("Unable to destroy game manager, it has not been created yet!");

            // Allow any data structures to save themselves.
            Console.WriteLine("Closing...");
            Instance.OnDestroy();

            // Actually destroy the instance and close the program.
            Instance = null;
        }

        protected virtual void OnDestroy()
        {
            _timePerTick.Stop();
            EndgameEvent?.Invoke();
        }

        protected abstract void OnTick();
    }
}