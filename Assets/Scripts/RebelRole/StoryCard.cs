using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryCard
{
    public string StoryTellerName { get; set; }
    public string StoryContent { get; set; }
    public RebelOption OptionA { get; set; }
    public RebelOption OptionB { get; set; }
    public bool IgnoreRandomization { get; set; }


}
