
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public class S_SignURL : MonoBehaviour
{
    //this Script Base on https://docs.unity3d.com/Packages/com.unity.addressables@1.14/manual/TransformInternalId.html
    public S_GenerateV4SignedReadUrl m_SingURL;
    void Start()
    {
        //Need To Fix All Bundle path in this func
        Addressables.ResourceManager.InternalIdTransformFunc += TransformFuncContent;
    }
    string TransformFuncContent(IResourceLocation location)
    {
        //Get the url you want to use to point to your current server
        string _OldContentURL = null;// == TestCloud....bundle (begin with Remote Load path on Addressable Asset Setitng)
        string _currentUrlToUse = null;// == https//..

        if (location.InternalId.StartsWith("TestCloud"))
        {
            _OldContentURL = location.InternalId;
            Debug.Log("Use Signed URL = " + location.InternalId);
            //Sign your Url to Google Cloud
            _currentUrlToUse = m_SingURL.Func_GetSignedURL(location.InternalId);
            Debug.LogWarning("location.InternalId.Replace = " + location.InternalId.Replace(location.InternalId, _currentUrlToUse));
            return location.InternalId.Replace(location.InternalId, _currentUrlToUse);
        }
        Debug.LogWarning("location.InternalId = " + location.InternalId);
        return location.InternalId;
    }

}
