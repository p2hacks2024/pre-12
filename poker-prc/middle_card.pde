//１０１
void opp_open_four_card() {
  int pq=0;
  int random_n;
  while(pq<4){
    random_n=int(random(7));
    if(open_card[random_n]==0){
      open_card[random_n]=1;
      pq++;
    }
  }
}
//１０２
void thr_mark_chan_rou() {
  int random_n;
  if(which_player==1){
  random_n = p1_which_mark[int(random(3))];
  }else{
    random_n = p2_which_mark[int(random(3))];
  }
  int t = 0;
  for (int i = 0; i < 7; i++) {
    if (card_check[i] == 1) {
      card_num[i] = random_n * 13 + int(random(13));
      card_mark[i] = random_n * 13;
      card_y[i] = 420;
    }
    if (which_player == 1) {
      all_card_num[i] = card_num[i];
    } else {
      card_check[i+7] = card_check[i];
      all_card_num[i+7] = card_num[i];
      card_check[i] = 0;
    }
  }
  int check = 0;
  int k = 0;
  while (check == 0) {
    for (int i = 0; i < all_card_num.length; i++) {
      if (card_check[i] == 1) {
        change_flag[i] = 0;
        for (int j = 1; j < all_card_num.length; j++) {
          if (i + j >= 14) {
            t = j - 14;
          } else {
            t = j;
          }
          if ( all_card_num[i] == all_card_num[i+t]) {
            k = 1;
            all_card_num[i] = all_card_num[i] + 1;
            change_flag[i] = 1;
            if (all_card_num[i] > card_mark[i] * 13 + 12) {
              all_card_num[i] = card_mark[i]*13;
            }
          }
        }
      }
      if (change_flag[i] == 0) {
        card_check[i] = 0;
      }
    }
    if (k == 0) {
      for (int i = 0; i < 7; i++) {
        if (which_player == 1) {
          card_num[i] = all_card_num[i];
        } else {
          card_num[i] = all_card_num[i+7];
        }
        check = 1;
      }
      k = 0;
    }
  }
}
//１０３
void opp_all_chan() {
  opp_card_chan = true;
  opp_chan_num = 7;
}
//１０４
void two_mark_chan_one() {
  int random_n;
  random_n = which_mark[int(random(2))];//指定されたマーク
  int t = 0;
  for (int i = 0; i < 7; i++) {
    if (card_check[i] == 1) {
      card_num[i] = random_n*13+int(random(13));
      card_mark[i] = random_n*13;
      card_y[i] = 420;
    }
  }
  int check = 0;
  int k = 0;
  while (check == 0) {
    for (int i = 0; i < all_card_num.length; i++) {
      if (card_check[i] == 1) {
        change_flag[i] = 0;
        for (int j = 1; j < all_card_num.length; j++) {
          if (i + j >= 14) {
            t = j - 14;
          } else {
            t = j;
          }
          if (all_card_num[i] == all_card_num[i+t]) {
            k = 1;
            all_card_num[i] = all_card_num[i] + 1;
            change_flag[i] = 1;
            if (all_card_num[i] >= 52) {
              card_num[i] = 0;
            }
          }
        }
      }
      if (change_flag[i] == 0) {
        card_check[i] = 0;
      }
    }
    if (k == 0) {
      for (int i = 0; i < 7; i++) {
        if (which_player == 1) {
          card_num[i] = all_card_num[i];
        } else {
          card_num[i] = all_card_num[i+7];
        }
        check = 1;
      }
      k = 0;
    }
  }
}
//１０５
void opp_thr_chan_ran(int card[]) {
  int random_n;
  int t;
  int pq = 0;
  if (which_player == 1) {
    while (pq < 3) {
      random_n = int(random(7));
      if (card_check[random_n+7] == 0) {
        card_check[random_n+7] = 1;
        pq = pq + 1;
      }
    }
  } else {
    while (pq < 3) {
      random_n=int(random(7));
      if (card_check[random_n] == 0) {
        card_check[random_n] = 1;
        pq = pq + 1;
      }
    }
  }
  for (int i = 0; i < 7; i++) {
    card_y[i] = 420;
  }
  int check = 0;
  int k = 0;
  while (check == 0) {
    for (int i = 0; i < all_card_num.length; i++) {
      if (card_check[i] == 1) {
        change_flag[i] = 0;
        for (int j = 1; j < all_card_num.length; j++) {
          if (i + j >= 14) {
            t = j - 14;
          } else {
            t = j;
          }
          if (all_card_num[i] == all_card_num[i+t]) {
            k = 1;
            all_card_num[i] = all_card_num[i] + 1;
            change_flag[i] = 1;
            if (all_card_num[i] > 52) {
              all_card_num[i] = 0;
            }
          }
        }
      }
      if (change_flag[i] == 0) {
        card_check[i] = 0;
      }
    }
    if (k == 0) {
      for (int i = 0; i < 7; i++) {
        card[i] = all_card_num[i];
      }
      check = 1;
    }
    k = 0;
  }
}
//１０６
void opp_no_card_thr() {
  int pq = 0;
  int random_n;
  if (which_player == 2) {
    while (pq < 3) {
      random_n = int(random(7));
      if (opp_no_change_p1[random_n] == 0) {
        opp_no_change_p1[random_n] = 1;
        pq = pq + 1;
      }
    }
  } else {
    while (pq < 3) {
      random_n = int(random(7));
      if (opp_no_change_p2[random_n] == 0) {
        opp_no_change_p2[random_n] = 1;
        pq = pq + 1;
      }
    }
  }
}
//１０７
void opp_chan_four_card_desi() {
  opp_card_chan = true;
  opp_chan_num = 4;
}
//１０８
void two_chan_num() {
  int n = 0;
  int random_n = 0;
  int no_can_chan = 0;
  n = which_num[0];
  int t = 0;
  for (int i = 0; i < 7; i++) {
    if (card_check[i] == 1) {
      card_num[i] = random_n * 13 + n;
      card_y[i] = 420;
      if (which_player == 1) {
        all_card_num[i] = card_num[i];
      } else {
        card_check[i+7] = card_check[i];
        all_card_num[i+7] = card_num[i];
        card_check[i] = 0;
      }
      break;
    }
  }
  int check = 0;
  int k = 0;
  int u = 0;
  while (check == 0) {
    for (int i = 0; i < all_card_num.length; i++) {
      if (card_check[i] == 1) {
        u = all_card_num[i] % 13;
        all_card_num[i] = random_n*13 + u;
        change_flag[i] = 0;
        for (int j = 1; j < all_card_num.length; j++) {
          if (i + j >= 14) {
            t = j - 14;
          } else {
            t = j;
          }
          if (all_card_num[i] == all_card_num[i+t]) {
            k = 1;
            random_n = random_n + 1;
            no_can_chan = no_can_chan + 1;
            u = all_card_num[i] % 13;
            all_card_num[i] = random_n * 13 + u;
            change_flag[i] = 1;
            if (no_can_chan > 4) {
              random_n = 0;
              u = u + 1;
            }
          }
        }
      }
      if (change_flag[i] == 0) {
        card_check[i] = 0;
      }
    }
    if (k == 0) {
      for (int i = 0; i < 7; i++) {
        if (which_player == 1) {
          card_num[i] = all_card_num[i];
        } else {
          card_num[i] = all_card_num[i+7];
        }
        check = 1;
      }
      k = 0;
    }
  }
}
//１０９
void two_chan_mark() {
  int random_n;
  int t = 0;
  random_n = which_mark[0];
  for (int i = 0; i < 7; i++) {
    if (card_check[i] == 1) {
      card_num[i] = random_n * 13 + int(random(13));
      card_mark[i] = random_n;
      card_y[i] = 420;
      if (which_player == 1) {
        all_card_num[i] = card_num[i];
      } else {
        card_check[i+7] = card_check[i];
        all_card_num[i+7] = card_num[i];
        card_check[i] = 0;
      }
      break;
    }
  }
  int check = 0;
  int k = 0;
  while (check == 0) {
    for (int i = 0; i < all_card_num.length; i++) {
      if (card_check[i] == 1) {
        change_flag[i] = 0;
        for (int j = 1; j < all_card_num.length; j++) {
          if (i + j >= 14) {
            t = j - 14;
          } else {
            t = j;
          }
          if (all_card_num[i] == all_card_num[i+t]) {
            k = 1;
            all_card_num[i] = all_card_num[i] + 1;
            change_flag[i] = 1;
            if (all_card_num[i] > card_mark[i] * 13 + 12) {
              all_card_num[i] = card_mark[i] * 13;
            }
          }
        }
      }
      if (change_flag[i] == 0) {
        card_check[i] = 0;
      }
    }
    if (k == 0) {
      for (int i = 0; i < 7; i++) {
        if (which_player == 1) {
          card_num[i] = all_card_num[i];
        } else {
          card_num[i] = all_card_num[i+7];
        }
        check = 1;
      }
      k = 0;
    }
  }
}
//１１０
void opp_no_supp_thr() {
  no_support = true;
  if (which_player == 1) {
    no_turn[2] = 3;
  } else {
    no_turn[1] = 3;
  }
}
// １１１
void opp_no_all_flush() {
  no_all_flush = true;
}
