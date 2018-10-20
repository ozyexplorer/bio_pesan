using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BarcodeLib;
using System.IO;
using System.Net;


namespace Biofarma.NFC.Business
{
    public class Code
    {
        
        public static string CreateCode(int seq, int codeLength)
        {
            string allChar = " 0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < codeLength; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(36);

                if (temp != -1 && temp == t)
                {
                    return CreateCode(seq,codeLength);
                }

                temp = t;

                randomCode += allCharArray[t];
            }

            return randomCode;
        }

        public static Image CreateBarCode(string codeText, int width, int height)
        {
            Barcode barcode = new Barcode()
            {
                IncludeLabel = false, //Can Be True to View
                Alignment = AlignmentPositions.CENTER,
                Width = width,
                Height = height,
                RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
            };

            Image image = barcode.Encode(TYPE.CODE128B, codeText);

            return image;
        }

        
    }
}

