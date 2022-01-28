using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : UnitySingleton<GameController>
{
    public UIController uiController;

    float timer = 0;
    List<ChaseObject> chaseObjects = new List<ChaseObject>();
    List<ChaseObject> killedChaseObjects = new List<ChaseObject>();

    List<Breakable> breakables = new List<Breakable>();
    List<Breakable> brokenBreakables = new List<Breakable>();

    bool gameIsRunning;

    // Start is called before the first frame update
    void Start()    
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        uiController.UpdateTimer(timer);
    }

    public void RegisterChaseObject(ChaseObject co)
    {
        chaseObjects.Add(co);
        uiController.UpdateCaughtChaseObjects(killedChaseObjects.Count, chaseObjects.Count);
    }

    public void RegisterBreakable(Breakable breakable)
    {
        breakables.Add(breakable);
    }

    public void KillChaseObject(ChaseObject co)
    {
        if (killedChaseObjects.Contains(co))
        {
            Debug.LogError("Trying to add an already chased object to the chased objects list, something went wrong!");
            return;
        }

        killedChaseObjects.Add(co);
        uiController.UpdateCaughtChaseObjects(killedChaseObjects.Count, chaseObjects.Count);

        // Transition to Owner playing state
    }
}
