using UnityEngine;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif
using System.Collections;

public class PlayAd : MonoBehaviour {

    public GameObject thePanel;

    public void ShowAd()
    {
        #if UNITY_ADS
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
        }
        #endif
    }

    #if UNITY_ADS
    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                GameManager.Instance.currency += 20;
                GameManager.Instance.Save();
                break;

            case ShowResult.Skipped:
                break;

            case ShowResult.Failed:
                thePanel.SetActive(true);
                break;
    }
    #endif

}
