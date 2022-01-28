using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fixable : MonoBehaviour
{
    [SerializeField] private TextMeshPro fixTmp;
    [SerializeField] private KeyCode input;
    [SerializeField] private int requiredInteractions = 5;
    [SerializeField] private int currentInteractions = 0;

    // Start is called before the first frame update
    void Start()
    {
        fixTmp = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        fixTmp.text = $"SMASH {input.ToString().ToUpper()} {currentInteractions}/{requiredInteractions}";
    }

    public bool IsFixing()
    {
        if (Input.GetKeyDown(input))
        {
            currentInteractions++;
        }

        if (currentInteractions >= requiredInteractions)
        {    
            return true;
        }
        else
        {
            return false;
        }
    }
}
