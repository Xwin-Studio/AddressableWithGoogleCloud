using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Cloud.Storage.V1;
using System;
using System.Net.Http;
using TMPro;
using System.IO;

public class S_GenerateV4SignedReadUrl : MonoBehaviour
{
    //This get signed Url to bundle file on google cloud
    public string m_BucketName;
    [SerializeField]
    private TextAsset _Key = null;
    public string Func_GetSignedURL(string _ObjectPathOnCloud)
    {
        string _SignURL = GenerateV4SignedReadUrl(
                m_BucketName, _ObjectPathOnCloud);
        return _SignURL;
    }

    public string GenerateV4SignedReadUrl(
        string bucketName = "your-unique-bucket-name",
        string objectName = "your-object-name")
    {
        //Cause FromServiceAccountPath need a path to file key but can't access file in android 
        //=>create new json file from Text asset key
        string _path = Application.persistentDataPath + "/Credentical.json";
        File.WriteAllText(_path, _Key.ToString());

        UrlSigner urlSigner = UrlSigner.FromServiceAccountPath(_path);
        // V4 is the default signing version.
        string url = urlSigner.Sign(bucketName, objectName, TimeSpan.FromSeconds(50), HttpMethod.Get);
        File.WriteAllText(_path, "");
        return url;
    }
}

