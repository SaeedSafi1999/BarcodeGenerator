using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

public class BarcodeGenerator
{
    private static readonly string[] Code128 = new string[]
    {
        "11011001100", "11001101100", "11001100110", "10010011000", "10010001100", "10001001100",
        "10011001000", "10011000100", "10001100100", "11001001000", "11001000100", "11000100100",
        "10110011100", "10011011100", "10011001110", "10111001100", "10011101100", "10011100110",
        "11001110010", "11001011100", "11001001110", "11011100100", "11001110100", "11101101110",
        "11101001100", "11100101100", "11100100110", "11101100100", "11100110100", "11100110010",
        "11011011000", "11011000110", "11000110110", "10100011000", "10001011000", "10001000110",
        "10110001000", "10001101000", "10001100010", "11010001000", "11000101000", "11000100010",
        "10110111000", "10110001110", "10001101110", "10111011000", "10111000110", "10001110110",
        "11101110110", "11010001110", "11000101110", "11011101000", "11011100010", "11011101110",
        "11101011000", "11101000110", "11100010110", "11101101000", "11101100010", "11100011010",
        "11101111010", "11001000010", "11110001010", "10100110000", "10100001100", "10010110000",
        "10010000110", "10000101100", "10000100110", "10110010000", "10110000100", "10011010000",
        "10011000010", "10000110100", "10000110010", "11000010010", "11001010000", "11110111010",
        "11000010100", "10001111010", "10100111100", "10010111100", "10010011110", "10111100100",
        "10011110100", "10011110010", "11110100100", "11110010100", "11110010010", "11011011110",
        "11011110110", "11110110110", "10101111000", "10100011110", "10001011110", "10111101000",
        "10111100010", "11110101000", "11110100010", "10111011110", "10111101110", "11101011110",
        "11110101110", "11010000100", "11010010000", "11010011100", "1100011101011", "11010100000"
    };

    public static Bitmap GenerateBarcode(string input, int width = 300, int height = 100)
    {
        string encodedString = EncodeCode128(input);

        Bitmap bitmap = new Bitmap(width, height);
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            graphics.Clear(Color.White);


            int x = 10;
            int y = 10;
            int barWidth = 2;
            int barcodeHeight = height - 40;


            foreach (char c in encodedString)
            {
                if (c == '1')
                {
                    graphics.FillRectangle(Brushes.Black, x, y, barWidth, barcodeHeight);
                }
                x += barWidth;
            }


            Font font = new Font("Arial", 10);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            graphics.DrawString(input, font, Brushes.Black, new PointF(width / 2, barcodeHeight + 20), stringFormat);
        }

        return bitmap;
    }

    private static string EncodeCode128(string input)
    {
        StringBuilder result = new StringBuilder();


        result.Append(Code128[104]);

        foreach (char c in input)
        {
            int index = (int)c - 32;
            result.Append(Code128[index]);
        }


        int checksum = 104;
        for (int i = 0; i < input.Length; i++)
        {
            checksum += (i + 1) * ((int)input[i] - 32);
        }
        checksum %= 103;
        result.Append(Code128[checksum]);

        result.Append(Code128[106]);

        return result.ToString();
    }

    public static void Main(string[] args)
    {

        string uniqueId = Guid.NewGuid().ToString().Substring(0, 8);

        Bitmap barcode = GenerateBarcode(uniqueId);
        var res = EncodeCode128(uniqueId);
        Console.WriteLine(res);
        barcode.Save("barcode.png", ImageFormat.Png);
        Console.WriteLine("Barcode saved as barcode.png");
    }
}
