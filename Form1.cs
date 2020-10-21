using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shoot_Out_XXL
{
    public partial class Form1 : Form
    {
        int[] player1Stats = new int[2];
        int[] player2Stats = new int[2];

        int player1X = 140;
        int player1Y = 280;
        int player2X = 920;
        int player2Y = 280;
        Random powerUpPlace = new Random();
        bool isTherePowerUps = false;
        bool down1, up1, right1, left1, power1, down2, up2, right2, left2, turnR1, turnL1, turnR2, turnL2, power2;

        /// <summary>
        /// waylands
        /// </summary>
        //to keep track of the amount of bullet each player has
        //each player can have a maxinum of 10 bullet on the screen at once
        PictureBox[] bullet1 = new PictureBox[10];
        PictureBox[] bullet2 = new PictureBox[10];

        //bullet location

        int[] x1 = new int[10];
        int[] y1 = new int[10];
        int[] x2 = new int[10];
        int[] y2 = new int[10];

        int[] array = new int[8];
        //to keep track of when player shoots
        bool fire1, fire2;
        // to keep track if a player has a powerup or not
        //if they do, they dont pick up ay powerup
        bool powerUp1, powerUp2;
        int second = 0;
        int minutes = 0;
        //to keep track of player's health
        int player1Health = 1000;
        int player2Health = 1000;

        // to keep track of how many bullet the player has shot
        int bullet1Amount = 0;
        int bullet2Amount = 0;

        int invinsible1, invinsible2;
        int lastWish1, lastWish2;
        PictureBox wallUp, wallDown, wallLeft, wallRight;


        void UpdateLocation()
        {
            //Updating all location
            picPlayer1.Location = new Point(player1X, player1Y);
            picPlayer2.Location = new Point(player2X, player2Y);
            PlayerDirection();
            PowerUpCollision();
        }

        void BulletTravel()
        {
            int[] array = PlayerDirection();
            if (fire1 == true)
            {
                bullet1[bullet1Amount].Location = picPlayer1.Location;
                if (array[0] == 1)
                {
                    x1[bullet1Amount] = x1[bullet1Amount] + 5;
                    y1[bullet1Amount] = y1[bullet1Amount] - 5;

                    bullet1Amount++;

                    MaxiumBulletCheck();
                }

                else if (array[0] == 2)
                {

                    x1[bullet1Amount] = x1[bullet1Amount] + 5;

                    bullet1Amount++;

                    MaxiumBulletCheck();
                }
                else if (array[0] == 3)
                {
                    x1[bullet1Amount] = x1[bullet1Amount] + 5;
                    y1[bullet1Amount] = y1[bullet1Amount] + 5;

                    bullet1Amount++;

                    MaxiumBulletCheck();
                }
                else if (array[0] == 4)
                {
                    y1[bullet1Amount] = y1[bullet1Amount] + 5;

                    bullet1Amount++;

                    MaxiumBulletCheck();
                }
                else if (array[0] == 5)
                {
                    x1[bullet1Amount] = x1[bullet1Amount] - 5;
                    y1[bullet1Amount] = y1[bullet1Amount] + 5;

                    bullet1Amount++;

                    MaxiumBulletCheck();
                }
                else if (array[0] == 6)
                {
                    x1[bullet1Amount] = x1[bullet1Amount] - 5;

                    bullet1Amount++;

                    MaxiumBulletCheck();
                }
                else if (array[0] == 7)
                {
                    x1[bullet1Amount] = x1[bullet1Amount] - 5;
                    y1[bullet1Amount] = y1[bullet1Amount] - 5;

                    bullet1Amount++;

                    MaxiumBulletCheck();
                }
                else if (array[0] == 0)
                {
                    y1[bullet1Amount] = y1[bullet1Amount] - 5;

                    bullet1Amount++;

                    MaxiumBulletCheck();
                }

            }

            if (fire2 == true)
            {
                bullet2[bullet2Amount].Location = picPlayer1.Location;
                if (array[0] == 1)
                {
                    x2[bullet2Amount] = x2[bullet2Amount] + 5;
                    y2[bullet2Amount] = y2[bullet2Amount] - 5;

                    bullet2Amount++;
                    UpdateBulletLocation();
                    MaxiumBulletCheck();
                }

                else if (array[0] == 2)
                {
                    x2[bullet2Amount] = x2[bullet2Amount] + 5;

                    bullet2Amount++;
                    UpdateBulletLocation();
                    MaxiumBulletCheck();
                }
                else if (array[0] == 3)
                {
                    x2[bullet2Amount] = x2[bullet2Amount] + 5;
                    y2[bullet2Amount] = y2[bullet2Amount] + 5;

                    bullet2Amount++;
                    UpdateBulletLocation();
                    MaxiumBulletCheck();
                }
                else if (array[0] == 4)
                {
                    y2[bullet2Amount] = y2[bullet2Amount] + 5;

                    bullet2Amount++;
                    UpdateBulletLocation();
                    MaxiumBulletCheck();
                }
                else if (array[0] == 5)
                {
                    x2[bullet2Amount] = x2[bullet2Amount] - 5;
                    y2[bullet2Amount] = y2[bullet2Amount] + 5;

                    bullet2Amount++;
                    UpdateBulletLocation();
                    MaxiumBulletCheck();
                }
                else if (array[0] == 6)
                {
                    x2[bullet2Amount] = x2[bullet2Amount] - 5;

                    bullet2Amount++;
                    UpdateBulletLocation();
                    MaxiumBulletCheck();
                }
                else if (array[0] == 7)
                {
                    x2[bullet2Amount] = x2[bullet2Amount] - 5;
                    y2[bullet2Amount] = y2[bullet2Amount] - 5;

                    bullet2Amount++;
                    UpdateBulletLocation();
                    MaxiumBulletCheck();
                }
                else if (array[0] == 0)
                {
                    y2[bullet2Amount] = y2[bullet2Amount] - 5;

                    bullet2Amount++;
                    UpdateBulletLocation();
                    MaxiumBulletCheck();
                }
            }
        }
        void MaxiumBulletCheck()
        {
            if (bullet1Amount == 9)
            {
                bullet1Amount = 0;
            }

            if (bullet2Amount == 9)
            {
                bullet2Amount = 0;
            }
        }
        void BulletCollusion()
        {
            for (int i = 0; i < bullet1.Length; i++)
            {
                if (bullet1[i].Bounds.IntersectsWith(picPlayer1.Bounds) == true)
                {
                    player1Health = player1Health - 10;

                    if (player1Health == 0)
                    {
                        lblStats.Text = "Player1 Has Died.";
                    }
                }
                else if (bullet2[i].Bounds.IntersectsWith(picPlayer1.Bounds) == true)
                {
                    player1Health = player1Health - 10;

                    if (player1Health == 0)
                    {
                        lblStats.Text = "Player1 Has Died.";
                    }
                }
            }

            for (int i = 0; i < bullet1.Length; i++)
            {
                if (bullet1[i].Bounds.IntersectsWith(picPlayer2.Bounds) == true)
                {
                    player2Health = player2Health - 10;

                    if (player2Health == 0)
                    {
                        lblStats.Text = "Player2 Has Died.";
                    }
                }

                else if (bullet2[i].Bounds.IntersectsWith(picPlayer2.Bounds) == true)
                {
                    player2Health = player2Health - 10;

                    if (player2Health == 0)
                    {
                        lblStats.Text = "Player2 Has Died.";
                    }
                }
            }
        }
        void PowerUp()
        { 
       // //if player1 touches heal powerup, their next shot heals them for 10 health
            if (picPlayer1.Bounds.IntersectsWith(picFirstAid.Bounds) == true)
            {
                if (powerUp1 == false)
                {
                    
                    powerUp1 = true;
                    
                    if(fire1 == true)
                    {
                        player1Health = player1Health + 30;
                    }
}

                else if (powerUp1 == true)
                {
                    BulletTravel();
                }
            }

            //if player1 touches invinsible powerup, their next shot makes them invinsible for next 5 seconds
            if (picPlayer1.Bounds.IntersectsWith(picSheild.Bounds) == true)
            {
                if (powerUp1 == false)
                {
                    
                    powerUp1 = true;

                    if (fire1 == true)
                    {
                        tmrInvincible1.Enabled = true;
                    }
                }

                else if (powerUp1 == true)
                {
                    BulletTravel();
                }
            }

            //if player1 touches lastwish powerup, their next makes it so that their health
            if (picPlayer1.Bounds.IntersectsWith(picJug.Bounds) == true)
            {
                if (powerUp1 == false)
                {
                    picJug.Visible = false;
                    powerUp1 = true;
                    tmrLastWish1.Enabled = true;
                }
            }

            //if player2 touches heal powerup, their next shot heals them for 10 health
            if (picPlayer2.Bounds.IntersectsWith(picFirstAid.Bounds) == true)
            {
                if (powerUp1 == false)
                {
                  

                    powerUp1 = true;

                    if(fire2 == true)
                    {
                        player2Health = player2Health + 30;
                    }
                }

                else if (powerUp2 == true)
                {
                    BulletTravel();
                }
            }
            //if player2 touches invinsible powerup, their next shot makes them invinsible for next 10 seconds
            if (picPlayer2.Bounds.IntersectsWith(picSheild.Bounds) == true)
            {
                if (powerUp1 == false)
                {
                    
                    powerUp1 = true;

                    tmrInvincible1.Enabled = true;

                    if (fire2 == true)
                    {
                        tmrInvincible1.Enabled = true;
                    }
                }

                else if (powerUp2 == true)
                {
                }
            }

            //if player2 touches lastwish powerup, their next makes it so that their health
            if (picPlayer2.Bounds.IntersectsWith(picJug.Bounds) == true)
            {
                if (powerUp2 == false)
                {
                 
                    powerUp2 = true;
                        tmrLastWish2.Enabled = true;
                }
            }
        }


             private void lblStart_Click(object sender, EventArgs e)
        {
            lblTime.Text = minutes.ToString() + ":" + second.ToString();
            tmrGame.Enabled = true;
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            if (tmrGame.Enabled == true)
            {
                second++;

                if (second > 59)
                {
                    minutes++;

                    second = 0;
                }
            }
        }

        //count how long the invinsibility last for for player1
        private void tmrInvinsible_Tick(object sender, EventArgs e)
        {
            if (tmrInvincible1.Enabled == true)
            {
                for (int i = 0; i < bullet1.Length; i++)
                {
                    if (picPlayer1.Bounds.IntersectsWith(bullet1[i].Bounds) == true)
                    {
                        player1Health = player1Health + 10;

                        BulletCollusion();
                    }

                    else if (picPlayer1.Bounds.IntersectsWith(bullet2[i].Bounds) == true)
                    {
                        player1Health = player1Health + 10;

                        BulletCollusion();
                    }
                }
            }
        }

        void BulletArray()
        {
            //add each bullet for player1 to the bullet1 array
            bullet1[0] = picPlayer1Bullet0;
            bullet1[1] = picPlayer1Bullet1;
            bullet1[2] = picPlayer1Bullet2;
            bullet1[3] = picPlayer1Bullet3;
            bullet1[4] = picPlayer1Bullet4;
            bullet1[5] = picPlayer1Bullet5;
            bullet1[6] = picPlayer1Bullet6;
            bullet1[7] = picPlayer1Bullet7;
            bullet1[8] = picPlayer1Bullet8;
            bullet1[9] = picPlayer1Bullet9;

            //add each bullet for player2 to the bullet2 array
            bullet2[0] = picPlayer2Bullet0;
            bullet2[1] = picPlayer2Bullet1;
            bullet2[2] = picPlayer2Bullet2;
            bullet2[3] = picPlayer2Bullet3;
            bullet2[4] = picPlayer2Bullet4;
            bullet2[5] = picPlayer2Bullet5;
            bullet2[6] = picPlayer2Bullet6;
            bullet2[7] = picPlayer2Bullet7;
            bullet2[8] = picPlayer2Bullet8;
            bullet2[9] = picPlayer2Bullet9;
        }
        //Update the location of each bullet
        void UpdateBulletLocation()
        {
            bullet1[0].Location = new Point(x1[0], y1[0]);
            bullet1[1].Location = new Point(x1[1], y1[1]);
            bullet1[2].Location = new Point(x1[2], y1[2]);
            bullet1[3].Location = new Point(x1[3], y1[3]);
            bullet1[4].Location = new Point(x1[4], y1[4]);
            bullet1[5].Location = new Point(x1[5], y1[5]);
            bullet1[6].Location = new Point(x1[6], y1[6]);
            bullet1[7].Location = new Point(x1[7], y1[7]);
            bullet1[8].Location = new Point(x1[8], y1[8]);
            bullet1[9].Location = new Point(x1[9], y1[9]);

            bullet2[0].Location = new Point(x2[0], y2[0]);
            bullet2[1].Location = new Point(x2[1], y2[1]);
            bullet2[2].Location = new Point(x2[2], y2[2]);
            bullet2[3].Location = new Point(x2[3], y2[3]);
            bullet2[4].Location = new Point(x2[4], y2[4]);
            bullet2[5].Location = new Point(x2[5], y2[5]);
            bullet2[6].Location = new Point(x2[6], y2[6]);
            bullet2[7].Location = new Point(x2[7], y2[7]);
            bullet2[8].Location = new Point(x2[8], y2[8]);
            bullet2[9].Location = new Point(x2[9], y2[9]);
        }
        private void tmrBullet_Tick(object sender, EventArgs e)
        {
            BulletCollusion();
            UpdateBulletLocation();
        }

        Array PlayerHealth()
        {
            int[] playerHealth = new int[2];
            playerHealth[0] = player1Health;
            playerHealth[1] = player2Health;
            return playerHealth;
        }
        //count how long the invinsibility last for for player2
        private void tmrInvinsible2_Tick(object sender, EventArgs e)
        {
            if (tmrInvincible2.Enabled == true)
            {
                invinsible2++;
                if (invinsible2 <= 5)
                {
                    for (int i = 0; i < bullet1.Length; i++)
                    {
                        if (picPlayer2.Bounds.IntersectsWith(bullet1[i].Bounds) == true)
                        {
                            player2Health = player1Health + 10;

                            BulletCollusion();
                        }

                        else if (picPlayer2.Bounds.IntersectsWith(bullet2[i].Bounds) == true)
                        {
                            player2Health = player1Health + 10;

                            BulletCollusion();
                        }
                    }
                }

                else if (invinsible2 > 5)
                {
                    invinsible2 = 0;
                    tmrLastWish2.Enabled = false;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void picPlayer1_Click(object sender, EventArgs e)
        {

        }

        private void lblNext_Click(object sender, EventArgs e)
        {
            
                //if the text is confirm then start the game
            if (lblNext.Text == "Confirm")
            {
                
                picIntro.Visible = false;
                //lblPlayer1Name.Text = txtName1.Text;
                //lblPlayer2Name.Text = txtName2.Text;
                //txtName1.Visible = false;
                //txtName2.Visible = false;
                MovePlayer();
                //start all timer
                this.TmrMove.Start();
                this.tmrRotate.Start();
                this.tmrPowerUp.Start();

            }

            //if the text is next change to confirm so code up above can happen
               
            if (lblNext.Text == "Next")
            {
                picStart.Visible = false;
                //lblIntro.Visible = true;
                //txtName1.Visible = true;
                //txtName2.Visible = true;
                picIntro.Visible = true;
                picIntro.Location = new Point(0, 0);
                lblNext.Text = "Confirm";
            }
        }




        //count how long last wish last for for player 1
        private void tmrLastWish1_Tick(object sender, EventArgs e)
        {
            int rgb = 0;
            if (tmrLastWish1.Enabled == true)
            {
                while (player1Health <= 1000 && rgb<50)
                {
                    player1Health++;
                    rgb++;
                }
            }
        }
        //count how long last wish last for for player2
        private void tmrLastWish2_Tick(object sender, EventArgs e)
        {
            if (tmrLastWish2.Enabled == true)
            {
                lastWish2++;
                int rgb = 0;
                if (tmrLastWish2.Enabled == true)
                {
                    while (player2Health <= 1000 && rgb < 50)
                    {
                        player1Health++;
                        rgb++;
                    }
                }
            }   
        }
        void ClearBullet()
        {
            for (int i = 0; i < bullet2.Length; i++)
            {
                bullet1[i].Location = new Point(1000, 1000);
                bullet2[i].Location = new Point(1000, 1000);

                y1[i] = y1[i] + 1;
                y2[i] = y2[i] + 1;
            }
        }
    




void MovePlayer()
        {
            //using the bool from up above and check every second if the value is true, if true the player will move if false nothing happens
            if (left1 == true)
            {
                //if the player1 is mot over the righter bound then move, if not nothing happens
                if (player1X > 50)
                {
                    player1X = player1X - 5;
                }
               
            }

            if (right1 == true)
            {
                //if the player1 is not over the righter bound then move, if not nothing happens
                if (player1X < 1020)
                {
                    player1X = player1X + 5;
                }
                
            }

            if (down1 == true)
            {
                //if the player1 is higher than the bottom bound then move, if not nothing happens
                if (player1Y < 520)
                {
                    player1Y = player1Y + 5;
                }
                
            }

            if (up1 == true)
            {
                //if the player1 is lower than the upper bound then move, if not nothing happens
                if (player1Y > 50)
                {
                    player1Y = player1Y - 5;
                }
                
            }
            if (left2 == true)
            {
                //if the player2 is mot over the righter bound then move, if not nothing happens
                if (player2X > 50)
                {
                    player2X = player2X - 5;
                }
                
            }

            if (right2 == true)
            {
                //if the player2 is not over the righter bound then move, if not nothing happens
                if (player2X < 1020)
                {
                    player2X = player2X + 5;
                }
                
            }

            if (down2 == true)
            {
                //if the player2 is higher than the bottom bound then move, if not nothing happens
                if (player2Y < 520)
                {
                    player2Y = player2Y + 5;
                }
                
            }

            if (up2 == true)
            {
                //if the player1 is lower than the upper bound then move, if not nothing happens
                if (player2Y > 50)
                {
                    player2Y = player2Y - 5;
                }
                
            }
            

        }
        void RotatePlayer()
        {
            //Same function as above one, but slower rotating speed
            //there is 0-7 representing each position, so if number decrease should not be lower than 0, if going lower, return to 7
            //if the number is over 7, then return to 1. apply for all
            if (turnL1 == true)
            {
                if (player1Stats[0] > 0)
                {
                    player1Stats[0] = player1Stats[0] - 1;
                }
                else
                {
                    player1Stats[0] = 7;
                }
            }
            if (turnR1 == true)
            {
                if (player1Stats[0] < 7)
                {
                    player1Stats[0] = player1Stats[0] + 1;
                }
                else
                {
                    player1Stats[0] = 0;
                }
            }
            if (turnL2 == true)
            {
                if (player2Stats[0] > 0)
                {
                    player2Stats[0] = player2Stats[0] - 1;
                }
                else
                {
                    player2Stats[0] = 7;
                }
            }
            if (turnR2 == true)
            {
                if (player2Stats[0] < 7)
                {
                    player2Stats[0] = player2Stats[0] + 1;
                }
                else
                {
                    player2Stats[0] = 0;
                }
            }
            //just for testing
            label1.Text = player2Stats[0].ToString();

        }

        int[] PlayerDirection()
        {
            //X and Y cord for both gun
            int picGun1X = 0;
            int picGun1Y = 0;
            int picGun2X = 0;
            int picGun2Y = 0;
            //this will be the array i return and will be used in waylan's program
            int[] rotations = new int[2];

            for (int i = 0; i < 8; i++)
            {
                //loop through all number, if matched will be saved to rotation and will be used later
                if (player1Stats[0] == i)
                {
                    rotations[0] = i;
                }
                if (player2Stats[0] == i)
                {
                    rotations[1] = i;
                }
                
                //if the cordination matches, update the location
                        if (rotations[0] == 0)
                        {
                            //same thing applyed
                            picGun1Y = player1Y - 22;
                            picGun1X = player1X + 28;
                        }
                        if (rotations[0] == 1)
                        {
                            
                            picGun1Y = player1Y - 15;
                            picGun1X = player1X + 70;
                        }
                        if (rotations[0] == 2)
                        {
                          
                            picGun1Y = player1Y + 28;
                            picGun1X = player1X + 80;
                        }
                        if (rotations[0] == 3)
                        {
                            picGun1Y = player1Y + 68;
                            picGun1X = player1X + 70;
                        }
                        if (rotations[0] == 4)
                        {
                            picGun1Y = player1Y + 80;
                            picGun1X = player1X + 28;
                        }
                        if (rotations[0] == 5)
                        {
                            picGun1Y = player1Y + 68;
                            picGun1X = player1X - 13;
                        }
                        if (rotations[0] == 6)
                        {
                            picGun1X = player1X - 23;
                            picGun1Y = player1Y + 28;
                        }
                        if (rotations[0] == 7)
                        {
                            picGun1Y = player1Y - 14;
                            picGun1X = player1X - 14;
                        }
                        if (rotations[1] == 0)
                        {
                            picGun2Y = player2Y - 22;
                            picGun2X = player2X + 28;
                        }
                        if (rotations[1] == 1)
                        {
                            picGun2Y = player2Y - 15;
                            picGun2X = player2X + 70;
                        }
                        if (rotations[1] == 2)
                        {
                            picGun2Y = player2Y + 28;
                            picGun2X = player2X + 80;
                        }
                        if (rotations[1] == 3)
                        {
                            picGun2Y = player2Y + 68;
                            picGun2X = player2X + 70;
                        }
                        if (rotations[1] == 4)
                        {
                            picGun2Y = player2Y + 80;
                            picGun2X = player2X + 28;
                        }
                        if (rotations[1] == 5)
                        {
                            picGun2Y = player2Y + 68;
                            picGun2X = player2X - 13;
                        }
                        if (rotations[1] == 6)
                        {
                            picGun2X = player2X - 23;
                            picGun2Y = player2Y + 28;
                        }
                        if (rotations[1] == 7)
                        {
                            picGun2Y = player2Y - 14;
                            picGun2X = player2X - 14;
                        }
            }
            //updating locations of both guns
            picGun1.Location = new Point(picGun1X, picGun1Y);
            picGun2.Location = new Point(picGun2X, picGun2Y);
            return rotations;
        }


        int[] PowerUpCollision()
        {
            //this will be the returning value(2) which represents which powerup they pick, and who picked it up
            int[] powerTypeandPlayer = new int[2];

            //If the player picked up an powerup(collision) it will store data in the int array powerTypeandPlayer and each of them 
            //will be returned.
            if (picFirstAid.Bounds.IntersectsWith(picPlayer1.Bounds))
            {
                powerTypeandPlayer[0] = 0;
                powerTypeandPlayer[1] = 0;
                //This will teleprt the powerup off of the screen 
                picFirstAid.Location = new Point(-1000, 1000);
                //this will enable more powerup to spawn due to the maximun powerup on screen at once is 1
                isTherePowerUps = false;
            }
            if (picSheild.Bounds.IntersectsWith(picPlayer1.Bounds))
            {
                //powertypeandPlayer[0] means which power up they picked, and [1] is who picked it(0 is player1 and 1 is player 2)
                powerTypeandPlayer[0] = 1;
                powerTypeandPlayer[1] = 0;
                picSheild.Location = new Point(-1000, 1000);
                isTherePowerUps = false;
            }
            if (picDash.Bounds.IntersectsWith(picPlayer1.Bounds))
            {
                //same thing applyed
                powerTypeandPlayer[0] = 2;
                powerTypeandPlayer[1] = 0;
                picDash.Location = new Point(-1000, 1000);
                isTherePowerUps = false;
                this.tmrStunCharge1.Start();
            }
            if (picJug.Bounds.IntersectsWith(picPlayer1.Bounds))
            {
                //same thing applyed
                powerTypeandPlayer[0] = 3;
                powerTypeandPlayer[1] = 0;
                picJug.Location = new Point(-1000, 1000);
                isTherePowerUps = false;
            }

            if (picFirstAid.Bounds.IntersectsWith(picPlayer2.Bounds))
            {
                //same thing applyed
                powerTypeandPlayer[0] = 0;
                powerTypeandPlayer[1] = 1;
                picFirstAid.Location = new Point(-1000, 1000);
                isTherePowerUps = false;
            }
            if (picSheild.Bounds.IntersectsWith(picPlayer2.Bounds))
            {
                //same thing applyed
                powerTypeandPlayer[0] = 1;
                powerTypeandPlayer[1] = 1;
                picSheild.Location = new Point(-1000, 1000);
                isTherePowerUps = false;
            }
            if (picDash.Bounds.IntersectsWith(picPlayer2.Bounds))
            {
                //same thing applyed
                powerTypeandPlayer[0] = 2;
                powerTypeandPlayer[1] = 1;
                picDash.Location = new Point(-1000, 1000);
                isTherePowerUps = false;
                this.tmrStunCharge2.Start();
            }
            if (picJug.Bounds.IntersectsWith(picPlayer2.Bounds))
            {
                //same thing applyed
                powerTypeandPlayer[0] = 3;
                powerTypeandPlayer[1] = 1;
                picJug.Location = new Point(-1000, 1000);
                isTherePowerUps = false;
            }

            //this will return the value and waylan can use it in his(his part is responsible for few of the power up's function
            return powerTypeandPlayer;
        }

        void PowerUpSpawn()
        {
            //randomizing the location of a powerup spawn 
            //this will randomnize the X of the powerup
            int powerUpX = powerUpPlace.Next(170, 900);
            int powerUpY = powerUpPlace.Next(150, 450);
            //this generate if the powerup spawn each tick
            int number = powerUpPlace.Next(0, 5);
            //testing
            label2.Text = number.ToString();
            //only if the number is 1(20% chance for a powerup to spawn each tick
            if (number == 1)
            {
                // randomize the type of powerup
                int type = powerUpPlace.Next(2, 2);
                if (isTherePowerUps == false)
                {
                    if (type == 0)
                    {
                        picFirstAid.Location = new Point(powerUpX, powerUpY);
                        isTherePowerUps = true;
                    }
                    if (type == 1)
                    {
                        picSheild.Location = new Point(powerUpX, powerUpY);
                        isTherePowerUps = true;
                    }
                    if (type == 2)
                    {
                        picDash.Location = new Point(powerUpX, powerUpY);
                        isTherePowerUps = true;
                    }
                    if (type == 3)
                    {
                        picJug.Location = new Point(powerUpX, powerUpY);
                        isTherePowerUps = true;
                    }
                    
                }
            }
        }

        void GameStart()
        {
            //updating the picture of the intro and rules
            picStart.Visible = true;
            picStart.Location = new Point(0, 0);
            lblNext.Visible = true;
        }

        void GameEnd()
        {
           picGun2.Visible = false;
        }

        public Form1()
        {
            //starting the game
            InitializeComponent();
            GameStart();
            this.TmrMove.Start();
            this.tmrRotate.Start();
            this.tmrPowerUp.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //each bool will be actived if the key is pressed
            if (e.KeyCode == Keys.A)
            {
                left1 = true;
            }
            if (e.KeyCode == Keys.S)
            {
                down1 = true;
            }
            if (e.KeyCode == Keys.D)
            {
                right1 = true;
            }
            if (e.KeyCode == Keys.W)
            {
                up1 = true;
            } 
            if (e.KeyCode == Keys.Left)
            {
                left2 = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                down2 = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                right2 = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                up2 = true;
            }
            if (e.KeyCode == Keys.Q)
            {
                turnL1 = true;
            }
            if (e.KeyCode == Keys.E)
            {
                turnR1 = true;
            }
            if (e.KeyCode == Keys.K)
            {
                turnL2 = true;
            }
            if (e.KeyCode == Keys.L)
            {
                turnR2 = true;
            }
            if (e.KeyCode == Keys.F)
            {
                power1 = true;
            }
            if (e.KeyCode == Keys.J)
            {
                power2 = true;
            }
            if(e.KeyCode == Keys.Space)
            {
                fire1 = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                fire2 = true;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //each bool will be deactived if user letgo
            if (e.KeyCode == Keys.A)
            {
                left1 = false;
            }
            if (e.KeyCode == Keys.S)
            {
                down1 = false;
            }
            if (e.KeyCode == Keys.D)
            {
                right1 = false;
            }
            if (e.KeyCode == Keys.W)
            {
                up1 = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                left2 = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                down2 = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                right2 = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                up2 = false;
            }
            if (e.KeyCode == Keys.Q)
            {
                turnL1 = false;
            }
            if (e.KeyCode == Keys.E)
            {
                turnR1 = false;
            }
            if (e.KeyCode == Keys.K)
            {
                turnL2 = false;
            }
            if (e.KeyCode == Keys.L)
            {
                turnR2 = false;
            }
            if (e.KeyCode == Keys.F)
            {
                power1 = false;
            }
            if (e.KeyCode == Keys.J)
            {
                power2 = false;
            }

        }
        private void tmrRotate_Tick(object sender, EventArgs e)
        {
            RotatePlayer();
            
        }
        private void TimerMove_Tick(object sender, EventArgs e)
        {
            MovePlayer();
            UpdateLocation();
            HealthBar();

            lblTime.Text = player1Health.ToString();
            
        }

        private void tmrPowerUp_Tick(object sender, EventArgs e)
        {
            PowerUpSpawn();
            
        }
        private void tmrStunCharge1_Tick(object sender, EventArgs e)
        {
            //same function as the program below
            int tick = 0;
            label3.Text = tick.ToString();
            if (power1 == true)
            {
                do
                {
                    if (picPlayer1.Bounds.IntersectsWith(picPlayer2.Bounds) == true)
                    {
                        player2Health--;
                    }
                        if (left1 == true)
                    {
                        if (player1X > 50)
                        {
                            player1X = player1X - 1;
                        }

                    }

                    if (right1 == true)
                    {
                        if (player1X < 1020)
                        {
                            player1X = player1X + 1;
                        }

                    }

                    if (down1 == true)
                    {
                        if (player1Y < 520)
                        {
                            player1Y = player1Y + 1;
                        }

                    }

                    if (up1 == true)
                    {
                        if (player1Y > 50)
                        {
                            player1Y = player1Y - 1;
                        }

                    }
                    UpdateLocation();
                    tick++;
                    

                } while (tick < 600);
            }
            if (tick == 600)
            {
                tick = 0;
                this.tmrStunCharge1.Stop();
            }
        }


        private void tmrStunCharge2_Tick(object sender, EventArgs e)
        {
            //counting all ticks 
            int tick = 0;
            //testing
            label3.Text = tick.ToString();
            if (power2 == true)
            {
                do
                {
                    if (picPlayer2.Bounds.IntersectsWith(picPlayer1.Bounds) == true)
                    {
                        player1Health--;
                    }
                    //do those while condition meet
                    if (left2 == true)
                    {
                        if (player2X > 50)
                        {
                            player2X = player2X - 1;
                        }

                    }

                    if (right2 == true)
                    {
                        if (player2X < 1020)
                        {
                            player2X = player2X + 1;
                        }

                    }

                    if (down2 == true)
                    {
                        if (player2Y < 520)
                        {
                            player2Y = player2Y + 1;
                        }

                    }

                    if (up2 == true)
                    {
                        //speeding things up by a little
                        if (player2Y > 50)
                        {
                            player2Y = player2Y - 1;
                        }

                    }
                    UpdateLocation();
                    tick++;
                    
                    //loop will not stop and player will keep moving if the tick is lower than 600
                } while (tick < 600);
            }
            if (tick == 600)
            {
                //it will end and reset
                tick = 0;
                this.tmrStunCharge2.Stop();
            }
        }
        void HealthBar()
        {
            //updating healthbarF
            try
            {
                pgbPlayer1.Value = player1Health / 10;
                pgbPlayer2.Value = player2Health / 10;
            }
            catch (Exception EX)
            {
                string message;
                message = player1Health  <= 0 ? "Player 1... YOU SUCK!!!" : player2Health<=0 ? "Player 2... YOU SUCK!!!" : null;
                TmrMove.Stop();
                
                MessageBox.Show(message);
                Application.Exit();
            }
        }
    }
}
