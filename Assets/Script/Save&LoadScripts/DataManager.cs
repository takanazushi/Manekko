using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //クラスの参照をする
    public Save saveClass;
    public Read readClass;

    // Start is called before the first frame update
    void Start()
    {

        if (PlayerManager.instance != null)
        {
            Debug.Log("あね");

            Debug.Log("1位のレベル" + PlayerManager.instance.FirstLevel);
            Debug.Log("1位の時間" + PlayerManager.instance.FirstGameTime);
            Debug.Log("1位の間違った回数" + PlayerManager.instance.FirstWrongCount);
            Debug.Log("1位の称号" + PlayerManager.instance.FirstTitle);

            DataSave();

            Debug.Log("1位のレベル" + PlayerManager.instance.FirstLevel);
            Debug.Log("1位の時間" + PlayerManager.instance.FirstGameTime);
            Debug.Log("1位の間違った回数" + PlayerManager.instance.FirstWrongCount);
            Debug.Log("1位の称号" + PlayerManager.instance.FirstTitle);

            DataRead();

            Debug.Log("1位のレベル" + PlayerManager.instance.FirstLevel);
            Debug.Log("1位の時間" + PlayerManager.instance.FirstGameTime);
            Debug.Log("1位の間違った回数" + PlayerManager.instance.FirstWrongCount);
            Debug.Log("1位の称号" + PlayerManager.instance.FirstTitle);
        }
    }

    private void DataRead()
    {
        //データの読み込み
        readClass.enabled = true;

        Debug.Log("読み込みが終わりましたYO～");
    }

    private void DataSave()
    {
        saveClass.enabled = true;

        Debug.Log("セーブができましたYO～");
    }
}
