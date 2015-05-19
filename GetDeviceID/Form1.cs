using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace GetDeviceID
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("coredll.dll")]
        private extern static int GetDeviceUniqueID([In, Out] byte[] appdata,
                                                    int cbApplictionData,
                                                    int dwDeviceIDVersion,
                                                    [In, Out] byte[] deviceIDOuput,
                                                    out uint pcbDeviceIDOutput);
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] buffer = GetDeviceID("DARMSYZX");  
            StringBuilder sb = new StringBuilder();  
            for (int x = 0; x < buffer.Length; x++)  
            {  
                //sb.Append('{');  
                sb.Append(string.Format("{0:x2}", buffer[x]));  
                //sb.Append("} ");  
            }
            string tempCode = sb.ToString();
            string strCode = tempCode.Substring(0, 16).ToUpper();
            string str1 = strCode.Substring(0, 4);
            string str2 = strCode.Substring(4, 4);
            string str3 = strCode.Substring(8, 4);
            string str4 = strCode.Substring(12, 4);
            textBox1.Text = str1 + "-" + str2 + "-" + str3 + "-" + str4;  

        }

        private byte[] GetDeviceID(string AppString)  
        {  
            // Call the GetDeviceUniqueID  
            byte[] AppData = new byte[AppString.Length];  
            for (int count = 0; count < AppString.Length; count++)  
            AppData[count] = (byte)AppString[count];  
            int appDataSize = AppData.Length;  
            byte[] DeviceOutput = new byte[20];             
            uint SizeOut = 20;  
            GetDeviceUniqueID(AppData, appDataSize, 1, DeviceOutput, out SizeOut);  
            return DeviceOutput;  
       }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }  

        
    }
}