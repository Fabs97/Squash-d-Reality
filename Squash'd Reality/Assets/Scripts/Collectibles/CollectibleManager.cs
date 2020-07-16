using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public void showText(string index)
    {
        if(PlayerPrefs.GetString("Collectible_"+index, "false") == "true")
        {
            string collectibleText = getCollectible(Int32.Parse(index));
       
        }
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

}
