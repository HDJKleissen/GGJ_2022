using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class GameController : UnitySingleton<GameController>
{
    public UIController UIController;
    public DogController Dog;
    public PlayerController Owner;
    public CinemachineVirtualCamera VirtualCamera;
    public GameStateSwitchOverlay GameStateSwitchOverlay;
    public GreyScaleScene GreyScaleScene;
    public ShakeEffectInCinemachine CamShakeEffect;
    public float OwnerTimeAmount;

    float timer = 0;
    List<ChaseObject> chaseObjects = new List<ChaseObject>();
    List<ChaseObject> killedChaseObjects = new List<ChaseObject>();
    List<ChaseObject> cleanedChaseObjects = new List<ChaseObject>();

    List<Breakable> breakables = new List<Breakable>();
    List<Breakable> brokenBreakables = new List<Breakable>();
    List<Breakable> fixedBreakables = new List<Breakable>();

    bool gameIsRunning;
    public ScoreObject scoreObject = new ScoreObject();

    public GameState GameState { get; private set; } = GameState.Dog;
    public bool PlayerHasControl = true;

    bool screenShakeEnabled = true;

    // Start is called before the first frame update
    void Start()    
    {
        //GreyScaleScene.Shade();
        screenShakeEnabled = PlayerPrefs.GetInt("ScreenShakeEnabled", 1) == 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GreyScaleScene.IsGreyScaled)
            GreyScaleScene.Shade();

        if (PlayerHasControl)
        {
            if (GameState == GameState.Dog)
            {
                timer += Time.deltaTime;
            }
            else if(GameState == GameState.Owner)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    // END GAME POG
                    EndGame();
                }
            }
            UIController.UpdateTimer(timer);
        }
    }

    public void ShakeCamera()
    {
        if (screenShakeEnabled)
        {
            CamShakeEffect.Shake();
        }
    }

    public void RegisterChaseObject(ChaseObject co)
    {
        chaseObjects.Add(co);
        UIController.UpdateCaughtChaseObjects(killedChaseObjects.Count, chaseObjects.Count);
    }

    public void RegisterBreakable(Breakable breakable)
    {
        breakables.Add(breakable);
    }

    public void BreakBreakable(Breakable breakable)
    {
        if (brokenBreakables.Contains(breakable))
        {
            Debug.LogError("Trying to add an already broken breakable to the broken breakables list, something went wrong!");
            return;
        }

        brokenBreakables.Add(breakable);
    }

    public void NoBrokenBreakablesCheck()
    {
        if (cleanedChaseObjects.Count == killedChaseObjects.Count && fixedBreakables.Count == brokenBreakables.Count)
        {
            EndGame();
        }
    }

    public void FixBreakable(Breakable breakable)
    {
        if (fixedBreakables.Contains(breakable))
        {
            Debug.LogError("Trying to add an already fixed breakable to the fixed breakables list, something went wrong!");
            return;
        }

        fixedBreakables.Add(breakable);
        if (cleanedChaseObjects.Count == killedChaseObjects.Count && fixedBreakables.Count == brokenBreakables.Count)
        {
            EndGame();
        }
    }

    public void KillChaseObject(ChaseObject co)
    {
        if (killedChaseObjects.Contains(co))
        {
            Debug.LogError("Trying to add an already chased object to the chased objects list, something went wrong!");
            return;
        }

        killedChaseObjects.Add(co);
        UIController.UpdateCaughtChaseObjects(killedChaseObjects.Count, chaseObjects.Count);

        // Transition to Owner playing state
        if (killedChaseObjects.Count == chaseObjects.Count)
        {
            scoreObject.DogTime = timer;
            scoreObject.BreakablesBroken = brokenBreakables.Count;

            GameStateSwitchOverlay.StartFade();
            MusicPlayer.Instance.SetOwner(true);
            MusicPlayer.Instance.SetSpeedUp(false);
        }
        else if(killedChaseObjects.Count > chaseObjects.Count/2)
        {
            MusicPlayer.Instance.SetSpeedUp(true);
        }
    }
    public void CleanChaseObject(ChaseObject co)
    {
        if (cleanedChaseObjects.Contains(co))
        {
            Debug.LogError("Trying to add an already cleaned object to the cleaned objects list, something went wrong!");
            return;
        }

        cleanedChaseObjects.Add(co);
        co.gameObject.SetActive(false);

        if(cleanedChaseObjects.Count == killedChaseObjects.Count && fixedBreakables.Count == brokenBreakables.Count)
        {
            EndGame();
        }
    }

    public void SwitchToOwnerState()
    {
        GameState = GameState.Owner;
        VirtualCamera.Follow = Owner.transform;
        timer = OwnerTimeAmount;
        Dog.gameObject.SetActive(false);
        Owner.gameObject.SetActive(true);
        foreach(Breakable breakable in brokenBreakables)
        {
            breakable.PrepForFixing();
        }
        GreyScaleScene.Shade();
    }

    public void EndGame()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Win_Fanfare");
        PlayerHasControl = false;
        scoreObject.TotalBreakables = breakables.Count;
        scoreObject.OwnerTime = GameController.Instance.OwnerTimeAmount - timer;
        scoreObject.BreakablesFixed = fixedBreakables.Count;
        UIController.OpenEndScreen(scoreObject);
        //UIController.OpenHighScoreScene(scoreObject);
        MusicPlayer.Instance.SetWin(true);
        MusicPlayer.Instance.SetOwner(false);
        MusicPlayer.Instance.SetSpeedUp(false);
    }
}

public enum GameState
{
    Dog,
    Owner
}
