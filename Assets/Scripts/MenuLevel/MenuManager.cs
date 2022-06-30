using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject hakkindaPanel;

    bool panelAcikmi;

    public void OyunaBasla()
    {
        SceneManager.LoadScene("EgitimLevel");
    }

    public void HakkindaPanelAc()
    {
        if(!panelAcikmi)
        {
            hakkindaPanel.GetComponent<CanvasGroup>().DOFade(1, 1f);
        }else
        {
            hakkindaPanel.GetComponent<CanvasGroup>().DOFade(0, 1f);
        }
        panelAcikmi = !panelAcikmi;
    }

    public void OyundanÇik()
    {
        Application.Quit();
        Debug.Log("Oyundan Çýktý");
    }
}
