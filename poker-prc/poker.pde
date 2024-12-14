void setup() { //<>//
  size(960, 540);
  for (int i=0; i<7; i++) {/*カード配布とカード配置とカードチェックの初期化*/
    card_num[i]=int(random(52));
    card_y[i]=420;
    card_check[i]=0;
  }
  for (int i=0; i<8; i++) {
    support_colortag[i]=0;
  }
  dif_check();//すべて別の数字への調整
}
void draw() {
  background(192, 192, 255);

  textSize(40);
  fill(0);
  text(which_player, 830, 400);
  /*for (int j=0; j<13; j++) {
   fill(255);
   rect(120+60*j, 220, 40, 40);
   fill(0);
   textSize(20);
   text(j+1, 140+60*j, 250);
   }*/
  for (int i=0; i<7; i++) {
    fill(155);
    rect(250 + 100 * i, 0, 80, 120);
  }
  //ターン終了ボタンの位置
  fill(255);
  rect(845, 420, 100, 100);
  fill(0);
  text("End", 860, 470);
  if (end_button==true) {
    fill(255);
    rect(260, 210, 150, 100);
    fill(255, 0, 0);
    rect(560, 210, 150, 100);
    fill(0);
    text("Yes", 310, 270);
    text("No", 610, 270);
  }

  //ここからヘルプ表示
  fill(255, 60);
  rect(0, 25, 50, 75);
  fill(0);
  textSize(40);
  text("?", 15, 80);
  //ヘルプ表示ここまで

  card(serect_card);//カードのタッチがあったか

  //ここからカードの表示
  for (int j=0; j<7; j++) {
    if (card_check[j]==1) {
      fill(255);
    } else {
      fill(150);
    }
    rect(card_x[j], card_y[j], 80, 120);
    /*textSize(30);
     fill(0);
     text("+",card_x[j]+60,card_y[j]+25);*/
    /*line(card_x[j],card_y[j],card_x[j]+80,card_y[j]);
     line(card_x[j],card_y[j],card_x[j],card_y[j]+120);
     line(card_x[j],card_y[j]+120,card_x[j]+80,card_y[j]+120);
     line(card_x[j]+80,card_y[j],card_x[j]+80,card_y[j]+120);
     */
    card_text(j);
  }
  //カード表示ここまで

  //ここからカードの使用の選択し表示
  if ((card_check[0]==1 || card_check[1]==1 || card_check[2]==1 || card_check[3]==1 || card_check[4]==1 || card_check[5]==1 || card_check[6]==1 )&& !opp_card_chan) {
    if (card_role) {
      fill(0, 60);
      rect(750, 175, 150, 75);
      fill(0);
      textSize(30);
      text("Submit", 775, 225);
    }
    fill(0, 60);
    rect(750, 270, 150, 75);
    fill(0);
    textSize(30);
    text("Change", 775, 320);
  }
  //ここまで

  //ここからサポートカードのアイコン
  rect(25, 400, 60, 90);
  fill(155);
  rect(30, 390, 60, 90);
  fill(255);
  rect(35, 380, 60, 90);
  fill(0);
  textSize(18);
  text("Support Cards", 15, 510);
  //ここまで

  role_check();//役の確認・表示

  //役の表示部分
  if (show_number==1 && use_card==0) {
    show_support();
    fill(255);
    if (!support_change) {
      rect(25, 50, 110, 70);
    }
    for (int i=0; i<8; i++) {
      if (!support_change && support_card[i]!=0) {
        fill(255);
        rect(150+120*i, 300, 100, 150);
        fill(0);
        textSize(30);
        text(support_card[i], 180+120*i, 380);
      } else if (support_change && support_serect[i]!=0) {
        card_support(support_tag);
        if (i<6) {
          if (support_colortag[i]==0) {
            fill(155);
          } else {
            fill(255);
          }
          rect(150+120*i, 300, 100, 150);
          fill(0);
          textSize(30);
          text(support_serect[i], 180+120*i, 380);
        } else {
          if (support_colortag[i]==0) {
            fill(155);
          } else {
            fill(255);
          }
          rect(150+120*(i-4), 50, 100, 150);
          fill(0);
          textSize(30);
          text(support_serect[i], 180+120*(i-4), 130);
        }
        if (support_count-serect_count<6) {
          deleat_tag=true;
          fill(230);
          rect(750, 100, 150, 75);
          fill(0);
          textSize(40);
          text("deleat", 775, 150);
        }
      }
    }
  }
  //ここまで

  support_do(support);
 

}
