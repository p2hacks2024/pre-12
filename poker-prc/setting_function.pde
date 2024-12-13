//セットアップ
void setting(int card[]){
  for (int i = 0; i<card.length; i++) {/*カード配布とカード配置とカードチェックの初期化*/
    card[i]=int(random(52));
    card_y[i]=420;
    card_check[i]=0;
  }
  dif_check(card); //配布されたトランプの数字をすべて別にする調整
  for(int i = 0; i < 7 ; i++){
    p1_card[i] = card[i];
    p2_card[i] = card[7+i];
  }
  which_player = int(random(2)) + 1; // 先行を決める
  
  //表示カードの決定
  if(which_player == 1){
    for(int i = 0; i < 7; i++){
      card_num[i] = p1_card[i];
    }
  }else{
    for(int i = 0; i < 7; i++){
      card_num[i] = p2_card[i];
    }
  }
}
