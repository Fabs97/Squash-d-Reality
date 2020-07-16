using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] private GameObject ShowCollectibleButton;
    [SerializeField] private TextMeshProUGUI ShowCollectibleText;
    [SerializeField] private GameObject ShowCollectibleEsc;
    [SerializeField] private GameObject CollectiblesPage;
    private GameObject collectibleSelected = null;
    private void Start()
    {
        ShowCollectibleButton.SetActive(false);
        //PlayerPrefs.DeleteAll();
    }

    public void showText(string index)
    {
        if(PlayerPrefs.GetString("Collectible_"+index, "false") == "true")
        {
            string collectibleText = getCollectible(Int32.Parse(index));
            ShowCollectibleText.text = collectibleText;
            ShowCollectibleButton.SetActive(true);
            ShowCollectibleEsc.SetActive(true);
            CollectiblesPage.SetActive(false);
            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(ShowCollectibleEsc);
        }
    }

    public void setSelected(GameObject selected)
    {
        collectibleSelected = selected;
    }
    public void esc()
    {
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(collectibleSelected);
    }
    //GET COLLECTIBLE TEXT
    public string getCollectible(int collectibleID)
    {
        try
        {
            XmlNode node = parseCollectible(collectibleID);
            XmlNodeList childNodes = node.ChildNodes;
            
            return childNodes[0].InnerText;
        }
        catch (NullReferenceException)
        {
            Debug.LogError("No collectible presence in XML for: "+ collectibleID);
            return null;
        }
    }
    
    //------------------------------------------------------XML E PARSER----------------------------------------------------------------------
    private XmlDocument getXMLFile(string name) {
        XmlDocument toReturn = new XmlDocument(); 
        TextAsset textAsset = (TextAsset) Resources.Load("Collectibles/" + name, typeof(TextAsset));
        toReturn.LoadXml(textAsset.text);
        return toReturn;
    }
    
    private XmlNode parseCollectible(int collectibleID)
    {
        XmlDocument newXml = getXMLFile("Collectibles");
        XmlNodeList collectibleList = newXml.GetElementsByTagName("collectible");
        XmlNode selectedNode = null;
        foreach (XmlNode node in collectibleList)
        {
            if (node.Attributes[0].Value == collectibleID.ToString())
            {
                selectedNode = node;
            }
        }
        return selectedNode;
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
