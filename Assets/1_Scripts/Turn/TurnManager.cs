using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance { get; private set; }
    public bool isSelectPiece = false;
    public IState currentState;
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
}
