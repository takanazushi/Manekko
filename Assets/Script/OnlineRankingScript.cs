
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlineRankingScript : MonoBehaviour
{

    [SerializeField]
    public int Score;

    // Start is called before the first frame update
    public void LoadOnlineRanking()
    {
        //naichilab.RankingLoader.Instance.SendScoreAndShowRanking(PlayerManager.instance.FirstScore);
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(Score, 0);
    }

    public void LoadOnlineRanking_EndlessMonde()
    {
        //naichilab.RankingLoader.Instance.SendScoreAndShowRanking(PlayerManager.instance.EndlessFirstScore);
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(Score, 1);
    }
}
