using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveText : MonoBehaviour {
    public float timeLoop;
    public float timeMax;
    public Vector3 direct;

    public TextMesh txt;

    public string poolName = "TextPool";


    public void OnEnable()
    {
        StartCoroutine(moveObject());
    }
    IEnumerator moveObject()
    {
        float _time = 0;

        //txt.CrossFadeAlpha(0.0f, timeMax, false);
        while (_time <timeMax)
        {
            _time+=timeLoop;
            transform.Translate(direct * Time.deltaTime);
            yield return new WaitForSeconds(timeLoop);
        }
        PoolManager.Instance.pushObject(poolName, gameObject, null);
    }

    public void setTextMessage(string _txt)
    {
        txt.text = _txt;
    }
    
}
