﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.iOS;
using Blackjack.Game;

namespace Blackjack.iOS
{
    public static class Application
    {
        public static void Main(string[] args) => GameApplication.Main(new BlackjackGame());
    }
}