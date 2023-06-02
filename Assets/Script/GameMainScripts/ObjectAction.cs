using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectAction : MonoBehaviour
{
    private GameMain gamemainScript;
    private MoveWave moveWave;
    private MovePendulum movependulum;
    private MoveScaleChange scaleChange;
    private MoveRotation moverotation;
    private IncreaseInSize increaseInSize;
    private int Level;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネント取得
        gamemainScript = FindObjectOfType<GameMain>();

        moveWave = this.gameObject.AddComponent<MoveWave>();
        movependulum = this.gameObject.AddComponent<MovePendulum>();
        scaleChange = this.gameObject.AddComponent<MoveScaleChange>();
        moverotation = this.gameObject.AddComponent<MoveRotation>();
        increaseInSize = this.gameObject.AddComponent<IncreaseInSize>();

        //各Moveコンポーネントの初期設定
        moveWave.enabled = false;
        movependulum.enabled = false;
        scaleChange.enabled = false;
        moverotation.enabled = false;
        increaseInSize.enabled = false;

        Level = PlayerManager.instance.NowLevel;

    }

    // Update is called once per frame
    void Update()
    {
        if (Level >= 15 && Level < 28)
        {
            increaseInSize.enabled = true;
        }
        else if (Level > 28 && Level < 43)
        {
            increaseInSize.enabled = false;
            moveWave.enabled = true;
        }
        else if (Level >= 43 && Level < 58)
        {
            moveWave.enabled = false;
            movependulum.enabled = true;
        }
        else if (Level >= 58 && Level < 73)
        {
            movependulum.enabled = false;
            scaleChange.enabled = true;
        }
        else if (Level >= 73 && Level < 88)
        {
            scaleChange.enabled = false;
            moverotation.enabled = true;
        }
        else if (Level >= 88 && Level < 99)
        {
            moverotation.enabled = true;
            moveWave.enabled = false;
            movependulum.enabled = true;
            scaleChange.enabled = true;
        }
        else if (Level >= 99 && Level < 101)
        {
            moverotation.enabled = true;
            moveWave.enabled = true;
            movependulum.enabled = false;
            scaleChange.enabled = true;
        }

    }

    //scale変化、波移動、振り子を関数化
    //レベルごとに混ぜるため
    //レベル30～：scale変化
    //レベル45～：波移動
    //レベル60～：振り子
    //レベル75～：波＋scale変化
    //レベル90～：振り子＋scale変化
    //もしくは、ランダムで取るか……うーん




}
