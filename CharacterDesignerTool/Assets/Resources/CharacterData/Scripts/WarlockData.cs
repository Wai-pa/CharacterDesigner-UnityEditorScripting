using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenuAttribute(fileName = "New Warlock Data", menuName = "Character Data/Warlock")]
public class WarlockData : CharacterData
{
    public WarlockSubclass warlockSubclass;
    public WarlockDawnblade warlockDawnblade;
    public WarlockStormcaller warlockStormcaller;
    public WarlockVoidwalker warlockVoidwalker;
}
