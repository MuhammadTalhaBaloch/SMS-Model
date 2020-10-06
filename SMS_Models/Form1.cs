using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMS_Models
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private Image DrawTextEmployee(Color backColor)
        {

            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            //   SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            //  img = new Bitmap((int)textSize.Width, (int)textSize.Height);
            img = new Bitmap(250, 370);


            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);


            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(fontFamily, 16, FontStyle.Bold, GraphicsUnit.Pixel);
            //create a brush for the text
            Brush textBrush = new SolidBrush(Color.Red);

            //drawing.DrawString("ID CARD", font, textBrush, 20, 10);
            //drawing.DrawString("|", font, textBrush, 100, 5);
            //drawing.DrawString("|", font, textBrush, 100, 20);
            //  drawing.DrawString("|", font, textBrush, 100, 16);
            Image src = new Bitmap("logo.png");

            drawing.DrawImage(src, new Rectangle(5, 4, 45, 45));

            font = new Font(fontFamily, 12, FontStyle.Bold, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Black);
            drawing.DrawString("Tapas Logistics Limited", font, textBrush, 60, 10);


            font = new Font(fontFamily, 8, FontStyle.Regular, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Black);
            drawing.DrawString("5th Streat Korangi Two Number,Karachi", font, textBrush, 60, 25);

            src = new Bitmap("t1.jpg");
            drawing.DrawImage(src, new Rectangle(70, 50, 100, 110));

            font = new Font(fontFamily, 16, FontStyle.Bold, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Black);
            drawing.DrawString("Muhammad Talha", font, textBrush, 50, 170);

            font = new Font(fontFamily, 11, FontStyle.Regular, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.DimGray);
            drawing.DrawString("Operation Manager", font, textBrush, 70, 190);

            font = new Font(fontFamily, 11, FontStyle.Regular, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Red);
            drawing.DrawString("_________________________________", font, textBrush, 20, 198);

            font = new Font(fontFamily, 11, FontStyle.Bold, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.DimGray);
            drawing.DrawString("45628-4641578419-5", font, textBrush, 30, 215);
            drawing.DrawString("Jan 5th 1990", font, textBrush, 30, 235);
            drawing.DrawString("+923242425025    +923245497165", font, textBrush, 30, 255);
            drawing.DrawString("Residentional Address", font, textBrush, 30, 275);
            drawing.DrawString("Postal Address", font, textBrush, 30, 295);
            drawing.DrawString("Valid Till: Sep 50th 2010", font, textBrush, 30, 315);

            FontFamily fontFamily1 = new FontFamily("Arial Black");
            font = new Font(fontFamily1, 11, FontStyle.Bold, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.DimGray);
            drawing.DrawString("Male", font, textBrush, 150, 235);
            font = new Font(fontFamily, 11, FontStyle.Regular, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Red);
            drawing.DrawString("_________________________________________", font, textBrush, 0, 320);

            font = new Font(fontFamily, 16, FontStyle.Bold, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Red);
            drawing.DrawString("ID: E3425", font, textBrush, 70, 340);

            font = new Font(fontFamily, 11, FontStyle.Regular, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Red);
            drawing.DrawString("_________________________________________", font, textBrush, 0, 350);



            //font = new Font(fontFamily, 10, FontStyle.Regular, GraphicsUnit.Pixel);
            //drawing.DrawString("writes something", font, textBrush, 120, 20);
            //drawing.DrawString("writes something", font, textBrush, 120, 30);
            //drawing.DrawString("writes something", font, textBrush, 120, 40);
            ////drawing.DrawString("2", font, textBrush, 0, 20);
            //Image src = new Bitmap("logo.png");

            //drawing.DrawImage(src, new Rectangle(0, 0, 100, 100));

            //src = new Bitmap("t1.jpg");
            //Image src1 = CropImage(src);
            //drawing.DrawImage(src1, new Rectangle(120, 90, 100, 100));

            //textBrush = new SolidBrush(Color.Blue);
            //font = new Font(fontFamily, 16, FontStyle.Bold, GraphicsUnit.Pixel);
            //drawing.DrawString("___________________________________", font, textBrush, 0, 180);
            //drawing.DrawString("Muhammad Talha", font, textBrush, 90, 200);
            //drawing.DrawString("IVA", font, textBrush, 150, 220);
            //drawing.DrawString("___________________________________", font, textBrush, 0, 225);
            //textBrush = new SolidBrush(Color.Black);
            //font = new Font(fontFamily, 12, FontStyle.Regular, GraphicsUnit.Pixel);
            //drawing.DrawString("D.O.B: 1999/09/09", font, textBrush, 5, 250);
            //drawing.DrawString("Blood Group: O+", font, textBrush, 200, 250);
            //drawing.DrawString("(F): FatherName", font, textBrush, 5, 270);
            //drawing.DrawString("(M): MotherName", font, textBrush, 5, 290);
            //drawing.DrawString("Address: ", font, textBrush, 5, 310);
            //drawing.DrawString("Malir,Karachi", font, textBrush, 110, 330);
            //drawing.DrawString("Ph: 0900-78601", font, textBrush, 5, 360);
            //drawing.DrawString("Principal:__________", font, textBrush, 200, 360);
            ////  drawing.DrawImage(src1, 50, 50,100,100);

            ////     drawing.DrawImage(img, 0, 40, 100, 100);


            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;

        }
    }
}
