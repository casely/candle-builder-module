﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CandleSW
{
    public partial class CandleForm : Form
    {

        CandleParametrs _parametr = new CandleParametrs();
        CandleBuilder _candle = new CandleBuilder();
        private bool _checkDetail = false;


        /// <summary>
        /// Конструктор формы
        /// </summary>
        public CandleForm()
        {
            InitializeComponent();
            label20.Visible = false;
            label18.Visible = false;
            label17.Visible = false;
            textBox9.MaxLength = 10;
        }

        /// <summary>
        /// Обработчик нажатия на кнопку построить
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
                ReadTextBox();
                if (_checkDetail == true)
                {
                    if (_parametr.ExistDetail)
                    {
                        _candle.SwApp.CloseDoc("Свеча");
                    }
                    _candle.BuildCandle(_parametr);
                }
        }

        /// <summary>
        /// Метод считывающий данные с полей
        /// </summary>
        private void ReadTextBox()
        {
            try
            {
                _parametr.CarvingLength = Convert.ToDouble(textBox5.Text);
                _parametr.NutLength = Convert.ToDouble(textBox3.Text);
                _parametr.IsolatorLength = Convert.ToDouble(textBox4.Text);
                _parametr.PlinthLength = Convert.ToDouble(textBox2.Text);
                
                _parametr.TextEtching = Convert.ToString(textBox9.Text);
                _checkDetail = true;
                    
            }
            catch (Exception e)
            {
                DialogResult res = MessageBox.Show(e.Message,"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (res == DialogResult.OK)
                    _checkDetail = false;
            }
        }

        /// <summary>
        /// Обработчик выбора
        /// </summary>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                groupBox1.Size = new System.Drawing.Size(215, 105);
                _parametr.ExistHead = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                groupBox1.Size = new System.Drawing.Size(215, 90);
                _parametr.ExistHead = false;
            }
        }

        /// <summary>
        /// Обработчик кнопки по-умолчанию
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text= "8";
            textBox2.Text = "30";
            textBox3.Text = "9";
            textBox4.Text = "8";
            textBox5.Text = "12";
            comboBox3.Text = "2";
            comboBox1.Text = "16";
            comboBox2.Text = "M12x1.25";
            textBox9.Text = "DENSO";
            radioButton1.Checked = true;
        }

        /// <summary>
        /// Выбор размера гайки
        /// </summary>
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    _parametr.NutSize = Convert.ToDouble(comboBox1.Text);
                    _parametr.ChamferRadius = 0.008;
                    break;
                case 1:
                    _parametr.NutSize = Convert.ToDouble(comboBox1.Text);
                    _parametr.ChamferRadius = 0.0095;
                    break;
                case 2:
                    _parametr.NutSize = Convert.ToDouble(comboBox1.Text);
                    _parametr.ChamferRadius = 0.01;
                    break;
            }
        }

        /// <summary>
        /// Обработка нажатия на закрытие формы
        /// </summary>
        private void CandleForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (DialogResult.Yes == MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            {
                _candle.SwApp.ExitApp();
                _candle.SwApp = null;
                Process[] processes = Process.GetProcessesByName("SLDWORKS");
                foreach (Process process in processes)
                {
                    process.Kill();
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Выбор типа резьбы
        /// </summary>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    _parametr.CarvingRadius = 5;
                    _parametr.PitchSize = 1.25;
                    break;
                case 1:
                    _parametr.CarvingRadius = 7;
                    _parametr.PitchSize = 1.5;
                    break;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    label20.Visible = true;
                    label18.Visible = true;
                    label17.Visible = true;
                    _parametr.ElectrodeLength = 1;
                    label20.Text = "3";
                    break;
                case 1:
                    label20.Visible = true;
                    label18.Visible = true;
                    label17.Visible = true;
                    _parametr.ElectrodeLength = 2;
                    label20.Text = "2";
                    break;
                case 2:
                    label20.Visible = true;
                    label18.Visible = true;
                    label17.Visible = true;
                    _parametr.ElectrodeLength = 3;
                    label20.Text = "1";
                    break;
            }
        }
    }
}
