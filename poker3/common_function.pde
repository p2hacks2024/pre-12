void dif_check(int card[]) {
  int check = 0;
  int k = 0;
  while (check == 0) {
    for (int i = 0; i < card.length; i++) {
      for (int j = i + 1; j < card.length; j++) {
        if (card[i] == card[j]) {
          k = 1;
          card[i] = card[i] + 1;
          if (card[i] >= 52) {
            card[i] = 0;
          }
        }
      }
    }
    if (k == 0) {
      check = 1;
    }
    k = 0;
  }
}
