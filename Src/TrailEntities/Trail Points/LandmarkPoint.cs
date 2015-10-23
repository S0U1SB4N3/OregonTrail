﻿using TrailCommon;

namespace TrailEntities
{
    public sealed class LandmarkPoint : PointOfInterest, ILandmarkPoint
    {
        private bool _canRest;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:TrailEntities.PointOfInterest" /> class.
        /// </summary>
        public LandmarkPoint(string name, ulong distanceLength, bool canRest) : base(name, distanceLength)
        {
            _canRest = canRest;
        }

        public override ModeType ModeType
        {
            get { return ModeType.Landmark; }
        }

        public bool CanRest
        {
            get { return _canRest; }
        }
    }
}