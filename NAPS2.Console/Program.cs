﻿/*
    NAPS2 (Not Another PDF Scanner 2)
    http://sourceforge.net/projects/naps2/
    
    Copyright (C) 2009       Pavel Sorejs
    Copyright (C) 2012       Michael Adams
    Copyright (C) 2013       Peter De Leeuw
    Copyright (C) 2012-2015  Ben Olden-Cooligan

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using NAPS2.Console.DI;
using NAPS2.Console.Lang.Resources;
using NAPS2.Util;
using Ninject;
using Ninject.Parameters;

namespace NAPS2.Console
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Log.Logger = new NLogLogger();
            var options = new AutomatedScanningOptions();
            try
            {
                if (!CommandLine.Parser.Default.ParseArguments(args, options))
                {
                    return;
                }
                var scanning = KernelManager.Kernel.Get<AutomatedScanning>(new ConstructorArgument("options", options));
                scanning.Execute();
            }
            catch (Exception ex)
            {
                Log.FatalException("An error occurred that caused the console application to close.", ex);
                System.Console.WriteLine(ConsoleResources.UnexpectedError);
            }
            finally
            {
                if (options.WaitForEnter)
                {
                    System.Console.ReadLine();
                }
            }
        }
    }
}
