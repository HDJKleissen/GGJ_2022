using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public abstract class Breakable : MonoBehaviour
{
    bool itemBroken = false;
    bool itemFixed = false;

    public void Break()
    {
        if (!itemBroken && !itemFixed)
        {
            HandleBreak();
            itemBroken = true;
        }
    }
    public void Fix()
    {
        if(itemBroken && !itemFixed)
        {
            HandleFix();
            itemFixed = true;
        }
    }

    public abstract void HandleBreak();
    public abstract void HandleFix();
}