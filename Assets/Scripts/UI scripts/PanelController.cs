using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    public GameObject[] panels;
    public void ShowPanel(int panelIndex){
        panels[panelIndex].SetActive(true);
    }

    public void HidePanel(int panelIndex){
        panels[panelIndex].SetActive(false);
    }

    public void HideAllPanels(){
        foreach (var panel in panels){
            panel.SetActive(false);
        }
    }

    public void ExitApp(){
        Application.Quit();
    }

    public void LaunchGame(){
        SceneManager.LoadScene(1);
    }
}
