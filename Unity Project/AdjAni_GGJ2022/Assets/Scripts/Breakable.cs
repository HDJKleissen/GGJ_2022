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

    public bool ItemBroken { get => itemBroken; }
    public bool ItemFixed { get => itemFixed; }

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
            if (HandleFix())
            {
                itemBroken = false;
                itemFixed = true;
            }
        }
    }

    public abstract void HandleBreak();
    public abstract bool HandleFix();
}