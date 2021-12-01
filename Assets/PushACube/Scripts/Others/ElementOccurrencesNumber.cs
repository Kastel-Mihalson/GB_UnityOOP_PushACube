using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ElementOccurrencesNumber
{
    public static Dictionary<int, int> ElementsOccNumber(this List<int> list)
    {
        return list.GroupBy(i => i).Where(i => i.Count() >= 1)
            .ToDictionary(k => k.Key, v => v.Count());
    }
}
