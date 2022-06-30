using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class EgitimManager : MonoBehaviour
{
    [SerializeField] private GameObject startBtn, geriDonBtn;
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject kokiciPrefab;
    [SerializeField] private GameObject aciklamaText;

    [SerializeField] private Transform content;

    [SerializeField] private Sprite[] kokiciResimler;
    [SerializeField] private Sprite[] kokdisiResimler;

    [SerializeField] private Image kokDisiImage;

    [SerializeField] private AudioClip alistirmaClip;

    void Start()
    {
        aciklamaText.SetActive(false);
        if(startBtn != null)
        {
            startBtn.GetComponent<RectTransform>().localScale = Vector3.zero;
        }        

        if(geriDonBtn != null)
        {
            geriDonBtn.GetComponent<RectTransform>().localScale = Vector3.zero;
        }

        fadePanel.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(ilkAyariYap);
    }

    public void ilkAyariYap()
    {
        fadePanel.SetActive(false);
        aciklamaText.SetActive(true);
        ButonlariAc();
        kokiciResimleriOlustur();
        SesiCal(alistirmaClip);
    }

    public void ButonlariAc()
    {
        startBtn.GetComponent<RectTransform>().DOScale(1, 1f).SetEase(Ease.OutBounce);
        geriDonBtn.GetComponent<RectTransform>().DOScale(1, 1f).SetEase(Ease.OutBounce);
    }

    public void kokiciResimleriOlustur()
    {
        for(int i=0; i<kokiciResimler.Length; i++)
        {
            GameObject kokiciItem = Instantiate(kokiciPrefab, content);

            kokiciItem.GetComponent<KokiciButtonManager>().buttonNo = i;

            kokiciItem.transform.GetChild(3).GetComponent<Image>().sprite = kokiciResimler[i];
        }
    }

    public void kokDisiResimGoster(int buttonNo)
    {
        kokDisiImage.sprite = kokdisiResimler[buttonNo];
    }

    public void menuyeDon()
    {
        SceneManager.LoadScene("menuLevel");
    }

    public void oyunaBasla()
    {
        SceneManager.LoadScene("gameLevel");
    }

    void SesiCal(AudioClip clip)
    {
        if(clip)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1f);
        }
    }
}
