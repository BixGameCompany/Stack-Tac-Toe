using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class attachSelf : MonoBehaviour
{
    public UnityEvent onSceneLoad;
    // Start is called before the first frame update
    void Start()
    {
        onSceneLoad.Invoke();
    }
}
