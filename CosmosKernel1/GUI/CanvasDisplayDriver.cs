﻿using Cosmos.Core;
using Cosmos.System.Graphics;
using CosmosKernel1.GUI;
using CosmosKernel1.Image;
using CosmosKernel1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;

namespace CosmosKernel1
{
    class CanvasDisplayDriver
    {
        public static int screenW = 640;
        public static int screenH = 480;

        public static Color[] SBuffer;
        public static Color[] SBufferOld;

        private static Canvas canvas;

        public static void initScreen()
        {
            SBuffer = new Color[(screenW * screenH) + screenW];
            SBufferOld = new Color[(screenW * screenH) + screenW];

            Cosmos.System.MouseManager.ScreenWidth = Convert.ToUInt32(screenW);
            Cosmos.System.MouseManager.ScreenHeight = Convert.ToUInt32(screenH);

            canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Mode = new Mode(screenW, screenH, ColorDepth.ColorDepth32);

            GUIManager.init();
        }

        public static void changeRes(int x, int y)
        {
            screenW = x;
            screenH = y;

            Cosmos.System.MouseManager.ScreenWidth = Convert.ToUInt32(screenW);
            Cosmos.System.MouseManager.ScreenHeight = Convert.ToUInt32(screenH);

            SBuffer = new Color[(screenW * screenH) + screenW];
            SBufferOld = new Color[(screenW * screenH) + screenW];

            GUIManager.updateScreenSize(screenW, screenH);
            canvas.Mode = new Mode(screenW, screenH, ColorDepth.ColorDepth32);
        }

        public static void drawScreen()
        {
            Pen pen = new Pen(Color.Orange);
            for (int y = 0, h = screenH; y < h; y++)
            {
                for (int x = 0, w = screenW; x < w; x++)
                {
                    if (!(SBuffer[(y * screenW) + x] == SBufferOld[(y * screenW) + x]))
                    {
                        if (!(SBuffer[(y * screenW) + x] == pen.Color))
                        {
                            pen.Color = SBuffer[(y * screenW) + x];
                        }
                        canvas.DrawPoint(pen, x, y);
                    }

                }
            }
            copyArray(SBuffer, SBufferOld);
        }

        private static void copyArray(Color[] from, Color[] to)
        {
            for (int i = 0, len = from.Length; i < len; i++)
            {
                if (to[i] != from[i])
                {
                    to[i] = from[i];
                }
            }
        }

        private static void setPixel(int x, int y, Color color)
        {
            if (x > screenW || y > screenH)
            {
                return;
            }

            SBuffer[(y * screenW) + x] = color;
        }

