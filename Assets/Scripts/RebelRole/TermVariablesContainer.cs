using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Term Variable Container",menuName ="Scriptable Objects/TermVariableContainer")]
public class TermVariablesContainer : ScriptableObject
{
    public string Variable1Name => _variable1Name;
    public string Variable2Name => _variable2Name;
    public string Variable3Name => _variable3Name;
    public string Variable4Name => _variable4Name;


    [SerializeField] private string _variable1Name;
    [SerializeField] private string _variable2Name;
    [SerializeField] private string _variable3Name;
    [SerializeField] private string _variable4Name;
}
