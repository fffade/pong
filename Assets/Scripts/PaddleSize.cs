using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleSize : MonoBehaviour
{
    private Transform _transform;
    
    // The original size of the paddle (determined at start)
    public float InitialSize { get; private set; }
    
    // The current modifier for the size of this paddle
    public float CurrentSizeModifier { get; private set; } = 1f;
    
    // The rate at which the paddle alters its size
    [SerializeField] private float changeSizeSpeed;


    void Awake()
    {
        _transform = transform;
    }
    
    void Start()
    {
        InitialSize = _transform.localScale.y;
    }

    void Update()
    {
        float desiredLength = InitialSize * CurrentSizeModifier;
        float currentLength = _transform.localScale.y;
        
        // Update size interpolated
        float newLength = Mathf.Lerp(currentLength, desiredLength, changeSizeSpeed * Time.deltaTime);

        _transform.localScale = new Vector3(_transform.localScale.x, newLength,
            _transform.localScale.z);
    }
    

    // Multiplies the modifier, modifying the size of the paddle
    public void MultiplyModifier(float multiplier)
    {
        CurrentSizeModifier *= multiplier;
    }
    
    // Returns the paddle to its first size
    public void ResetModifier(float value = 1f)
    {
        CurrentSizeModifier = value;
    }
}
