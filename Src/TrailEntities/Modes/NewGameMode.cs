﻿using System;
using System.Collections.Generic;
using TrailCommon;

namespace TrailEntities
{
    public sealed class NewGameMode : GameMode, INewGame
    {
        private bool _hasChosenNames;
        private bool _hasChosenProfession;
        private bool _hasChosenStartingItems;
        private bool _hasStartedGame;
        private List<string> _playerNames;

        private Profession _playerProfession;

        public override ModeType Mode
        {
            get { return ModeType.NewGame; }
        }

        public void ChooseNames()
        {
            Console.Clear();
            Console.WriteLine("Party leader name?");
            var playerName = GetPlayerName();
            _playerNames.Add(playerName);
            Console.WriteLine("Added: " + playerName);

            Console.WriteLine("Party member two name?");
            playerName = GetPlayerName();
            _playerNames.Add(playerName);
            Console.WriteLine("Added: " + playerName);

            Console.WriteLine("Party member three name?");
            playerName = GetPlayerName();
            _playerNames.Add(playerName);
            Console.WriteLine("Added: " + playerName);

            Console.WriteLine("Party member four name?");
            playerName = GetPlayerName();
            _playerNames.Add(playerName);
            Console.WriteLine("Added: " + playerName);

            Console.WriteLine("Your Party Members:");
            var crewNumber = 1;
            foreach (var name in _playerNames)
            {
                var isLeader = _playerNames.IndexOf(name) == 0;
                if (isLeader)
                {
                    Console.WriteLine(crewNumber + ")." + name + " (leader)");
                }
                else
                {
                    Console.WriteLine(crewNumber + ")." + name);
                }
                crewNumber++;
            }

            Console.WriteLine("Does this look correct? Y/N");
            var namesCorrectResponse = Console.ReadLine();
            if (!string.IsNullOrEmpty(namesCorrectResponse))
            {
                namesCorrectResponse = namesCorrectResponse.Trim().ToLowerInvariant();
                if (namesCorrectResponse.Equals("n"))
                {
                    _playerNames.Clear();
                    ChooseNames();
                }
            }
            else
            {
                _playerNames.Clear();
                ChooseNames();
            }
        }

        public void ChooseProfession()
        {
            _playerProfession = Profession.Banker;
            var professionCount = 1;

            Console.Clear();
            Console.WriteLine("What profession is " + _playerNames[0] + "?");

            foreach (var possibleProfession in Enum.GetValues(typeof (Profession)))
            {
                Console.WriteLine(professionCount + "). " + possibleProfession);
                professionCount++;
            }

            switch (Console.ReadKey(true).KeyChar.ToString())
            {
                case "1":
                    _playerProfession = Profession.Banker;
                    break;
                case "2":
                    _playerProfession = Profession.Carpenter;
                    break;
                case "3":
                    _playerProfession = Profession.Farmer;
                    break;
                default:
                    ChooseProfession();
                    break;
            }

            Console.WriteLine("You selected " + _playerProfession);
            Console.WriteLine("Does this look correct? Y/N");
            var professionCorrectResponse = Console.ReadLine();
            if (!string.IsNullOrEmpty(professionCorrectResponse))
            {
                professionCorrectResponse = professionCorrectResponse.Trim().ToLowerInvariant();
                if (professionCorrectResponse.Equals("n"))
                {
                    _playerProfession = Profession.Banker;
                    ChooseProfession();
                }
            }
            else
            {
                _playerProfession = Profession.Banker;
                ChooseProfession();
            }
        }

        public void BuyInitialItems()
        {
        }

        /// <summary>
        ///     Fired by simulation when it wants to request latest text user interface data for the game mode, this is used to
        ///     display to user console specific information about what the simulation wants.
        /// </summary>
        public override string GetTUI()
        {
            return "CHANGE ME";
        }

        private string GetPlayerName()
        {
            var readLine = Console.ReadLine();
            if (readLine != null)
            {
                readLine = readLine.Trim();
                if (!string.IsNullOrEmpty(readLine) &&
                    !string.IsNullOrWhiteSpace(readLine))
                {
                    return readLine;
                }
            }

            // Just return a random name if there is invalid input.
            string[] names = {"Bob", "Joe", "Sally", "Tim", "Steve"};
            return names[GameSimulationApp.Instance.Random.Next(names.Length)];
        }

        public void StartGame()
        {
            Console.WriteLine("Adding " + _playerNames.Count + " people to vehicle...");
            foreach (var name in _playerNames)
            {
                // First name in list in leader.
                var isLeader = _playerNames.IndexOf(name) == 0;
                GameSimulationApp.Instance.Vehicle.AddPerson(new Person(_playerProfession, name, isLeader));
            }
        }

        public override void TickMode()
        {
            // Every new game has you picking names, profession, and starting items.
            if (!_hasChosenNames && !_hasChosenProfession && !_hasChosenStartingItems && !_hasStartedGame)
            {
                _hasChosenNames = true;
                ChooseNames();
            }
            else if (_hasChosenNames && !_hasChosenProfession && !_hasChosenStartingItems && !_hasStartedGame)
            {
                _hasChosenProfession = true;
                ChooseProfession();
            }
            else if (_hasChosenNames && _hasChosenProfession && !_hasChosenStartingItems && !_hasStartedGame)
            {
                _hasChosenStartingItems = true;
                BuyInitialItems();
            }
            else if (_hasChosenNames && _hasChosenProfession && _hasChosenStartingItems && !_hasStartedGame)
            {
                _hasStartedGame = true;
                StartGame();
            }
        }

        /// <summary>
        ///     Fired by the currently ticking and active game mode in the simulation. Implementation is left entirely up to
        ///     concrete handlers for game mode.
        /// </summary>
        /// <param name="returnedLine">Passed in command from controller, was already checking if null, empty, or whitespace.</param>
        protected override void OnReceiveCommand(string returnedLine)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Fired when this game mode is removed from the list of available and ticked modes in the simulation.
        /// </summary>
        public override void OnModeRemoved()
        {
            throw new NotImplementedException();
        }
    }
}