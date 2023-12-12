using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleForcefield : MonoBehaviour
{
    /* Allows a paddle to control its respective forcefield */

    [SerializeField] public Forcefield forcefield;
    
    // The key (if applicable) to activate the forcefield
    // A keycode of DELETE means none
    [SerializeField] private KeyCode activateKey;
    
    // How many uses this forcefield has by default
    [SerializeField] private int defaultUses;
    
    // How many uses this paddle has for their forcefield
    public int uses;


    void Start()
    {
        ResetUses();
    }

    void Update()
    {
        // DELETE Is the n/a key
        // it means this paddle's forcefield is not input controlled
        if (activateKey != KeyCode.Delete)
        {
            CheckInput();
        }
    }

    private void CheckInput()
    {
        // Check input for a forcefield activation
        if (Input.GetKeyDown(activateKey) && !forcefield.IsActive &&
            uses > 0)
        {
            forcefield.Activate();
            uses--;
        }
    }
    
    // Returns use count to original
    public void ResetUses()
    {
        uses = defaultUses;
    }
}
