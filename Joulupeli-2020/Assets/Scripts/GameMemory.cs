﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Stores values between scene loads.
    /// </summary>
    public class GameMemory : MonoBehaviour
    {
        /// <summary>
        /// Returns the currently active GameMemory.
        /// </summary>
        public static GameMemory Instance { get; private set; }

        /// <summary>
        /// Count how many times the game has been failed in a row.
        /// </summary>
        public int GameFailedInRowCount { get; private set; } = 0;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        /// <summary>
        /// Increments the game failed in row count.
        /// </summary>
        public void IncrementGameFailedInRowCount()
        {
            GameFailedInRowCount++;
        }

        /// <summary>
        /// Resets the game failed in row count to 0.
        /// </summary>
        public void ResetGameFailedInRowCount()
        {
            GameFailedInRowCount = 0;
        }
    }
}
