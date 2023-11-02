using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Stat Randomization Info",menuName ="Scriptable Objects/Stat Randomization Info")]
public class StatRandomizationInfoScriptableObject : ScriptableObject
{
    public List<StatRandomizationInfo> StatRandomizationInfo = new List<StatRandomizationInfo>();
}
