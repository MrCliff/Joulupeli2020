using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    /// <summary>
    /// Controller that handles the general game states.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        public const string PlayerTag = "Player";
        public const string GameControllerTag = "GameController";

        private const string ActionMapPlayer = "Player";
        private const string ActionMapUI = "UI";

        [SerializeField]
        private GameObject startGamePanel;

        [SerializeField]
        private GameObject victoryPanel;

        [SerializeField]
        private GameObject gameOverPanel;

        [SerializeField]
        private PlayerInput playerInput;

        [SerializeField]
        private CarEngine carEngine;

        private void Start()
        {
            DeactivateAllPanels();
            playerInput.SwitchCurrentActionMap(ActionMapUI);
            startGamePanel.SetActive(true);
            carEngine.PullHandbrake(true);
        }

        /// <summary>
        /// Ends the game and shows the victory panel.
        /// </summary>
        public void EndGameAsVictorious()
        {
            playerInput.SwitchCurrentActionMap(ActionMapUI);
            victoryPanel.SetActive(true);
            carEngine.PullHandbrake(true);
        }

        /// <summary>
        /// Ends the game and shows the game over panel.
        /// </summary>
        public void EndGameAsFailure()
        {
            playerInput.SwitchCurrentActionMap(ActionMapUI);
            gameOverPanel.SetActive(true);
            carEngine.PullHandbrake(true);
        }

        public void StartGame()
        {
            DeactivateAllPanels();
            playerInput.SwitchCurrentActionMap(ActionMapPlayer);
            carEngine.PullHandbrake(false);
        }

        public void RestartGame()
        {
            DeactivateAllPanels();
            ReloadCurrentScene();
        }

        public void ExitGame()
        {
            Debug.Log("Quitting the game...");
            Application.Quit();
        }

        /// <summary>
        /// Deactivates all UI-panels.
        /// </summary>
        private void DeactivateAllPanels()
        {
            startGamePanel.SetActive(false);
            victoryPanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }

        private void ReloadCurrentScene()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex);
        }

        /// <summary>
        /// Returns the currently active GameController.
        /// </summary>
        /// <returns></returns>
        public static GameController GetInstance()
        {
            return GameObject.FindWithTag(GameControllerTag).GetComponent<GameController>();
        }
    }
}