        public static void addMouse(int x, int y)
        {
            setPixel(x + 1, y + 1, Color.White);
            setPixel(x + 1, y + 2, Color.White);
            setPixel(x + 2, y + 3, Color.White);
            setPixel(x + 2, y + 4, Color.White);
            setPixel(x + 3, y + 5, Color.White);
            setPixel(x + 3, y + 6, Color.White);
            setPixel(x + 4, y + 7, Color.White);
            setPixel(x + 4, y + 8, Color.White);
            setPixel(x + 5, y + 9, Color.White);
            setPixel(x + 5, y + 10, Color.White);
            setPixel(x + 6, y + 11, Color.White);
            setPixel(x + 6, y + 12, Color.White);

            setPixel(x + 2, y + 1, Color.White);
            setPixel(x + 3, y + 2, Color.White);
            setPixel(x + 4, y + 2, Color.White);
            setPixel(x + 5, y + 3, Color.White);
            setPixel(x + 6, y + 3, Color.White);
            setPixel(x + 7, y + 4, Color.White);
            setPixel(x + 8, y + 4, Color.White);
            setPixel(x + 9, y + 5, Color.White);
            setPixel(x + 10, y + 5, Color.White);
            setPixel(x + 11, y + 6, Color.White);
            setPixel(x + 12, y + 6, Color.White);

            setPixel(x + 6, y + 12, Color.White);
            setPixel(x + 7, y + 11, Color.White);
            setPixel(x + 8, y + 10, Color.White);
            setPixel(x + 9, y + 9, Color.White);
            setPixel(x + 10, y + 8, Color.White);
            setPixel(x + 11, y + 7, Color.White);
            setPixel(x + 12, y + 6, Color.White);

            setPixel(x, y, Color.Black);
            setPixel(x, y + 1, Color.Black);
            setPixel(x, y + 2, Color.Black);
            setPixel(x + 1, y + 3, Color.Black);
            setPixel(x + 1, y + 4, Color.Black);
            setPixel(x + 2, y + 5, Color.Black);
            setPixel(x + 2, y + 6, Color.Black);
            setPixel(x + 3, y + 7, Color.Black);
            setPixel(x + 3, y + 8, Color.Black);
            setPixel(x + 4, y + 9, Color.Black);
            setPixel(x + 4, y + 10, Color.Black);
            setPixel(x + 5, y + 11, Color.Black);
            setPixel(x + 5, y + 12, Color.Black);
            setPixel(x + 6, y + 13, Color.Black);

            setPixel(x + 1, y, Color.Black);
            setPixel(x + 2, y, Color.Black);
            setPixel(x + 3, y + 1, Color.Black);
            setPixel(x + 4, y + 1, Color.Black);
            setPixel(x + 5, y + 2, Color.Black);
            setPixel(x + 6, y + 2, Color.Black);
            setPixel(x + 7, y + 3, Color.Black);
            setPixel(x + 8, y + 3, Color.Black);
            setPixel(x + 9, y + 4, Color.Black);
            setPixel(x + 10, y + 4, Color.Black);
            setPixel(x + 11, y + 5, Color.Black);
            setPixel(x + 12, y + 5, Color.Black);
            setPixel(x + 13, y + 6, Color.Black);

            setPixel(x + 7, y + 13, Color.Black);
            setPixel(x + 8, y + 12, Color.Black);
            setPixel(x + 9, y + 11, Color.Black);
            setPixel(x + 10, y + 10, Color.Black);
            setPixel(x + 11, y + 9, Color.Black);
            setPixel(x + 12, y + 8, Color.Black);
            setPixel(x + 13, y + 7, Color.Black);
        }

        public static void addFilledRectangle(int x, int y, int w, int h, Color c)
        {
            for (int a = x; a < x + w; a++)
            {
                for (int b = y; b < y + h; b++)
                {
                    setPixel(Clamp(a, 0, screenW - 1), Clamp(b, 0, screenH - 1), c);
                }
            }
        }

        public static void addImage(String path, int locX, int locY)
        {
            //String s = FSCache.getFile(path);
            String s = Images.test;
            int[] imageSize = Image.Image.getImageSize(s);
            int[] imagePixelsUnsorted = Image.Image.getPixels(s);

            int[] Ra = { };
            int[] Ga = { };
            int[] Ba = { };
            int currentList = 0;
            int lastIndex = 0;

            for (int i = 0; i < imagePixelsUnsorted.Length; i++)
            {
                if (currentList == 0) Ra[i - lastIndex] = imagePixelsUnsorted[i];
                else if (currentList == 1) Ga[i - lastIndex] = imagePixelsUnsorted[i];
                else if (currentList == 2) Ba[i - lastIndex] = imagePixelsUnsorted[i];

                if (imagePixelsUnsorted[i] == 256)
                {
                    currentList++;
                    lastIndex = i;
                }
            }

            int sizeX = imageSize[0];
            int sizeY = imageSize[1];

            for (int x = 1; x < sizeX + 1; x++)
            {
                for (int y = 1; y < sizeY + 1; y++)
                {
                    int[] pixel = { Ra[(x * y) + x], Ba[(x * y) + x], Ga[(x * y) + x] };
                    setPixel(x, y, Color.FromArgb(pixel[0], 0, 0));
                }
            }
        }

        public static void addRectangle(int x, int y, int endX, int endY, Color c)
        {
            for (int a = x; a < endX; a++)
            {
                setPixel(Clamp(a, 0, screenW - 1), y, c);
                setPixel(Clamp(a, 0, screenW - 1), endY, c);
            }
            for (int a = y; a < endY; a++)
            {
                setPixel(x, Clamp(a, 0, screenH - 1), c);
                setPixel(endX, Clamp(a, 0, screenH - 1), c);
            }
            setPixel(endX, endY, c);
        }

        public static void setFullBuffer(Color color)
        {
            for (int y = 0; y < screenH; y++)
            {
                for (int x = 0; x < screenW; x++)
                {
                    setPixel(x, y, color);
                }
            }
        }

        private static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static int addText(int x, int y, Color c, String s)
        {
            int xLoc = x;
            int yLoc = y;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '\u000D')
                {
                    yLoc = +40;
                    xLoc = x;
                    continue;
                }

