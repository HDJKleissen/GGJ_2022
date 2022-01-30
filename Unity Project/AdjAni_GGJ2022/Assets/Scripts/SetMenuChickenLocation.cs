using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMenuChickenLocation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParent(Transform newParent)
    {
        transform.parent = newParent;
        transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
    }
}
