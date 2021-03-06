﻿// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace MyHalp
{
    public sealed class MyJobQueue
    {
        /// <summary>
        /// Create new queue.
        /// </summary>
        /// <param name="onDone">Called when the queue execution is done.</param>
        /// <returns>Your new MyJobQueue.</returns>
        public static MyJobQueue Create(Action onDone = null)
        {
            return new MyJobQueue
            {
                _onDone = onDone
            };
        }
        
        private Action _onDone;
        private bool _executing;
        private volatile float _progress;
        private readonly List<WaitCallback> _queuedCallbacks = new List<WaitCallback>();

        /// <summary>
        /// Queue an new delegate or sth.
        /// </summary>
        /// <param name="callback">The 'sth' Sugered usage: x => SomeMethod() or delegate{ some; code; }</param>
        public void Queue(WaitCallback callback)
        {
            if (_executing)
            {
                throw new UnityException("JobQueue is already began to execute already, cannot queue new callback.");
            }
            
            // TODO: Maybe weights of the callbacks?

            _queuedCallbacks.Add(callback);
        }

        /// <summary>
        /// Execute this queue.
        /// WARINING: You won't be able to queue new callbacks!
        /// </summary>
        public void Execute()
        {
            _executing = true;
            MyJob.Run(delegate
            {
                var queuedItems = _queuedCallbacks.Count;
                
                // TODO: Maybe multi-threading? - Just use MyJob.Run instead of .Invoke...?
                for (var i = 0; i < queuedItems; i ++)
                {
                    var item = _queuedCallbacks[i];
                    item.Invoke(null);
                    _progress = i / (float)queuedItems;
                }

                _queuedCallbacks.Clear();

                // Dispatch OnDone callback if there is one.
                if (MyDispatcher.IsInitialized && _onDone != null)
                {
                    MyDispatcher.Dispatch(x => _onDone());
                }
            });
        }

        /// <summary>
        /// Get progress of current execution.
        /// </summary>
        /// <returns>Progress in 0.0f-1.0f range.</returns>
        public float GetProgress()
        {
            return _progress;
        }
    }
}