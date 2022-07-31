using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static GameManager m_instance; // 싱글톤이 할당될 static 변수
    
    public bool isGameover { get; private set; } // 게임 오버 상태

    // 게임 감정 상태
    //0 : HUI, 1 : ENJOY, 2 : WANT, ...
    public bool[] belongEmotions = new bool[3] { false, false, false };
    public bool[] spawnEmotions = new bool[3] { true, false, false };

    //public enum MouseState { BASIC,MOUSEOVER, DIALOGUE, CAMERA};
    //public MouseState mouseState = MouseState.BASIC;

    //public enum GameMode { STORY,PLAY, SETTING, DIALOGUE };
    //public GameMode gameMode = GameMode.PLAY;

    //public enum PlayState { PLAY, SETTING, DIALOGUE};
    //public PlayState playState = PlayState.PLAY;
    
    //public enum UserState { MOVE, INTERACTION };
    //public UserState userState = UserState.MOVE;

    enum GameState { STORY, PLAY, SETTING, DIALOGUE};
    [SerializeField] GameState gameState = GameState.PLAY;
    enum UserState { MOVE, INTERACTION, HEAR};
    [SerializeField] UserState userState = UserState.MOVE;

    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        print("game start");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }


    public void SetGameStateToStory()
    {
        gameState = SetState(GameState.STORY);
    }
    public void SetGameStateToPlay()
    {
        gameState = SetState(GameState.PLAY);
    }
    public void SetGameStateToDialogue()
    {
        gameState = SetState(GameState.DIALOGUE);
    }
    public void SetGameStateToSetting()
    {
        gameState = SetState(GameState.SETTING);
    }
    public bool IsGameStateStory()
    {
        return IsGameState(GameState.STORY);
    }
    public bool IsGameStatePlay()
    {
        return IsGameState(GameState.PLAY);
    }
    public bool IsGameStateDialogue()
    {
        return IsGameState(GameState.DIALOGUE);
    }
    public bool IsGameStateSetting()
    {
        return IsGameState(GameState.SETTING);
    }

    public void SetUserStateToMove()
    {
        userState = SetState(UserState.MOVE);
    }
    public void SetUserStateToInteration()
    {
        userState = SetState(UserState.INTERACTION);
    }
    public void SetUserStateToHear()
    {
        userState = SetState(UserState.HEAR);
    }
    public bool IsUserStateMove()
    {
        return IsUserState(UserState.MOVE);
    }
    public bool IsUserStateInteraction()
    {
        return IsUserState(UserState.INTERACTION);
    }
    public bool IsUserStateHear()
    {
        return IsUserState(UserState.HEAR);
    }


   

    T SetState<T>(T state){
        return state;
    }

    bool IsGameState(GameState gs)
    {
        return (gameState == gs);
    }

    bool IsUserState(UserState gs)
    {
        return (userState == gs);
    }


}
