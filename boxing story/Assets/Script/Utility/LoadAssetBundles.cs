using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetBundles : MonoBehaviour {

    public string BundleURL; 
    public int version;
    void Start()
    {
        StartCoroutine (LoadAssetBundle());
    }
    IEnumerator LoadAssetBundle()
    {
        while (!Caching.ready)
            yield return null;
        using (WWW www = WWW.LoadFromCacheOrDownload(BundleURL, version))
        {
            yield return www;
            if (www.error != null)
                Debug.Log("WWW 다운로드에 에러가 생겼습니다.:" + www.error);

            AssetBundle bundle = www.assetBundle;

            for (int i = 0; i < 3; i++)
            {
                AssetBundleRequest request = bundle.LoadAssetAsync("Cube " + (i + 1), typeof(GameObject));

                yield return request;

                GameObject obj = Instantiate(request.asset) as GameObject;
                obj.transform.position = new Vector3(-10.0f + (i * 10), 0.0f, 0.0f);
            }
            bundle.Unload(false);
            www.Dispose();
        }

    }
}
