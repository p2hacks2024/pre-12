using DG.Tweening;
using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PokerSceneDirector : MonoBehaviour
{
    //エディタから設定
    [SerializeField] CardsDirector cardsDirector;
    [SerializeField] Button buttonBetCoin;
    [SerializeField] Button buttonPlay;
    [SerializeField] Button buttonChange;
    [SerializeField] Text textGameInfo;
    [SerializeField] Text textRate;
    //ボタン内のテキスト（なかのテキストを変えるため）
    Text textButtonBetCoin;
    Text textButtonChange;
    //全カード
    List<CardController> cards;
    // Featured Cards
    public int[] FeaturedCardsLowHand = new int[2];
    public int[] FeaturedCardsMidHand = new int[2];
    public int[] FeaturedCardsHighHand = new int[2];
    public int LowNumber = 0;
    public int MidNumber = 0;
    public int HighNumber = 0;
    //手札
    List<CardController> hand;
    //交換するカード
    List<CardController> selectCards;
    //山札のインデックス番号（山札から何枚とったか）
    int dealCardCount = 0;
    //プレイヤーの持ちコイン
    [SerializeField] int playerCoin;
    //交換出来る回数
    //[SerializeField] int cardChangeCountMax;
    public int cardChangeCountMax = 100;
    //ベットしたコイン
    int betCoin;
    //交換した回数
    int cardChangeCount;
    //倍率設定
    int straightFlushRate = 10;
    int fourCardRate = 8;
    int fullHouseRate = 6;
    int flushRate = 5;          
    int straightRate = 4;      
    int threeCardRate = 3;    
    int twoPairRate = 2;      
    int onePairRate = 1;
    //Players hand's amount
    public int[] PlayersHandLength = new int[2];
    //round count
    public int[] RoundCount = new int[2];
    //アニメーション時間
    const float SortHandTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //カードを取得
        cards = cardsDirector.GetShuffleCards();

        //配列データを初期化
        hand = new List<CardController>();
        selectCards = new List<CardController>();

        //ボタン内のテキストを取得
        textButtonBetCoin = buttonBetCoin.GetComponentInChildren<Text>();
        textButtonChange = buttonChange.GetComponentInChildren<Text>();

        restartGame(false);
        //テキストとボタンを初期化
        updateTexts();
        setButtonsInPlay(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                //ヒットしたオブジェクトからCardControllerを取得
                CardController card = hit.collider.gameObject.GetComponent<CardController>();
                //カード選択処理
                setSelectCard(card);
            }
        }
        if (Input.GetKeyDown(KeyCode.B)) // press B for adding a card to hand
        {
            AddHandLength();
            sortHand();
        }
        else if (Input.GetKeyDown(KeyCode.N)) // press N for reset RoundConut
        {
            RoundCount[0] = 0; 
            RoundCount[1] = 0;
        }
        else if (Input.GetKeyDown(KeyCode.H)) // press H for reset PlayersHandLength
        {
            PlayersHandLength[0] = 0;
            PlayersHandLength[1] = 0;
        }
    }
    //手札を加える
    CardController addHand()
    {   //山札からカードを取得してインデックスを進める
        CardController card = cards[0]; // CardController card = cards[dealCardCount++]
        //手札に加える
        hand.Add(card);
        cards.Remove(card);
        //引いたカードを返す
        return card;
        
    }

    //手札をめくる
    void openHand(CardController card)
    {// 回転アニメーション
        card.transform.DORotate(Vector3.zero, SortHandTime)
            .OnComplete(() => { card.FlipCard(); });//アニメが終わった後に呼び出す関数です
    }

    
    void AddHandLength()
    {
        if (PlayersHandLength[0] < 9)
        {
            Debug.Log("HandLength is added");
            PlayersHandLength[0]++;
            openHand(addHand());
        }
    }
    //手札を並べる
    void sortHand()
    {   //初期位置
        float x = -CardController.Width * (PlayersHandLength[0] / 2);
        Debug.Log("PlayersHandLength"+PlayersHandLength[0]);
        //手札を枚数分並べる
        foreach (var item in hand) //５枚あるので5回
        {//表示位置へアニメーションして移動
            Vector3 pos = new Vector3(x, 0, -0.2f); //この座標に移動
            item.transform.DOMove(pos, SortHandTime); //SortHandTime秒かけて移動するよ
            //次回の表示位置x
            x += CardController.Width;
        }
    }

    //ゲームをスタート
    void restartGame(bool deal = true)
    {
        //手札、選択カードをリセット
        hand.Clear();
        selectCards.Clear();
        //カードを引ける枚数をリセット
        cardChangeCount = cardChangeCountMax;

        //山札から引いた回数をリセット
        dealCardCount = 0;

        //カードシャッフル
        cardsDirector.ShuffleCards(cards);
        //カードの初期設定
        foreach(var item in cards)
        {   
            //捨て札は非表示状態なので表示する
            item.gameObject.SetActive(true);
            //裏向きにする
            item.FlipCard(false);
            //山札の場所へ
            item.transform.position = new Vector3(0, 0, 0);
        }
        //ここから下は配る処理
        if (!deal) return;

        // プレイヤーの手札の数を７枚に初期化とか
        for (int i = 0; i < 2; i++)
        {
            // 手札の数はラウンドごとに変えるようにして、ラウンド取得数はゲームごとに変える
            PlayersHandLength[i] = 7;
            RoundCount[i] = 0;
        }


        //7枚配って表向きにする
        for(int i = 0; i < PlayersHandLength[0] ; i++)
        {
            openHand(addHand());//手札に加えてオープンする
        }
        //カードを並べる
        sortHand();
    }
    
    //レート表を更新
    void updateTexts()
    {
        
        textButtonBetCoin.text = "手持ちコイン" + playerCoin;
        textGameInfo.text = "BET枚数" + betCoin;
        /*
        textRate.text = "ストレートフラッシュ" + (straightFlushRate * betCoin) + "\n"
        + "フォーカード" + (fourCardRate * betCoin) + "\n"
        + "フルハウス" + (fullHouseRate * betCoin) + "\n"
        + "フラッシュ" + (flushRate * betCoin) + "\n"
        + "スリーカード" + (threeCardRate * betCoin) + "\n"
        + "ツーペア" + (twoPairRate * betCoin) + "\n"
        + "ワンペア" + (onePairRate * betCoin) + "\n";
        */
    } 
    
    //ゲーム中のボタンを表示する
    void setButtonsInPlay(bool disp = true)
    {
        textButtonChange.text = "終了";
        //交換ボタン表示設定
        buttonChange.gameObject.SetActive(disp);
        //ベットとプレイボタン表示設定（非ゲームプレイ）
        buttonBetCoin.gameObject.SetActive(!disp);
        buttonPlay.gameObject.SetActive(!disp);
    }

    //コインをベットする
    public void OnClickBetCoin()
    {
        Debug.Log("BetButton is clicked");
        if (1 > playerCoin) return;
        //コインを減らしてテキストを更新
        playerCoin--; 
        betCoin++;
        updateTexts(); //ベットしたらレートも変わるからレートを更新
    }

    //ゲームプレイボタン
    public void OnClickPlay()
    {
        Debug.Log("PlayButton is clicked");
        //デッキと手札を初期化
        restartGame();
        //ゲーム中のボタンとテキストの更新
        setButtonsInPlay();
        updateTexts();
    }

    //カードの選択状態
    void setSelectCard(CardController card)
    {
        //選択できないカードなら終了
        if (!card || !card.isFrontUp) return;

        //カードの現在地
        Vector3 pos = card.transform.position;

        //２回目選択されたら非選択
        if (selectCards.Contains(card))
        {
            pos.z -= 0.02f;
            selectCards.Remove(card);
        }
        //選択状態（カード上限を超えないように） 
        //else if(cards.Count > dealCardCount + selectCards.Count) // tabun kore tigau
        else
        {
            pos.z += 0.02f;
            selectCards.Add(card);
        }

        //更新された場所
        card.transform.position = pos;

        //ボタン更新 (選択枚数が0枚なら終了ボタンに変更)
        textButtonChange.text = "交換";
        if(selectCards.Count < 1)
        {
            textButtonChange.text = "終了";
        }
    }

    //カード交換
    public void OnClickChange()
    {
        foreach (var i in cards) // YOUKAKUNIN
        {
            //Debug.Log("cards"+i);
        }
        //交換しないなら1回で終了
        if(1 > selectCards.Count)
        {
            cardChangeCount = 0;
        }
        //捨てカードを手札から削除
        foreach(var item in selectCards)
        {
            //Debug.Log("selectCards"+item);
            item.gameObject.SetActive(false); //非表示にする
            cards.Add(item);// 山札に追加する
            hand.Remove(item);// 手札から消す
            openHand(addHand());//捨てたらカードを追加
        }
        // cardsDirector.ShuffleCards(cards); // maybe this is wrong
        Debug.Log("card.count "+ cards.Count);

        selectCards.Clear();
        //並べる
        sortHand();
        setButtonsInPlay();
        //カード交換可能回数
        
        if(1 > cardChangeCount)
        {
            // TODO 役を精算する
            checkHandRank();
        }
    }

    void checkHandRank()
    {  
        
        
        //フラッシュチェック
        int flushcount = 0;
        bool flush = false;
        //1枚目のカードのマーク
        SuitType suit = hand[0].Suit;

        foreach(var item in hand)
        {
            //マークが同じなら
            if(suit == item.Suit)
            {
                flushcount += 1;
                
            }
            if(flushcount >= 5)
            {
                flush = true;
            }
        }

        bool straight = false;
        for(int i = 0; i < hand.Count; i++)
        {   //何枚の数字が連続したか
            int straightcount = 0;
            //現在のカード番号
            int cardno = hand[i].No;
            //１枚目から連続しているか調べる
            for(int j = 0; j < hand.Count; j++)
            {   //同じカードはスキップ
                if (i == j) continue;

                //見つけたい数字は現在の数字の+1
                int targetno = cardno + 1;
                //13の次は1
                if (13 < targetno) targetno = 1;

                //ターゲットの数字発見
                if(targetno == hand[j].No)
                {
                    //連続回数をカウント
                    straightcount++;
                    //今回のカード番号（次回は＋！される）
                    cardno = hand[j].No;
                    //jはまた０から
                    j = -1;
                }
            }
            if(3 < straightcount)
            {
                straight = true;
                break;
            }
        }

        //同じ数字の判定
        int pair = 0;
        bool threecard = false;
        bool fourcard = false;
        List<CardController> checkcards = new List<CardController>();

        for(int i = 0; i < hand.Count; i++)
        {
            if (checkcards.Contains(hand[i])) continue;

            //同じ数字のカード枚数
            int samenocount = 0;
            int cardno = hand[i].No;

            for(int j = 0; j < hand.Count; j++)
            {
                if (i == j) continue;
                if(cardno == hand[j].No)
                {
                    samenocount++;
                    checkcards.Add(hand[j]);
                }
            }

            if(1 == samenocount)
            {
                pair++;
            }
            else if(2 == samenocount)
            {
                threecard = true;
            }
            else if(3 == samenocount)
            {
                fourcard = true;
            }
        }

        bool fullhouse = false;
        if(1 == pair && threecard)
        {
            fullhouse = true;
        }


        bool straightflush = false;
        if(flush && straight)
        {
            straightflush = true;
        }

        //役が揃ったら
        int addcoin = 0;
        string infotext = "役なし";

        if (straightflush)
        {
            addcoin = straightFlushRate * betCoin;
            infotext = "ストレートフラッシュ！！";
            RoundCount[0] += 2;
        }
        else if (fourcard)
        {
            addcoin = fourCardRate * betCoin;
            infotext = "フォーカード";
            HighNumber = 1;
        }
        else if (fullhouse)
        {
            addcoin = fullHouseRate * betCoin;
            infotext = "フルハウス!!";
            HighNumber = 1;
        }
        else if (flush)
        {
            addcoin = flushRate * betCoin;
            infotext = "フラッシュ！！";
            RoundCount[0] += 1;
        }
        else if (straight)
        {
            addcoin = straightRate * betCoin;
            infotext = "ストレート！！";
            MidNumber = 1;
        }
        else if (threecard)
        {
            addcoin = threeCardRate * betCoin;
            infotext = "スリーカード！！";
            MidNumber = 1;
        }
        else if (2 == pair)
        {
            addcoin = twoPairRate * betCoin;
            infotext = "ツーペア！！";
            LowNumber = 2;
        }
        else if (1 == pair)
        {
            addcoin = onePairRate * betCoin;
            infotext = "ワンペア！！";
            LowNumber = 1;
        }

        Debug.Log("RonundCount"+RoundCount[0]);
        //コイン取得
        playerCoin += addcoin;

        //テキスト更新
        updateTexts();
        textGameInfo.text = infotext + addcoin;

        //次回のゲーム用
        betCoin = 0;
        setButtonsInPlay(false);

        ChooseFeaturedCards();
    }

    void ChooseFeaturedCards()
    {
        for (int i = 0; i < LowNumber; i++)
        {
            int rnd = Random.Range(0, 9);
            FeaturedCardsLowHand[0] = rnd;
        }
        for (int i = 0; i < MidNumber; i++)
        {
            int rnd = Random.Range(0, 14);
            FeaturedCardsMidHand[0] = rnd;
        }
        for (int i = 0; i < HighNumber; i++)
        {
            int rnd = Random.Range(0, 7);
            FeaturedCardsHighHand[0] = rnd;
        }
        LowNumber = 0;
        MidNumber = 0;
        HighNumber = 0;
    }
}
