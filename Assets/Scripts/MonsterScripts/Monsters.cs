using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * This class is meant for getting monster properties prior to instantiation. 
 */
public static class Monsters {
	
	public static Dictionary<GameObject, Vector3> createMonsterToSpawnOffsetMap() {
		// Instantiate dictionary
		Dictionary<GameObject, Vector3> mobToOffsetDict = new Dictionary<GameObject, Vector3> ();

		// Get monster GameObjects
		GameObject ghostWhite = GameObject.Find("Monsters/Ghost/Ghost_White");
		GameObject ghostViolet = GameObject.Find("Monsters/Ghost/Ghost_Violet");
		GameObject ghostBrown = GameObject.Find("Monsters/Ghost/Ghost_Brown");
		GameObject ghostGreen = GameObject.Find("Monsters/Ghost/Ghost_Green");
		GameObject ghostBoss = GameObject.Find("Monsters/Ghost/Ghost_Boss");

		GameObject rabbitYellow = GameObject.Find("Monsters/Rabbit/Rabbit_Yellow");
		GameObject rabbitGreen = GameObject.Find("Monsters/Rabbit/Rabbit_Green");
		GameObject rabbitRed = GameObject.Find("Monsters/Rabbit/Rabbit_Red");
		GameObject rabbitCyan = GameObject.Find("Monsters/Rabbit/Rabbit_Cyan");
		GameObject rabbitBoss = GameObject.Find("Monsters/Rabbit/Rabbit_Boss");

		// Populate dictionary with monster GameObject and its corresponding offset
		mobToOffsetDict.Add(ghostWhite, Ghost.getSpawnOffset());
		mobToOffsetDict.Add(ghostViolet, Ghost.getSpawnOffset());
		mobToOffsetDict.Add(ghostBrown, Ghost.getSpawnOffset());
		mobToOffsetDict.Add(ghostGreen, Ghost.getSpawnOffset());
		mobToOffsetDict.Add(ghostBoss, Ghost.getSpawnOffset());

		mobToOffsetDict.Add(rabbitYellow, Vector3.zero);
		mobToOffsetDict.Add(rabbitGreen, Vector3.zero);
		mobToOffsetDict.Add(rabbitRed, Vector3.zero);
		mobToOffsetDict.Add(rabbitCyan, Vector3.zero);
		mobToOffsetDict.Add(rabbitBoss, Vector3.zero);

		return mobToOffsetDict;
	}

}
