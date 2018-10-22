using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public string AdsID;

    void Start()
    {
        Advertisement.Initialize(AdsID, true);
    }

    public void ShowAds()
    {
        if (GlobalManager.Instance.DATA.SaveData.telahMembeliNoAds == false) //cek jika dia belum beli InApp NoAds
        {
            StartCoroutine(ShowAdWhenReady()); //Maka Tampilkan Ads
        }
    }

    IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady()) //menunggu sampai Ads Siap ditampilkan
            yield return null;

        Advertisement.Show(); //Tampilkan Ads
    }
}