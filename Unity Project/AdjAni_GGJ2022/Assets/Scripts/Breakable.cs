using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public abstract class Breakable : MonoBehaviour
{
    [SerializeField] protected bool itemBroken = false;
    [SerializeField] protected bool itemFixed = false;
    [field: SerializeField] public Fixable FixableObject { get; private set;}

    public bool ItemBroken => itemBroken;
    public bool ItemFixed => itemFixed;

    private void Start()
    {
        GameController.Instance.RegisterBreakable(this);
    }

    public void Break()
    {
        if (!itemBroken && !itemFixed && GameController.Instance.GameState == GameState.Dog)
        {
            GameController.Instance.CamShakeEffect.Shake();
            HandleBreak();
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Pot_Break", gameObject);
            itemBroken = true;
            GameController.Instance.BreakBreakable(this);
        }
    }

    public void Fix()
    {
        if (itemBroken && !itemFixed && GameController.Instance.GameState == GameState.Owner)
        {
            if (HandleFix())
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Fixed_Ding");
                GameController.Instance.ShakeCamera();
                itemFixed = true;
                GameController.Instance.FixBreakable(this);
            }
        }
    }

    public void PrepForFixing()
    {
        if (itemBroken && !ItemFixed)
        {
            HandlePrepForFixing();
        }
    }

    public abstract void HandlePrepForFixing();
    public abstract void HandleBreak();
    public abstract bool HandleFix();
}