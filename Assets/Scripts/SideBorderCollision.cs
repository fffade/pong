using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBorderCollision : MonoBehaviour
{
    private Game _game;
    
    // Which side this border is on
    [SerializeField] private int side;


    void Awake()
    {
        _game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Side border " + side + " hit by ball");
        
        _game.OnBorderHit(side);
    }
}
