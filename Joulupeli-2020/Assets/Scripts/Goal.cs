using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    /// <summary>
    /// Goal point that triggers winning the game.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Goal : MonoBehaviour
    {
        private const string PlayerTag = "Player";

        private Collider2D goalTrigger;
        private GameController gameController;

        private void Awake()
        {
            goalTrigger = GetComponent<Collider2D>();
            gameController = GameController.GetInstance();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == PlayerTag)
            {
                gameController.EndGameAsVictorious();
            }
        }
    }
}
