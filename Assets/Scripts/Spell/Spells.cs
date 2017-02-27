using System.Collections.Generic;
using UnityEngine;

public static class Spells {

    [System.Serializable]
    public enum spellEnum { 
        none,
        fire, water, earth, wind,
        fireFire,   fireWater,  fireEarth,  fireWind,
        waterFire,  waterWater, waterEarth, waterWind,
        earthFire,  earthWater, earthEarth, earthWind,
        windFire,   windWater,  windEarth,  windWind
    }; // Spell enumerator.


    /*
     * below is previous code; just for reference on what spells are like
    public enum Spells
    {
        none, fire, water, earth, wind,
        fire2, water2, earth2, wind2,
        steam, magma, elec, mud, storm, sandstorm
    }; // Spell enumerator
    */

    public static Dictionary<ElementsPair, spellEnum> elementCombToSpellEnum= 
        new Dictionary<ElementsPair, spellEnum>
    {
        {new ElementsPair(Elements.elemEnum.fire, Elements.elemEnum.none) , spellEnum.fire},
        {new ElementsPair(Elements.elemEnum.water, Elements.elemEnum.none) , spellEnum.water},
        {new ElementsPair(Elements.elemEnum.earth, Elements.elemEnum.none) , spellEnum.earth},
        {new ElementsPair(Elements.elemEnum.wind, Elements.elemEnum.none) , spellEnum.wind},
        {new ElementsPair(Elements.elemEnum.fire, Elements.elemEnum.fire) , spellEnum.fireFire},
        {new ElementsPair(Elements.elemEnum.fire, Elements.elemEnum.water) , spellEnum.fireWater},
        {new ElementsPair(Elements.elemEnum.fire, Elements.elemEnum.earth) , spellEnum.fireEarth},
        {new ElementsPair(Elements.elemEnum.fire, Elements.elemEnum.wind) , spellEnum.fireWind}
    };


    /*
     * This is for inspector to work.
     * Use it like following:
     * [SerializeField]
     * private List<Spell.SpellDictionaryEntry> inspectorDictionary;
     * 
     * and pass it to createSpellEnumToSpellObjectMap(List<Spell.SpellDictionaryEntry> inspectorDictionary); to create
     */
    [System.Serializable]
    public class SpellDictionaryEntry
    {
        public Elements.elemEnum firstElement;
        public Elements.elemEnum secondElement;
        public GameObject spellObject;
    }

    public static Dictionary<spellEnum, GameObject> createSpellEnumToSpellObjectMap(List<Spells.SpellDictionaryEntry> inspectorDictionary)
    {

        var myDictionary = new Dictionary<spellEnum, GameObject>();
        foreach (SpellDictionaryEntry entry in inspectorDictionary)
        {
            var key = elementCombToSpellEnum[new ElementsPair(entry.firstElement, entry.secondElement)];
            myDictionary.Add(key, entry.spellObject);
        }

        return myDictionary;
    }
}
