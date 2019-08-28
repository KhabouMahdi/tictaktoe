using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictaktoe
{
    public partial class tictactoe : Form
    {
        bool turn = true;//true =turn X false =turn O
        int turn_count = 0;
        bool againstCmp = true;
        int ocount=0;
        int dcount = 0;
        int xcount = 0;

        public tictactoe()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("BY Mahdi Khabou");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonClick(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (turn)
                b.Text = "X";
            else
                b.Text = "O";
            
            turn = !turn;
            b.Enabled = false;
            turn_count++;
            checkWinn();
            if ((!turn)&&(againstCmp))
            {
                computer_make_move();
            }
        }
        private void checkWinn()
        {
            bool win = false;


            //hor check
            if (A1.Text == A2.Text && A2.Text == A3.Text && !A1.Enabled)
                win = true;
             if (B1.Text == B2.Text && B2.Text == B3.Text && !B1.Enabled)
                win = true;
             if (C1.Text == C2.Text && C2.Text == C3.Text && !C1.Enabled)
                win = true;
            //vert check
             if (A1.Text == B1.Text && B1.Text == C1.Text && !A1.Enabled)
                win = true;
             if (A2.Text == B2.Text && B2.Text == C2.Text && !A2.Enabled)
                win = true;
           if (A3.Text == B3.Text && B3.Text == C3.Text && !A3.Enabled)
                win = true;
            //diag check
            if (A1.Text == B2.Text && B2.Text == C3.Text && !B2.Enabled)
                win = true;
            if (A3.Text == B2.Text && B2.Text == C1.Text && !B2.Enabled)
                win = true;
            if (win)
            {
               
                disableButton();
                String winner = "";
                if (turn)
                {
                    winner = "O";
                    MessageBox.Show(winner + "   win congratulation");
                    txtYcount.Text = (Convert.ToInt32(txtYcount.Text)+(ocount + 1)).ToString();
                }
                else
                {
                    winner = "X";
                    MessageBox.Show(winner + "   win congratulation");
                    txtxCount.Text = (Convert.ToInt32( txtxCount.Text) +(xcount + 1)).ToString();
                }

            }
            else
            {
                if (turn_count == 9)
                {
                    MessageBox.Show("Draw");
                    txtDrawcount.Text = (Convert.ToInt32(txtDrawcount.Text)+(dcount + 1))
                        .ToString();
                }

            }

        }
        private void disableButton()
        {
            try
           {
                foreach (Control c in this.Controls)
                {
                    if (c.GetType() == typeof(Button))
                    {
                        Button b = (Button)c;
                        b.Enabled = false;
                    }
                }
            }
            catch { }

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
           turn = true;
           turn_count = 0;
            if (againstCmp == false)
                againstCmp = true;
            foreach (Control c in Controls)
                {
                if (c.GetType() == typeof(Button))
                {
                    Button a = (Button)c;
                    a.Enabled = true;
                    a.Text = "";
                }
                }
          
        }


        private void computer_make_move()
        {
            System.Threading.Thread.Sleep(700);

            //priority 1:  get tick tac toe
            //priority 2:  block x tic tac toe
            //priority 3:  go for corner space
            //priority 4:  pick open space

            Button move = null;
            try
            {
                //look for tic tac toe opportunities
                move = look_for_win_or_block("O"); //look for win
                if (move == null)
                {
                    move = look_for_win_or_block("X"); //look for block
                    if (move == null)
                    {
                        move = look_for_corner();
                        if (move == null)
                        {
                            move = look_for_open_space();
                        }//end if
                    }//end if
                }//end if

                move.PerformClick();
            }
            catch { };
        }

        private Button look_for_open_space()
        {
            Console.WriteLine("Looking for open space");
            Button b = null;
            foreach (Control c in Controls)
            {
                b = c as Button;
                if (b != null)
                {
                    if (b.Text == "")
                        return b;
                }//end if
            }//end if

            return null;
        }

        private Button look_for_corner()
        {
            Console.WriteLine("Looking for corner");
            if (A1.Text == "O")
            {
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (A3.Text == "O")
            {
                if (A1.Text == "")
                    return A1;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (C3.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C1.Text == "")
                    return C1;
            }

            if (C1.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
            }

            if (A1.Text == "")
                return A1;
            if (A3.Text == "")
                return A3;
            if (C1.Text == "")
                return C1;
            if (C3.Text == "")
                return C3;

            return null;
        }

        private Button look_for_win_or_block(string mark)
        {
            Console.WriteLine("Looking for win or block:  " + mark);
            //HORIZONTAL TESTS
            if ((A1.Text == mark) && (A2.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A2.Text == mark) && (A3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (A3.Text == mark) && (A2.Text == ""))
                return A2;

            if ((B1.Text == mark) && (B2.Text == mark) && (B3.Text == ""))
                return B3;
            if ((B2.Text == mark) && (B3.Text == mark) && (B1.Text == ""))
                return B1;
            if ((B1.Text == mark) && (B3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((C1.Text == mark) && (C2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((C2.Text == mark) && (C3.Text == mark) && (C1.Text == ""))
                return C1;
            if ((C1.Text == mark) && (C3.Text == mark) && (C2.Text == ""))
                return C2;

            //VERTICAL TESTS
            if ((A1.Text == mark) && (B1.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B1.Text == mark) && (C1.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C1.Text == mark) && (B1.Text == ""))
                return B1;

            if ((A2.Text == mark) && (B2.Text == mark) && (C2.Text == ""))
                return C2;
            if ((B2.Text == mark) && (C2.Text == mark) && (A2.Text == ""))
                return A2;
            if ((A2.Text == mark) && (C2.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B3.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B3.Text == mark) && (C3.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C3.Text == mark) && (B3.Text == ""))
                return B3;

            //DIAGONAL TESTS
            if ((A1.Text == mark) && (B2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B2.Text == mark) && (C3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B2.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B2.Text == mark) && (C1.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C1.Text == mark) && (B2.Text == ""))
                return B2;

            return null;
        }

        private void tictactoe_Load(object sender, EventArgs e)
        {

        }

        private void buttonEnter(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                if (turn) { 
                    b.ForeColor = Color.White;
                b.Text = "X";
            }
            else
                b.Text = "O";
                // turn = !turn;
                //   b.Enabled = false;

            }
        }

        private void buttonLeave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            b.Text = "";
        }

        private void resetGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtxCount.Text = "0";
            txtDrawcount.Text = "0";
            txtYcount.Text = "0";
            
               
                
        }

        private void vs1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turn_count = 0;
            if (againstCmp == true)
                againstCmp = false;
            foreach (Control c in Controls)
            {
                // try
                // {
                if (c.GetType() == typeof(Button))
                {
                    Button a = (Button)c;
                    a.Enabled = true;
                    a.Text = "";
                }
            }
        }
    }
}
