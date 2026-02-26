using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance { get; private set; }
    public bool isSelectPiece = false;
    public IState currentState;
    public Image EndTextBox;
    public TextMeshProUGUI EndText;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        ChangeState(new WhiteTurnState());
    }
    private void Update()
    {
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
    }
    internal void ChangeTurn()
    {
        if (TurnManager.instance.currentState.ToString() == "WhiteTurnState")
            TurnManager.instance.ChangeState(new BlackTurnState());
        else
            TurnManager.instance.ChangeState(new WhiteTurnState());
    }
}
