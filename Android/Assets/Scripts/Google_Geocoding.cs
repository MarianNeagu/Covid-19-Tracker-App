﻿using UnityEngine;
using LitJson;
using System;
using System.Collections;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using System.Globalization;

public class parseJSON
{
    public string title;
    public string id;
    public ArrayList but_title;
    public ArrayList but_image;
}
public class Google_Geocoding : MonoBehaviour
{
    // Sample JSON for the following script has attached.
    public CreateHomeLocURL createHomeURL_Script;

    public void StartRequest()
    {
        StartCoroutine(Request());
       // request cand apesi SAVE la profil
    }


    IEnumerator Request()
    {

        UnityWebRequest webData = UnityWebRequest.Get(createHomeURL_Script.myURL);
        yield return webData.SendWebRequest();
   

        //string url = "http://rama-project.com/text.json";
        //WWW www = new WWW(url);
      //  yield return www;

        string json_data = System.Text.Encoding.UTF8.GetString(webData.downloadHandler.data);
        Debug.Log(json_data);
        search(json_data);
        //Processjson(json_data);
        //Debug.Log(www.text.ToString());      
    }
    private void Processjson(string jsonString)
    {
       
        JsonData jsonvale = JsonMapper.ToObject(jsonString);
        parseJSON parsejson;
        parsejson = new parseJSON();
        //Debug.Log(parsejson["results"]);
    }

    private void search(string data)
    {
        //Debug.Log(data[data.IndexOf("lat") + 6]);
        int pos1 = data.IndexOf("lat");
        int pos2 = data.IndexOf("lng");
        //Debug.Log(data.Substring(pos));
        //Debug.Log(data.Substring(pos + 4));
        string lat = data.Substring(pos1);
        string lng = data.Substring(pos2);
        var theLngNumber = Regex.Match(lng, "[\\+\\-]?\\d+\\.?\\d+").Value;

        var theNumberString = Regex.Match(lat, "[\\+\\-]?\\d+\\.?\\d+").Value;
        
        print(theNumberString);
        print(theLngNumber);
        float latitudine = float.Parse(theNumberString, CultureInfo.InvariantCulture.NumberFormat);
        float longitudine = float.Parse(theLngNumber, CultureInfo.InvariantCulture.NumberFormat);

        PlayerPrefs.SetFloat("home_lat", latitudine);
        PlayerPrefs.SetFloat("home_lng", longitudine);

        print(latitudine);
        print(longitudine);
    }
}