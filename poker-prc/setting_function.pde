//セットアップ
void setting(int card[]) {

  for(int i=0;i<4;i++){
    which_mark[i]=4;
  }

  for (int i = 0; i<card.length; i++) {/*カード配布とカード配置とカードチェックの初期化*/
    card[i]=int(random(52));
    card_y[i]=420;
    card_check[i]=0;
  }
  dif_check(card); //配布されたトランプの数字をすべて別にする調整
  for (int i = 0; i < 7; i++) {
    p1_card[i] = card[i];
    p2_card[i] = card[7+i];
  }
  which_player = int(random(2)) + 1; // 先行を決める

  //表示カードの決定
  if (which_player == 2) {
    for (int i = 0; i < 7; i++) {
      support_card[i]=support_card_p2[i];
      all_card_num[i] = p1_card[i];
      card_num[i] = p2_card[i];
      support_card[i]=support_card_p1[i];
    }
  } else {
    for (int i = 0; i < 7; i++) {
      support_card[i]=support_card_p1[i];
      all_card_num[i+7]=p2_card[i];
      card_num[i] = p1_card[i];
      support_card[i]=support_card_p2[i];
    }
  }
}
