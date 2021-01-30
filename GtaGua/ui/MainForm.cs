using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GtaGua.core;
using GtaGua.ui;

namespace GtaGua
{
    public partial class MainForm : Form
    {

        //窗口消息-热键
        private const int WM_HOTKEY = 0x312;
        //窗口消息-创建
        private const int WM_CREATE = 0x1;
        //窗口消息-销毁
        private const int WM_DESTROY = 0x2;
        //开始
        private const int HOT_KEY_EVENT_START = 0x3572;
        //结束
        private const int HOT_KEY_EVENT_END = 0x3573;


        private Action<String> logger;
        private Action<String> asyLogger;

        private Gua gua;

        public MainForm()
        {
            InitializeComponent();
            logger = new Action<String>(msg => log(msg));
            asyLogger = new Action<String>(msg => printLogMsg(msg));
            gua = new Gua(asyLogger);
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            startSpeaker();
        }

        private int? getIntFormTextBox(TextBox textBox)
        {
            try
            {
                return int.Parse(textBox.Text);
            }
            catch
            {
                return null;
            }
        }


        private void printLogMsg(String msg)
        {
            //如果调用控件的线程和创建创建控件的线程不是同一个则为True
            if (this.tipsTextBox.InvokeRequired)
            {
                while (!this.tipsTextBox.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.tipsTextBox.Disposing || this.tipsTextBox.IsDisposed)
                        return;
                }

                this.tipsTextBox.Invoke(logger, msg);
            }
            else
            {
                log(msg);
            }
        }

        private void log(String msg)
        {
            if (logTextBox.Text.Length > 20000)
            {
                logTextBox.Text = logTextBox.Text.Substring(15000);
            }

            logTextBox.AppendText(msg + Environment.NewLine);
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            stopGua();
        }

        private void startSpeaker() {
            int? speed = getIntFormTextBox(speedTextBox);
            if (null == speed)
            {
                MessageBox.Show("输入速度不正确");
                return;
            }

            int? speakCount = getIntFormTextBox(speakCountTextBox);
            if (null == speakCount)
            {
                MessageBox.Show("输入喊话次数不正确");
                return;
            }

            String chatText = tipsTextBox.Text;
            if (String.IsNullOrEmpty(chatText))
            {
                MessageBox.Show("请输入喊话内容");
                return;
            }

            try
            {
                gua.startSpeaker(speed.Value, speakCount.Value, chatText);
                //gua.test();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void stopGua()
        {
            gua.stop();
        }


        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_HOTKEY: //窗口消息-热键ID

                    switch (m.WParam.ToInt32())
                    {
                        case HOT_KEY_EVENT_START:
                            startSpeaker();
                            break;
                        case HOT_KEY_EVENT_END:
                            stopGua();
                            break;
                        default:
                            break;
                    }

                    break;

                case WM_CREATE:
                    //窗口消息-创建
                    AppHotKey.RegKey(Handle, HOT_KEY_EVENT_START, AppHotKey.KeyModifiers.None, Keys.F8);
                    AppHotKey.RegKey(Handle, HOT_KEY_EVENT_END, AppHotKey.KeyModifiers.None, Keys.F10);
                    break;
                case WM_DESTROY:
                    //窗口消息-销毁
                    //销毁热键
                    AppHotKey.UnRegKey(Handle, HOT_KEY_EVENT_START);
                    AppHotKey.UnRegKey(Handle, HOT_KEY_EVENT_END);
                    break;
                default:
                    break;
            }

        }

    }
}
