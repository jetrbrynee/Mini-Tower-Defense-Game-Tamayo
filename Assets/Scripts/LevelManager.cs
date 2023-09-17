using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    // Public references to the Start Point and Path transforms.
    public Transform startPoint;
    public Transform[] path;

    private void Awake()
    {
        main = this;
    }
}
