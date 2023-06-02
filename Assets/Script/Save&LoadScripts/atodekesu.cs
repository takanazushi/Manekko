using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atodekesu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("一時領域のパス："+Application.temporaryCachePath);
        Debug.Log("ストリーミングアセットのパス：" + Application.streamingAssetsPath);
        Debug.Log("Unityが利用するデータが保存されるパス：" + Application.dataPath);
        Debug.Log("実行中に保存されるファイルがあるパス：" + Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
