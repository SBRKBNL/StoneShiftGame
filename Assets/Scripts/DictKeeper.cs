using System.Collections.Generic;
using UnityEngine;

public class DictKeeper
{
    // Özellik (Property) olarak tanımlama
    public Dictionary<(float, float), string> Lvl1I0 { get; private set; } 
    public Dictionary<(float, float), GameObject> Lvl1FFO { get; private set; }

    // Constructor içinde başlatma
    public DictKeeper()
    {
        Lvl1I0 = new Dictionary<(float, float), string>
        {
            { (0.5f, 0.5f), "Elma" }
        };

        Lvl1FFO = new Dictionary<(float, float), GameObject>
        {
            
        };

    }
    
    

    
}
