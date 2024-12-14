void setup() {
  size(960, 540);
  setting(all_card_num);
  Title = loadImage("TitleFlush.png");
  Rule = loadImage("ルール .png");
  Pair = loadImage("役一覧.jpg");
  Game = loadImage("poker_bord.png");
  Result1 = loadImage("P2LOSE_P1WIN.png");
  Result2 = loadImage("P1LOSE_P2WIN.png");
  minim = new Minim(this);

  // BGMと効果音のファイルを読み込み
  bgm = minim.loadFile("ShotGlass.mp3");//BGM
  submit = minim.loadFile("トランプ・カードをはじく音.mp3"); //交換やペアを出したりする音
  start = minim.loadFile("トランプ・カードを配る音.mp3"); //ゲームを始めるときの音
  change = minim.loadFile("卓上ベル・カウンターベル.mp3"); //交代するときの音
  card_change = minim.loadFile("トランプ・カードをめくる音.mp3"); //カードの交換の音
  // BGMの音量設定（0から1）
  bgm.setVolume(0.5);
  // BGMの再生
  bgm.loop(); // ループ再生
}
void draw() {
  if (!bgm.isPlaying()) {
    bgm.rewind(); // BGMを先頭に戻す
    bgm.play(); // 再生
  }
  //タイトル画面
  if (scene==0) {
    image(Title, 0, 0);
    fill(255);
    rect(380, 200, 200, 100);//スタートボタン
    rect(380, 390, 200, 100);//ルールボタン
    fill(0);
    textSize(50);
    text("START", 405, 270);
    text("RULE", 415, 460);
  }
  //ゲーム画面
  if (scene==1) {
    image(Game, 0, 0);
    textSize(40);
    fill(0);
    text(which_player, 830, 400);

    if (which_player==1) {
      fill(255, 255, 0);
      text("FLUSH", 10, 240);
      text(win_point_p1, 55, 280);
    } else {
      fill(255, 255, 0);
      text("FLUSH", 10, 240);
      text(win_point_p2, 55, 280);
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

    //勝ち負け判定
    if (win_point_p1 == 3) {
      scene=4;
    } else if (win_point_p2 == 3) {
      scene=5;
    }
  }
  //ゲームルール画面
  if (scene==2) {
    image(Rule, 0, 0);
    fill(255);
    rect(650, 20, 200, 100);
    fill(0);
    text("Home", 680, 85);
  }
  //ペアの例
  if (scene==3) {
    image(Pair, 0, 0);
    fill(255);
    rect(330, 450, 100, 80);//戻るボタン
    rect(530, 450, 120, 80);//サポートカードの説明
    fill(0);
    text("back", 350, 500);
    text("support", 540, 500);
  }
  //ゲーム結果
  if (scene==4) {
    image(Result1, 0, 0);
    textSize(80);
    fill(255);
    text("Home", 380, 280);
    fill(255, 255, 0);
    text(win_point_p1, 400, 380);
    fill(0);
    text(win_point_p2, 530, 380);
  }
  if (scene==5) {
    image(Result2, 0, 0);
    textSize(80);
    fill(255);
    text("Home", 380, 280);
    fill(0);
    text(win_point_p1, 400, 380);
    fill(255, 255, 0);
    text(win_point_p2, 530, 380);
  }
}


//画面選択
void mousePressed() {
  //タイトル画面出のボタン操作
  if (scene==0) {
    if (380<=mouseX && 200<=mouseY&& 580>=mouseX && 300>=mouseY) {
      scene=1;
      start.rewind(); // 交代の音を先頭に戻す
      start.play(); // 交代の音再生
    }
    if (380<=mouseX && 390<=mouseY&& 580>=mouseX && 490>=mouseY) {
      scene=2;
    }
  }

  if (scene==1) {
    //endボタン管理
    if (845<=mouseX && 420<=mouseY&& 945>=mouseX && 520>=mouseY) {
      end_button = true;
    }
    //yesボタン管理
    if (end_button == true && 260<=mouseX && 410>=mouseX && 210<=mouseY && 310>=mouseY) {
      end_button = false;
      if (which_player == 1) {
        change.rewind(); // 交代の音を先頭に戻す
        change.play(); // 交代の音再生
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
        change.rewind(); // 交代の音を先頭に戻す
        change.play(); // 交代の音再生
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

    //?ボタンの管理
    if (0<=mouseX && 25<=mouseY&& 50>=mouseX && 100>=mouseY) {
      scene = 3;
    }

    //カード選択
    //p1用
    for (int i=0; i<7; i++) {
      if (!support_change && show_number==0) {
        if ((card_x[i]<=mouseX && mouseX<=card_x[i]+80)&&(card_y[i]<=mouseY && mouseY<=card_y[i]+120)) {
          mouse_press=true;
          serect_card=i;
          submit.rewind(); // カードの出す音を先頭に戻す
          submit.play(); // カードの出す音再生
        }
      }
    }


    //ここまで

    //カードの引き直し部分
    if (750<=mouseX && mouseX<=900 && 270<=mouseY && mouseY<=345 &&show_number==0) {
      card_change();
      card_change.rewind(); // カードの交換音を先頭に戻す
      card_change.play(); // カードの交換音再生
    }

    //役の確定
    if (750<=mouseX && mouseX<=900 && 175<=mouseY && mouseY<=250 && show_number==0) {
      submit();
      card_change.rewind(); // カードの交換音を先頭に戻す
      card_change.play(); // カードの交換音再生
    }

    //サポートカードの表示（消し）
    if (25<=mouseX && mouseX<135 && 50<=mouseY && mouseY<=120 && show_number==1 && !support_change) {
      show_number=0;
      submit.rewind(); // カードの出す音を先頭に戻す
      submit.play(); // カードの出す音再生
    }

    //サポートカードの表示（付け）
    if (25<=mouseX && mouseX<=95 && 380<=mouseY && mouseY<=490 && show_number==0) {
      show_number=1;
      submit.rewind(); // カードの出す音を先頭に戻す
      submit.play(); // カードの出す音再生
    }

    //サポートカードの交換部分
    //p1
    if (which_player==1) {
      for (int i=0; i<8; i++) {
        if (support_change) {
          if (i<6 && 150+120*i<=mouseX && mouseX<=250+120*i && 300<=mouseY && mouseY<=450) {
            mouse_press=true;
            support_tag=i;
            submit.rewind(); // カードの出す音を先頭に戻す
            submit.play(); // カードの出す音再生
          } else if (i>=6 && 150+120*(i-4)<=mouseX && mouseX<=250+120*(i-4) && 50<=mouseY && mouseY<=200) {
            mouse_press=true;
            support_tag=i;
            submit.rewind(); // カードの出す音を先頭に戻す
            submit.play(); // カードの出す音再生
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
            submit.rewind(); // カードの出す音を先頭に戻す
            submit.play(); // カードの出す音再生
          } else if (i>=6 && 150+120*(i-4)<=mouseX && mouseX<=250+120*(i-4) && 50<=mouseY && mouseY<=200) {
            mouse_press=true;
            support_tag=i;
            submit.rewind(); // カードの出す音を先頭に戻す
            submit.play(); // カードの出す音再生
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
  //ルール画面でのボタン操作 rect(650,20,200,100);
  if (scene==2) {
    if (650<=mouseX && 20<=mouseY&& 850>=mouseX && 120>=mouseY) {
      scene=0;
    }
  }
  //？のところからペアの組み合わせ
  if (scene==3) {
    if (330<=mouseX && 450<=mouseY&& 430>=mouseX && 530>=mouseY) {
      scene=1;
    }//戻るボタン
    if (530<=mouseX && 450<=mouseY&& 650>=mouseX && 530>=mouseY) {
      scene=0;
    }//サポートカードの説明
  }
  //ゲーム結果text("Home", 380, 280);//rect(380,200,200,100);
  if (scene==4||scene==5) {
    if (380<=mouseX && 200<=mouseY&& 580>=mouseX && 300>=mouseY) {
      scene=0;
    }
  }
}
