﻿//
/*
 * Based on:
 * https://github.com/AlekseyMartynov/shazam-for-real
 * https://github.com/marin-m/SongRec
 */

namespace RetopBot.ShazamFolder.Magic
{

    internal readonly struct Landmark
    {

        public readonly int StripeIndex;
        public readonly float InterpolatedBin;
        public readonly float InterpolatedLogMagnitude;

        public Landmark(int stripeIndex, float interpolatedBin, float interpolatedLogMagnitude)
        {
            StripeIndex = stripeIndex;
            InterpolatedBin = interpolatedBin;
            InterpolatedLogMagnitude = interpolatedLogMagnitude;
        }

    }

}
