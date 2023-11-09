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
        char[] charArray = index.ToCharArray();
        string formatted = new string(charArray, 0, charArray.Length - 1);

        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].BothOptionsEvent)
            {
                
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
        return null;
    }
    public void ExecuteEvent(string index)
    {
        char[] charArray = index.ToCharArray();
        string formatted = new string(charArray, 0, charArray.Length - 1);

        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].BothOptionsEvent)
            {
                Debug.Log("Formatted : " + formatted + " , storyId : " + events[i].StoryID);
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
