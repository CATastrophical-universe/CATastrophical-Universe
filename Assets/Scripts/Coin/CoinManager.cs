using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int CoinCount;
    public GameObject LevelEndBlock;
    public Text CoinText;
    public int LevelClearCoinCount;
    public bool blockDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        LevelClearCoinCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        CoinText.text = CoinCount.ToString() + " / " + LevelClearCoinCount.ToString();

        if (CoinCount >= LevelClearCoinCount && !blockDestroyed)
        {
            blockDestroyed = true;
            Destroy(LevelEndBlock);
        }
    }
}
