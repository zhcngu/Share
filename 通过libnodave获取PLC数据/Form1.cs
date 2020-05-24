using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 通过libnodave获取PLC数据
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        libnodave.daveConnection dc;
        libnodave.daveInterface di;
        libnodave.daveOSserialType fds;
        private void button1_Click(object sender, EventArgs e)
        {
        
           fds.rfd = libnodave.openSocket(102, "192.168.186.130");
           fds.wfd = fds.rfd;
            if (fds.rfd > 0)
            {
                di = new libnodave.daveInterface(fds, "IF1", 2,libnodave.daveProtoISOTCP, libnodave.daveSpeed1500k);//libnodave.daveProtoISOTCP
                di.setTimeout(0xbb8);
                dc = new libnodave.daveConnection(di, 0, 2, 0);
                 int  res=dc.connectPLC();
                if (res == 0)
                {
                    MessageBox.Show("链接到PLC");
                }
            }
        }

       

        private void btnRead_Click(object sender, EventArgs e)
        {
            byte[] buff = new byte[8];
            //byte[] buff2 = new byte[3000];
            int x1 = this.dc.readBits(libnodave.daveDB, 100, 110 * 8+1, 1, buff);//  DB100.110.1这个点
            //int x2 = this.dc.readManyBytes(libnodave.daveDB, 100, 344, 20, buff2);//


            //byte[] buff3 = new byte[1];   //0,1.0,0
            //int res = dc.readBits(libnodave.daveFlags, 100, 124*8+5, 1, buff3);

        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            byte[] buff = { 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48 };
            int res=dc.writeBytes(libnodave.daveDB,1450, 64, 11, buff);
            MessageBox.Show(res.ToString());
          
          
        }
    }
}
