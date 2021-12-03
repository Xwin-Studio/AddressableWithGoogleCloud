using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.ResourceManagement.AsyncOperations;
public class S_LoadAddressable : MonoBehaviour
{
    public AssetReference m_Scene;
    public TextMeshProUGUI m_Text_Alert;
    public TextMeshProUGUI m_Text_Process;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            m_Text_Alert.text = "Start Load";
            //Addressables.GetDownloadSizeAsync(m_LoadAsset);
            StartCoroutine(LoadAssets());
        }
    }


    private IEnumerator LoadAssets()
    {
        var isDone = false;
        //Addessable will Load m_Scene
        var download = Addressables.LoadSceneAsync(m_Scene, LoadSceneMode.Additive);

        download.Completed += Func_Complete;
        download.Completed += (operation) =>
        {
            isDone = true;
            m_Text_Process.text = "Done";
        };

        //Show process Download
        while (!isDone)
        {
            m_Text_Process.text = download.PercentComplete.ToString();
            yield return 0f;
        }

        yield return new WaitUntil(() => isDone);
    }

    //After Download Complete
    public void Func_Complete(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> obj)
    {
        if(obj.Status == AsyncOperationStatus.Succeeded)
        {
            m_Text_Alert.text = "Load Scene Succeeded";
        }
    }
}
