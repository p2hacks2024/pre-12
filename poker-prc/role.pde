void role_check() {
  int mark_count[]={0, 0, 0, 0}, num_count[]=new int[13];
  int card_dex[]=new int[7];
  int card_mark[]=new int[7];
  boolean five_mark=false;
  boolean four=false, thr=false;
  int two_cards=0, straight=0;
  card_role=false;
  for (int i=0; i<7; i++) {
    r=card_num[i]%13;
    q=(card_num[i]-r)/13;
    card_dex[i]=r;
    card_mark[i]=q;
  }
  
  for (int t=0; t<7; t++) {
    if (card_check[t]!=0) {
      num_count[card_dex[t]]++;
      mark_count[card_mark[t]]++;
    }
  }
  for (int t=0; t<13; t++) {
    if (num_count[t]==2) {
      two_cards++;
    }
    if (num_count[t]==3) {
      thr=true;
    }
    if (num_count[t]==4) {
      four=true;
    }
    if (straight<5) {
      if (num_count[t]>0) {
        straight++;
      } else {
        straight=0;
      }
    }
  }
  if (num_count[9]>0 && num_count[10]>0 && num_count[11]>0 && num_count[12]>0 && num_count[0]>0) {
    straight=5;
  }
  for (int t=0; t<4; t++) {
    if (mark_count[t]==5) {
      five_mark=true;
    }
  }
  textSize(30);
  if (num_count[9]>0 && num_count[10]>0 && num_count[11]>0 && num_count[12]>0 && num_count[0]>0 && five_mark) {
    //print("Royal Straight Flush!");
    card_role=true;
    role=9;
    text("Royal Straight Flush", 350, 350);
  } else if (straight>=5 && five_mark) {
    //print("Straight flush!");
    card_role=true;
    role=8;
    text("Straight Flush", 450, 350);
  } else if (four) {
    //print("Four Cards!");
    card_role=true;
    role=7;
    text("Four Cards", 420, 350);
  } else if (thr==true && two_cards >= 1) {
    //print("Full House!");
    card_role=true;
    role=6;
    text("Full House", 450, 350);
  } else if (five_mark) {
    //print("Flush!");
    card_role=true;
    role=5;
    text("Flush", 450, 350);
  } else if (straight>=5) {
    //print("Straight!");
    card_role=true;
    role=4;
    text("Straight", 450, 350);
  } else if (thr==true) {
    //print("Three Cards!");
    card_role=true;
    role=3;
    text("Three Cards", 420, 350);
  } else if (two_cards==2) {
    //print("Two Pair!");
    card_role=true;
    role=2;
    text("Two Pair", 420, 350);
  } else if (two_cards==1) {
    //print("One pair!");
    card_role=true;
    role=1;
    text("One Pair", 420, 350);
  }
}
