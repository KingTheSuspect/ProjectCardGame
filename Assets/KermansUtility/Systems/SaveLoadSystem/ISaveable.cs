using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    public string SavePath { get; set; }
    public void SetDefaultValues();
}
