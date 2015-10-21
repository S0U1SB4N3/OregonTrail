﻿using System;
using TrailCommon;

namespace TrailEntities
{
    public sealed class SettlementMode : GameMode, ISettlement
    {
        private readonly bool _canRest;
        private readonly IStore _store;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:TrailEntities.GameMode" /> class.
        /// </summary>
        public SettlementMode()
        {
            _canRest = true;
            _store = new StoreMode();
        }

        public override ModeType Mode
        {
            get { return ModeType.Settlement; }
        }

        public bool CanRest
        {
            get { return _canRest; }
        }

        public IStore Store
        {
            get { return _store; }
        }

        public void GoToStore()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Fired by simulation when it wants to request latest text user interface data for the game mode, this is used to
        ///     display to user console specific information about what the simulation wants.
        /// </summary>
        public override string GetTUI()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Fired by game simulation system timers timer which runs on same thread, only fired for active (last added), or
        ///     top-most game mode.
        /// </summary>
        public override void TickMode()
        {
            throw new NotImplementedException();
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