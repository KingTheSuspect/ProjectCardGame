using KermansUtility.Patterns.Singleton;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryEventHandler : MonoSingleton<StoryEventHandler>
{
    public List<StoryEventContainer> events = new List<StoryEventContainer>();

    public void PrintEventsContainerIds()
    {
        foreach (StoryEventContainer container in events)
        {
            Debug.Log(container.StoryID);
        }
    }
    public StoryEventContainer GetEventWithIndex(string index) 
    {
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].BothOptionsEvent)
            {
                char[] charArray = index.ToCharArray();
                string formatted = new string(charArray, 0, charArray.Length - 1);
                if (events[i].StoryID == formatted)
                {
                    return events[i];
                }
            }
            else
            {
                if (events[i].StoryID == index)
                {
                    return events[i];
                }
            }
        }
        Debug.LogWarning("Böyle bir event bulunamadý ustam");
        return null;
    }
    public void ExecuteEvent(string index)
    {
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].BothOptionsEvent)
            {
                char[] charArray = index.ToCharArray();
                string formatted = new string(charArray, 0, charArray.Length - 1);

                if (events[i].StoryID == formatted)
                {
                    events[i].Event?.Invoke();
                }
            }
            else
            {
                if (events[i].StoryID == index)
                {
                    events[i].Event?.Invoke();
                }
            }
        }
    }

}
