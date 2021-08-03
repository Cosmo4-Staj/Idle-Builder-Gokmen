using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    PlayerManager playerManager;
    public static Rock Instance;
    public List<Transform> rockPieces = new List<Transform>();

    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();

        Instance = this;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            // Adding rocks to the list so that workers can find them

            rockPieces.Add(transform.GetChild(i).transform);
        }
    }

    void Update() 
    {

    }
}
