using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBorderCollision : MonoBehaviour
{
    private Game _game;

    private Forcefield _forcefield;
    
    // Which side this border is on
    [SerializeField] private int side;
    
    // The audio to play on the border score hit
    [SerializeField] private AudioClip hitAudio;


    void Awake()
    {
        _game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();

        _forcefield = GetComponent<Forcefield>();
    }
    
    /* Triggered when this border is passed by the ball */
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (!_forcefield.IsActive)
        {
            _game.OnBorderHit(side);
            
            GlobalAudio.Instance.PlayAudio(hitAudio);
        }
    }
}
