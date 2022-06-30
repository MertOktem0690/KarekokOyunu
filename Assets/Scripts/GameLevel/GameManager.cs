using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite[] kokiciResimler;
    [SerializeField] private Sprite[] kokDisiResimler;

    [SerializeField] private Image morKokDisiImage, maviKokDisiImage, griKokDisiImage;
    [SerializeField] private Image sariKokDisiImage, kirmiziKokDisiImage, yesilKokDisiImage;
    [SerializeField] private Image ustkokiciImage, altkokiciImage;

    [SerializeField] private Transform sorularCarki;
    [SerializeField] private Transform solBar, sagBar;

    [SerializeField] private GameObject dogruImage, yanlisImage, dogruYanlisObje;
    [SerializeField] private GameObject bonusObje;

    [SerializeField] private Text dogruText, yanlisText, puanText;

    [SerializeField] private AudioClip baslangicSesi, daireSesi, kapanisSesi;

    int hangiSoru;
    int kacinciYanlis;
    public int dogruAdet, yanlisAdet;
    public int puanArtisi, toplamPuan;
    int bonusAdet;

    bool carkDonsunmu;
    bool daireUsttemi;
    bool butonaBasilsinmi;

    string butondakiResim;

    Vector3 birinciSolBar = new Vector3(-350, 110, 0);
    Vector3 ikinciSolBar = new Vector3(-240, 110, 0);
    Vector3 ucuncuSolBar = new Vector3(-140, 110, 0);

    Vector3 birinciSagBar = new Vector3(350, 110, 0);
    Vector3 ikinciSagBar = new Vector3(240, 110, 0);
    Vector3 ucuncuSagBar = new Vector3(140, 110, 0);

    void Start()
    {
        daireUsttemi = true;
        carkDonsunmu = true;
        butonaBasilsinmi = true;

        kacinciYanlis = 0;
        bonusAdet = 0;

        puanText.text = toplamPuan.ToString();

        SesiCal(baslangicSesi);

        ResimleriYerlestir();
    }

    public void ResimleriYerlestir()
    {
        hangiSoru = Random.Range(0, kokDisiResimler.Length - 3);

        int rastgeleDeger = Random.Range(0, 100);

        if (daireUsttemi)
        {
            if (rastgeleDeger <= 33)
            {
                morKokDisiImage.sprite = kokDisiResimler[hangiSoru];
                maviKokDisiImage.sprite = kokDisiResimler[hangiSoru + 1];
                griKokDisiImage.sprite = kokDisiResimler[hangiSoru + 2];
            }else if (rastgeleDeger <= 66)
            {
                morKokDisiImage.sprite = kokDisiResimler[hangiSoru + 1];
                maviKokDisiImage.sprite = kokDisiResimler[hangiSoru];
                griKokDisiImage.sprite = kokDisiResimler[hangiSoru + 2];
            }else
            {
                morKokDisiImage.sprite = kokDisiResimler[hangiSoru + 1];
                maviKokDisiImage.sprite = kokDisiResimler[hangiSoru + 2];
                griKokDisiImage.sprite = kokDisiResimler[hangiSoru];
            }
        }else
        {
            if (rastgeleDeger <= 33)
            {
                sariKokDisiImage.sprite = kokDisiResimler[hangiSoru];
                kirmiziKokDisiImage.sprite = kokDisiResimler[hangiSoru + 1];
                yesilKokDisiImage.sprite = kokDisiResimler[hangiSoru + 2];
            }else if (rastgeleDeger <= 66)
            {
                sariKokDisiImage.sprite = kokDisiResimler[hangiSoru + 1];
                kirmiziKokDisiImage.sprite = kokDisiResimler[hangiSoru];
                yesilKokDisiImage.sprite = kokDisiResimler[hangiSoru + 2];
            }else
            {
                sariKokDisiImage.sprite = kokDisiResimler[hangiSoru + 1];
                kirmiziKokDisiImage.sprite = kokDisiResimler[hangiSoru + 2];
                yesilKokDisiImage.sprite = kokDisiResimler[hangiSoru];
            }
        }

        if(daireUsttemi)
        {
            ustkokiciImage.sprite = kokiciResimler[hangiSoru];
        }else
        {
            altkokiciImage.sprite = kokiciResimler[hangiSoru];
        }

        daireUsttemi = !daireUsttemi;
    }

    public void ButonaBasildi()
    {
        butondakiResim = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite.name;

        if(butonaBasilsinmi)
        {
            butonaBasilsinmi = false;

            SonucuKontrolEt();
        }

    }

    public void SonucuKontrolEt()
    {
        if(butondakiResim == kokDisiResimler[hangiSoru].name)
        {
            dogruAdet++;
            bonusAdet++;
            dogruText.text = dogruAdet.ToString();
            DogruYanlisImageKontrolEt(true);
            SorularDonsun();
          //Kaç doðru cevapta bonus baþlýyacaðý burdan kontrol ediliyor.
            if(bonusAdet>=2)
            {
                puanArtisi += 30;
                BonusScaleOn();
            }else
            {
                puanArtisi += 20;
                BonusScaleOff();
            }
        }else
        {
            yanlisAdet++;
            BonusScaleOff();
            bonusAdet = 0;
            yanlisText.text = yanlisAdet.ToString();
            DogruYanlisImageKontrolEt(false);
            kacinciYanlis++;
            BarlariKapat(kacinciYanlis);
            puanArtisi -= 5;
        }

        toplamPuan += puanArtisi;

        //Sýfýrýn altýna düþmeyi engelleyen kod
        //if(toplamPuan<=0)
        //{
        //    toplamPuan = 0;
        //}

        puanArtisi = 0;
        puanText.text = toplamPuan.ToString();
    }

    public void SorularDonsun()
    {
        if(carkDonsunmu)
        {
            carkDonsunmu = false;
            kacinciYanlis = 0;

            solBar.DOLocalMove(birinciSolBar, 0.5f);
            sagBar.DOLocalMove(birinciSagBar, 0.5f);

            SesiCal(daireSesi);

            ResimleriYerlestir();
            sorularCarki.DORotate(sorularCarki.rotation.eulerAngles + new Vector3(0, 0, 180f), 0.5f).OnComplete(CarkDonsunmuTrueYap);
        }
    }

    public void CarkDonsunmuTrueYap()
    {
        butonaBasilsinmi = true;
        carkDonsunmu = true;
    }

    public void BarlariKapat(int kacinciYanlis)
    {
        SesiCal(kapanisSesi);

        if(kacinciYanlis==1)
        {
            butonaBasilsinmi = true;
            solBar.DOLocalMove(ikinciSolBar, 0.5f);
            sagBar.DOLocalMove(ikinciSagBar, 0.5f);
        }else if(kacinciYanlis==2)
        {
            butonaBasilsinmi = false;
            solBar.DOLocalMove(ucuncuSolBar, .05f);
            sagBar.DOLocalMove(ucuncuSagBar, .05f).OnComplete(BarlarinYenilenmesiniBekle);
        }
    }

    public void BarlarinYenilenmesiniBekle()
    {
        CarkDonsunmuTrueYap();

        Invoke("SorularDonsun", 0.5f);
    }

    public void DogruYanlisImageKontrolEt(bool dogrumu)
    {
        dogruYanlisObje.GetComponent<CanvasGroup>().alpha = 0;
        if(dogrumu)
        {
            dogruImage.SetActive(true);
            yanlisImage.SetActive(false);
        }else
        {
            yanlisImage.SetActive(true);
            dogruImage.SetActive(false);
        }
        dogruYanlisObje.GetComponent<CanvasGroup>().DOFade(1, 0.5f).OnComplete(DogruYanlisObjeYokEt);
    }

    public void DogruYanlisObjeYokEt()
    {
        dogruYanlisObje.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
    }

    public void BonusScaleOn()
    {
        bonusObje.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutElastic);
    }

    public void BonusScaleOff()
    {
        bonusObje.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InElastic);
    }

    void SesiCal(AudioClip clip)
    {
        if (clip)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1f);
        }
    }

    public void GeriDon()
    {
        SceneManager.LoadScene("egitimLevel");
    } 
    
    public void TekrarOyna()
    {
        SceneManager.LoadScene("gameLevel");
    }
}