using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkinData
{
    public int equippedSkinId;

    public List<int> ownedSkinIds;


    public SkinData(List<int> ownedSkinIds, int equippedSkinId = 0)
    {
        this.equippedSkinId = equippedSkinId;
        this.ownedSkinIds = ownedSkinIds;
    }
}
