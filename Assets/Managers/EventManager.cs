using System;

public class EventManager : Singleton<EventManager>
{
    public event Action OnStart,OnGame,OnFinish,OnLose,OnEmpty;
    void Update()
    {
      switch (GameManager.Instance.Gamestate)
      {
        case GameManager.GAMESTATE.Start: OnStart?.Invoke();
            break;
        case GameManager.GAMESTATE.Ingame: OnGame?.Invoke();
            break;
        case GameManager.GAMESTATE.GameOver: OnLose?.Invoke();
            break;
        case GameManager.GAMESTATE.Empty: OnEmpty?.Invoke();
            break;
      }
    }
}
