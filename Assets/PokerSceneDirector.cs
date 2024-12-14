using DG.Tweening;
using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class PokerSceneDirector : MonoBehaviour
{
    //エディタから設定
    [SerializeField] CardsDirector cardsDirector;
    [SerializeField] Button buttonBetCoin;
    [SerializeField] Button buttonPlay;
    [SerializeField] Button buttonChange;
    [SerializeField] Button buttonHandIn;
    [SerializeField] Text textGameInfo;
    [SerializeField] Text textRate;
    //ボタン内のテキスト（なかのテキストを変えるため）
    Text textButtonBetCoin;
    Text textButtonChange;
    Text textButtonHandIn;

    //全カード（山札）
    List<CardController> cards;
    // Player
    public int Player = 0;
    // Featured Cards
    public const int LowCardsMax = 9;
    public const int MidCardsMax = 13;
    public const int HighCardsMax = 7;
    public const int FeaturedCardsMAX = 6;

    //public int[,] FeaturedCardsLowHand = new int[2, LowCardsMax];
    //public int[,] FeaturedCardsMidHand = new int[2, MidCardsMax];
    //public int[,] FeaturedCardsHighHand = new int[2, HighCardsMax];
    List<List<int>> FeaturedCardsLowHand = new List<List<int>>
    {
        new List<int> { },
        new List<int> { }
    };
    List<List<int>> FeaturedCardsMidHand = new List<List<int>>
    {
        new List<int> { },
        new List<int> { }
    };
    List<List<int>> FeaturedCardsHighHand = new List<List<int>>
    {
        new List<int> { },
        new List<int> { }
    };


    public int[] LowIndex = new int[2];
    public int[] MidIndex = new int[2];
    public int[] HighIndex = new int[2];
    public int LowNumber = 0;
    public int MidNumber = 0;
    public int HighNumber = 0;


    //手札
    public int[] PlayersHandLength = new int[2];
    List<List<CardController>> hand = new List<List<CardController>>
    {
        new List<CardController> {},
        new List<CardController> {}
    };
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
    
    //round count
    public int[] RoundCount = new int[2];
    //アニメーション時間
    const float SortHandTime = 0.5f;

    // featuredcard : constrain mark to draw
    List<List<SuitType>> ConstrainMark = new List<List<SuitType>>()
    {
        new List<SuitType> {SuitType.Heart, SuitType.Spade, SuitType.Club},
        new List<SuitType> {}
    };
    public int ConstrainNum = 0;
    public bool ConstrainRound = false;

    // turn
    public int turn = 0;

    // Start is called before the first frame update
    void Start()
    {
        //カードを取得
        cards = cardsDirector.GetShuffleCards();

        //配列データを初期化
        hand[0] = new List<CardController>();
        hand[1] = new List<CardController>();
        selectCards = new List<CardController>();

        //ボタン内のテキストを取得
        textButtonBetCoin = buttonBetCoin.GetComponentInChildren<Text>();
        textButtonChange = buttonChange.GetComponentInChildren<Text>();
        
        textButtonHandIn = buttonHandIn.GetComponentInChildren<Text>();

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
            if (Physics.Raycast(ray, out RaycastHit hit))
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

        if (selectCards.Count > 0)
        {
            textGameInfo.text = "";
        }
    }
    //手札を加える
    CardController addHand()
    {   //山札からカードを取得してインデックスを進める
        CardController card = cards[0]; // CardController card = cards[dealCardCount++]
        SuitType suit = card.Suit;
        //手札に加える
        int temp = 0;
        
        while (!(ConstrainMark[Player].Contains(suit)) && ConstrainNum > 0)
        {
            temp++;
            card = cards[temp];
            suit = card.Suit;
        }

        hand[Player].Add(card);
        cards.Remove(card);
        
        
        //引いたカードを返す
        return card;

    }

    //手札をめくる
    public void openHand(CardController card)
    {// 回転アニメーション
        card.transform.DORotate(Vector3.zero, SortHandTime)
            .OnComplete(() => { card.FlipCard(); });//アニメが終わった後に呼び出す関数です
    }



    //手札を並べる
    public void sortHand()
    {   //初期位置
        float x = -CardController.Width * (PlayersHandLength[Player] / 2);
        Debug.Log("PlayersHandLength" + PlayersHandLength[Player]);
        //手札を枚数分並べる
        foreach (var item in hand[Player]) //５枚あるので5回
        {//表示位置へアニメーションして移動
            Vector3 pos = new Vector3(x, 0, -0.2f); //この座標に移動
            item.transform.DOMove(pos, SortHandTime); //SortHandTime秒かけて移動するよ
            //次回の表示位置x
            x += CardController.Width;
        }
    }

    //ゲームをスタート

    public void restartGame(bool deal = true)
    {
        //手札、ラウンド取得数, 選択カードをリセット

        //hand[Player].Clear();
        PlayersHandLength[Player] = 7;
        FeaturedCardsLowHand[Player].Clear();
        FeaturedCardsMidHand[Player].Clear();
        FeaturedCardsHighHand[Player].Clear();
        RoundCount[Player] = 0;




        selectCards.Clear();
        //カードを引ける枚数をリセット
        cardChangeCount = cardChangeCountMax;

        //山札から引いた回数をリセット
        dealCardCount = 0;

        //カードシャッフル
        cardsDirector.ShuffleCards(cards);
        //カードの初期設定
        foreach (var item in hand[Player])
        {
            cards.Add(item);
        }
        hand[Player].Clear();
        foreach (var item in cards)
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


        //7枚配って表向きにする
        for (int i = 0; i < PlayersHandLength[Player]; i++)
        {
            openHand(addHand());//手札に加えてオープンする
        }
        //カードを並べる
        sortHand();
    }

    public void restartRound(bool deal = true)
    {
        //手札、手札の数, 選択カードをリセット
        
        hand[Player].Clear();
        PlayersHandLength[Player] = 7;
        

        selectCards.Clear();
        //カードを引ける枚数をリセット
        cardChangeCount = cardChangeCountMax;

        //山札から引いた回数をリセット
        dealCardCount = 0;

        //カードシャッフル
        cardsDirector.ShuffleCards(cards);
        //カードの初期設定
        foreach (var item in cards)
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

        //7枚配って表向きにする

        for (int i = 0; i < PlayersHandLength[Player]; i++)
        {
            openHand(addHand());//手札に加えてオープンする
        }
        
        //カードを並べる
        sortHand();
    }
    
    //レート表を更新
    public void updateTexts()
    {
        
        textButtonBetCoin.text = "手持ちコイン" + playerCoin;
        textGameInfo.text = "a";//"BET枚数" + betCoin;
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
    public void setButtonsInPlay(bool disp = true)
    {
        //textButtonChange.text = "終了";
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
        /*
        for (int i = 0; i < 2; i++)
        {
            Player = i;
            restartGame();
        }
        */
        Player = 0;
        restartGame();
        //ゲーム中のボタンとテキストの更新
        setButtonsInPlay();
        updateTexts();
    }

    //カードの選択状態
    public void setSelectCard(CardController card)
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
        //else if(cards.Count > dealCardCount + selectCards.Count) // tabun kore iranai
        else
        {
            pos.z += 0.02f;
            selectCards.Add(card);
        }

        //更新された場所
        card.transform.position = pos;

        //ボタン更新 (選択枚数が0枚なら終了ボタンに変更)
        textButtonChange.text = "交換";
        /*
        if(selectCards.Count < 1)
        {
            textButtonChange.text = "終了";
        }
        */
    }

    //カード交換
    public void OnClickChange()
    {
        
        //交換しないなら1回で終了
        if(selectCards.Count < 1)
        {
            cardChangeCount = 0;
        }
        //捨てカードを手札から削除
        foreach(var item in selectCards)
        {
            //Debug.Log("selectCards"+item);
            item.gameObject.SetActive(true);
            //裏向きにする
            item.FlipCard(false);
            //山札の場所へ
            item.transform.position = new Vector3(0, 0, 0);
            cards.Add(item);// 山札に追加する
            hand[Player].Remove(item);// 手札から消す
            openHand(addHand());//捨てたらカードを追加
        }
        cardsDirector.ShuffleCards(cards); // nakutemo iikamo?
        Debug.Log("card.count "+ cards.Count);

        selectCards.Clear();
        //並べる
        sortHand();
        setButtonsInPlay();
        //カード交換可能回数
        
        /*
        if(cardChangeCount < 1)
        {
            
            // TODO 役を精算する
            checkHandRank();
        }
        */
        
    }

    public void OnClickHandIn()
    {
        checkHandRank();
        if (!(textGameInfo.text == "役なし"))
        {
            //交換しないなら1回で終了
            if (selectCards.Count < 1)
            {
                cardChangeCount = 0;
            }
            //捨てカードを手札から削除
            foreach (var item in selectCards)
            {
                //Debug.Log("selectCards"+item);
                item.gameObject.SetActive(true);
                //裏向きにする
                item.FlipCard(false);
                //山札の場所へ
                item.transform.position = new Vector3(0, 0, 0);
                cards.Add(item);// 山札に追加する
                hand[Player].Remove(item);// 手札から消す
                openHand(addHand());//捨てたらカードを追加
            }
            setButtonsInPlay();
            
            if (textGameInfo.text == "ストレートフラッシュ！！" || textGameInfo.text == "フラッシュ！！")
            {
                foreach(var item in hand[Player])
                {
                    item.gameObject.SetActive(true);
                    item.FlipCard(false);
                    item.transform.position = new Vector3(0, 0, 0);
                    cards.Add(item);
                }
                hand[Player].Clear();
                setButtonsInPlay(false);

            }
            cardsDirector.ShuffleCards(cards); // nakutemo iikamo?
            selectCards.Clear();
            //並べる
            sortHand();
            
        }
        

    }
    public void OnClickHelp()
    {
        Debug.Log("ButtonHelp is clicked");

    }

    public void checkHandRank()
    {   
        //フラッシュチェック
        int flushcount = 0;
        bool flush = false;
        //1枚目のカードのマーク
        SuitType suit = selectCards[0].Suit;
        
        foreach (var item in selectCards)
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
        for(int i = 0; i < selectCards.Count; i++)
        {   //何枚の数字が連続したか
            int straightcount = 0;
            //現在のカード番号
            int cardno = selectCards[i].No;
            //１枚目から連続しているか調べる
            for(int j = 0; j < selectCards.Count; j++)
            {   //同じカードはスキップ
                if (i == j) continue;

                //見つけたい数字は現在の数字の+1
                int targetno = cardno + 1;
                //13の次は1
                if (13 < targetno) targetno = 1;

                //ターゲットの数字発見
                if(targetno == selectCards[j].No)
                {
                    //連続回数をカウント
                    straightcount++;
                    //今回のカード番号（次回は＋！される）
                    cardno = selectCards[j].No;
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

        for(int i = 0; i < selectCards.Count; i++)
        {
            if (checkcards.Contains(selectCards[i])) continue;

            //同じ数字のカード枚数
            int samenocount = 0;
            int cardno = selectCards[i].No;

            for(int j = 0; j < selectCards.Count; j++)
            {
                if (i == j) continue;
                if(cardno == selectCards[j].No)
                {
                    samenocount++;
                    checkcards.Add(selectCards[j]);
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
            RoundCount[Player] += 2;
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
            RoundCount[Player] += 1;
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

        Debug.Log("RonundCount"+RoundCount[Player]);
        //コイン取得
        playerCoin += addcoin;

        //テキスト更新
        updateTexts();
        textGameInfo.text = infotext; //+ addcoin;
        
        //次回のゲーム用
        betCoin = 0;
        setButtonsInPlay(false);
       
        ChooseFeaturedCards();
    }


    //
    //FeaturedCard
    //

    public void ChooseFeaturedCards()
    {
        if (LowIndex[Player] + MidIndex[Player] + HighIndex[Player] < FeaturedCardsMAX) // 手持ちの特殊カードが最大数ない時
        {
            for (int i = 0; i < LowNumber; i++)
            {
                int rnd = Random.Range(0, LowCardsMax);
                Debug.Log("Low FeaturedCard " + rnd);
                FeaturedCardsLowHand[Player].Add(rnd);
                LowIndex[Player]++;
            }
            for (int i = 0; i < MidNumber; i++)
            {
                int rnd = Random.Range(0, MidCardsMax);
                Debug.Log("Mid FeaturedCard " + rnd);
                FeaturedCardsMidHand[Player].Add(rnd);
                MidIndex[Player]++;
            }
            for (int i = 0; i < HighNumber; i++)
            {
                int rnd = Random.Range(0, HighCardsMax);
                Debug.Log("High FeaturedCard " + rnd);
                FeaturedCardsHighHand[Player].Add(rnd);
                HighIndex[Player]++;
            }
        }
        else //　手持ちの特殊カードが最大数ある時は交換機能をつけたい
        {
            for (int i = 0;i < LowNumber; i++)
            {

            }
            for (int i = 0; i < MidNumber; i++)
            {

            }
            for (int i =0;  i < HighNumber; i++)
            {

            }
        }
        LowNumber = 0;
        MidNumber = 0;
        HighNumber = 0;
    }

    public void AddHandLength()
    {
        if (PlayersHandLength[Player] < 9)
        {
            Debug.Log("HandLength is added");
            PlayersHandLength[Player]++;
            openHand(addHand());
        }
    }

    public void ChooseConstrainMark()
    {
        
    }

    public void UsingFeaturedCard()
    {
        int Low = 0;
        switch (Low)
        {
            case 0: // ３種類のマークを指定して交換（１回）
                ChooseConstrainMark();
                ConstrainNum = 1;
                break;
            case 1: // 相手の手札を2枚指定で交換

                break;
            case 2: // 相手の手札を1枚ランダムで交換
                break;
            case 3: // 相手の手札を２枚公開
                break;
            case 4: // 相手はフラッシュはできなくなる（１ターン）
                break;
            case 5: // １枚好きなマークと交換
                break;
            case 6: // 相手のサポート妨害カード使用禁止（１ターン）
                break;
            case 7: // 好きな枚数だけ手札を交換する
                break;
            case 8: // １枚好きな数字と交換
                break;
        }
        int Mid = 0;
        switch (Mid)
        {
            case 0: // 相手の手札を４枚公開
                break;
            case 1: // ３種類のマークを指定して手札を交換（１ラウンド）
                ChooseConstrainMark();
                ConstrainRound = true;
                break;
            case 2: // 相手の手札をすべて交換
                break;
            case 3: // ２種類のマークを指定して手札を交換（１回）
                ChooseConstrainMark();
                ConstrainNum = 1;
                break;
            case 4: // 相手の手札を３枚ランダムで交換
                break;
            case 5: // 相手の手札を３枚使えなくする（１ターン）
                break;
            case 6: // 相手の手札4枚を好きなやつと交換
                break;
            case 7: // ２枚好きな数字と交換
                break;
            case 8: // ２枚好きなマークと交換
                break;
            case 9: // 手札が１枚増える（最大手札９枚まで）
                AddHandLength();
                break;
            case 10: // 相手のサポート妨害カード使用禁止（３ターン）
                break;
            case 11: // 相手はすべてのフラッシュはできなくなる（１ターン）
                break;
            case 12: //相手の手札を７枚にする
                PlayersHandLength[Player] = 7;
                break;
        }
        int High = 0;
        switch (High)
        {
            case 0: // .相手の手札を５枚にする（ゲームエンドでリセット）
                PlayersHandLength[Player] = 5;
                break;
            case 1: // ２種類のマークを指定して交換（１ラウンド）
                ChooseConstrainMark();
                ConstrainRound = true;
                break;
            case 2: // 相手の手札を４枚を使えなくする（１ターン）
                break;
            case 3: // ４枚好きな数字と交換
                break;
            case 4: // ３枚好きなマークと交換
                ChooseConstrainMark();
                break;
            case 5: // 手札が２枚増える（最大手札９枚まで）
                AddHandLength();
                AddHandLength();
                break;
            case 6: // 相手の勝利点を１減らす（最低０まで）
                if (RoundCount[Player] > 0) RoundCount[Player]--;
                break;
        }
    }

    
}
