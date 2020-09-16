﻿using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.Core;
using Cosmos.HAL;
using System.IO;
using Cosmos.System.Graphics;
using System.Threading;
using Cosmos.Core.IOGroup;
using System.Drawing;
using Cosmos.System;
using Console = System.Console;

namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {
        private static Boolean graphicsMode = false;
        private static Boolean fsMode = false;
        private static String cd = "0:\\";
        public static Sys.FileSystem.CosmosVFS fs;

        protected override void BeforeRun()
        {
            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

            WaitSeconds(1);

            Console.Clear();
            Console.WriteLine("    ##### ##### ####  ##### #    #####     ##### #####");
            Console.WriteLine("   #     #   # #  #  #   # #      #       #   # #    ");
            Console.WriteLine("  #     #   # ####  ##### #      #       #   # #####");
            Console.WriteLine(" #     #   # #   # #   # #      #       #   #     #");
            Console.WriteLine("##### ##### ##### #   # #####  #       ##### #####");
            Console.WriteLine("Enter 'help' for help.");
            Cosmos.System.PCSpeaker.Beep();
        }

        protected override void Run()
        {
            if (graphicsMode == true)
            {
                GUIManager.tick();
            } else if (fsMode == true)
            {
                Console.Write(cd + " >");
                processFSConsole(Console.ReadLine());
            } else {
                Console.Write("> ");
                processConsole(Console.ReadLine());
            }
        }

        private void processConsole(String input)
        {
            if (input == "gui")
            {
                graphicsMode = true;

                GUIManager.init();
                DisplayDriver.initScreen();

                Cosmos.System.MouseManager.ScreenWidth = Convert.ToUInt32(DisplayDriver.screenW);
                Cosmos.System.MouseManager.ScreenHeight = Convert.ToUInt32(DisplayDriver.screenH);

                return;

            } else if (input == "cpu")
            {
                Console.WriteLine("CPU: " + Cosmos.Core.ProcessorInformation.GetVendorName() + " @ " + (Cosmos.Core.CPU.GetCPUCycleSpeed() / 1000) + "Ghz");
                return;
            } else if (input == "mem")
            {
                Console.WriteLine("RAM: " + (Cosmos.Core.CPU.GetAmountOfRAM() < 1024 ? Cosmos.Core.CPU.GetAmountOfRAM() + " MB" : Cosmos.Core.CPU.GetAmountOfRAM() / 1024.00 + " GB"));
                return;
            } else if (input == "time")
            {
                Console.WriteLine("Current Date: " + Cosmos.HAL.RTC.Month + "/" + Cosmos.HAL.RTC.DayOfTheMonth + "/" + Cosmos.HAL.RTC.Year + " " + Cosmos.HAL.RTC.Hour + ":" + Cosmos.HAL.RTC.Minute + ":" + Cosmos.HAL.RTC.Second.ToString().PadLeft(2, '0'));
                return;
            } else if (input == "help")
            {
                Console.WriteLine("Help:");
                Console.WriteLine("    cpu: Returns CPU model and speed.");
                Console.WriteLine("    mem: Returns Memory size.");
                Console.WriteLine("    time: Returns current time according to the BIOS.");
                Console.WriteLine("    gui: Activates the GUI.");
                Console.WriteLine("    help: Returns this message.");
                return;
            } else if (input == "fs")
            {
                fsMode = true;

                return;
            } else
            {
                Console.WriteLine("Unknown command! Use 'help' for help!");
                return;
            }
        }

        private static void processFSConsole(String input)
        {
            if (input == "exit")
            {
                fsMode = false;
            }

            var directory_list = fs.GetDirectoryListing(cd);
            if (input == "ls")
            {
                foreach (var directoryEntry in directory_list)
                {
                    if (directoryEntry.mEntryType == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.File)
                    {
                        Console.WriteLine(" - " + directoryEntry.mName);
                    } else
                    {
                        Console.WriteLine(directoryEntry.mName);
                    }
                }
            } else if (input == "cd")
            {
                String[] inputSplit = input.Split(" ");
                if (inputSplit.Length > 1)
                {
                    Console.WriteLine("go");
                    String[] pieces = input.Split(new[] { ',' }, 2);
                    Console.WriteLine("go 2");
                    if (Directory.Exists(pieces[1]))
                    {
                        Console.WriteLine("Exists");
                    } else
                    {
                        Console.WriteLine("Not Exists");
                    }
                } else
                {
                    Console.WriteLine("Invalid syntax! cd [dir] ");
                }
            } else if (input == "mkdir")
            {
                String[] inputSplit = input.Split(" ");
                if (inputSplit.Length > 1)
                {
                    String dir = "";
                    for (int i = 1; i < inputSplit.Length; i++)
                    {
                        dir = dir + inputSplit[i];
                    }
                    fs.CreateDirectory(cd + dir);
                }
                else
                {
                    Console.WriteLine("Invalid syntax! mkdir [name] ");
                }
            }
        }

        private static void WaitSeconds(int secNum)
        {
            int StartSec = Cosmos.HAL.RTC.Second;
            int EndSec;
            if (StartSec + secNum > 59)
            {
                EndSec = 0;
            }
            else
            {
                EndSec = StartSec + secNum;
            }
            while (Cosmos.HAL.RTC.Second != EndSec)
            {
                ;
            }
        }
    }
}
