using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableSwapGO : Breakable
{
    public GameObject FixedGO, BrokenGO;
    public Collider2D BrokenCollider;
    public override void HandleBreak()
    {
        BrokenGO.SetActive(true);
        FixedGO.SetActive(false);
    }

    public override bool HandleFix()
    {
        if (FixableObject.IsFixing())
        {
            BrokenGO.SetActive(false);
            FixedGO.SetActive(true);
            return true;
        }

        return false;
    }

    public override void HandlePrepForFixing()
    {
        BrokenCollider.enabled = true;
    }
}
