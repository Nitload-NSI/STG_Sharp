﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StgSharp.Math
{
    public static unsafe partial class Calc
    {

        public const float E = 2.718281828f;
        /// <summary>
        /// Get the value of E ^ x;
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static unsafe float Exp(float x)
        {
            if (x == 1)
            {
                return E;
            }
            if (x == 0)
            {
                return 1;
            }

            bool sign = false;
            if (x<0)
            {
                sign = true;
                x = -x;
            }

            float z = x , t;
            if (x<0.5f)
            {
                t = 1.0f + x * (
                    1.0f + x * (
                    0.5f + x * (
                    0.1666666667f + x * (
                    0.0416666666667f + x *
                    8.333333333e-3f))));
            }
            else
            {
                uint zBit = *(uint*)&z;
                uint zBitAbs = zBit & 0b_0111_1111_1111_1111_1111_1111_1111_1111;
                uint uintm = (zBitAbs >> 23);
                int m = (*(int*)&uintm) - 126;


                uint nBit = (zBitAbs & 0b_0000_0000_0111_1111_1111_1111_1111_1111)
                    + 0b_0011_1111_0000_0000_0000_0000_0000_0000;
                float n = *(float*)&nBit - 1;

                t = 2.718281828f + n * (
                    2.718281828f + n * (
                    1.359140914f + n * (
                    0.4530469714f + n * (
                    0.1132617429f + n *
                    0.02265234857f))));

                for (int i = 0; i < m; i++)
                {
                    t = t * t;
                }
            }

            if (sign)
            {
                t = 1 / t;
            }
            return t;
        }
    }
}