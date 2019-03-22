using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int points = 0;
    public float time = 10;
    public Text pointLabel;
    public Text pointLabelEndGame;
    public Text timerLabel;
    public Text endGameCause;

    public Animator pointPanel;
    public Animator endGamePanel;
    public Animator quitAskPanel;
    public Animator pausePanel;

    [HideInInspector]
    public bool gameIsEnded = false;
    public bool gameIsPaused = false;
    private float currentTime;

    public void Start()
    {
        Init();
    }

    public void Update()
    {
        PauseToggle();
        UpdateCurrentTime();
    }

    public void PauseToggle()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameIsEnded)
        {
            Pause();
        }
    }

    public void Pause()
    {
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused)
        {
            pausePanel.SetTrigger("PausePanelEnter");
        }
        else
        {
            pausePanel.SetTrigger("PausePanelExit");
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void QuitAsk()
    {
        quitAskPanel.SetTrigger("QuitPanelAskEnter");
        endGamePanel.SetTrigger("EndGamePanelExit");
        pausePanel.SetTrigger("PausePanelExit");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Init()
    {
        points = 0;
        currentTime = time;
        timerLabel.text = Mathf.RoundToInt(currentTime).ToString();
        pointLabel.text = "Points: " + points;
    }

    public void AddPoints(int point)
    {
        points += point;
        pointLabel.text = "Points: " + points;
    }

    public void UpdateCurrentTime()
    {
        if (gameIsPaused)
        {
            return;
        }

        if (currentTime <= 0)
        {
            EndGame("Il tempo è scaduto");
        }

        if (gameIsEnded)
        {
            Init();
        } else
        {
            currentTime -= Time.deltaTime;
            timerLabel.text = Mathf.RoundToInt(currentTime).ToString();
        }
    }

    public void EndGame(string cause)
    {
        gameIsEnded = true;
        pointLabelEndGame.text = points.ToString();
        endGameCause.text = cause;
        pointPanel.SetTrigger("PointPanelExit");
        endGamePanel.SetTrigger("EndGamePanelEnter");
    }
}
