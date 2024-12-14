//画面選択
void mousePressed() {
  //endボタン管理
  if (845<=mouseX && 420<=mouseY&& 945>=mouseX && 520>=mouseY && (support==0 || support==2 || support==9)) {
    end_button = true;
  }
  //yesボタン管理
  if (end_button == true && 260<=mouseX && 410>=mouseX && 210<=mouseY && 310>=mouseY) {
    end_button = false;
    if (which_player == 1) {
      which_player = 2;
      support_count1=support_count;
      support_count=support_count2;
      no_flush=false;
      no_all_flush=false;
      change(support);
      support=0;
      use_card=0;
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
      no_flush=false;
      no_all_flush=false;
      change(support);
      support=0;
      use_card=0;
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
  if (750<=mouseX && mouseX<=900 && 270<=mouseY && mouseY<=345 && show_number!=1 && support==0) {
    if (p1_change==2||p2_change==2) {
    } else if (p1_change==1||p2_change==1) {
    } else {
      card_change();
    }
  }

  //役の確定
  if (750<=mouseX && mouseX<=900 && 175<=mouseY && mouseY<=250 && show_number!=1 && support==0) {
    submit();
  }

  //サポートカードの表示（消し）
  if (25<=mouseX && mouseX<135 && 50<=mouseY && mouseY<=120 && show_number==1 && !support_change) {
    show_number=0;
  }

  //サポートカードの表示（付け）
  if (25<=mouseX && mouseX<=95 && 380<=mouseY && mouseY<=490 && show_number==0 &&!no_support ) {
    show_number=1;
  }

  //サポートカードの交換部分
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
  if (support==1) {
    mark_serect(3);
  }
  you_must_change();
  if (show_number==1) {
    for (int i=0; i<6; i++) {
      if (150+120*i<mouseX && mouseX<=250+120*i && 300<=mouseY && mouseY<=450) {
        support=support_card[i];
        if (support==7) {
          opp_open_card_two();
        }
        support_card[i]=0;
        use_card++;
      }
    }
  }
  if (support==5) {
    mark_serect(1);
  }
  if (support==6 && up_card==1) {
    if (750<=mouseX && mouseX<=900 && 270<=mouseY && mouseY<=345 && show_number!=1 && support==0) {
      card_chan_num_one();
    }
  }
  if (support==7) {
    if (750<=mouseX && mouseX<=900 && 270<=mouseY && mouseY<=345 && show_number!=1 && support==0) {
      any_card_chan();
    }
  }
  if (support==104) {
    mark_serect(2);
  }
  if (support==108 && up_card==2) {
    if (750<=mouseX && mouseX<=900 && 270<=mouseY && mouseY<=345 && show_number!=1 && support==0) {
      two_chan_num();
    }
  }
  if (support==109) {
    mark_serect(2);
  }
  if (support==203 && up_card==4) {
    if (750<=mouseX && mouseX<=900 && 270<=mouseY && mouseY<=345 && show_number!=1 && support==0) {
      four_chan_num();
    }
  }
  if (support==204) {
    mark_serect(4);
  }
}
