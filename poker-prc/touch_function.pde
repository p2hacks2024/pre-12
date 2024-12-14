void mark_serect(int i) {
  if (marks<i) {
    for (int j=0; j<4; j++) {
      if (320+100*j<mouseX && mouseX<400+100*j && 220<mouseY && mouseY <=300 && which_mark[marks]==4) {
        which_mark[marks]=j;
        marks++;
      }
      if (320+100*j<mouseX && mouseX<400+100*j && 220<mouseY && mouseY <=300 && which_mark[marks]!=4) {
        which_mark[marks]=0;
        marks--;
      }
    }
  }
  if (marks==1 && !mark_thr) {
    if (750<=mouseX && mouseX<=900 && 270<=mouseY && mouseY<=345) {
      if (i==3) {
        thr_mark_change_one();
      } else if (i==2) {
        two_mark_chan_one();
      } else {
        one_mark_chan();
      }
      support=0;
    }
  } else if (marks==i && mark_thr) {
    if (750<=mouseX && mouseX<=900 && 270<=mouseY && mouseY<=345) {
      mark_thr=false;
      support=0;
    }
  }
}

void num_serect() {
  if (nums==0) {
    for (int j=0; j<13; j++) {
      if (120+60*j<mouseX && mouseX<=160+60*j &&220<mouseY&&mouseY<=260) {
        which_num[0]=j;
        nums=1;
      }
    }
  } else {
    for (int j=0; j<13; j++) {
      if (120+60*j<mouseX && mouseX<160+60*j && 220<mouseY && mouseY <=260 && which_num[0]!=0) {
        which_num[0]=13;
        nums=0;
      }
    }
  }
}

void you_must_change() {
  if (up_card==opp_chan_num && opp_card_chan) {
    if (750<=mouseX && mouseX<=900 && 270<=mouseY && mouseY<=345) {
      card_change();
      support=0;
      opp_card_chan=false;
    }
  }
}
