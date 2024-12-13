int p1_card[] = new int[7]; //プレイヤー１の手札
int p2_card[] = new int[7]; //プレイヤー２の手札
int all_card_num[] = new int[14]; //各プレイヤーの数字をまとめたもの：０～６までがプレイヤー１、７～１３がプレイヤー２：カードの番号とマーク一括管理用　13*a+bとして管理、aはマークに該当、bは数字に該当
int card_num[] = new int[7]; //表示用のカード
int card_check[] = new int[14]; //同じカードがないかの確認用

int which_player = 1; //１ならプレイヤー１の手札を表示する。２ならプレイヤー２の手札を表示する

int card_x[] = {150, 250, 350, 450, 550, 650, 750};   //カードの初期配置x座標
int card_y[] = new int[14]; //カードの初期配置y座標 ７～１３は使用なし

int change_flag[] = new int[14]; //カードの変更はされたかの確認
int serect_card; //タッチしたカードの番号保存
int support_card[] = new int[8]; //サポートカード表示用：７と８は使用しない
int support_serect[] = new int[8]; //カード数が６枚超えたときのカードの交換用
int support_tag; //サポート交換用のタグ
int support_colortag[] = new int[8]; //交換の時にタッチしたか
int serect_count = 0; //交換しようとしてるのは何枚か
int add_count = 0; //どれだけ特殊カードが超過しているか
int change_count = 0; //どれだけ残すか
int support_num = 0; //サポートカードのナンバリング：３桁で構成、１００の位は強さ、０：弱、１：中、２：強　下２桁はカード番号
int r, q; //カード番号をマークと数字に分解する用

int role = 0; //役の種類を数字で分けるもの
int support_strong = 0; //引くカードの強さの決定用
int support_count = 0; //引くカードの番号決定用
int win_point = 0; //勝ち点
int show_number=0; //サポートカードを見せるよう
boolean support_change=false; //サポートカードが６枚超えたて交換が必要かのもの
boolean deleat_tag=false; //サポートカードを捨てるの選んで捨てるよう
boolean mouse_press=false; //現在の使用はほぼなし
boolean same_counter=true; //現在の使用はなし
boolean card_role=false; //役ありかの確認
