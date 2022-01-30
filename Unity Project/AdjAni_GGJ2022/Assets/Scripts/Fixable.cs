using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Fixable : MonoBehaviour
{
    private TextMeshPro fixTmp;
    [SerializeField] private KeyCode input;
    [SerializeField] private int requiredInteractions = 5;
    [SerializeField] private int currentInteractions = 0;

    [SerializeField] private ParticleSystem PS;
    
    // Start is called before the first frame update
    void Start()
    {
        fixTmp = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        fixTmp.text = $"{input.ToString().ToUpper()} {currentInteractions}/{requiredInteractions}";
        //fixTmp.text = $"SMASH {input.ToString().ToUpper()} \nTO CLEAN DAT SHIT";
    }

    public bool IsFixing()
    {
        if (Input.GetKeyDown(input))
        {
            //  ?????

            GameController.Instance.Owner.OwnerAnimator.Sweep();
            GameController.Instance.ShakeCamera();

            if(PS != null)
            {
                PS.gameObject.SetActive(true);
                PS.Stop();
                PS.Play();
            }
            transform.DOShakeRotation(0.3f, new Vector3(0, 0, 1), 5,10);
            //transform.DOPunchScale(new Vector3((float)transform.localScale.x+0.1f, (float)transform.localScale.y + 0.1f, 1), 0.3f, 10, 1);
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
