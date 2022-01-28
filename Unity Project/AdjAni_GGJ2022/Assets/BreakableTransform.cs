using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTransform : Breakable
{
    public override void HandleBreak()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0,0, Random.Range(0f,360f)));
        GetComponent<Collider2D>().isTrigger = true;
    }

    public override void HandleFix()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
