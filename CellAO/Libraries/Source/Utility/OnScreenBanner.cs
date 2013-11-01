﻿#region License

// Copyright (c) 2005-2013, CellAO Team
// 
// 
// All rights reserved.
// 
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
// 
// 
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//     * Neither the name of the CellAO Team nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
// 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
// EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
// PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
// PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
// LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// 
// Last modified: 2013-10-30 22:52
// Created:       2013-10-30 17:28

#endregion

namespace Utility
{
    #region Usings ...

    using System;

    #endregion

    public static class OnScreenBanner
    {
        private static void CenteredString(string text, string boundary, ConsoleColor c = ConsoleColor.Black)
        {
            int consoleWidth = Console.WindowWidth;
            // Mono "fix"
            if (consoleWidth == 0)
            {
                // Since Console.WindowWidth doesnt work on mono, lets assume 80 chars
                consoleWidth = 80;
            }

            ConsoleColor oldColor = Console.ForegroundColor;
            int centered = (consoleWidth - text.Length) / 2;
            Console.Write(boundary.PadRight(centered, ' '));

            if (c != ConsoleColor.Black)
            {
                Console.ForegroundColor = c;
            }

            Console.Write(text);
            Console.ForegroundColor = oldColor;
            Console.Write(boundary.PadLeft(consoleWidth - (text.Length + centered), ' '));
        }

        public static void PrintCellAOBanner(ConsoleColor titleColor)
        {
            int consoleWidth = Console.WindowWidth;

            Console.Clear();

            Console.Write("**".PadRight(consoleWidth, '*'));
            CenteredString("", "**");
            CenteredString("CellAO " + AssemblyInfoclass.Title, "**", titleColor);
            CenteredString(AssemblyInfoclass.AssemblyVersion, "**", ConsoleColor.White);
            CenteredString(AssemblyInfoclass.RevisionName, "**", ConsoleColor.DarkGray);
            CenteredString("", "**");
            Console.Write("**".PadRight(consoleWidth, '*'));
        }
    }
}