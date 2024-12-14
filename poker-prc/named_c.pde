//表示カードタブ
int p1_card[] = new int[7]; //プレイヤー１の手札
int p2_card[] = new int[7]; //プレイヤー２の手札
int all_card_num[] = new int[14]; //各プレイヤーの数字をまとめたもの：０～６までがプレイヤー１、７～１３がプレイヤー２：カードの番号とマーク一括管理用　13*a+bとして管理、aはマークに該当、bは数字に該当
int card_num[] = new int[7]; //表示用のカード
int card_x[] = {150, 250, 350, 450, 550, 650, 750};   //カードの初期配置x座標
int card_y[] = new int[14]; //カードの初期配置y座標 ７～１３は使用なし
//手札判定タブ
int card_check[] = new int[14]; //同じカードがないかの確認用
int up_card=0;//どれぐらい手札を選択したか
int card_mark[]=new int[7];//それぞれのカードのマークを保存するもの
int change_flag[] = new int[14]; //カードの変更はされたかの確認
int serect_card; //タッチしたカードの番号保存
int r, q; //カード番号をマークと数字に分解する用
//プレイヤー判定
int which_player = 1; //１ならプレイヤー１の手札を表示する。２ならプレイヤー２の手札を表示する
//サポートカード表示タブ
int show_number=0; //サポートカードを見せるよう
int support_card[] = new int[8]; //サポートカード表示用：７と８は使用しない
int support_card_p1[] = new int[8]; //サポートカード表示用：７と８は使用しない
int support_card_p2[] = new int[8]; //サポートカード表示用：７と８は使用しない
//サポート交換タブ
int support_serect[] = new int[8]; //カード数が６枚超えたときのカードの交換用
int support_colortag[] = new int[8]; //交換の時にタッチしたか
int support_tag; //サポート交換用のタグ
int serect_count = 0; //交換しようとしてるのは何枚か
int change_count = 0; //どれだけ残すか
int support_num = 0; //サポートカードのナンバリング：３桁で構成、１００の位は強さ、０：弱、１：中、２：強　下２桁はカード番号
int support_strong = 0; //引くカードの強さの決定用
int support_count = 0; //引くカードの番号決定用
int support_count1 = 0; //プレイヤー１のサポートカードの枚数
int support_count2 = 0; //プレイヤー２のサポートカード枚数
boolean support_change=false; //サポートカードが６枚超えたて交換が必要かのもの
boolean deleat_tag=false; //サポートカードを捨てるの選んで捨てるよう
//勝利点
int win_point = 0; //勝ち点
int win_point_p1 = 0; //ぷれいやー１の勝ち点
int win_point_p2 = 0; //プレイヤー２の勝ち点
//役に付いての判定
int role = 0; //役の種類を数字で分けるもの
boolean card_role=false; //役ありかの確認
//特殊カード用の変数
int p1_change=0;//プレイヤー１の何種類のマークでカードを交換するか：２なら２：１なら３：０なら４
int p2_change=0;//プレイヤー２の何種類のマークでカードを交換するか：２なら２：１なら３：０なら４
int opp_chan_num=0;//強制交換何枚か
int marks=0;//選択したマークの数
int which_mark[]=new int[4];//どのマークを選択したか保存用
int p1_which_mark[]=new int[4];//プレイヤー１の選択したマーク保存用
int p2_which_mark[]=new int[4];//プレイヤー２の選択したマーク保存用
int which_num[]={13,13};//度の数字を選択したか保存用
int opp_no_change_p1[]=new int[7];//プレイヤー１の変更できないカード保存用
int opp_no_change_p2[]=new int[7];//プレイヤー２の変更できないカード保存用
boolean opp_card_chan=false;//カード利用ができなくなるもの
boolean no_flush=false;//フラッシュを禁止するもの
boolean no_all_flush=false;//すべてのフラッシュを禁止するもの
boolean no_support=false;//サポートカードを使用禁止するもの


boolean mouse_press=false; //現在の使用はほぼなし
boolean same_counter=true; //現在の使用はなし


int no_turn[]=new int[2];

boolean end_button = false;//endボタンの判定
boolean turn_end_button = false;//ターン終了判定
boolean support_use=false;
int support=0;

int use_card=0;
int nums=0;
int open_card[]=new int[7];
boolean mark_thr=false;
boolean mark_two=false;
