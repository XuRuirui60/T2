using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        SerialPort BT = new SerialPort();
        public Form1()
        {
            InitializeComponent();
            comBoxSet();//加入偵測到的port口到combox裡面

            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            label2.BackColor = Color.Red;
            label2.Text = "Disnnected";
            SerialPort serialPort = new SerialPort();

            comboBox1.Click += new EventHandler(comboBox1_Click); // 綁定comboBox1的Click事件
        }
        public void comBoxSet()//加入偵測到的port口到combox裡面
        {
            comboBox1.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (BT.IsOpen)
            {
                //串口已經處於打開狀態
                BT.Close();    //關閉串口
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                label2.BackColor = Color.Red;
                label2.Text = "Disnnected";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(comboBox1.Text.Trim()))
            {
                BT.PortName = comboBox1.Text;
                BT.BaudRate = 38400;//藍芽預設連接速度
                BT.WriteTimeout = 3000;//預設連接時間3秒
                BT.Open();//開啟藍芽Tx com port
                label2.BackColor = Color.Green;
                label2.Text = "Connected";
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            textBox1.Text = $"{DateTime.Now.ToString("tt hh:mm:ss")}";

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int inputNumber;
            if (textBox2.Text.Length != 3) // 如果輸入的數字不是三位數，彈出提示視窗
            {
                MessageBox.Show("Not 3 Digit Dec Format"); // 顯示提示視窗
                textBox2.Text = ""; // 清空輸入框
            }
            if (!int.TryParse(textBox2.Text, out inputNumber) || inputNumber < 100 || inputNumber > 255)
            {
                textBox2.Text = ""; // 清空輸入框
            }
            else
            {
                BT.Write("E" + textBox2.Text); // 傳送正確的數字
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comBoxSet(); // 每次點擊時更新端口
        }
    }
}
