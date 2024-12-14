void setup() {
  size(960, 540);
  setting(all_card_num);
}
void draw() {
  background(192, 192, 255);
  textSize(40);
  fill(0);
  text(which_player, 830, 400);

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
  if (card_check[0]==1 || card_check[1]==1 || card_check[2]==1 || card_check[3]==1 || card_check[4]==1 || card_check[5]==1 || card_check[6]==1 ) {
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
  if (show_number==1) {
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
        if (support_count - serect_count<=6) {
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
}


//画面選択
void mousePressed() {

  //endボタン管理
  if (845<=mouseX && 420<=mouseY&& 945>=mouseX && 520>=mouseY) {
    end_button = true;
  }
  //yesボタン管理
  if (end_button == true && 260<=mouseX && 410>=mouseX && 210<=mouseY && 310>=mouseY) {
    end_button = false;
    if (which_player == 1) {
      which_player = 2;
      support_count1=support_count;
      support_count=support_count2;
      for (int i = 0; i < 7; i++) {
        all_card_num[i] = p1_card[i];
        card_num[i] = p2_card[i];
        support_card_p1[i]=support_card[i];
        support_card[i]=support_card_p2[i];
      }
    } else {
      which_player = 1;
      support_count2=support_count;
      support_count=support_count1;
      for (int i = 0; i < 7; i++) {
        all_card_num[i+7]=p2_card[i];
        card_num[i] = p1_card[i];
        support_card_p2[i]=support_card[i];
        support_card[i]=support_card_p1[i];
      }
    }
  }
  //noボタン管理
  if (end_button == true && 560<=mouseX && 710>=mouseX && 210<=mouseY && 310 >= mouseY) {
    end_button = false;
  }

  //カード選択
  //p1用

  for (int i=0; i<7; i++) {
    if (!support_change && show_number==0) {
      if ((card_x[i]<=mouseX && mouseX<=card_x[i]+80)&&(card_y[i]<=mouseY && mouseY<=card_y[i]+120)) {
        mouse_press=true;
        serect_card=i;
      }
    }
  }


  //ここまで

  //カードの引き直し部分
  if (750<=mouseX && mouseX<=900 && 270<=mouseY && mouseY<=345 &&show_number==0) {
    card_change();
  }

  //役の確定
  if (750<=mouseX && mouseX<=900 && 175<=mouseY && mouseY<=250 && show_number==0) {
    submit();
  }

  //サポートカードの表示（消し）
  if (25<=mouseX && mouseX<135 && 50<=mouseY && mouseY<=120 && show_number==1 && !support_change) {
    show_number=0;
  }

  //サポートカードの表示（付け）
  if (25<=mouseX && mouseX<=95 && 380<=mouseY && mouseY<=490 && show_number==0) {
    show_number=1;
  }

  //サポートカードの交換部分
  //p1
  if (which_player==1) {
    for (int i=0; i<8; i++) {
      if (support_change) {
        if (i<6 && 150+120*i<=mouseX && mouseX<=250+120*i && 300<=mouseY && mouseY<=450) {
          mouse_press=true;
          support_tag=i;
        } else if (i>=6 && 150+120*(i-4)<=mouseX && mouseX<=250+120*(i-4) && 50<=mouseY && mouseY<=200) {
          mouse_press=true;
          support_tag=i;
        }
      }
    }
  }
  //p2
  if (which_player==2) {
    for (int i=0; i<8; i++) {
      if (support_change) {
        if (i<6 && 150+120*i<=mouseX && mouseX<=250+120*i && 300<=mouseY && mouseY<=450) {
          mouse_press=true;
          support_tag=i;
        } else if (i>=6 && 150+120*(i-4)<=mouseX && mouseX<=250+120*(i-4) && 50<=mouseY && mouseY<=200) {
          mouse_press=true;
          support_tag=i;
        }
      }
    }
  }
  //p1
  if (which_player==1) {
    if (deleat_tag && 750<=mouseX && mouseX<=900 && 100<=mouseY && mouseY<=175) {
      for (int i=0; i<8; i++) {
        support_card_p1[i]=0;
        if (support_colortag[i]==0) {
          support_card_p1[change_count]=support_serect[i];
          change_count++;
        }
        support_colortag[i]=0;
        support_serect[i]=0;
      }
      serect_count=0;
      change_count=0;
      deleat_tag=false;
      support_change=false;
      serect_card=0;
      show_number=0;
      support_count=6;
    }
  }
  //p2
  if (which_player==2) {
    if (deleat_tag && 750<=mouseX && mouseX<=900 && 100<=mouseY && mouseY<=175) {
      for (int i=0; i<8; i++) {
        support_card_p2[i]=0;
        if (support_colortag[i]==0) {
          support_card_p2[change_count]=support_serect[i];
          change_count++;
        }
        support_colortag[i]=0;
        support_serect[i]=0;
      }
      serect_count=0;
      change_count=0;
      deleat_tag=false;
      support_change=false;
      serect_card=0;
      show_number=0;
      support_count=6;
    }
  }
}
