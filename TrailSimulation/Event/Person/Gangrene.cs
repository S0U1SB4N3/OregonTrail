﻿// Created by Ron 'Maxwolf' McDowell (ron.mcdowell@gmail.com) 
// Timestamp 12/12/2015@6:36 AM

namespace TrailSimulation.Event.Person
{
    using System.Diagnostics.CodeAnalysis;
    using Entity.Person;
    using Module.Director;
    using Prefab;

    /// <summary>
    ///     Localized death and decomposition of body tissue, resulting from either obstructed circulation or bacterial
    ///     infection.
    /// </summary>
    [DirectorEvent(EventCategory.Person)]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class Gangrene : PersonInfect
    {
        /// <summary>Fired after the event has executed and the infection flag set on the person.</summary>
        /// <param name="person">Person whom is now infected by whatever you say they are here.</param>
        /// <returns>Name or type of infection the person is currently affected with.</returns>
        protected override string OnPostInfection(Person person)
        {
            return $"{person.Name} has gangrene.";
        }
    }
}