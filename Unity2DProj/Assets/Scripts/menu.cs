using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{

    public GameObject mainmenu;
    public GameObject controls;
    public RectTransform canvas;
    public GameObject button;

    public List<GameObject> buttons;
    public void ExitGame()
    {
        Application.Quit();
    }


    public void ChooseLevel()
    {
        
        int fCount = Directory.GetFiles("Assets/Resources/", "*", SearchOption.AllDirectories).Length / 2;
        
        for (int i = 0; i < fCount; i++)
        {

            //instantiate new button
            GameObject goButton = (GameObject)Instantiate(button);
            

            // set position
            goButton.transform.SetParent(canvas , false);
            goButton.transform.position = new Vector3(766, 440 - (i * 50), 0);
            
            //Set text
            Button tempButton = goButton.GetComponent<Button>();
            tempButton.GetComponentInChildren<Text>().text = "Level: " + (i + 1);

            //Set listener
            //tempButton.onClick.AddListener(() => SetLevel(i));
            int temp = i;
            tempButton.onClick.AddListener(delegate { SetLevel(temp); });
            
        }
        
        mainmenu.SetActive(false);
    }

    public void GetControls()
    {

        mainmenu.SetActive(false);
        controls.SetActive(true);
    }    

    public void MainMenu()
    {
        mainmenu.SetActive(true);
        controls.SetActive(false);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetLevel(int level)
    {


        //Debug.Log("Set level to " + level);
        //set player prefs to level
        PlayerPrefs.SetInt("level", level);
        //switch scene
        SceneManager.LoadScene(1);
        //get player prefs
    }
}
