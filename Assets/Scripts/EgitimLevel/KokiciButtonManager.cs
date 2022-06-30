using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KokiciButtonManager : MonoBehaviour
{
    [SerializeField] private Image kokiciImage;

    public int buttonNo;

    EgitimManager egitimManager;

    private void Awake()
    {
        egitimManager = Object.FindObjectOfType<EgitimManager>();
    }
    
    public void ButonaTiklandi()
    {
        egitimManager.kokDisiResimGoster(buttonNo);
    }
}
