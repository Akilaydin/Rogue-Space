using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    public GameObject[] panels;
    public Slider loadingSlider;

    void Start(){
        HideAllPanels();
    }
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
    public void LaunchGame(){
        StartCoroutine(loadLevelAsync(1));
    }

    IEnumerator loadLevelAsync(int sceneIndex){
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneIndex);

        while (loadOperation.isDone == false){
            float loadProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = loadProgress;
            yield return null;
        }
    }
}
