//カードの表示
void card(int i) {
  //カードが選択されたときの表示
  if (mouse_press==true && card_check[i]==0 && show_number==0) {
    //card_x[i]+=40;
    card_y[i]-=40;
    mouse_press=false;
    card_check[i]=1;
  }

  //選択解除された時の表示
  if (mouse_press && card_check[i]==1 && show_number==0) {
    card_y[i]+=40;
    mouse_press=false;
    card_check[i]=0;
  }

  //カードが画面外に行かないための制限（使用はしてない）
  /*if (card_x[i]<=0) {
   card_x[i]=0;
   }
   if (card_x[i]+80>=width) {
   card_x[i]=width-80;
   }
   if (card_y[i]<=0) {
   card_y[i]=0;
   }
   if (card_y[i]+120>=height) {
   card_y[i]=height-120;
   }
   */
}

//カードの数字の表示
void card_text(int i) {
  //カードの数字とマークに変換
  r=card_num[i]%13;//カードの数字に変換
  q=(card_num[i]-r)/13;//カードのマークを計算

  //カードの数字とマークに直す
  r++;
  q++;

  //カードの数字とマークを表示
  fill(0);
  textSize(20);
  text(q, card_x[i]+5, card_y[i]+25);
  textSize(55);
  text(r, card_x[i]+20, card_y[i]+70);
}

void dif_check() {
  check=0;
  k=0;
  while (check==0) {
    for (int i=0; i<7; i++) {
      for (int j=i+1; j<7; j++) {
        if (card_num[i]==card_num[j]) {
          k=1;
          card_num[i]++;
          if (card_num[i]>=52) {
            card_num[i]=0;
          }
        }
      }
    }
    if (k==0) {
      check=1;
    }
    k=0;
  }
}

void card_change() {
  int t=0;
  for (int i=0; i<7; i++) {
    if (card_check[i]==1) {
      card_num[i]=int(random(52));
      card_y[i]=420;
    }
  }
  check=0;
  k=0;
  while (check==0) {
    for (int i=0; i<7; i++) {
      if (card_check[i]==1) {
        change_flag[i]=0;
        for (int j=1; j<7; j++) {
          if (i+j>=7) {
            t=j-7;
          } else {
            t=j;
          }
          if (card_num[i]==card_num[i+t]) {
            k=1;
            card_num[i]++;
            change_flag[i]=1;
            if (card_num[i]>=52) {
              card_num[i]=0;
            }
          }
        }
      }
      if (change_flag[i]==0) {
        card_check[i]=0;
      }
    }
    if (k==0) {
      check=1;
    }
    k=0;
  }
}

//カードを出し、交換する部分
void submit() {
  //役の判定
  role_check();
  
  //役の判定、妨害カードの配布
  switch(role) {
  case 1:
  //ここは実験
  /*for(int i=0;i<8;i++){
    support_strong=0;
    add_count=2;
    support_num=int(random(9))+1;
    support_card[support_count]=support_strong*100+support_num;
    support_count++;
  }*/
  
    support_strong=0;
    support_num=int(random(9))+1;
    
    if (support_count>=6) {
      show_number=1;
      support_change=true;
      for (int i=0; i<7; i++) {
        if (i<6) {
          support_serect[i]=support_card[i];
        } else {
          add_count++;
          support_serect[i]=support_strong*100+support_num;
        }
      }
    } else {
      support_card[support_count]=support_strong*100+support_num;
      support_count++;
    }
    break;
  case 2:
    support_strong=0;
    for (int i=0; i<2; i++) {
      support_num=int(random(9))+1;
      if (support_count>=6) {
        show_number=1;
        support_change=true;
        for (int k=0; k<8; k++) {
          if (k<6) {
            support_serect[k]=support_card[k];
          } else {
            add_count++;
            support_serect[k]=support_strong*100+support_num;
          }
        }
      } else {
        support_card[support_count]=support_strong*100+support_num;
        support_count++;
      }
    }
    break;
  case 3:
    support_strong=1;
    support_num=int(random(13))+1;
    if (support_count>=6) {
      show_number=1;
      support_change=true;
      for (int i=0; i<7; i++) {
        if (i<6) {
          support_serect[i]=support_card[i];
        } else {
          add_count++;
          support_serect[i]=support_strong*100+support_num;
        }
      }
    } else {
      support_card[support_count]=support_strong*100+support_num;
      support_count++;
    }
    break;
  case 4:
    break;
  case 6:
    support_strong=1;
    support_num=int(random(15))+1;
    if (support_count>=6) {
      show_number=1;
      support_change=true;
      for (int i=0; i<7; i++) {
        if (i<6) {
          support_serect[i]=support_card[i];
        } else {
          add_count++;
          support_serect[i]=support_strong*100+support_num;
        }
      }
    } else {
      support_card[support_count]=support_strong*100+support_num;
      support_count++;
    }
    break;
  case 7:
    support_strong=2;
    support_num=int(random(7))+1;
    if (support_count>=6) {
      show_number=1;
      support_change=true;
      for (int i=0; i<7; i++) {
        if (i<6) {
          support_serect[i]=support_card[i];
        } else {
          add_count++;
          support_serect[i]=support_strong*100+support_num;
        }
      }
    } else {
      support_card[support_count]=support_strong*100+support_num;
      support_count++;
    }
    break;
  case 5:
    win_point+=1;
    break;
  case 8:
    win_point+=2;
    break;
  case 9:
    win_point+=3;
    break;
  }
  card_change();
}

void show_support() {
  fill(0, 95);
  rect(10, 10, width-20, height-20);
}

void card_support(int i) {
  //カードが選択されたときの表示
  if (mouse_press && support_colortag[i]==0 && serect_count<add_count) {
    mouse_press=false;
    support_colortag[i]=1;
    serect_count++;
  }

  //選択解除された時の表示
  if (mouse_press && support_colortag[i]==1) {
    mouse_press=false;
    support_colortag[i]=0;
    serect_count--;
  }
}
