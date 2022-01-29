using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTransform : Breakable
{
    public override void HandleBreak()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f)));
        GetComponent<Collider2D>().enabled = false;
    }

    public override bool HandleFix()
    {
        Debug.Log("Check for Fixing predicate..");
        if (FixableObject.IsFixing())
        {
            Debug.Log("Fixed object!");
            transform.rotation = Quaternion.Euler(Vector3.zero);
            return true;
        }

        return false;
    }

    public override void HandlePrepForFixing()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}
