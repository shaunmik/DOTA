using System.Collections.Generic;
using UnityEngine;

public static class Spells
{

    [System.Serializable]
    public enum spellEnum
    {
        none,
        fire, water, earth, wind,
        fireFire, fireWater, fireEarth, fireWind,
        waterFire, waterWater, waterEarth, waterWind,
        earthFire, earthWater, earthEarth, earthWind,
        windFire, windWater, windEarth, windWind
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

    public static Dictionary<ElementsPair, spellEnum> elementsPairToSpellEnum = new Dictionary<ElementsPair, spellEnum>
    {
        {new ElementsPair(Elements.elemEnum.none, Elements.elemEnum.none) , spellEnum.none},

        {new ElementsPair(Elements.elemEnum.fire, Elements.elemEnum.none) , spellEnum.fire},
        {new ElementsPair(Elements.elemEnum.water, Elements.elemEnum.none) , spellEnum.water},
        {new ElementsPair(Elements.elemEnum.earth, Elements.elemEnum.none) , spellEnum.earth},
        {new ElementsPair(Elements.elemEnum.wind, Elements.elemEnum.none) , spellEnum.wind},

        {new ElementsPair(Elements.elemEnum.fire, Elements.elemEnum.fire) , spellEnum.fireFire},
        {new ElementsPair(Elements.elemEnum.fire, Elements.elemEnum.water) , spellEnum.fireWater},
        {new ElementsPair(Elements.elemEnum.fire, Elements.elemEnum.earth) , spellEnum.fireEarth},
        {new ElementsPair(Elements.elemEnum.fire, Elements.elemEnum.wind) , spellEnum.fireWind},


        {new ElementsPair(Elements.elemEnum.water, Elements.elemEnum.fire) , spellEnum.waterFire},
        {new ElementsPair(Elements.elemEnum.water, Elements.elemEnum.water) , spellEnum.waterWater},
        {new ElementsPair(Elements.elemEnum.water, Elements.elemEnum.earth) , spellEnum.waterEarth},
        {new ElementsPair(Elements.elemEnum.water, Elements.elemEnum.wind) , spellEnum.waterWind},

        {new ElementsPair(Elements.elemEnum.earth, Elements.elemEnum.fire) , spellEnum.earthFire},
        {new ElementsPair(Elements.elemEnum.earth, Elements.elemEnum.water) , spellEnum.earthWater},
        {new ElementsPair(Elements.elemEnum.earth, Elements.elemEnum.earth) , spellEnum.earthEarth},
        {new ElementsPair(Elements.elemEnum.earth, Elements.elemEnum.wind) , spellEnum.earthWind},

        {new ElementsPair(Elements.elemEnum.wind, Elements.elemEnum.fire) , spellEnum.windFire},
        {new ElementsPair(Elements.elemEnum.wind, Elements.elemEnum.water) , spellEnum.windWater},
        {new ElementsPair(Elements.elemEnum.wind, Elements.elemEnum.earth) , spellEnum.windEarth},
        {new ElementsPair(Elements.elemEnum.wind, Elements.elemEnum.wind) , spellEnum.windWind}
    };



    public static Dictionary<spellEnum, SpellDetails> createSpellsEnumToSpellDetailsMap(List<Spells.InspectableSpellDictionaryEntry> inspectorDictionary)
    {

        var spellsEnumToSpellDetailsMap = new Dictionary<spellEnum, SpellDetails>();
        int i = 0;
        foreach (InspectableSpellDictionaryEntry entry in inspectorDictionary)
        {
            var key = elementsPairToSpellEnum[new ElementsPair(entry.firstElement, entry.secondElement)];
            if (spellsEnumToSpellDetailsMap.ContainsKey(key))
            {
                Debug.Log("[Spells][createSpellsEnumToSpellDetailsMap] Warning: There exists duplicte key for element " + i + ": "
                    + entry.firstElement + " | " + entry.secondElement);
            }
            else
            {
                spellsEnumToSpellDetailsMap.Add(key, entry.spellDetails);

                // below is a way to ignore order of spells
                if (entry.secondElement != Elements.elemEnum.none && entry.firstElement != entry.secondElement)
                {
                    var secondkey = elementsPairToSpellEnum[new ElementsPair(entry.secondElement, entry.firstElement)];
                    var secondSpellDetails = new SpellDetails(entry.spellDetails.secondElementCost, entry.spellDetails.firstElementCost, entry.spellDetails.spellObject);
                    spellsEnumToSpellDetailsMap.Add(secondkey, secondSpellDetails);
                }
            }
            i++;
        }

        return spellsEnumToSpellDetailsMap;
    }



    /*
     * This is for inspector to work.
     * Use it like following:
     * [SerializeField]
     * private List<Spells.SpellDictionaryEntry> inspectorDictionary;
     * 
     * and pass it to createSpellEnumToSpellObjectMap(List<Spell.SpellDictionaryEntry> inspectorDictionary); to create
     */
    [System.Serializable]
    public class InspectableSpellDictionaryEntry
    {
        public Elements.elemEnum firstElement;
        public Elements.elemEnum secondElement;
        public SpellDetails spellDetails;
    }

    [System.Serializable]
    public class SpellDetails
    {
        public int firstElementCost;
        public int secondElementCost;
        public GameObject spellObject;

        public SpellDetails(int firstElementCost, int secondElementCost, GameObject spellObject)
        {
            this.firstElementCost = firstElementCost;
            this.secondElementCost = secondElementCost;
            this.spellObject = spellObject;
        }
    }
}