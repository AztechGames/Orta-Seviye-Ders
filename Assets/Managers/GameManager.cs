using UnityEngine;
public class GameManager : Singleton<GameManager>
{
    #region GameState
    public enum GAMESTATE
    {
        Start,
        Ingame,
        GameOver,
        Empty
    }
    [SerializeField]GAMESTATE _gamestate;
    public GAMESTATE Gamestate
    {
        get { return _gamestate;}
        set
        {
            _gamestate = value;
            UIManager.Instance.PanelController(_gamestate);
        }
    }
    #endregion
    void Start()
    {
        Gamestate = GAMESTATE.Start;
    }
    void Update()
    {
        if (Input.anyKeyDown && Gamestate == GAMESTATE.Start)
            Gamestate = GAMESTATE.Ingame;
    }
}