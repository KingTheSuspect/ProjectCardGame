using KermansUtility.Patterns.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEventHandler : MonoSingleton<StoryEventHandler>
{
    public List<StoryEventContainer> events = new List<StoryEventContainer>();

    public StoryEventContainer GetEventWithIndex(int index) 
    {
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].StoryID == index)
            {
                return events[i];
            }
        }
        Debug.LogWarning("Böyle bir event bulunamadý ustam");
        return null;
    }
    public void ExecuteEvent(int index)
    {
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].StoryID == index)
            {
                events[i].Event?.Invoke();
            }
        }
    }

}
