using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance { get; private set; }
    public bool isSelectPiece = false;
    public IState currentState;
    public Image EndTextBox;
    public TextMeshProUGUI EndText;
    public TextMeshProUGUI WhiteTimer;
    public TextMeshProUGUI BlackTimer;
    public bool isMatched = false;
    private double WhiteTime = 600;
    private double BlackTime = 600;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        ChangeState(new WhiteTurnState());
    }
    private void Update()
    {
        WhiteTimer.text = $"{(Convert.ToInt32(WhiteTime)/60).ToString("00")}:{(Convert.ToInt32(WhiteTime)%60).ToString("00")}";
        BlackTimer.text = $"{(Convert.ToInt32(BlackTime) / 60).ToString("00")}:{(Convert.ToInt32(BlackTime) % 60).ToString("00")}";
        currentState?.Update();
    }
    public void ChangeState(IState nextState)
    {
        currentState?.Exit();
        currentState = nextState;
        currentState?.Enter();
    }
    public void GameEnd(string message)
    {
        EndText.text = message;
        EndTextBox.gameObject.SetActive(true);
        StartCoroutine(End());
    }
    IEnumerator End()
    {
        //ChessClient.Instance.End(ChessClient.Instance.roomId);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Loby");
    }
    internal void ChangeTurn()
    {
        if (currentState.ToString() == "WhiteTurnState")
        {
            ChangeState(new BlackTurnState());
            ChessClient.Instance.TurnChange("BLACK");
        }
        else
        {
            ChangeState(new WhiteTurnState());
            ChessClient.Instance.TurnChange("WHITE");
        }
    }
    public void TimeChange(string turn, int time)
    {
        if(turn == "WHITE")
        {
            WhiteTime = time;
        }
        else
        {
            BlackTime = time;
        }
    }
    public IEnumerator Matched(string playerOne, string playerTwo)
    {
        UIManager.instance.WhiteName.text = playerOne;
        UIManager.instance.BlackName.text = playerTwo;
        var logText = UIManager.instance.LogText;
        logText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        logText.gameObject.SetActive(false);
        isMatched = true;
    }
}
