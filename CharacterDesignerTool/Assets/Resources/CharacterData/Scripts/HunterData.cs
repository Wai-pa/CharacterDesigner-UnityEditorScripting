using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenuAttribute(fileName = "New Hunter Data", menuName = "Character Data/Hunter")]
public class HunterData : CharacterData
{
    public HunterSubclass hunterSubclass;
    public HunterGunslinger hunterGunslinger;
    public HunterArcstrider hunterArcstrider;
    public HunterNightstalker hunterNightstalker;
}
