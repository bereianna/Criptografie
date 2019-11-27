using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace handbookWFA
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
        private static string Readfile()
        {
            TextReader s = new StreamReader("textCLAR.txt");
            string b = s.ReadToEnd();
            return b;
        }
        public void EncryptAesManaged(string raw)
        {
            try
            {   
                using (AesManaged aes = new AesManaged())
                {
                    if (r1.Checked == true)
                        aes.Mode = CipherMode.CBC;
                    if (r2.Checked == true)
                        aes.Mode = CipherMode.CBC;
                    if (r3.Checked == true)
                        aes.Mode = CipherMode.CBC;
                    if (r4.Checked == true)
                        aes.Mode = CipherMode.CBC;
                    if (r5.Checked == true)
                        aes.Mode = CipherMode.CBC;

                    if (r6.Checked == true)
                        aes.Padding= PaddingMode.ANSIX923;
                    if (r7.Checked == true)
                        aes.Padding = PaddingMode.ISO10126;
                    if (r8.Checked == true)
                        aes.Padding = PaddingMode.None;
                    if (r9.Checked == true)
                        aes.Padding = PaddingMode.PKCS7;
                    if (r10.Checked == true)
                        aes.Padding = PaddingMode.Zeros;

                    textBox1.Text = raw;
                    byte[] encrypted = Encrypt(raw, aes.Key, aes.IV);
                    textBox2.Text=System.Text.Encoding.UTF8.GetString(encrypted);
                    string decrypted = Decrypt(encrypted, aes.Key, aes.IV);
                    textBox3.Text = decrypted;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;  
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream())
                {  
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {  
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            } 
            return encrypted;
        }
        static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            using (AesManaged aes = new AesManaged())
            {  
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {  using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            string c = Readfile();
            EncryptAesManaged(c);
        }
    }
}
