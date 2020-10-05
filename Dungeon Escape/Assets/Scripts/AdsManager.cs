using System.Collections;

using UnityEngine;

using UnityEngine.Advertisements;



public class AdsManager : MonoBehaviour, IUnityAdsListener

{

    string gameId = "3786775"; // get this from your unity dashboard

    string placement = "rewardedVideo";

    bool testMode = true;



    void Start()

    {

        Advertisement.AddListener(this);

        Advertisement.Initialize(gameId, testMode);

    }



    public void ShowAd()

    {

        Advertisement.Show(placement);

    }



    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)

    {

        switch (showResult)

        {

            case ShowResult.Finished:

                GameManager.Instance.Player.AddGems(100);
                UIManager.Instance.OpenShop(GameManager.Instance.Player.diamonds);

                Debug.Log("100 gems awarded to you");

                break;

            case ShowResult.Skipped:

                Debug.Log("You skipped ad no gems awarded to you");

                break;

            case ShowResult.Failed:

                Debug.Log("Ad video failed to play");

                break;

        }

    }



    public void OnUnityAdsDidError(string message)

    {



    }



    public void OnUnityAdsDidStart(string placementId)

    {



    }



    public void OnUnityAdsReady(string placementId)

    {



    }



} // end of AdsManager