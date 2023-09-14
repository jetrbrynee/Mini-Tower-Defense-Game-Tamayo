using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;

   
    void Update()
    {
        Debug.Log("Player Health: " + health);
    }
}
