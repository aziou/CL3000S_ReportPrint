using System;
using System.Collections.Generic;
using System.Text;
//using CLDC_DataCore.Model.DnbModel;
using CLDC_Comm.Enum;
using CLDC_Comm.Command;
using CLDC_CTNProtocol;

namespace CLDC_Comm.Command
{
    class ServerControl:CTNPCommand
	{
			//�����������ֶ�
			//public DnbGroupInfo DnbData=null;		//���ܱ���Ϣ���ϣ��ɿͻ��˷���,�������ӷ����л�ȡ��ǰ������ʲôλ����
			public bool ControlOK=false;				//���������أ��ͻ����յ�ȷ�Ϻ������ܷ��������ƣ� 
			public ServerControl()	//����Ϊ��Ӧ��������
			{
                this.ControlOK = false;
			}
	}

}
