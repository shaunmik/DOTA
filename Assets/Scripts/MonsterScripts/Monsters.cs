using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * This class is meant for getting monster properties prior to instantiation. 
 */
public static class Monsters {
	
	public static Dictionary<GameObject, Vector3> createMonsterToSpawnOffsetMap() {
		GameObject ghostWhite = GameObject.Find("Monsters/Ghost/Ghost_White");
		GameObject ghostViolet = GameObject.Find("Monsters/Ghost/Ghost_Violet");
        GameObject ghostGreen = GameObject.Find("Monsters/Ghost/Ghost_Green");
        GameObject ghostBoss = GameObject.Find("Monsters/Ghost/Ghost_Boss");
		GameObject rabbitYellow = GameObject.Find("Monsters/Rabbit/Rabbit_Yellow");


		Dictionary<GameObject, Vector3> mobToOffsetDict = new Dictionary<GameObject, Vector3> ();

		mobToOffsetDict.Add(ghostWhite, Ghost.getSpawnOffset());
		mobToOffsetDict.Add(ghostBoss, Ghost.getSpawnOffset());
		mobToOffsetDict.Add(ghostViolet, Ghost.getSpawnOffset());
        mobToOffsetDict.Add(ghostGreen, Ghost.getSpawnOffset());
        mobToOffsetDict.Add(rabbitYellow, Vector3.zero);

		return mobToOffsetDict;
	}

}
