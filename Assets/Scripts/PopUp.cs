  using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour {

    public GameObject PanelInternet;
    public GameObject PanelgetCurrency;

    public void Start()
    {
        PanelInternet.SetActive(false);
        PanelgetCurrency.SetActive(false);
    }

    public void close()
    {
        PanelInternet.SetActive(false);
        PanelgetCurrency.SetActive(false);
    }

    public void open()
    {
        PanelgetCurrency.SetActive(true);
    }
    
}
