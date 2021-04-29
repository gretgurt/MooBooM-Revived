using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager{

    
    //private static List<int> levelStarsList = new List<int>();
    //private static List<List<int>> levelStarsofChapters = new List<List<int>>();    
    private static int[][] levelStarsofChapters;     //[chapter][levels]
    private static int[] levelStarsChap1 = new int[9];
    private static int[] levelStarsChap2 = new int[9];
    private static int[] levelStarsChap3 = new int[9];

    private static int chapter = 1;
    public static void SaveLevelStars(GameController gameController, LevelSelect levelSelect) {
        
        if (levelStarsofChapters == null)
        {
            levelStarsofChapters = new int[3][];
            levelStarsofChapters[0] = levelStarsChap1;
            levelStarsofChapters[1] = levelStarsChap2;
            levelStarsofChapters[2] = levelStarsChap3;
        }

        BinaryFormatter formatter = new BinaryFormatter();  //Creates a binary formatter
        //vvv -- A save path that is different on PC, Mac or i.e Android but end up in a file called "playerProgress.save"
        string path = Application.persistentDataPath + "/playerProgress.save";
        FileStream fileStream = new FileStream(path, FileMode.Create); //A stream of data contained in a file

        if(fileStream.Length > 0)
        {
            levelStarsofChapters = formatter.Deserialize(fileStream) as int[][];
        }
        
        int levelNumber = levelSelect.getLevelNameAsInt();
        int starstoAdd = gameController.getStarsCount();

        //If the level has been played before and the existing starCount is lower than the new one  -> replace
        //This adds them in order
        if (levelStarsofChapters[chapter - 1][levelNumber - 1] < starstoAdd)
        {
            levelStarsofChapters[chapter - 1][levelNumber - 1] = starstoAdd;
        }

        
        string output = "";
        foreach (int x in levelStarsofChapters[chapter - 1])
        {
            output = output + (" " + x);
            
        }
        Debug.Log("Earened stars on chapter " + chapter + ": " + output);

        formatter.Serialize(fileStream, levelStarsofChapters);  //Write data to the file, binary
        fileStream.Close();

        
    }

    public static void SetChapterNumber(int chapterNumber) {
        chapter = chapterNumber;
    }

    public static int getChapterNumber() {
        return chapter;
    }

    public static bool isLevelUnlocked(int chapter, int currentLevel)
    {
        /* //Loads the players progress if it hasn't already been loaded
        if (levelStarsofChapters == null)
        {
            levelStarsofChapters = LoadSaveProgress();
        }*/

        //If the specified level have 1 or more stars, you can play the next level
        if (levelStarsofChapters != null)
        {
            if (levelStarsofChapters[chapter - 1][currentLevel - 1] > 0)
            {
                return true;
            }
        }
        return false;
    }

    public static int[][] LoadSaveProgress() {
        string path = Application.persistentDataPath + "/playerProgress.save";
        FileStream fileStream = new FileStream(path, FileMode.Open);  //Open the existing data
        if (File.Exists(path) && fileStream.Length > 0)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            levelStarsofChapters = formatter.Deserialize(fileStream) as int[][]; //Reads the file ( -> from binary to original) casts it to int[][]
            fileStream.Close();
            return levelStarsofChapters;
        }
        else
        {
            Debug.LogError("Could not find saved data from " + path);   //error message
            return null; 
        }
    }
}