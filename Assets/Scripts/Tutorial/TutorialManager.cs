using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
    public Camera cam;

    [Header("Cube")]
    public GameObject cube;
    public int numberOfCubes;

    [Header("Dialogs")]
    public GameObject[] dialogs;

    int dialogIndex = 0;

    bool training_Cube_Started = false;
    bool training_Cube_Completed = false;

    bool training_MakeSpell_Started = false;
    bool training_MakeSpell_Completed = false;

    bool training_MakeSpell2_Started = false;
    bool training_MakeSpell2_Completed = false;

    bool training_CastSpell_Started = false;
    bool training_CastSpell_Completed = false;

    bool training_GLHF_Started = false;
    bool training_GLHF_Completed = false;

    Dictionary<GameObject, bool[]> dialogDict = new Dictionary<GameObject, bool[]>();

    SpellController spellController;

    // Use this for initialization
    void Start () {
        spellController = cam.GetComponent<SpellController>();

        for (int i = 0; i < dialogs.Length; i++)
        {
            dialogDict.Add(dialogs[i], new bool[] {false, false});
        }

        // display initial dialog
        dialogs[0].gameObject.SetActive(true);
        dialogDict[dialogs[0]][0] = true;
        dialogIndex ++;
    }

    // Update is called once per frame
    void Update () {

        if ((training_CastSpell_Started && !training_CastSpell_Completed) && GameObject.FindGameObjectsWithTag("Monster").Length < 1)
        {
            training_CastSpell_Completed = true;
        }

        if ((training_Cube_Started && !training_Cube_Completed) || (training_CastSpell_Started && !training_CastSpell_Completed))
            return;

        if (dialogDict[dialogs[dialogIndex - 1]][0] && !checkActivity(dialogs[dialogIndex - 1]))
        {
            dialogDict[dialogs[dialogIndex - 1]][1] = true;

            if (!training_Cube_Completed && dialogs[dialogIndex-1].gameObject.name == "dialogQueue_AimAtBoxes")
            {
                training_Cube_Started = true;
                SpawnCubes();
            }

            if (!training_MakeSpell_Completed && dialogs[dialogIndex - 1].gameObject.name == "dialogQueue_MakeSpells")
            {
                training_MakeSpell_Started = true;
                if (checkHandSpellSingleHand())
                {
                    training_MakeSpell_Completed = true;
                }
            }

            if (!training_MakeSpell2_Completed && dialogs[dialogIndex - 1].gameObject.name == "dialogQueue_MakeSpells2")
            {
                training_MakeSpell2_Started = true;
                if (checkHandSpellBothHand())
                {
                    training_MakeSpell2_Completed = true;
                }
            }

            if (!training_CastSpell_Completed && dialogs[dialogIndex - 1].gameObject.name == "dialogQueue_TriggerSpells")
            {
                training_CastSpell_Started = true;
                SpawnCubes();
            }

            if (!training_GLHF_Completed && dialogs[dialogIndex - 1].gameObject.name == "dialogQueue_GLHF")
            {
                training_GLHF_Started = true;
            }
        }

        if (dialogs[dialogIndex - 1].gameObject.name == "dialogQueue_GLHF" && dialogDict[dialogs[dialogIndex - 1]][1])
        {
            SceneManager.LoadScene("GameStart");
        }

        // When index is out of bond, do not load the next dialog
        if (dialogIndex > dialogs.Length - 1) { 
            return;
        }

        if (dialogDict[dialogs[dialogIndex - 1]][1] 
            && (!training_Cube_Started || (training_Cube_Started && training_Cube_Completed))
            && (!training_MakeSpell_Started || (training_MakeSpell_Started && training_MakeSpell_Completed))
            && (!training_MakeSpell2_Started || (training_MakeSpell2_Started && training_MakeSpell2_Completed))
            && (!training_CastSpell_Started || (training_CastSpell_Started && training_CastSpell_Completed))
            )
        {
            dialogs[dialogIndex].gameObject.SetActive(true);
            dialogDict[dialogs[dialogIndex]][0] = true;

            dialogIndex++;
        }
    }

    bool checkActivity(GameObject i)
    {
        return i.gameObject.transform.GetChild(0).gameObject.activeSelf;
    }

    bool checkHandSpellSingleHand()
    {
        return (!spellController.LeftHandElementsPair.isNonePair() || !spellController.RightHandElementsPair.isNonePair());
    }

    bool checkHandSpellBothHand()
    {
        return (!spellController.LeftHandElementsPair.isNonePair() && !spellController.RightHandElementsPair.isNonePair());
    }

    void SpawnCubes()
    {
        // this has two for loops to ensure there are consistent number of cubes on both left and right side
        for (int i = 0; i < numberOfCubes+1; i++)
        {
            //Randomizes position of monster
            int spawnPositionX = Random.Range(0, 90);
            int spawnPositionY = Random.Range(10, 180);
            int spawnPositionZ = Random.Range(10, 90);

            var position = new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ);

            //Creates monster
            var newCube = GameObject.Instantiate(cube, position, Quaternion.identity);
            newCube.SetActive(true);
        }

        for (int i = 0; i < numberOfCubes+1; i++)
        {
            //Randomizes position of monster
            int spawnPositionX = Random.Range(-90, 0);
            int spawnPositionY = Random.Range(10, 180);
            int spawnPositionZ = Random.Range(10, 90);

            var position = new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ);

            //Creates monster
            var newCube = GameObject.Instantiate(cube, position, Quaternion.identity);
            newCube.SetActive(true);
        }
    }

    public void setTrainingCubeCompleted(bool b)
    {
        training_Cube_Completed = b;
    }
}

