﻿using System;
using TrailCommon;

namespace TrailEntities
{
    public sealed class TravelMode : GameMode<TravelCommands>, ITravel
    {
        public override ModeType Mode
        {
            get { return ModeType.Travel; }
        }

        public void Hunt()
        {
            throw new NotImplementedException();
        }

        public void Rest()
        {
            throw new NotImplementedException();
        }

        public void Trade()
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
        ///     Called by the active game mode when the text user interface is called. This will create a string builder with all
        ///     the data and commands that represent the concrete handler for this game mode.
        /// </summary>
        protected override string OnGetModeTUI()
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