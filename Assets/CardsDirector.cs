using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsDirector : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabSpades;
    [SerializeField] List<GameObject> prefabClubs;
    [SerializeField] List<GameObject> prefabDiamonds;
    [SerializeField] List<GameObject> prefabHearts;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�V���b�t��
    public void ShuffleCards(List<CardController> cards)
    {
        Debug.Log("Card.Count "+cards.Count);
        for(int i= 0; i < cards.Count; i++)
        {
            int rnd = Random.Range(0, cards.Count);
            CardController tmp = cards[i];

            cards[i] = cards[rnd];
            cards[rnd] = tmp;
        }
    }

    //�J�[�h�쐬
    List<CardController> createCards(SuitType suittype)
    {


        List<CardController> ret = new List<CardController>();

        //�J�[�h�̎��
        List<GameObject> prefabcards = prefabSpades;
        Color suitcolor = Color.black;

        if(SuitType.Club == suittype)
        {
            prefabcards = prefabClubs;
        }
        else if(SuitType.Diamond == suittype)
        {
            prefabcards = prefabDiamonds;
            suitcolor = Color.red;
        }
        else if (SuitType.Heart == suittype)
        {
            prefabcards = prefabHearts;   
            suitcolor = Color.red;
        }
       
        
        //�J�[�h����
        for(int i = 0; i < prefabcards.Count; i++) 
        {
            GameObject obj = Instantiate(prefabcards[i]);

            //�����蔻��
            BoxCollider bc = obj.AddComponent<BoxCollider>();
            //�����蔻�茟�m
            Rigidbody rb = obj.AddComponent<Rigidbody>();
            //�J�[�h���m�̓����蔻��ƕ������Z���g��Ȃ�
            bc.isTrigger = true;
            rb.isKinematic = true;

            //�J�[�h�Ƀf�[�^���Z�b�g
            CardController ctrl = obj.AddComponent<CardController>();

            ctrl.Suit = suittype;
            ctrl.SuitColor = suitcolor;
            ctrl.PlayerNo = -1;
            ctrl.No = i + 1;

            ret.Add(ctrl);
        }
        return ret;
    }

    public List<CardController> GetShuffleCards()
    {
        List<CardController> allCards = new List<CardController>();

        // �e�X�[�g�̃J�[�h��ǉ�
        allCards.AddRange(createCards(SuitType.Spade));
        allCards.AddRange(createCards(SuitType.Club));
        allCards.AddRange(createCards(SuitType.Diamond));
        allCards.AddRange(createCards(SuitType.Heart));
       

        // �J�[�h���V���b�t��
        ShuffleCards(allCards);

        return allCards;
    }
}
