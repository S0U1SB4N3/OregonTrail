﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BadWater.cs" company="Ron 'Maxwolf' McDowell">
//   ron.mcdowell@gmail.com
// </copyright>
// <summary>
//   The bad water.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TrailSimulation.Event
{
    using System;
    using Game;

    /// <summary>
    ///     The bad water.
    /// </summary>
    [DirectorEvent(EventCategory.Warning, EventExecution.ManualOnly)]
    public sealed class BadWater : EventProduct
    {
        /// <summary>Fired when the event handler associated with this enum type triggers action on target entity. Implementation is
        ///     left completely up to handler.</summary>
        /// <param name="userData">Entities which the event is going to directly affect. This way there is no confusion about
        ///     what entity the event is for. Will require casting to correct instance type from interface instance.</param>
        public override void Execute(RandomEventInfo userData)
        {
            // TODO: All the event warnings are not actually events.. they are location info that can be shown on travel info.
            throw new NotImplementedException();
        }

        /// <summary>Fired when the simulation would like to render the event, typically this is done AFTER executing it but this could
        ///     change depending on requirements of the implementation.</summary>
        /// <param name="userData"></param>
        /// <returns>Text user interface string that can be used to explain what the event did when executed.</returns>
        protected override string OnRender(RandomEventInfo userData)
        {
            throw new NotImplementedException();
        }
    }
}