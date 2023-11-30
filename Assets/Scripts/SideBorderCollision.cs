using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBorderCollision : MonoBehaviour
{
    private Game _game;

    private Forcefield _forcefield;
    
    // Which side this border is on
    [SerializeField] private int side;


    void Awake()
    {
        _game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();

        _forcefield = GetComponent<Forcefield>();
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Side border " + side + " hit by ball");

        if (!_forcefield.IsActive)
        {
            _game.OnBorderHit(side);
        }
    }
}
