using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Media;
namespace game2048
{
    public partial class Game2048 : Form
    {
       
        SoundPlayer sound = new SoundPlayer(Application.StartupPath+"/andiem.wav");//KAZANMA SESİ
        SoundPlayer sound2 = new SoundPlayer(Application.StartupPath+"/blip.wav");//KAYBETME SESİ
        Random Rd = new Random();
        bool tekrarOyun = true;
        static ArrayList dizi1 = new ArrayList();//DİZİ1
        public Game2048()
        {
            //asagı ve yukarı haraket ve sag ve sol haraket birbirinin tersi işlem yapar yani biri diziyi arttırırken diğeri azaltır.
            //ortak noktaları textlerin içinde ki sayıları toplamak ve var olan score eklemek.
            //
           
            InitializeComponent();
            
        }
      
        public void renkislemi()
        {// bu fonksiyonda sayıların ve boştextin rengini belirliyorum
            Label[,] Game = { 
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {

                    if(Game[i,j].Text==""){
                        Game[i, j].BackColor = System.Drawing.Color.DarkMagenta;
                    }
                    if (Game[i, j].Text == "2")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.LightGray;
                        Game[i, j].ForeColor = System.Drawing.Color.White;

                    }
                    if (Game[i, j].Text == "4")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.Gray;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "8")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.Orange;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "16")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.OrangeRed;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "32")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.DarkOrange;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "64")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.LightPink;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "128")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.Red;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "256")
                    {
                        Game[i, j].BackColor = Color.DarkRed;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "512")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.LightBlue;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "1024")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.Blue;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "2048")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.Green;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    //if (Game[i, j].Text == "4096")
                    //{
                    //    Game[i, j].BackColor = System.Drawing.Color.Gray;
                    //    Game[i, j].ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    //}
                    //if (Game[i, j].Text == "8192")
                    //{
                    //    Game[i, j].BackColor = System.Drawing.Color.Gray;
                    //    Game[i, j].ForeColor = System.Drawing.Color.Yellow;
                    //}
                }
            }
            
        } //RENK İŞLEMLERİ
        
        public void sayiUret() {
            dizi1.Clear();

            Label[,] Game = { //labelleri diziye atıyoruz
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            for (int i = 0; i < 4;i++ )
            {
                for (int j = 0; j < 4;j++)
                {
                    if(Game[i,j].Text==""){
                        dizi1.Add(i*4+j+1);//16 tane oldugu için iç içe 2 for ve bunları diziye ekliyoruz
                    }
                }
            }
            
            if(dizi1.Count>0){
               
                int sayidoldur = int.Parse(dizi1[Rd.Next(0,dizi1.Count-1)].ToString());
                int i0 = (sayidoldur - 1) / 4;
                int j0 = (sayidoldur - 1) - i0 *4;
                int dizi2 = Rd.Next(1,10);//DİZİ 2 //random olarak atayabilmek için
                if (dizi2 == 1 || dizi2 == 2 || dizi2 == 3 || dizi2 == 4 || dizi2 == 5||dizi2==6 )
                {
                    Game[i0, j0].Text = "2";
                }//2 veya 4 üretme koşulumuz. 1 2 3 4 5  labellara  2 gelir.  diğerlerine   4 sayısı gelir.
                else { 
                    Game[i0,j0].Text="4";
                }

            }
            renkislemi();
        } 
        public void yukariHareket() {
            bool yukarıKontrol = true;
            bool kazan1 = false;
            bool yeniSayi = false;
            Label[,] Game = { //labelleri diziye atıyoruz
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            for (int i = 0; i < 4;i++ )
            {
                int toplam = 0;
                for (int j = 0; j < 4;j++ )
                {
                    for (int k = j+1; k < 4;k++ )
                    {
                        if(Game[k,i].Text!=""){
                            if(Game[k,i].Text==Game[j,i].Text){
                                kazan1 = true;
                            }
                            break;
                        }
                    }
                    if (Game[j, i].Text == "")
                    {
                        toplam++;//yukarı tarafda aynı sayı degerinden bir text dolu ise bunları topluyor
                    }
                    else {
                        for (int m = j; m >= 0;m-- )
                        {
                            if(Game[m,i].Text==""){
                                yeniSayi = true;
                                break;
                            }
                        }
                        if (j + 1 < 4)
                        {
                            bool extrasayi = true;
                            
                            for (int k = j + 1; k < 4; k++)
                            {
                                if (Game[k, i].Text != "")
                                {
                                    if (Game[j, i].Text == Game[k, i].Text)//toplanacak sayı varsa 
                                    {
                                        yukarıKontrol = false;
                                        lblScore.Text = (int.Parse(lblScore.Text) + int.Parse(Game[ j,i].Text) * 2).ToString();//score ekle
                                      
                                        yeniSayi = true;
                                        extrasayi = false;
                                        Game[j, i].Text = (int.Parse(Game[j, i].Text) * 2).ToString();
                                        if(toplam!=0){
                                            Game[j - toplam, i].Text = Game[j, i].Text;
                                            Game[j, i].Text = "";
                                            
                                        }
                                        Game[k, i].Text = "";
                                       
                                        break;
                                        
                                    }
                                    break;
                                }
                            }
                            if(extrasayi==true && toplam!=0){
                                yukarıKontrol = false;
                                Game[j - toplam, i].Text = Game[j, i].Text;
                                Game[j, i].Text = "";
                                
                            }
                        }
                        else {
                            if(toplam!=0){
                                yukarıKontrol = false;
                                Game[j - toplam, i].Text = Game[j, i].Text;
                                Game[j, i].Text = "";
                                
                            }
                        }
                        
                       
                    }
                }
            }
            if(yukarıKontrol==false && kazan1==true){
                sound.Play();
            }
            if (yukarıKontrol == false && kazan1 == false)
            {
                sound2.Play();
            }
            if(yeniSayi==true){
                sayiUret();
            }
            
        }//YUKARI İŞLEMİ
       
        private void Form1_Load(object sender, EventArgs e)
        {
            sayiUret();
            sayiUret();
            sayiUret();//ilk açılışta kaç tane sayı vericeğimizi burada belirliyoruz
        }// İLK AÇILIŞ
        public void asagiHaraket()
        {
            bool asagiKontrol = true;
            bool kazan1 = false;
            bool yeniSayi = false;
            Label[,] Game = { 
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            for (int i = 0; i < 4; i++)
            {
                int toplam = 0;
                for (int j = 3; j >=0; j--)
                {
                    for (int k = j - 1; k >= 0;k-- )
                    {
                        if(Game[k,i].Text!=""){
                            if(Game[k,i].Text==Game[j,i].Text){
                                kazan1 = true;
                            }
                            break;
                        }
                    }
                    if (Game[j, i].Text == "")
                    {
                        toplam++;
                    }
                    else
                    {
                        for (int m = j+1; m <= 3; m++)
                        {
                            if (Game[m, i].Text == "")
                            {
                                yeniSayi = true;
                                break;
                            }
                        }
                        if (j-1>=0)
                        {
                            bool extrasayi = true;
                            
                            for (int k = j -1 ; k >= 0; k--)
                            {
                                if (Game[k, i].Text != "")
                                {
                                    if (Game[j, i].Text == Game[k, i].Text)
                                    {
                                        asagiKontrol = false;
                                        lblScore.Text = (int.Parse(lblScore.Text) + int.Parse(Game[ j,i].Text) * 2).ToString();
                                       
                                        yeniSayi = true;
                                        extrasayi = false;
                                        Game[j, i].Text = (int.Parse(Game[j, i].Text) * 2).ToString();
                                        if (toplam != 0)
                                        {
                                            Game[j + toplam, i].Text = Game[j, i].Text;
                                            Game[j, i].Text = "";
                                            
                                        }
                                        Game[k, i].Text = "";
                                        break;

                                    }
                                    break;
                                }
                            }
                            if (extrasayi == true && toplam != 0)
                            {
                                asagiKontrol = false;
                                Game[j + toplam, i].Text = Game[j, i].Text;
                                Game[j, i].Text = "";
                                
                            }
                        }
                        else
                        {
                            if (toplam != 0)
                            {
                                asagiKontrol = false;
                                Game[j + toplam, i].Text = Game[j, i].Text;
                                Game[j, i].Text = "";
                                
                            }
                        }


                    }
                }
            }
            if (asagiKontrol == false && kazan1 == true)
            {
                sound.Play();
            }
            if (asagiKontrol == false && kazan1 == false)
            {
                sound2.Play();
            }
            if (yeniSayi == true)
            {
                sayiUret();
            }
        }//ASAĞI İŞLEMİ
        public void solHaraket()
        {
            bool solKontrol=true;
            bool kazan1 = false;
            bool yeniSayi = false;
            Label[,] Game = { 
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            for (int i = 0; i < 4; i++)
            {
                int toplam = 0;
                for (int j =0; j <4; j++)
                {

                    for (int k = j + 1; k < 4;k++ )
                    {
                        if(Game[i,k].Text!=""){
                            if(Game[i,j].Text==Game[i,k].Text){
                                kazan1 = true;
                            }
                            break;
                        }
                    }
                    if (Game[i,j].Text == "")
                    {
                        toplam++;
                    }
                    else
                    {
                        for (int m = j-1; m >= 0; m--)
                        {
                            if (Game[i, m].Text == "")
                            {
                                yeniSayi = true;
                                break;
                            }
                        }
                        if (j + 1 < 4)
                        {
                            bool extrasayi = true;
                            
                            for (int k = j + 1; k <4; k++)
                            {
                                if (Game[i,k].Text != "")
                                {
                                    
                                    if (Game[i,j].Text == Game[i,k].Text)
                                    {
                                        solKontrol = false;
                                        lblScore.Text = (int.Parse(lblScore.Text) + int.Parse(Game[i, j].Text) * 2).ToString();
                                       
                                        yeniSayi = true;
                                        extrasayi = false;
                                        Game[i,j].Text = (int.Parse(Game[i,j].Text) * 2).ToString();
                                        if (toplam != 0)
                                        {
                                            Game[i,j - toplam].Text = Game[i,j].Text;
                                            Game[i,j].Text = "";
                                            
                                        }
                                        Game[i,k].Text = "";
                                        break;

                                    }
                                    break;
                                }
                            }
                            if (extrasayi == true && toplam != 0)
                            {
                                solKontrol = false;
                                Game[i,j - toplam].Text = Game[i,j].Text;
                                Game[i,j].Text = "";
                               
                            }
                        }
                        else
                        {
                            if (toplam != 0)
                            {
                                solKontrol = false;
                                Game[i,j - toplam].Text = Game[i, j].Text;
                                Game[i,j].Text = "";
                                
                            }
                        }


                    }
                }
            }
            if (solKontrol == false && kazan1 == true)
            {
                sound.Play();
            }
            if (solKontrol == false && kazan1 == false)
            {
                sound2.Play();
            }
            if (yeniSayi == true)
            {
                sayiUret();
            }
        }//SOL HARAKET
        public void sagHaraket()
        { //bu fonksiyon sag haraketi kontrol eder.
            bool sagKontrol = true;
            bool kazan1=false;
            bool yeniSayi = false;
            Label[,] Game = { 
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            for (int i = 0; i < 4; i++)
            {
                int toplam = 0;
                for (int j = 3; j >= 0; j--)
                {
                    for (int k = j - 1; k >= 0;k-- )
                    {
                        if(Game[i,k].Text!=""){
                            if(Game[i,k].Text==Game[i,j].Text){
                                kazan1 = true;
                            }
                            break;
                        }
                    }
                    if (Game[i,j].Text == "")
                    {
                        toplam++;
                    }
                    else
                    {
                        for (int m = j + 1; m < 4; m++)
                        {
                            if (Game[i,m].Text == "")
                            {
                                yeniSayi = true;
                                break;
                            }
                        }
                        if (j - 1 >= 0)
                        {
                            bool extrasayi = true;
                            
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (Game[i,k].Text != "")
                                {
                                    
                                    
                                    if (Game[i,j].Text == Game[i,k].Text)
                                    {
                                        sagKontrol = false;
                                        lblScore.Text = (int.Parse(lblScore.Text) + int.Parse(Game[i, j].Text) * 2).ToString();
                                       
                                        yeniSayi = true;
                                        extrasayi = false;
                                        Game[i,j].Text = (int.Parse(Game[i,j].Text) * 2).ToString();
                                        if (toplam != 0)
                                        {
                                            Game[i, j+toplam].Text = Game[ i,j].Text;
                                            Game[i,j].Text = "";
                                            
                                        }
                                        Game[i,k].Text = "";
                                        break;

                                    }
                                    break;
                                }
                            }
                            if (extrasayi == true && toplam != 0)
                            {
                                sagKontrol = false;
                                Game[i,j+ toplam].Text = Game[i,j].Text;
                                Game[ i,j].Text = "";
                                
                            }
                        }
                        else
                        {
                            if (toplam != 0)
                            {
                                sagKontrol = false;
                                Game[ i,j + toplam].Text = Game[ i,j].Text;
                                Game[ i,j].Text = "";
                                
                            }
                        }
                    }
                }
            }
            if (sagKontrol == false && kazan1 == true)
            {
                sound.Play();//sayılar toplanırsa bu ses çıkarır
            }
            if (sagKontrol == false && kazan1 == false)
            {
                sound2.Play();//eger toplanıcak sayı yoksa bu ses çıkar
            }
            if (yeniSayi == true)
            {
                sayiUret();//yeniden sayı üretmemizi saglar
            }
        }// SAG HARAKET
        public bool sayiYaz() {
            Label[,] Game = { //16 text den dizi oluşturuyoruz.
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            for (int i = 0; i < 4;i++ )
            {
                for (int j = 0; j < 4;j++ )
                {
                    if(Game[i,j].Text==""){
                        return false;
                    }
                    for (int k = j+1; k < 4;k++ )
                    {
                        if(Game[i,j].Text!=""){
                            if(Game[i,j].Text==Game[i,k].Text){
                                return false;
                            }
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Game[j, i].Text == "")
                    {
                        return false;
                    }
                    for (int k = j + 1; k < 4; k++)
                    {
                        if (Game[k,i].Text != "")//boş olan text kontrolu
                        {
                            if (Game[j,i].Text == Game[k,i].Text)//boş ise yaz
                            {
                                return false;
                            }
                            break;
                        }
                    }
                }
            }
            return true;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (sayiYaz() == false)//yeni sayı üretme degeri 0 dönmesi lazım tuşları kullanabilmemiz için
            {
                if (e.KeyCode == Keys.Up)
                {
                    yukariHareket();//yukarı ok a basıldıgında çalışır

                }
                if (e.KeyCode == Keys.Down)
                {
                    asagiHaraket();//asagı ok a basıldıgında çalışır
                }
                if (e.KeyCode == Keys.Left)
                {
                    solHaraket();//sol ok a basıldıgında çalışır
                }
                if (e.KeyCode == Keys.Right)
                {
                    sagHaraket();//sag ok a basıldıgında çalışır
                }
               

            }
            else {
               // continueToolStripMenuItem.Visible = false;
               // lblGameOver.Text = "OYUN BİTTİ";//oyun biterse bunu ekrana basıyoruz.
                tekrarOyun = false;
                lblGameOver.Visible = true;
                btnNewGame.Visible = true;
                btnExit.Visible = true;
                btnExit.Enabled = true;
                btnNewGame.Enabled = true;
                this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            }
           
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        { //bu fonksiyon game over oldukdan sonra yeniden baslatmaya yarar. bir alttaki new game ise oyun devam ederken yeniden baslatmak istersek.
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            lblScore.Text = "0";//yeni oyun oldugu için score text i 0
            Label[,] Game = {  //16 tane labeldan dizi oluşturuyoruz
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            lblGameOver.Visible = false;
            btnExit.Visible = false;
            tekrarOyun = true;
            btnNewGame.Visible = false;
            btnNewGame.Enabled = false;
            btnExit.Enabled = false;
            for (int i = 0; i < 4;i++ )//16 tane textimiz oldugu için bunu iç içe 2 tane for döngüsüyle yapıyoruz
            {
                for (int j = 0; j < 4;j++ )
                {
                    Game[i, j].Text = "";
                }
            }
            sayiUret();
            sayiUret();
            sayiUret();// baslangıçta 3 sayı üretiyoruz.
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();//programı kapatmamızı saglayan fonksiyon
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            //label1.Visible = true;
            //continueToolStripMenuItem.Visible = true;
            lblAbout.Visible = false;
            label2.Visible = true;
            lblScore.Visible = true;

            if(tekrarOyun==false){
                this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            }
            tekrarOyun = true;
            lblScore.Text = "0";
            Label[,] Game = { //16 tane labeldan dizi oluşturuyoruz
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            lblGameOver.Visible = false;
            btnExit.Visible = false;
            btnNewGame.Visible = false;
            btnNewGame.Enabled = false;
            btnExit.Enabled = false;
            for (int i = 0; i < 4; i++)//16 tane textimiz oldugu için bunu iç içe 2 tane for döngüsüyle yapıyoruz
            {
                for (int j = 0; j < 4; j++)
                {
                    Game[i, j].Visible = true;
                    Game[i, j].Text = "";//16 tane textden rastgele 3 tane sayı ile dolduruyoruz. i ve j degişkenlerine.
                }
            }
            sayiUret();
            sayiUret();
            sayiUret();// baslangıçta 3 tane sayı vererek baslıyoruz
        }

        //private void continueToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //label1.Visible = true;
        //    lblAbout.Visible = false;
        //    label2.Visible = true;
        //    lblScore.Visible = true;

        //    if (tekrarOyun == false)
        //    {
        //        this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
        //    }
        //    Label[,] Game = {
        //                        {lbl1,lbl2,lbl3,lbl4},
        //                        {lbl5,lbl6,lbl7,lbl8},
        //                        {lbl9,lbl10,lbl11,lbl12},
        //                        {lbl13,lbl14,lbl15,lbl16}
        //                      };
        //    lblGameOver.Visible = false;// burada ki ifadelerde game over labeli, exit butonu false degerler döndürmesini saglar
        //    btnExit.Visible = false;
        //    btnNewGame.Visible = false;
        //    btnNewGame.Enabled = false;
        //    btnExit.Enabled = false;
        //    for (int i = 0; i < 4; i++)
        //    {
        //        for (int j = 0; j < 4; j++)
        //        {
        //            Game[i, j].Visible = true;//i ve j degişkenlerinde bulunan degerler aynen devam ediyor contiune butonuna basıldıgında
        //        }
        //    }
        //}

        //private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //   // MessageBox.Show("Oyun Yön tuşları ile oynanır..");//message box ile bilgi veriyoruz
            
        //}

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();//programın kapanmasını saglar
        }

        private void btnNewGame_MouseHover(object sender, EventArgs e)
        {
            btnNewGame.BackColor = System.Drawing.Color.Green;//buttonun rengini belirler
        }

        private void btnNewGame_MouseLeave(object sender, EventArgs e)
        {
            btnNewGame.BackColor = System.Drawing.Color.Orange;//buttonun rengini belirler
        }

        private void btnExit_MouseHover(object sender, EventArgs e)
        {
            btnExit.BackColor = System.Drawing.Color.Green;//buttonun rengini belirler
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            btnExit.BackColor = System.Drawing.Color.Orange;//buttonun rengini belirler
        }

        private void ptbImage_Click(object sender, EventArgs e)
        {

        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lbl3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblScore_Click(object sender, EventArgs e)
        {

        }

        private void lblGameOver_Click(object sender, EventArgs e)
        {

        }

        //private void bilgiToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //MessageBox.Show("Bu Oyun Yön tuşlarıyla oynanır. Bol Şans..");//mesajbox ile bilgi veriyoruz
        //}

        private void gamePlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
          MessageBox.Show("This game a single player sliding block puzzle game. Use arrow keys to move the tiles.When two tiles with the same number touch, they merge into one. The game's objective is to slide numbered tiles on a grid to combine them to create a tile with the number 2048. 2048 was originally written in JavaScript and CSS by  Italian web developer Gabriele Cirulli.","How To Play",MessageBoxButtons.OK,MessageBoxIcon.Information);
            
        }

        private void ınformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("2016 Term Project by Zülbiye Akdeniz ","Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
            
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
