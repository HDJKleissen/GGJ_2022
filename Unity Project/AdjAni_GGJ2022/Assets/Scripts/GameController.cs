using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class GameController : UnitySingleton<GameController>
{
    public UIController UIController;
    public MusicPlayer MusicPlayer;
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

    List<Breakable> breakables = new List<Breakable>();
    List<Breakable> brokenBreakables = new List<Breakable>();
    List<Breakable> fixedBreakables = new List<Breakable>();

    bool gameIsRunning;
    public ScoreObject scoreObject = new ScoreObject();

    public GameState GameState { get; private set; } = GameState.Dog;
    public bool PlayerHasControl = true;
    // Start is called before the first frame update
    void Start()    
    {
        MusicPlayer.SetSpeedUp(true);
        //GreyScaleScene.Shade();
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
        if (brokenBreakables.Count == 0)
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
        if (fixedBreakables.Count == brokenBreakables.Count)
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
            MusicPlayer.SetOwner(true);
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
        PlayerHasControl = false;
        scoreObject.TotalBreakables = breakables.Count;
        scoreObject.OwnerTime = GameController.Instance.OwnerTimeAmount - timer;
        scoreObject.BreakablesFixed = fixedBreakables.Count;
        //UIController.OpenEndScreen(scoreObject);
        UIController.OpenHighScoreScene(scoreObject);
    }
}

public enum GameState
{
    Dog,
    Owner
}
