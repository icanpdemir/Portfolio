using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectLetterController : MonoBehaviour
{
    public static Letter selectedLetterObject;

    static Dictionary<int, Letter> letterObjects = new Dictionary<int, Letter>();

    static int currentSelectionId;

    public static void initLetterObjects(int id, Letter letterObject)
    {
        letterObjects[id] = letterObject;
    }

    public static void clearLetterObjectsList(){
        letterObjects.Clear();
    }

    IEnumerator Start()
    {
        yield return null;

        foreach (var item in letterObjects)
        {
            if (item.Value.getIsLetterInteractable() && selectedLetterObject == null)
            {
                changeSelectionV2(item.Key, item.Value);
            }
        }

    }

    public static void changeSelectionV2(int id, Letter chosenLetter)
    {
        if (chosenLetter.getIsLetterInteractable())
        {
            if (selectedLetterObject != null)
            {
                selectedLetterObject.changeLetterColor("default");
            }
            selectedLetterObject = letterObjects[id];
            currentSelectionId = id;
            selectedLetterObject.changeLetterColor("selected");
        }
        else
        {
            if (selectedLetterObject == null)
            { // if there is no selected object warn the user
                Debug.Log("Warning! Please select a letter!");
            }
        }

    }

    public static void moveSelectionToNextLetter()
    {
        if (selectedLetterObject != null)
        {
            int start = selectedLetterObject.getLetterId();
            int curIndex;

            for (int i = 1; i <= letterObjects.Count; i++)
            {
                curIndex = (i + start) % letterObjects.Count;

                if (letterObjects[curIndex].getIsLetterInteractable())
                {
                    changeSelectionV2(curIndex, letterObjects[curIndex]);
                    //Debug.Log("Selected item's index is: " + curIndex);
                    return;
                }
            }
        }
        else
        {

            foreach (var item in letterObjects)
            {
                if (item.Value.getIsLetterInteractable())
                {
                    changeSelectionV2(item.Key, item.Value);
                    return;
                }
            }

        }
    }

    public static bool getIsLevelCompleted()
    {
        bool isLevelCompleted = true;
        foreach (var item in letterObjects)
        {
            if (item.Value.getIsLetterInteractable())
            {
                isLevelCompleted = false;
            }
        }
        return isLevelCompleted;
    }

    public static void moveSelectionToPreviousLetter()
    {
        if (selectedLetterObject != null)
        {
            int start = selectedLetterObject.getLetterId();
            int size = letterObjects.Count;
            int curIndex;

            for (int i = 1; i < size; i++)
            {
                curIndex = (start - i + size) % size;

                if (letterObjects[curIndex].getIsLetterInteractable())
                {
                    changeSelectionV2(curIndex, letterObjects[curIndex]);
                    //Debug.Log("Selected item's index is: " + curIndex);
                    return;
                }
            }
        }
        else
        {

            foreach (var item in letterObjects)
            {
                if (item.Value.getIsLetterInteractable())
                {
                    changeSelectionV2(item.Key, item.Value);
                    return;
                }
            }

        }
    }

    public static void checkSelection()
    {
        if (selectedLetterObject != null)
        {
            selectedLetterObject.GetComponent<Letter>().changeLetterColor("selected");
        }
    }



}
