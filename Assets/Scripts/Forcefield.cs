using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcefield : MonoBehaviour
{
    // Side border forcefield animator
    private Animator _animator;
    
    // Whether this forcefield is currently active
    public bool IsActive { get; private set; }
    
    private float _timer;
    // How long this forcefield lasts once turned on
    [SerializeField] private float activeTime;
    
    // Activation sound effect
    // Constant sound effect while on
    [SerializeField] private AudioClip activateAudio,
                                            stayAudio;


    void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (IsActive)
        {
            _timer += Time.deltaTime;

            if (_timer >= activeTime)
            {
                IsActive = false;
                GlobalAudio.Instance.StopLoopAudio(stayAudio);
            }
        }

        _animator.SetBool("IsActive", IsActive);
    }
    
    // Turns on this forcefield a single time
    public void Activate()
    {
        if (IsActive)
            return;

        _timer = 0;
        IsActive = true;
        
        GlobalAudio.Instance.PlayAudio(activateAudio);
        GlobalAudio.Instance.PlayLoopAudio(stayAudio);
    }
    
    
}
