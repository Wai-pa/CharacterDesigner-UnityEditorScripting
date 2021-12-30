using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenuAttribute(fileName = "New Titan Data", menuName = "Character Data/Titan")]
public class TitanData : CharacterData
{
    public TitanSubclass titanSubclass;
    public TitanSunbreaker titanSunbreaker;
    public TitanStriker titanStriker;
    public TitanSentinel titanSentinel;
}
