using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI
{
    public partial class Waiting : UserControl
    {
        /// <summary>
        /// �ȴ���ʾ�� , ���߳���ʾ��������ô���ʾ����߳�����������ͬ����������״̬ 
        /// </summary>
        public Waiting()
        {

            InitializeComponent();
        }

        /// <summary>
        /// �ȴ���ʾ��, ���߳���ʾ��������ô���ʾ����߳�����������ͬ����������״̬ 
        /// </summary>
        /// <param name="strNotice">��ʾ����</param>
        public Waiting(string strNotice)
        {
            InitializeComponent();
            SetNotice(strNotice);
        }

        /// <summary>
        /// ������ʾ����
        /// </summary>
        /// <param name="strNotice">��ʾ����</param>
        public void SetNotice(string strNotice)
        {
            Lab_Notice.Text = strNotice;
        }
    }
}