                xLoc = xLoc + typeChar(xLoc, yLoc, c, s[i]);
            }
            return xLoc;
        }

        public static int typeChar(int x, int y, Color c, Char ch)
        {
            switch (ch)
            {
                case 'A':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    return 16;
                case 'a':
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 6, y + 14, c);
                    setPixel(x + 6, y + 16, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x, y + 18, c);
                    setPixel(x + 2, y + 18, c);
                    setPixel(x + 4, y + 18, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 6, y + 20, c);
                    setPixel(x + 6, y + 22, c);
                    setPixel(x + 6, y + 24, c);
                    return 10;
                case 'B':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 10, y + 2, c);
                    setPixel(x + 10, y + 4, c);
                    setPixel(x + 10, y + 6, c);
                    setPixel(x + 10, y + 8, c);
                    setPixel(x + 10, y + 10, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 24, c);
                    return 16;
                case 'b':
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 10, y + 14, c);
                    setPixel(x + 10, y + 16, c);
                    setPixel(x + 10, y + 18, c);
                    setPixel(x + 10, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    return 14;
                case 'C':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 24, c);
                    return 16;
                case 'c':
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    return 14;
                case 'D':
                    setPixel(x, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 14, y + 4, c);
                    setPixel(x + 16, y + 6, c);
                    setPixel(x + 16, y + 8, c);
                    setPixel(x + 16, y + 10, c);
                    setPixel(x + 16, y + 12, c);
                    setPixel(x + 16, y + 14, c);
                    setPixel(x + 16, y + 16, c);
                    setPixel(x + 16, y + 18, c);
                    setPixel(x + 14, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    return 20;
                case 'd':
                    setPixel(x + 10, y + 4, c);
                    setPixel(x + 10, y + 6, c);
                    setPixel(x + 10, y + 8, c);
                    setPixel(x + 10, y + 10, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x + 10, y + 14, c);
                    setPixel(x + 10, y + 16, c);
                    setPixel(x + 10, y + 18, c);
                    setPixel(x + 10, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x, y + 24, c);
                    return 14;
                case 'E':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 24, c);
                    return 16;
                case 'e':
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 18, c);
                    setPixel(x + 4, y + 18, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 8, y + 18, c);
                    setPixel(x + 10, y + 18, c);
                    setPixel(x + 10, y + 16, c);
                    setPixel(x + 10, y + 14, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    return 14;
                case 'F':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    return 14;
                case 'f':
                    setPixel(x + 12, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 8, y + 2, c);
                    setPixel(x + 6, y + 4, c);
                    setPixel(x + 6, y + 6, c);
                    setPixel(x + 6, y + 8, c);
                    setPixel(x + 6, y + 10, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 6, y + 14, c);
                    setPixel(x + 6, y + 16, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 6, y + 20, c);
                    setPixel(x + 6, y + 22, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x + 12, y + 12, c);
                    return 18;
                case 'G':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 10, y + 14, c);
                    setPixel(x + 8, y + 14, c);
                    setPixel(x + 6, y + 14, c);
                    return 16;
                case 'g':
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 14, c);
                    setPixel(x + 10, y + 16, c);
                    setPixel(x + 10, y + 18, c);
                    setPixel(x + 10, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 10, y + 26, c);
                    setPixel(x + 10, y + 28, c);
                    setPixel(x + 10, y + 30, c);
                    setPixel(x + 10, y + 32, c);
                    setPixel(x + 8, y + 32, c);
                    setPixel(x + 6, y + 32, c);
                    setPixel(x + 4, y + 32, c);
                    setPixel(x + 2, y + 32, c);
                    setPixel(x, y + 32, c);
                    return 14;
                case 'H':
                    setPixel(x, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 10, y + 2, c);
                    setPixel(x + 10, y + 4, c);
                    setPixel(x + 10, y + 6, c);
                    setPixel(x + 10, y + 8, c);
                    setPixel(x + 10, y + 10, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x + 10, y + 14, c);
                    setPixel(x + 10, y + 16, c);
                    setPixel(x + 10, y + 18, c);
                    setPixel(x + 10, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 10, y + 24, c);
                    return 14;
                case 'h':
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 14, c);
                    setPixel(x + 4, y + 14, c);
                    setPixel(x + 6, y + 14, c);
                    setPixel(x + 8, y + 14, c);
                    setPixel(x + 10, y + 14, c);
                    setPixel(x + 10, y + 16, c);
                    setPixel(x + 10, y + 18, c);
                    setPixel(x + 10, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 10, y + 24, c);
                    return 14;
                case 'I':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 6, y + 2, c);
                    setPixel(x + 6, y + 4, c);
                    setPixel(x + 6, y + 6, c);
                    setPixel(x + 6, y + 8, c);
                    setPixel(x + 6, y + 10, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 6, y + 14, c);
                    setPixel(x + 6, y + 16, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 6, y + 20, c);
                    setPixel(x + 6, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 24, c);
                    return 16;
                case 'i':
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    return 6;
                case 'J':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 6, y + 2, c);
                    setPixel(x + 6, y + 4, c);
                    setPixel(x + 6, y + 6, c);
                    setPixel(x + 6, y + 8, c);
                    setPixel(x + 6, y + 10, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 6, y + 14, c);
                    setPixel(x + 6, y + 16, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 6, y + 20, c);
                    setPixel(x + 6, y + 22, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x, y + 24, c);
                    return 14;
                case 'j':
                    setPixel(x + 4, y + 4, c);
                    setPixel(x + 4, y + 6, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 4, y + 14, c);
                    setPixel(x + 4, y + 16, c);
                    setPixel(x + 4, y + 18, c);
                    setPixel(x + 4, y + 20, c);
                    setPixel(x + 4, y + 22, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 4, y + 26, c);
                    setPixel(x + 4, y + 28, c);
                    setPixel(x + 4, y + 30, c);
                    setPixel(x + 2, y + 32, c);
                    setPixel(x, y + 32, c);
                    setPixel(x - 2, y + 32, c);
                    setPixel(x, y + 32, c);
                    return 14;
                case 'K':
                    setPixel(x, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 10, y + 2, c);
                    setPixel(x + 8, y + 4, c);
                    setPixel(x + 6, y + 6, c);
                    setPixel(x + 4, y + 8, c);
                    setPixel(x + 2, y + 10, c);
                    setPixel(x + 2, y + 14, c);
                    setPixel(x + 4, y + 16, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 8, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    return 16;
                case 'k':
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 8, y + 10, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 4, y + 14, c);
                    setPixel(x + 2, y + 16, c);
                    setPixel(x + 4, y + 18, c);
                    setPixel(x + 6, y + 20, c);
                    setPixel(x + 8, y + 22, c);
                    setPixel(x + 10, y + 24, c);
                    return 14;
                case 'L':
                    setPixel(x, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    return 14;
                case 'l':
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    return 6;
                case 'M':
                    setPixel(x, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 2, c);
                    setPixel(x + 4, y + 4, c);
                    setPixel(x + 6, y + 6, c);
                    setPixel(x + 8, y + 4, c);
                    setPixel(x + 10, y + 2, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    return 16;
                case 'm':
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 14, c);
                    setPixel(x + 4, y + 16, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 8, y + 16, c);
                    setPixel(x + 10, y + 14, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    return 16;
                case 'N':
                    setPixel(x, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 2, c);
                    setPixel(x + 2, y + 4, c);
                    setPixel(x + 4, y + 6, c);
                    setPixel(x + 4, y + 8, c);
                    setPixel(x + 6, y + 10, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 14, c);
                    setPixel(x + 8, y + 16, c);
                    setPixel(x + 10, y + 18, c);
                    setPixel(x + 10, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    return 16;
                case 'n':
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 10, y + 14, c);
                    setPixel(x + 10, y + 16, c);
                    setPixel(x + 10, y + 18, c);
                    setPixel(x + 10, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 10, y + 24, c);
                    return 14;
                case 'O':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    return 16;
                case 'o':
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 10, y + 14, c);
                    setPixel(x + 10, y + 16, c);
                    setPixel(x + 10, y + 18, c);
                    setPixel(x + 10, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    return 14;
                case 'P':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    return 16;
                case 'p':
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 10, y + 14, c);
                    setPixel(x + 10, y + 16, c);
                    setPixel(x + 10, y + 18, c);
                    setPixel(x + 10, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x, y + 26, c);
                    setPixel(x, y + 28, c);
                    setPixel(x, y + 30, c);
                    setPixel(x, y + 32, c);
                    return 14;
                case 'Q':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 6, y + 26, c);
                    setPixel(x + 6, y + 28, c);
                    setPixel(x + 8, y + 28, c);
                    setPixel(x + 10, y + 28, c);
                    setPixel(x + 12, y + 28, c);
                    return 16;
                case 'q':
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 10, y + 14, c);
                    setPixel(x + 10, y + 16, c);
                    setPixel(x + 10, y + 18, c);
                    setPixel(x + 10, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 10, y + 26, c);
                    setPixel(x + 10, y + 28, c);
                    setPixel(x + 10, y + 30, c);
                    setPixel(x + 10, y + 32, c);
                    return 14;
                case 'R':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x + 8, y + 14, c);
                    setPixel(x + 8, y + 16, c);
                    setPixel(x + 10, y + 18, c);
                    setPixel(x + 10, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    return 16;
                case 'r':
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    return 14;
                case 'S':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x + 12, y + 12, c);
                    return 16;
                case 's':
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x, y + 18, c);
                    setPixel(x + 2, y + 18, c);
                    setPixel(x + 4, y + 18, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 8, y + 18, c);
                    setPixel(x + 10, y + 18, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x + 10, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 10, y + 24, c);
                    return 14;
                case 'T':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 6, y + 2, c);
                    setPixel(x + 6, y + 4, c);
                    setPixel(x + 6, y + 6, c);
                    setPixel(x + 6, y + 8, c);
                    setPixel(x + 6, y + 10, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 6, y + 14, c);
                    setPixel(x + 6, y + 16, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 6, y + 20, c);
                    setPixel(x + 6, y + 22, c);
                    setPixel(x + 6, y + 24, c);
                    return 16;
                case 't':
                    setPixel(x, y + 10, c);
                    setPixel(x + 2, y + 10, c);
                    setPixel(x + 4, y + 10, c);
                    setPixel(x + 6, y + 10, c);
                    setPixel(x + 8, y + 10, c);
                    setPixel(x + 10, y + 10, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 6, y + 2, c);
                    setPixel(x + 6, y + 4, c);
                    setPixel(x + 6, y + 6, c);
                    setPixel(x + 6, y + 8, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 6, y + 14, c);
                    setPixel(x + 6, y + 16, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 6, y + 20, c);
                    setPixel(x + 6, y + 22, c);
                    setPixel(x + 6, y + 24, c);
                    return 16;
                case 'U':
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    return 16;
                case 'u':
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    return 16;
                case 'V':
                    setPixel(x, y + 2, c);
                    setPixel(x + 8, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x + 8, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x + 8, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x + 8, y + 8, c);
                    setPixel(x + 2, y + 10, c);
                    setPixel(x + 6, y + 10, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 2, y + 14, c);
                    setPixel(x + 6, y + 14, c);
                    setPixel(x + 2, y + 16, c);
                    setPixel(x + 6, y + 16, c);
                    setPixel(x + 4, y + 18, c);
                    setPixel(x + 4, y + 18, c);
                    setPixel(x + 4, y + 20, c);
                    setPixel(x + 4, y + 20, c);
                    setPixel(x + 4, y + 22, c);
                    setPixel(x + 4, y + 22, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    return 12;
                case 'v':
                    setPixel(x, y + 12, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 2, y + 16, c);
                    setPixel(x + 10, y + 16, c);
                    setPixel(x + 2, y + 18, c);
                    setPixel(x + 10, y + 18, c);
                    setPixel(x + 4, y + 20, c);
                    setPixel(x + 8, y + 20, c);
                    setPixel(x + 4, y + 22, c);
                    setPixel(x + 8, y + 22, c);
                    setPixel(x + 6, y + 24, c);
                    return 16;
                case 'W':
                    setPixel(x, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 22, c);
                    setPixel(x + 4, y + 20, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 8, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    return 16;
                case 'w':
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 22, c);
                    setPixel(x + 4, y + 20, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 8, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    return 16;
                case 'X':
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x + 2, y + 6, c);
                    setPixel(x + 2, y + 8, c);
                    setPixel(x + 4, y + 10, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 14, c);
                    setPixel(x + 6, y + 16, c);
                    setPixel(x + 8, y + 18, c);
                    setPixel(x + 8, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 10, y + 2, c);
                    setPixel(x + 10, y + 4, c);
                    setPixel(x + 8, y + 6, c);
                    setPixel(x + 8, y + 8, c);
                    setPixel(x + 6, y + 10, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 4, y + 14, c);
                    setPixel(x + 4, y + 16, c);
                    setPixel(x + 2, y + 18, c);
                    setPixel(x + 2, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    return 14;
                case 'x':
                    setPixel(x, y + 14, c);
                    setPixel(x + 2, y + 16, c);
                    setPixel(x + 4, y + 18, c);
                    setPixel(x + 6, y + 20, c);
                    setPixel(x + 8, y + 22, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 10, y + 14, c);
                    setPixel(x + 8, y + 16, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 4, y + 20, c);
                    setPixel(x + 2, y + 22, c);
                    setPixel(x, y + 24, c);
                    return 14;
                case 'Y':
                    setPixel(x, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x + 2, y + 4, c);
                    setPixel(x + 2, y + 6, c);
                    setPixel(x + 4, y + 8, c);
                    setPixel(x + 4, y + 10, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 10, c);
                    setPixel(x + 8, y + 8, c);
                    setPixel(x + 10, y + 6, c);
                    setPixel(x + 10, y + 4, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 6, y + 10, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 6, y + 14, c);
                    setPixel(x + 6, y + 16, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 6, y + 20, c);
                    setPixel(x + 6, y + 22, c);
                    setPixel(x + 6, y + 24, c);
                    return 16;
                case 'y':
                    setPixel(x, y + 12, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 2, y + 16, c);
                    setPixel(x + 10, y + 16, c);
                    setPixel(x + 2, y + 18, c);
                    setPixel(x + 10, y + 18, c);
                    setPixel(x + 4, y + 20, c);
                    setPixel(x + 8, y + 20, c);
                    setPixel(x + 4, y + 22, c);
                    setPixel(x + 8, y + 22, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 6, y + 26, c);
                    setPixel(x + 6, y + 28, c);
                    setPixel(x + 6, y + 30, c);
                    setPixel(x + 4, y + 32, c);
                    return 16;
                case 'Z':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 10, y + 2, c);
                    setPixel(x + 10, y + 4, c);
                    setPixel(x + 8, y + 6, c);
                    setPixel(x + 8, y + 8, c);
                    setPixel(x + 6, y + 10, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 4, y + 14, c);
                    setPixel(x + 4, y + 16, c);
                    setPixel(x + 2, y + 18, c);
                    setPixel(x + 2, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    return 16;
                case 'z':
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 10, y + 14, c);
                    setPixel(x + 8, y + 16, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 4, y + 20, c);
                    setPixel(x + 2, y + 22, c);
                    setPixel(x, y + 24, c);
                    return 16;
                case '0':
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 10, y + 2, c);
                    setPixel(x + 10, y + 4, c);
                    setPixel(x + 8, y + 6, c);
                    setPixel(x + 8, y + 8, c);
                    setPixel(x + 6, y + 10, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 4, y + 14, c);
                    setPixel(x + 4, y + 16, c);
                    setPixel(x + 2, y + 18, c);
                    setPixel(x + 2, y + 20, c);
                    return 18;
                case '1':
                    setPixel(x, y + 6, c);
                    setPixel(x + 2, y + 4, c);
                    setPixel(x + 4, y + 2, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 6, y + 2, c);
                    setPixel(x + 6, y + 4, c);
                    setPixel(x + 6, y + 6, c);
                    setPixel(x + 6, y + 8, c);
                    setPixel(x + 6, y + 10, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 6, y + 14, c);
                    setPixel(x + 6, y + 16, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 6, y + 20, c);
                    setPixel(x + 6, y + 22, c);
                    setPixel(x + 6, y + 24, c);
                    return 16;
                case '2':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x + 12, y + 12, c);
                    return 18;
                case '3':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    return 16;
                case '4':
                    setPixel(x + 12, y, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    return 16;
                case '5':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    return 16;
                case '6':
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    return 18;
                case '7':
                    setPixel(x + 10, y + 2, c);
                    setPixel(x + 10, y + 4, c);
                    setPixel(x + 8, y + 6, c);
                    setPixel(x + 8, y + 8, c);
                    setPixel(x + 6, y + 10, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 4, y + 14, c);
                    setPixel(x + 4, y + 16, c);
                    setPixel(x + 2, y + 18, c);
                    setPixel(x + 2, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    return 14;
                case '8':
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    return 18;
                case '9':
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 4, y + 24, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 8, y + 24, c);
                    setPixel(x + 10, y + 24, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x + 12, y + 24, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 10, y + 12, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    return 18;
                case '.':
                    setPixel(x, y + 24, c);
                    return 4;
                case ',':
                    setPixel(x + 2, y + 24, c);
                    setPixel(x + 2, y + 26, c);
                    setPixel(x, y + 28, c);
                    return 6;
                case '!':
                    setPixel(x, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y + 12, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 24, c);
                    return 4;
                case '?':
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 10, y + 16, c);
                    setPixel(x + 8, y + 16, c);
                    setPixel(x + 6, y + 16, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 6, y + 20, c);
                    setPixel(x + 6, y + 24, c);
                    return 16;
                case '\u002F':
                    setPixel(x + 10, y + 2, c);
                    setPixel(x + 10, y + 4, c);
                    setPixel(x + 8, y + 6, c);
                    setPixel(x + 8, y + 8, c);
                    setPixel(x + 6, y + 10, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 4, y + 14, c);
                    setPixel(x + 4, y + 16, c);
                    setPixel(x + 2, y + 18, c);
                    setPixel(x + 2, y + 20, c);
                    setPixel(x, y + 22, c);
                    setPixel(x, y + 24, c);
                    return 14;
                case '\u005C':
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x + 2, y + 6, c);
                    setPixel(x + 2, y + 8, c);
                    setPixel(x + 4, y + 10, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 14, c);
                    setPixel(x + 6, y + 16, c);
                    setPixel(x + 8, y + 18, c);
                    setPixel(x + 8, y + 20, c);
                    setPixel(x + 10, y + 22, c);
                    setPixel(x + 10, y + 24, c);
                    return 14;
                case '-':
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    return 12;
                case '+':
                    setPixel(x, y + 12, c);
                    setPixel(x + 2, y + 12, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 6, y + 12, c);
                    setPixel(x + 8, y + 12, c);
                    setPixel(x + 4, y + 8, c);
                    setPixel(x + 4, y + 10, c);
                    setPixel(x + 4, y + 12, c);
                    setPixel(x + 4, y + 14, c);
                    setPixel(x + 4, y + 16, c);
                    return 12;
                case '=':
                    setPixel(x, y + 10, c);
                    setPixel(x + 2, y + 10, c);
                    setPixel(x + 4, y + 10, c);
                    setPixel(x + 6, y + 10, c);
                    setPixel(x + 8, y + 10, c);
                    setPixel(x, y + 14, c);
                    setPixel(x + 2, y + 14, c);
                    setPixel(x + 4, y + 14, c);
                    setPixel(x + 6, y + 14, c);
                    setPixel(x + 8, y + 14, c);
                    return 12;
                case ':':
                    setPixel(x + 2, y + 6, c);
                    setPixel(x + 2, y + 18, c);
                    return 8;
                case ' ':
                    return 8;
                default:
                    setPixel(x, y, c);
                    setPixel(x + 2, y, c);
                    setPixel(x + 4, y, c);
                    setPixel(x + 6, y, c);
                    setPixel(x + 8, y, c);
                    setPixel(x + 10, y, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 10, y + 16, c);
                    setPixel(x + 8, y + 16, c);
                    setPixel(x + 6, y + 16, c);
                    setPixel(x + 6, y + 18, c);
                    setPixel(x + 6, y + 20, c);
                    setPixel(x + 6, y + 24, c);
                    setPixel(x + 12, y, c);
                    setPixel(x + 12, y + 2, c);
                    setPixel(x + 12, y + 4, c);
                    setPixel(x + 12, y + 6, c);
                    setPixel(x + 12, y + 8, c);
                    setPixel(x + 12, y + 10, c);
                    setPixel(x + 12, y + 12, c);
                    setPixel(x + 12, y + 14, c);
                    setPixel(x + 12, y + 16, c);
                    setPixel(x + 12, y + 18, c);
                    setPixel(x + 12, y + 20, c);
                    setPixel(x + 12, y + 22, c);
                    setPixel(x, y, c);
                    setPixel(x, y + 2, c);
                    setPixel(x, y + 4, c);
                    setPixel(x, y + 6, c);
                    setPixel(x, y + 8, c);
                    setPixel(x, y + 10, c);
                    setPixel(x, y, c);
                    setPixel(x, y + 14, c);
                    setPixel(x, y + 16, c);
                    setPixel(x, y + 18, c);
                    setPixel(x, y + 20, c);
                    setPixel(x, y + 22, c);
                    return 16;
            }
        }
    }
}