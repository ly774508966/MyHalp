﻿// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using UnityEngine;

namespace MyHalp
{
    /// <summary>
    /// MyColor class - helps with colors.
    /// </summary>
    public struct MyColor
    {
        public float R, G, B, A;

        public MyColor(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public MyColor(Color color)
        {
            R = color.r;
            B = color.b;
            G = color.g;
            A = color.a;
        }

        public Color GetColor()
        {
            return new Color(R, G, B, A);
        }
        
        public static implicit operator Color(MyColor color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }

        public static implicit operator MyColor(Color color)
        {
            return new MyColor(color.r, color.g, color.b, color.a);
        }

        public static MyColor Red()
        {
            return Color.red;
        }

        public static MyColor Black()
        {
            return Color.black;
        }
    }
}
