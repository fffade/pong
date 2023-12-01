using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSize : MonoBehaviour
{
    private Transform _transform;

    // The starting size of the ball / the default
    [SerializeField] private float initialRadius;
    
    // The current modifier for the ball's size
    [SerializeField] private float currentRadiusModifier = 1f;
    
    // The rate the ball changes size
    [SerializeField] private float changeRadiusSpeed;
    

    void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        float desiredDiameter = initialRadius * currentRadiusModifier * 2f;
        float currentDiameter = _transform.localScale.x;
        
        // Update diameter interpolated
        float newDiameter = Mathf.Lerp(currentDiameter, desiredDiameter, changeRadiusSpeed * Time.deltaTime);
        
        // Update ball size each frame
        _transform.localScale = new Vector3(newDiameter, newDiameter, _transform.localScale.z);
    }
    
    /* Alters the modifier for the ball's radius */
    public void MultiplyModifier(float multiplier)
    {
        currentRadiusModifier *= multiplier;
    }

    public void ResetModifier(float value = 1f)
    {
        currentRadiusModifier = value;
    }
}
