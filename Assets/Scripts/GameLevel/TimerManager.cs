using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private Text sureText;

    [SerializeField] private GameObject sonucPanel;

    [SerializeField] private AudioClip oyunBitisSesi;

    public int kalanSure;

    GameManager gameManager;

    SonucManager sonucManager;

    private void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
    }

    void Start()
    {
        StartCoroutine(SureTimerRoutine());
    }

    void SesiCal(AudioClip clip)
    {
        if (clip)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1f);
        }
    }

    IEnumerator SureTimerRoutine()
    {
        for (int i = 0; i < kalanSure; i--)
        {
            yield return new WaitForSeconds(1f);

            if(kalanSure<10)
            {
                sureText.text = "0" + kalanSure.ToString();
                sureText.color = Color.red;
            }else
            {
                sureText.text = kalanSure.ToString();
            }

            if(kalanSure<=0)
            {
                sureText.text = "00";
                sonucPanel.SetActive(true);

                SesiCal(oyunBitisSesi);

                if (sonucPanel!=null)
                {
                    sonucManager = Object.FindObjectOfType<SonucManager>();
                    sonucManager.SonuclariYazdir(gameManager.dogruAdet, gameManager.yanlisAdet, gameManager.toplamPuan);
                }
            }

            kalanSure--;
        }
    }
}
