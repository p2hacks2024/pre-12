//００１：3種類のマークを指定して交換（１回）
void thr_mark_change_one() {
  int random_n;
  random_n = which_mark[int(random(3))];//指定されたマーク
  int t = 0;
  for (int i = 0; i < 7; i++) {
    if (card_check[i] == 1) {
      card_num[i] = random_n*13+int(random(13));
      card_mark[i] = random_n*13;
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
        for (int j = 1; j<all_card_num.length; j++) {
          if ( i + j >= 14) {
            t = j - 14;
          } else {
            t = j;
          }
          if ( all_card_num[i] == all_card_num[i+t] ) {
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
  support=0;
}
//００２
void opp_chan_two_desi() {
  opp_card_chan = true;
  opp_chan_num = 2;
}

//００３
void opp_chan_one_ran(int card[]) {
  int random_n;
  random_n = int(random(7));
  int t;
  if (which_player == 1) {
    card_check[random_n+7] = 1;
  } else {
    card_check[random_n] = 1;
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
            t = j-14;
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
//００４
void opp_no_flush() {
  no_flush = true;
}
//００５
void one_mark_chan() {
  int random_n;
  random_n = which_mark[0];
  int t = 0;
  for (int i = 0; i < 7; i++) {
    if (card_check[i] == 1) {
      card_num[i] = random_n * 13 + int(random(13));
      card_mark[i] = random_n;
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
          if (i+j >= 14) {
            t = j - 14;
          } else {
            t = j;
          }
          if (all_card_num[i] == all_card_num[i+t]) {
            k = 1;
            all_card_num[i] = all_card_num[i] + 1;
            change_flag[i] = 1;
            if (card_num[i] > card_mark[i] * 13 + 12) {
              card_num[i] = card_mark[i] * 13;
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
//００６
void card_chan_num_one() {
  int n;
  int random_n = 0;
  int no_can_chan = 0;
  n = which_num[0];
  int t = 0;
  for (int i = 0; i < 7; i++) {
    if (card_check[i] == 1) {
      card_num[i] = random_n*13+n;
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
        all_card_num[i] = random_n*13 + n;
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
            all_card_num[i] = random_n * 13 + n;
            change_flag[i] = 1;
            if (no_can_chan > 4) {
              random_n = 0;
              n = n + 1;
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
//００７
void opp_no_supp() {
  no_support = true;
  if (which_player == 1) {
    no_turn[2] = 1;
  } else {
    no_turn[1] = 1;
  }
}
//００８
void any_card_chan() {
  card_change();
}

//００９
void opp_open_card_two() {
  int pq=0;
  int random_n;
  while(pq<2){
    random_n=int(random(7));
    if(open_card[random_n]==0){
      open_card[random_n]=1;
      pq++;
    }
  }
}
