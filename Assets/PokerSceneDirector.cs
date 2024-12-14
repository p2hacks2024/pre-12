using DG.Tweening;
using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PokerSceneDirector : MonoBehaviour
{
    //�G�f�B�^����ݒ�
    [SerializeField] CardsDirector cardsDirector;
    [SerializeField] Button buttonBetCoin;
    [SerializeField] Button buttonPlay;
    [SerializeField] Button buttonChange;
    [SerializeField] Text textGameInfo;
    [SerializeField] Text textRate;
    //�{�^�����̃e�L�X�g�i�Ȃ��̃e�L�X�g��ς��邽�߁j
    Text textButtonBetCoin;
    Text textButtonChange;
    //�S�J�[�h
    List<CardController> cards;
    // Featured Cards
    public int[] FeaturedCardsLowHand = new int[2];
    public int[] FeaturedCardsMidHand = new int[2];
    public int[] FeaturedCardsHighHand = new int[2];
    public int LowNumber = 0;
    public int MidNumber = 0;
    public int HighNumber = 0;
    //��D
    List<CardController> hand;
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
    //Players hand's amount
    public int[] PlayersHandLength = new int[2];
    //round count
    public int[] RoundCount = new int[2];
    //�A�j���[�V��������
    const float SortHandTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //�J�[�h���擾
        cards = cardsDirector.GetShuffleCards();

        //�z��f�[�^��������
        hand = new List<CardController>();
        selectCards = new List<CardController>();

        //�{�^�����̃e�L�X�g���擾
        textButtonBetCoin = buttonBetCoin.GetComponentInChildren<Text>();
        textButtonChange = buttonChange.GetComponentInChildren<Text>();

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
            if(Physics.Raycast(ray, out RaycastHit hit))
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
    }
    //��D��������
    CardController addHand()
    {   //�R�D����J�[�h���擾���ăC���f�b�N�X��i�߂�
        CardController card = cards[0]; // CardController card = cards[dealCardCount++]
        //��D�ɉ�����
        hand.Add(card);
        cards.Remove(card);
        //�������J�[�h��Ԃ�
        return card;
        
    }

    //��D���߂���
    void openHand(CardController card)
    {// ��]�A�j���[�V����
        card.transform.DORotate(Vector3.zero, SortHandTime)
            .OnComplete(() => { card.FlipCard(); });//�A�j�����I�������ɌĂяo���֐��ł�
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
    //��D����ׂ�
    void sortHand()
    {   //�����ʒu
        float x = -CardController.Width * (PlayersHandLength[0] / 2);
        Debug.Log("PlayersHandLength"+PlayersHandLength[0]);
        //��D�𖇐������ׂ�
        foreach (var item in hand) //�T������̂�5��
        {//�\���ʒu�փA�j���[�V�������Ĉړ�
            Vector3 pos = new Vector3(x, 0, -0.2f); //���̍��W�Ɉړ�
            item.transform.DOMove(pos, SortHandTime); //SortHandTime�b�����Ĉړ������
            //����̕\���ʒux
            x += CardController.Width;
        }
    }

    //�Q�[�����X�^�[�g
    void restartGame(bool deal = true)
    {
        //��D�A�I���J�[�h�����Z�b�g
        hand.Clear();
        selectCards.Clear();
        //�J�[�h�������閇�������Z�b�g
        cardChangeCount = cardChangeCountMax;

        //�R�D����������񐔂����Z�b�g
        dealCardCount = 0;

        //�J�[�h�V���b�t��
        cardsDirector.ShuffleCards(cards);
        //�J�[�h�̏����ݒ�
        foreach(var item in cards)
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
        for (int i = 0; i < 2; i++)
        {
            // ��D�̐��̓��E���h���Ƃɕς���悤�ɂ��āA���E���h�擾���̓Q�[�����Ƃɕς���
            PlayersHandLength[i] = 7;
            RoundCount[i] = 0;
        }


        //7���z���ĕ\�����ɂ���
        for(int i = 0; i < PlayersHandLength[0] ; i++)
        {
            openHand(addHand());//��D�ɉ����ăI�[�v������
        }
        //�J�[�h����ׂ�
        sortHand();
    }
    
    //���[�g�\���X�V
    void updateTexts()
    {
        
        textButtonBetCoin.text = "�莝���R�C��" + playerCoin;
        textGameInfo.text = "BET����" + betCoin;
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
    void setButtonsInPlay(bool disp = true)
    {
        textButtonChange.text = "�I��";
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
        restartGame();
        //�Q�[�����̃{�^���ƃe�L�X�g�̍X�V
        setButtonsInPlay();
        updateTexts();
    }

    //�J�[�h�̑I�����
    void setSelectCard(CardController card)
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
        //else if(cards.Count > dealCardCount + selectCards.Count) // tabun kore tigau
        else
        {
            pos.z += 0.02f;
            selectCards.Add(card);
        }

        //�X�V���ꂽ�ꏊ
        card.transform.position = pos;

        //�{�^���X�V (�I�𖇐���0���Ȃ�I���{�^���ɕύX)
        textButtonChange.text = "����";
        if(selectCards.Count < 1)
        {
            textButtonChange.text = "�I��";
        }
    }

    //�J�[�h����
    public void OnClickChange()
    {
        foreach (var i in cards) // YOUKAKUNIN
        {
            //Debug.Log("cards"+i);
        }
        //�������Ȃ��Ȃ�1��ŏI��
        if(1 > selectCards.Count)
        {
            cardChangeCount = 0;
        }
        //�̂ăJ�[�h����D����폜
        foreach(var item in selectCards)
        {
            //Debug.Log("selectCards"+item);
            item.gameObject.SetActive(false); //��\���ɂ���
            cards.Add(item);// �R�D�ɒǉ�����
            hand.Remove(item);// ��D�������
            openHand(addHand());//�̂Ă���J�[�h��ǉ�
        }
        // cardsDirector.ShuffleCards(cards); // maybe this is wrong
        Debug.Log("card.count "+ cards.Count);

        selectCards.Clear();
        //���ׂ�
        sortHand();
        setButtonsInPlay();
        //�J�[�h�����\��
        
        if(1 > cardChangeCount)
        {
            // TODO ���𐸎Z����
            checkHandRank();
        }
    }

    void checkHandRank()
    {  
        
        
        //�t���b�V���`�F�b�N
        int flushcount = 0;
        bool flush = false;
        //1���ڂ̃J�[�h�̃}�[�N
        SuitType suit = hand[0].Suit;

        foreach(var item in hand)
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
        for(int i = 0; i < hand.Count; i++)
        {   //�����̐������A��������
            int straightcount = 0;
            //���݂̃J�[�h�ԍ�
            int cardno = hand[i].No;
            //�P���ڂ���A�����Ă��邩���ׂ�
            for(int j = 0; j < hand.Count; j++)
            {   //�����J�[�h�̓X�L�b�v
                if (i == j) continue;

                //�������������͌��݂̐�����+1
                int targetno = cardno + 1;
                //13�̎���1
                if (13 < targetno) targetno = 1;

                //�^�[�Q�b�g�̐�������
                if(targetno == hand[j].No)
                {
                    //�A���񐔂��J�E���g
                    straightcount++;
                    //����̃J�[�h�ԍ��i����́{�I�����j
                    cardno = hand[j].No;
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

        for(int i = 0; i < hand.Count; i++)
        {
            if (checkcards.Contains(hand[i])) continue;

            //���������̃J�[�h����
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

        //������������
        int addcoin = 0;
        string infotext = "���Ȃ�";

        if (straightflush)
        {
            addcoin = straightFlushRate * betCoin;
            infotext = "�X�g���[�g�t���b�V���I�I";
            RoundCount[0] += 2;
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
            RoundCount[0] += 1;
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

        Debug.Log("RonundCount"+RoundCount[0]);
        //�R�C���擾
        playerCoin += addcoin;

        //�e�L�X�g�X�V
        updateTexts();
        textGameInfo.text = infotext + addcoin;

        //����̃Q�[���p
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
