using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance { get; private set; }
    public bool isSelectPiece = false;
    IState currentState;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        ChangeScene(new WhiteTurnState());
    }
    private void Update()
    {
        currentState?.Update();
    }
    public void ChangeScene(IState nextState)
    {
        currentState?.Exit();
        currentState = nextState;
        currentState?.Enter();
    }
}
