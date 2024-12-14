using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//マーク
public enum SuitType
{
    Spade,
    Club,
    Diamond,
    Heart,
}
public class CardController : MonoBehaviour
{//カードのサイズ
    public const float Width = 0.06f;
    public const float Height = 0.09f;
    //カードのマーク
    public SuitType Suit;

    public int No;
    //どのプレイヤーのカードか
    public int PlayerNo;

    public int Index;
    //手札の初期位置
    public Vector3 HandPosition;

    public Vector2Int IndexPosition;
    //カードの色
    public Color SuitColor;
    //表面が上になっているか
    public bool isFrontUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //カードをめくる
    public void FlipCard(bool frontup = true)
    {
        float anglez = 0;
        if (!frontup)
        {
            anglez = 180;//180度回転
        }

        isFrontUp = frontup;
        transform.eulerAngles = new Vector3(0, 0, anglez);
    }


}
