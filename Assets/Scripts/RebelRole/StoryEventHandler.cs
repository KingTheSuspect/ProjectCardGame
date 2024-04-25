using Ink.Parsed;
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
       
         Debug.Log("index ="+ index );
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].BothOptionsEvent)
            {
                if (events[i].StoryID == index)
                {   
                    Debug.Log("if üst " +"Story id bu : "+events[i].StoryID+"story id bu :"+index);
                    Debug.Log("�al��t�");
                    events[i].Event?.Invoke(); // mesela event 7 yi çağır ///dikkat
                    Debug.Log("if alt " +"Story id bu : "+events[i].StoryID+"story id bu :"+index); 
                }
            }
            else
            {
                if (events[i].StoryID == index)
                {
                    events[i].Event?.Invoke(); // dikkat
                    Debug.Log("else if " +"Story id bu : "+events[i].StoryID+"story id bu :"+index);
                }
            }
        }
    }

}
