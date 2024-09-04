using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverWindow : MonoBehaviour
{
    public Text winnerName;
    public Button restartButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(RestartGame);
    }

    public void SetName(string s)
    {
        winnerName.text = s;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }    
}
