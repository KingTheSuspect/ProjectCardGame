using KermansUtility.Patterns.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TermManager : MonoSingleton<TermManager>
{
    [SerializeField] private List<TermVariablesContainer> _termVariablesContainers = new List<TermVariablesContainer>();

    public TermVariablesContainer GetTermVairablesContainerWithIndex(int index)
    {
        if (index < 0 || index >= _termVariablesContainers.Count)
        {
            Debug.LogError("Ge�ersiz �ndex Giri�i, (d�nem konteyn�r� bulunamad�)");
            return null;
        }

        return _termVariablesContainers[index];
    }

    public enum TermType
    {
        Kurulus,Fetret,Lale,Cokus,KurulusAra
    }
}
