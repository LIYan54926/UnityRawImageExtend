using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RawImageExtend>().uvRect = new Rect(0, 0, 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
