using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlayerPref : MonoBehaviour
{
    public string PrefName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerPrefBool(bool value)
    {
        if (value)
        {
            UpdatePlayerPrefInt(1);
        }
        else
        {
            UpdatePlayerPrefInt(0);
        }
    }

    public void UpdatePlayerPrefInt(int value)
    {
        PlayerPrefs.SetInt(PrefName, value);
    }
}
