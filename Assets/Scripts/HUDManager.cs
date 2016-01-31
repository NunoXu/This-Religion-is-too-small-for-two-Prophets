using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HUDManager : MonoBehaviour
    {
        public GameObject GameOverPanel;
        public Text GameOverText;
        public float StartTime;

        public void SetGameOver(int player)
        {
            GameOverPanel.SetActive(true);
            GameOverText.text = "Player " + player + " Won!";
            StartTime = Time.time;
        }

        public void RestartLevel ()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void Update()
        {
            if (GameOverPanel.activeSelf && Input.anyKeyDown && Time.time - StartTime >= 1f)
            {
                RestartLevel();
            }
        }
    }
}
