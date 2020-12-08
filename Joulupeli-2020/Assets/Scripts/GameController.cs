using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
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

        private const string ActionPoint = "Point";
        private const string ActionClick = "Click";

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

        [SerializeField]
        private InputSystemUIInputModule inputModule;

        private void Start()
        {
            DeactivateAllPanels();
            SwitchActionMapTo(ActionMapUI);
            startGamePanel.SetActive(true);
            carEngine.PullHandbrake(true);
        }

        /// <summary>
        /// Ends the game and shows the victory panel.
        /// </summary>
        public void EndGameAsVictorious()
        {
            SwitchActionMapTo(ActionMapUI);
            victoryPanel.SetActive(true);
            carEngine.PullHandbrake(true);
        }

        /// <summary>
        /// Ends the game and shows the game over panel.
        /// </summary>
        public void EndGameAsFailure()
        {
            SwitchActionMapTo(ActionMapUI);
            gameOverPanel.SetActive(true);
            carEngine.PullHandbrake(true);
        }

        public void StartGame()
        {
            DeactivateAllPanels();
            SwitchActionMapTo(ActionMapPlayer);
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

        private void SwitchActionMapTo(string actionMapName)
        {
            playerInput.SwitchCurrentActionMap(actionMapName);
            InputAction pointAction = inputModule.actionsAsset.FindActionMap(actionMapName).FindAction(ActionPoint);
            inputModule.point = InputActionReference.Create(pointAction);
            InputAction clickAction = inputModule.actionsAsset.FindActionMap(actionMapName).FindAction(ActionClick);
            inputModule.leftClick = InputActionReference.Create(clickAction);
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
