﻿// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System;
using System.Threading;

namespace MyHalp
{
    public sealed class MyJob
    {
        /// <summary>
        /// Run new job.
        /// </summary>
        /// <param name="callback">Delegate or something. x => SomeMethod() or delegate{ some; code; }</param>
        public static void Run(WaitCallback callback)
        {
            ThreadPool.QueueUserWorkItem(callback);
        }

        /// <summary>
        /// Waits for state = false. 
        /// This will lock the current thread until state will be false.
        /// </summary>
        /// <param name="state">The state</param>
        public static void Wait(ref bool state)
        {
            while (state) { }
        }
        
        // TODO: WaitMiliseconds

        public static void WaitSeconds(float seconds)
        {
            var start = DateTime.Now;
            while (true)
            {
                var diff = DateTime.Now - start;
                if (diff.Seconds >= seconds)
                    return;

                Thread.Sleep(250);
            }
        }
    }
}