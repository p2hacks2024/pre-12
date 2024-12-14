void support_do(int i) {
  switch(i) {
  case 1:
    show_number=0;
    for (int j=0; j<4; j++) {
      if (j==which_mark[j]) {
        fill(255);
      } else {
        fill(155);
      }
      rect(320+100*j, 220, 80, 80);
      fill(0);
      textSize(40);
      text(j+1, 350+100*j, 280);
    }
    if (marks==3) {
      fill(0, 60);
      rect(750, 270, 150, 75);
      fill(0);
      textSize(30);
      text("Change", 775, 320);
    }
    break;
  case 2:
    text("You must change", 350, 350);
    if (up_card==opp_chan_num && opp_card_chan) {
      fill(0, 60);
      rect(750, 270, 150, 75);
      fill(0);
      textSize(30);
      text("Change", 775, 320);
    }
    break;
  case 3:
    if (which_player==1) {
      opp_chan_one_ran(p2_card);
    } else {
      opp_chan_one_ran(p1_card);
    }
  case 5:
    for (int j=0; j<4; j++) {
      if (j==which_mark[j]) {
        fill(255);
      } else {
        fill(155);
      }
      rect(320+100*j, 220, 80, 80);
      fill(0);
      textSize(40);
      text(j+1, 350+100*j, 280);
    }
    if (marks==1) {
      fill(0, 60);
      rect(750, 270, 150, 75);
      fill(0);
      textSize(30);
      text("Change", 775, 320);
    }
    break;
  case 6:
    for (int j=0; j<13; j++) {
      if (j==which_num[0]) {
        fill(255);
      } else {
        fill(155);
      }
      rect(120+60*j, 220, 40, 40);
      fill(0);
      textSize(20);
      text(j+1, 140+60*j, 250);
    }
    if (nums==1) {
      fill(0, 60);
      rect(750, 270, 150, 75);
      fill(0);
      textSize(30);
      text("Change", 775, 320);
    }
    break;
  case 8:
    fill(0, 60);
    rect(750, 270, 150, 75);
    fill(0);
    textSize(30);
    text("Change", 775, 320);
    break;
  case 9:
    if (which_player==1) {
      for (int j=0; j<7; j++) {
        q=all_card_num[j]%13;
        r=(all_card_num[j]-q)/13;
        if (open_card[j]==1) {
          textSize(10);
          text(q+1, 265+100*j, 20);
          textSize(50);
          text(r+1, 280+100*j, 40);
        }
      }
    } else {
      for (int j=0; j<7; j++) {
        q=all_card_num[j+7]%13;
        r=(all_card_num[j+7]-q)/13;
        if (open_card[j]==1) {
          textSize(10);
          text(q+1, 265+100*j, 20);
          textSize(50);
          text(r+1, 280+100*j, 40);
        }
      }
    }
    break;
  case 101:
    if (which_player==1) {
      for (int j=0; j<7; j++) {
        q=all_card_num[j]%13;
        r=(all_card_num[j]-q)/13;
        if (open_card[j]==1) {
          textSize(10);
          text(q+1, 265+100*j, 20);
          textSize(50);
          text(r+1, 280+100*j, 40);
        }
      }
    } else {
      for (int j=0; j<7; j++) {
        q=all_card_num[j+7]%13;
        r=(all_card_num[j+7]-q)/13;
        if (open_card[j]==1) {
          textSize(10);
          text(q+1, 265+100*j, 20);
          textSize(50);
          text(r+1, 280+100*j, 40);
        }
      }
    }
    break;
  case 102:
    mark_thr=true;
    if (which_player==1) {
      p1_change=1;
    } else {
      p2_change=1;
    }
    show_number=0;
    if (mark_thr) {
      if (which_player==1) {
        for (int j=0; j<4; j++) {
          if (j==p1_which_mark[j]) {
            fill(255);
          } else {
            fill(155);
          }
          rect(320+100*j, 220, 80, 80);
          fill(0);
          textSize(40);
          text(j+1, 350+100*j, 280);
        }
        if (marks==3) {
          fill(0, 60);
          rect(750, 270, 150, 75);
          fill(0);
          textSize(30);
          text("Just in", 775, 320);
        }
      } else {
        for (int j=0; j<4; j++) {
          if (j==p2_which_mark[j]) {
            fill(255);
          } else {
            fill(155);
          }
          rect(320+100*j, 220, 80, 80);
          fill(0);
          textSize(40);
          text(j+1, 350+100*j, 280);
        }
        if (marks==3) {
          fill(0, 60);
          rect(750, 270, 150, 75);
          fill(0);
          textSize(30);
          text("Just in", 775, 320);
        }
      }
    }
    break;
  case 104:
    for (int j=0; j<4; j++) {
      if (j==which_mark[j]) {
        fill(255);
      } else {
        fill(155);
      }
      rect(320+100*j, 220, 80, 80);
      fill(0);
      textSize(40);
      text(j+1, 350+100*j, 280);
    }
    if (marks==2) {
      fill(0, 60);
      rect(750, 270, 150, 75);
      fill(0);
      textSize(30);
      text("Change", 775, 320);
    }
    break;
  case 108:
    for (int j=0; j<13; j++) {
      if (j==which_num[0]) {
        fill(255);
      } else {
        fill(155);
      }
      rect(120+60*j, 220, 40, 40);
      fill(0);
      textSize(20);
      text(j+1, 140+60*j, 250);
    }
    if (nums==1) {
      fill(0, 60);
      rect(750, 270, 150, 75);
      fill(0);
      textSize(30);
      text("Change", 775, 320);
    }
    break;
  case 109:
    for (int j=0; j<4; j++) {
      if (j==which_mark[j]) {
        fill(255);
      } else {
        fill(155);
      }
      rect(320+100*j, 220, 80, 80);
      fill(0);
      textSize(40);
      text(j+1, 350+100*j, 280);
    }
    if (marks==1) {
      fill(0, 60);
      rect(750, 270, 150, 75);
      fill(0);
      textSize(30);
      text("Change", 775, 320);
    }
    break;
  case 201:
    mark_two=true;
    if (which_player==1) {
      p1_change=2;
    } else {
      p2_change=2;
    }
    show_number=0;
    if (mark_two) {
      if (which_player==1) {
        for (int j=0; j<4; j++) {
          if (j==p1_which_mark[j]) {
            fill(255);
          } else {
            fill(155);
          }
          rect(320+100*j, 220, 80, 80);
          fill(0);
          textSize(40);
          text(j+1, 350+100*j, 280);
        }
        if (marks==2) {
          fill(0, 60);
          rect(750, 270, 150, 75);
          fill(0);
          textSize(30);
          text("Just in", 775, 320);
        }
      } else {
        for (int j=0; j<4; j++) {
          if (j==p2_which_mark[j]) {
            fill(255);
          } else {
            fill(155);
          }
          rect(320+100*j, 220, 80, 80);
          fill(0);
          textSize(40);
          text(j+1, 350+100*j, 280);
        }
        if (marks==2) {
          fill(0, 60);
          rect(750, 270, 150, 75);
          fill(0);
          textSize(30);
          text("Just in", 775, 320);
        }
      }
    }
    break;
  case 203:
    for (int j=0; j<13; j++) {
      if (j==which_num[0]) {
        fill(255);
      } else {
        fill(155);
      }
      rect(120+60*j, 220, 40, 40);
      fill(0);
      textSize(20);
      text(j+1, 140+60*j, 250);
    }
    if (nums==1) {
      fill(0, 60);
      rect(750, 270, 150, 75);
      fill(0);
      textSize(30);
      text("Change", 775, 320);
    }
    break;
  case 204:
    for (int j=0; j<4; j++) {
      if (j==which_mark[j]) {
        fill(255);
      } else {
        fill(155);
      }
      rect(320+100*j, 220, 80, 80);
      fill(0);
      textSize(40);
      text(j+1, 350+100*j, 280);
    }
    if (marks==1) {
      fill(0, 60);
      rect(750, 270, 150, 75);
      fill(0);
      textSize(30);
      text("Change", 775, 320);
    }
    break;
  }
}

void change(int i) {
  switch(i) {
  case 2:
    opp_chan_two_desi();
    break;
  case 4:
    opp_no_flush();
    break;
  case 7:
    opp_no_supp();
    break;
  case 103:
    opp_all_chan();
    break;
  case 105:
    if (which_player==1) {
      opp_thr_chan_ran(p2_card);
    } else {
      opp_thr_chan_ran(p1_card);
    }
    break;
  case 106:
    opp_no_card_thr();
    break;
  case 107:
    opp_chan_four_card_desi();
    break;
  case 110:
    opp_no_supp_thr();
    break;
  case 111:
    opp_no_all_flush();
    break;
  case 202:
    opp_no_four_card();
    break;
  }
}
