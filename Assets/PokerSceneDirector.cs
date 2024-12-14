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
    //�G�f�B�^����ݒ�
    [SerializeField] CardsDirector cardsDirector;
    [SerializeField] Button buttonBetCoin;
    [SerializeField] Button buttonPlay;
    [SerializeField] Button buttonChange;
    [SerializeField] Button buttonHandIn;
    [SerializeField] Text textGameInfo;
    [SerializeField] Text textRate;
    //�{�^�����̃e�L�X�g�i�Ȃ��̃e�L�X�g��ς��邽�߁j
    Text textButtonBetCoin;
    Text textButtonChange;
    Text textButtonHandIn;

    //�S�J�[�h�i�R�D�j
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


    //��D
    public int[] PlayersHandLength = new int[2];
    List<List<CardController>> hand = new List<List<CardController>>
    {
        new List<CardController> {},
        new List<CardController> {}
    };
    //��������J�[�h
    List<CardController> selectCards;
    //�R�D�̃C���f�b�N�X�ԍ��i�R�D���牽���Ƃ������j
    int dealCardCount = 0;
    //�v���C���[�̎����R�C��
    [SerializeField] int playerCoin;
    //�����o�����
    //[SerializeField] int cardChangeCountMax;
    public int cardChangeCountMax = 100;
    //�x�b�g�����R�C��
    int betCoin;
    //����������
    int cardChangeCount;
    //�{���ݒ�
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
    //�A�j���[�V��������
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
        //�J�[�h���擾
        cards = cardsDirector.GetShuffleCards();

        //�z��f�[�^��������
        hand[0] = new List<CardController>();
        hand[1] = new List<CardController>();
        selectCards = new List<CardController>();

        //�{�^�����̃e�L�X�g���擾
        textButtonBetCoin = buttonBetCoin.GetComponentInChildren<Text>();
        textButtonChange = buttonChange.GetComponentInChildren<Text>();
        
        textButtonHandIn = buttonHandIn.GetComponentInChildren<Text>();

        restartGame(false);
        //�e�L�X�g�ƃ{�^����������
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
                //�q�b�g�����I�u�W�F�N�g����CardController���擾
                CardController card = hit.collider.gameObject.GetComponent<CardController>();
                //�J�[�h�I������
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
    //��D��������
    CardController addHand()
    {   //�R�D����J�[�h���擾���ăC���f�b�N�X��i�߂�
        CardController card = cards[0]; // CardController card = cards[dealCardCount++]
        SuitType suit = card.Suit;
        //��D�ɉ�����
        int temp = 0;
        
        while (!(ConstrainMark[Player].Contains(suit)) && ConstrainNum > 0)
        {
            temp++;
            card = cards[temp];
            suit = card.Suit;
        }

        hand[Player].Add(card);
        cards.Remove(card);
        
        
        //�������J�[�h��Ԃ�
        return card;

    }

    //��D���߂���
    public void openHand(CardController card)
    {// ��]�A�j���[�V����
        card.transform.DORotate(Vector3.zero, SortHandTime)
            .OnComplete(() => { card.FlipCard(); });//�A�j�����I�������ɌĂяo���֐��ł�
    }



    //��D����ׂ�
    public void sortHand()
    {   //�����ʒu
        float x = -CardController.Width * (PlayersHandLength[Player] / 2);
        Debug.Log("PlayersHandLength" + PlayersHandLength[Player]);
        //��D�𖇐������ׂ�
        foreach (var item in hand[Player]) //�T������̂�5��
        {//�\���ʒu�փA�j���[�V�������Ĉړ�
            Vector3 pos = new Vector3(x, 0, -0.2f); //���̍��W�Ɉړ�
            item.transform.DOMove(pos, SortHandTime); //SortHandTime�b�����Ĉړ������
            //����̕\���ʒux
            x += CardController.Width;
        }
    }

    //�Q�[�����X�^�[�g

    public void restartGame(bool deal = true)
    {
        //��D�A���E���h�擾��, �I���J�[�h�����Z�b�g

        //hand[Player].Clear();
        PlayersHandLength[Player] = 7;
        FeaturedCardsLowHand[Player].Clear();
        FeaturedCardsMidHand[Player].Clear();
        FeaturedCardsHighHand[Player].Clear();
        RoundCount[Player] = 0;




        selectCards.Clear();
        //�J�[�h�������閇�������Z�b�g
        cardChangeCount = cardChangeCountMax;

        //�R�D����������񐔂����Z�b�g
        dealCardCount = 0;

        //�J�[�h�V���b�t��
        cardsDirector.ShuffleCards(cards);
        //�J�[�h�̏����ݒ�
        foreach (var item in hand[Player])
        {
            cards.Add(item);
        }
        hand[Player].Clear();
        foreach (var item in cards)
        {
            //�̂ĎD�͔�\����ԂȂ̂ŕ\������
            item.gameObject.SetActive(true);
            //�������ɂ���
            item.FlipCard(false);
            //�R�D�̏ꏊ��
            item.transform.position = new Vector3(0, 0, 0);
        }

        //�������牺�͔z�鏈��
        if (!deal) return;

        // �v���C���[�̎�D�̐����V���ɏ������Ƃ�


        //7���z���ĕ\�����ɂ���
        for (int i = 0; i < PlayersHandLength[Player]; i++)
        {
            openHand(addHand());//��D�ɉ����ăI�[�v������
        }
        //�J�[�h����ׂ�
        sortHand();
    }

    public void restartRound(bool deal = true)
    {
        //��D�A��D�̐�, �I���J�[�h�����Z�b�g
        
        hand[Player].Clear();
        PlayersHandLength[Player] = 7;
        

        selectCards.Clear();
        //�J�[�h�������閇�������Z�b�g
        cardChangeCount = cardChangeCountMax;

        //�R�D����������񐔂����Z�b�g
        dealCardCount = 0;

        //�J�[�h�V���b�t��
        cardsDirector.ShuffleCards(cards);
        //�J�[�h�̏����ݒ�
        foreach (var item in cards)
        {
            //�̂ĎD�͔�\����ԂȂ̂ŕ\������
            item.gameObject.SetActive(true);
            //�������ɂ���
            item.FlipCard(false);
            //�R�D�̏ꏊ��
            item.transform.position = new Vector3(0, 0, 0);
        }
        //�������牺�͔z�鏈��
        if (!deal) return;

        //7���z���ĕ\�����ɂ���

        for (int i = 0; i < PlayersHandLength[Player]; i++)
        {
            openHand(addHand());//��D�ɉ����ăI�[�v������
        }
        
        //�J�[�h����ׂ�
        sortHand();
    }
    
    //���[�g�\���X�V
    public void updateTexts()
    {
        
        textButtonBetCoin.text = "�莝���R�C��" + playerCoin;
        textGameInfo.text = "a";//"BET����" + betCoin;
        /*
        textRate.text = "�X�g���[�g�t���b�V��" + (straightFlushRate * betCoin) + "\n"
        + "�t�H�[�J�[�h" + (fourCardRate * betCoin) + "\n"
        + "�t���n�E�X" + (fullHouseRate * betCoin) + "\n"
        + "�t���b�V��" + (flushRate * betCoin) + "\n"
        + "�X���[�J�[�h" + (threeCardRate * betCoin) + "\n"
        + "�c�[�y�A" + (twoPairRate * betCoin) + "\n"
        + "�����y�A" + (onePairRate * betCoin) + "\n";
        */
    } 
    
    //�Q�[�����̃{�^����\������
    public void setButtonsInPlay(bool disp = true)
    {
        //textButtonChange.text = "�I��";
        //�����{�^���\���ݒ�
        buttonChange.gameObject.SetActive(disp);
        //�x�b�g�ƃv���C�{�^���\���ݒ�i��Q�[���v���C�j
        buttonBetCoin.gameObject.SetActive(!disp);
        buttonPlay.gameObject.SetActive(!disp);
    }

    //�R�C�����x�b�g����
    public void OnClickBetCoin()
    {
        Debug.Log("BetButton is clicked");
        if (1 > playerCoin) return;
        //�R�C�������炵�ăe�L�X�g���X�V
        playerCoin--; 
        betCoin++;
        updateTexts(); //�x�b�g�����烌�[�g���ς�邩�烌�[�g���X�V
    }

    //�Q�[���v���C�{�^��
    public void OnClickPlay()
    {
        Debug.Log("PlayButton is clicked");
        //�f�b�L�Ǝ�D��������
        /*
        for (int i = 0; i < 2; i++)
        {
            Player = i;
            restartGame();
        }
        */
        Player = 0;
        restartGame();
        //�Q�[�����̃{�^���ƃe�L�X�g�̍X�V
        setButtonsInPlay();
        updateTexts();
    }

    //�J�[�h�̑I�����
    public void setSelectCard(CardController card)
    {
        //�I���ł��Ȃ��J�[�h�Ȃ�I��
        if (!card || !card.isFrontUp) return;

        //�J�[�h�̌��ݒn
        Vector3 pos = card.transform.position;

        //�Q��ڑI�����ꂽ���I��
        if (selectCards.Contains(card))
        {
            pos.z -= 0.02f;
            selectCards.Remove(card);
        }
        //�I����ԁi�J�[�h����𒴂��Ȃ��悤�Ɂj 
        //else if(cards.Count > dealCardCount + selectCards.Count) // tabun kore iranai
        else
        {
            pos.z += 0.02f;
            selectCards.Add(card);
        }

        //�X�V���ꂽ�ꏊ
        card.transform.position = pos;

        //�{�^���X�V (�I�𖇐���0���Ȃ�I���{�^���ɕύX)
        textButtonChange.text = "����";
        /*
        if(selectCards.Count < 1)
        {
            textButtonChange.text = "�I��";
        }
        */
    }

    //�J�[�h����
    public void OnClickChange()
    {
        
        //�������Ȃ��Ȃ�1��ŏI��
        if(selectCards.Count < 1)
        {
            cardChangeCount = 0;
        }
        //�̂ăJ�[�h����D����폜
        foreach(var item in selectCards)
        {
            //Debug.Log("selectCards"+item);
            item.gameObject.SetActive(true);
            //�������ɂ���
            item.FlipCard(false);
            //�R�D�̏ꏊ��
            item.transform.position = new Vector3(0, 0, 0);
            cards.Add(item);// �R�D�ɒǉ�����
            hand[Player].Remove(item);// ��D�������
            openHand(addHand());//�̂Ă���J�[�h��ǉ�
        }
        cardsDirector.ShuffleCards(cards); // nakutemo iikamo?
        Debug.Log("card.count "+ cards.Count);

        selectCards.Clear();
        //���ׂ�
        sortHand();
        setButtonsInPlay();
        //�J�[�h�����\��
        
        /*
        if(cardChangeCount < 1)
        {
            
            // TODO ���𐸎Z����
            checkHandRank();
        }
        */
        
    }

    public void OnClickHandIn()
    {
        checkHandRank();
        if (!(textGameInfo.text == "���Ȃ�"))
        {
            //�������Ȃ��Ȃ�1��ŏI��
            if (selectCards.Count < 1)
            {
                cardChangeCount = 0;
            }
            //�̂ăJ�[�h����D����폜
            foreach (var item in selectCards)
            {
                //Debug.Log("selectCards"+item);
                item.gameObject.SetActive(true);
                //�������ɂ���
                item.FlipCard(false);
                //�R�D�̏ꏊ��
                item.transform.position = new Vector3(0, 0, 0);
                cards.Add(item);// �R�D�ɒǉ�����
                hand[Player].Remove(item);// ��D�������
                openHand(addHand());//�̂Ă���J�[�h��ǉ�
            }
            setButtonsInPlay();
            
            if (textGameInfo.text == "�X�g���[�g�t���b�V���I�I" || textGameInfo.text == "�t���b�V���I�I")
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
            //���ׂ�
            sortHand();
            
        }
        

    }
    public void OnClickHelp()
    {
        Debug.Log("ButtonHelp is clicked");

    }

    public void checkHandRank()
    {   
        //�t���b�V���`�F�b�N
        int flushcount = 0;
        bool flush = false;
        //1���ڂ̃J�[�h�̃}�[�N
        SuitType suit = selectCards[0].Suit;
        
        foreach (var item in selectCards)
        {
            //�}�[�N�������Ȃ�
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
        {   //�����̐������A��������
            int straightcount = 0;
            //���݂̃J�[�h�ԍ�
            int cardno = selectCards[i].No;
            //�P���ڂ���A�����Ă��邩���ׂ�
            for(int j = 0; j < selectCards.Count; j++)
            {   //�����J�[�h�̓X�L�b�v
                if (i == j) continue;

                //�������������͌��݂̐�����+1
                int targetno = cardno + 1;
                //13�̎���1
                if (13 < targetno) targetno = 1;

                //�^�[�Q�b�g�̐�������
                if(targetno == selectCards[j].No)
                {
                    //�A���񐔂��J�E���g
                    straightcount++;
                    //����̃J�[�h�ԍ��i����́{�I�����j
                    cardno = selectCards[j].No;
                    //j�͂܂��O����
                    j = -1;
                }
            }
            if(3 < straightcount)
            {
                straight = true;
                break;
            }
        }
        
        //���������̔���
        int pair = 0;
        bool threecard = false;
        bool fourcard = false;
        List<CardController> checkcards = new List<CardController>();

        for(int i = 0; i < selectCards.Count; i++)
        {
            if (checkcards.Contains(selectCards[i])) continue;

            //���������̃J�[�h����
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

        //������������
        int addcoin = 0;
        string infotext = "���Ȃ�";

        if (straightflush)
        {
            addcoin = straightFlushRate * betCoin;
            infotext = "�X�g���[�g�t���b�V���I�I";
            RoundCount[Player] += 2;
        }
        else if (fourcard)
        {
            addcoin = fourCardRate * betCoin;
            infotext = "�t�H�[�J�[�h";
            HighNumber = 1;
        }
        else if (fullhouse)
        {
            addcoin = fullHouseRate * betCoin;
            infotext = "�t���n�E�X!!";
            HighNumber = 1;
        }
        else if (flush)
        {
            addcoin = flushRate * betCoin;
            infotext = "�t���b�V���I�I";
            RoundCount[Player] += 1;
        }
        else if (straight)
        {
            addcoin = straightRate * betCoin;
            infotext = "�X�g���[�g�I�I";
            MidNumber = 1;
        }
        else if (threecard)
        {
            addcoin = threeCardRate * betCoin;
            infotext = "�X���[�J�[�h�I�I";
            MidNumber = 1;
        }
        else if (2 == pair)
        {
            addcoin = twoPairRate * betCoin;
            infotext = "�c�[�y�A�I�I";
            LowNumber = 2;
        }
        else if (1 == pair)
        {
            addcoin = onePairRate * betCoin;
            infotext = "�����y�A�I�I";
            LowNumber = 1;
        }

        Debug.Log("RonundCount"+RoundCount[Player]);
        //�R�C���擾
        playerCoin += addcoin;

        //�e�L�X�g�X�V
        updateTexts();
        textGameInfo.text = infotext; //+ addcoin;
        
        //����̃Q�[���p
        betCoin = 0;
        setButtonsInPlay(false);
       
        ChooseFeaturedCards();
    }


    //
    //FeaturedCard
    //

    public void ChooseFeaturedCards()
    {
        if (LowIndex[Player] + MidIndex[Player] + HighIndex[Player] < FeaturedCardsMAX) // �莝���̓���J�[�h���ő吔�Ȃ���
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
        else //�@�莝���̓���J�[�h���ő吔���鎞�͌����@�\��������
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
            case 0: // �R��ނ̃}�[�N���w�肵�Č����i�P��j
                ChooseConstrainMark();
                ConstrainNum = 1;
                break;
            case 1: // ����̎�D��2���w��Ō���

                break;
            case 2: // ����̎�D��1�������_���Ō���
                break;
            case 3: // ����̎�D���Q�����J
                break;
            case 4: // ����̓t���b�V���͂ł��Ȃ��Ȃ�i�P�^�[���j
                break;
            case 5: // �P���D���ȃ}�[�N�ƌ���
                break;
            case 6: // ����̃T�|�[�g�W�Q�J�[�h�g�p�֎~�i�P�^�[���j
                break;
            case 7: // �D���Ȗ���������D����������
                break;
            case 8: // �P���D���Ȑ����ƌ���
                break;
        }
        int Mid = 0;
        switch (Mid)
        {
            case 0: // ����̎�D���S�����J
                break;
            case 1: // �R��ނ̃}�[�N���w�肵�Ď�D�������i�P���E���h�j
                ChooseConstrainMark();
                ConstrainRound = true;
                break;
            case 2: // ����̎�D�����ׂČ���
                break;
            case 3: // �Q��ނ̃}�[�N���w�肵�Ď�D�������i�P��j
                ChooseConstrainMark();
                ConstrainNum = 1;
                break;
            case 4: // ����̎�D���R�������_���Ō���
                break;
            case 5: // ����̎�D���R���g���Ȃ�����i�P�^�[���j
                break;
            case 6: // ����̎�D4�����D���Ȃ�ƌ���
                break;
            case 7: // �Q���D���Ȑ����ƌ���
                break;
            case 8: // �Q���D���ȃ}�[�N�ƌ���
                break;
            case 9: // ��D���P��������i�ő��D�X���܂Łj
                AddHandLength();
                break;
            case 10: // ����̃T�|�[�g�W�Q�J�[�h�g�p�֎~�i�R�^�[���j
                break;
            case 11: // ����͂��ׂẴt���b�V���͂ł��Ȃ��Ȃ�i�P�^�[���j
                break;
            case 12: //����̎�D���V���ɂ���
                PlayersHandLength[Player] = 7;
                break;
        }
        int High = 0;
        switch (High)
        {
            case 0: // .����̎�D���T���ɂ���i�Q�[���G���h�Ń��Z�b�g�j
                PlayersHandLength[Player] = 5;
                break;
            case 1: // �Q��ނ̃}�[�N���w�肵�Č����i�P���E���h�j
                ChooseConstrainMark();
                ConstrainRound = true;
                break;
            case 2: // ����̎�D���S�����g���Ȃ�����i�P�^�[���j
                break;
            case 3: // �S���D���Ȑ����ƌ���
                break;
            case 4: // �R���D���ȃ}�[�N�ƌ���
                ChooseConstrainMark();
                break;
            case 5: // ��D���Q��������i�ő��D�X���܂Łj
                AddHandLength();
                AddHandLength();
                break;
            case 6: // ����̏����_���P���炷�i�Œ�O�܂Łj
                if (RoundCount[Player] > 0) RoundCount[Player]--;
                break;
        }
    }

    
}
