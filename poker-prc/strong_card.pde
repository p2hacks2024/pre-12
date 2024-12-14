//２０１
void two_chan_mark_ron() {
  int random_n;
  random_n = which_mark[int(random(2))];
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
          if ( all_card_num[i] == all_card_num[i+t] ) {
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
//２０２
void opp_no_four_card() {
  int pq = 0;
  int random_n;
  if (which_player == 2) {
    while (pq < 4) {
      random_n = int(random(7));
      if (opp_no_change_p1[random_n] == 0) {
        opp_no_change_p1[random_n] = 1;
        pq = pq + 1;
      }
    }
  } else {
    while (pq < 4) {
      random_n = int(random(7));
      if (opp_no_change_p1[random_n] == 0) {
        opp_no_change_p1[random_n] = 1;
        pq = pq + 1;
      }
    }
  }
}
//２０３
void four_chan_num() {
  int n;
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
            all_card_num[i] = random_n*13 + u;
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
//２０４
void thr_chan_mark() {
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
        check=1;
      }
      k = 0;
    }
  }
}
