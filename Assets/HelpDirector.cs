using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpDirector : MonoBehaviour
{

    [SerializeField] Button buttonHelp;
    public bool IsHelpPanel = false;
    public GameObject HelpPanel;
    public GameObject HelpPanel2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    
    public void OnClickHelp()
    {
        Debug.Log("ButtonHelp is clicked");
        IsHelpPanel = !(IsHelpPanel);
        HelpPanel.SetActive(IsHelpPanel);
        HelpPanel2.SetActive(IsHelpPanel);
        
    }
    
}
