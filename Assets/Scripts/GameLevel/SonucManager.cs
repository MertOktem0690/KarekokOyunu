using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SonucManager : MonoBehaviour
{
    [SerializeField] private GameObject sonucBackgroundImage;
    [SerializeField] private GameObject puanObje, timerObje, dogruYanlisObje, daireObje;

    [SerializeField] private Text dogruText, yanlisText, puanText;
 
    private void OnEnable()
    {
        sonucBackgroundImage.transform.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.OutBack);
        sonucBackgroundImage.GetComponent<CanvasGroup>().DOFade(1, 1.5f);
        EkraniTemizle();
    }

    public void SonuclariYazdir(int dogruAdet,int yanlisAdet, int toplamPuan)
    {
        dogruText.text = dogruAdet.ToString() + " DOÐRU";
        yanlisText.text = yanlisAdet.ToString() + " YANLIÞ";
        puanText.text = toplamPuan.ToString() + " PUAN";
    }

    void EkraniTemizle()
    {
        puanObje.SetActive(false);
        timerObje.SetActive(false);
        dogruYanlisObje.SetActive(false);
        daireObje.SetActive(false);
    }

}
