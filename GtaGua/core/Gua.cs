using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dm;
using System.Threading;
using System.Runtime.CompilerServices;

namespace GtaGua.core
{
    class Gua
    {

        public const String imgPath = ".";

        //图片相似度
        public const double SIMILARITY = 0.7;

        //循环间隔
        public const int DEFAULT_LOOP_INTERVAL = 50;

        private Action<String> logger;

        private dmsoft dm;

        private Thread looper;

        private Boolean isLive;

        private int hwnd;

        private int winPosX;
        private int winPosY;

        //当前执行动作
        private GuaAction curAction;

        public Gua(Action<String> logger)
        {
            this.logger = logger;
        }

        private void checkNeedInit()
        {
            if (dm != null)
            {
                return;
            }

            logger("dm初始化...");
            dm = new dmsoft();
            dm.SetPath(imgPath);
            dm.SetDict(0, "pic/gta.txt");


            hwnd = int.Parse(dm.EnumWindow(0, "Grand Theft Auto V", "", 1 + 4 + 8 + 16));

            Object x1;
            Object x2;
            Object y1;
            Object y2;
            dm.GetWindowRect(hwnd, out x1, out y1, out x2, out y2);

            winPosX = int.Parse(x1.ToString());
            winPosY = int.Parse(y1.ToString());
            logger("winPosX:" + winPosX);
            logger("winPosY:" + winPosY);


            isLive = true;
            looper = new Thread(new ThreadStart(loop));
            looper.Start();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool isRunning()
        {
            return isLive;
        }

        private void loop()
        {
            while (isLive)
            {
                if (null != curAction)
                {
                    curAction.loop();
                }

                //Thread.Sleep(DEFAULT_LOOP_INTERVAL);
            }
        }

        public void delay(int delayMs)
        {
            Thread.Sleep(delayMs);
        }

        public void test()
        {
            //logger("test");
            //CommUtils.sendMessage(1705882, "123测试fsdfdsfdsfsdewrewrewrewrwe是   是是是是是是   是是是是是是   是是是是是是   是是是是是是   是是是是是是   是是是是是是   是是是是是是   是是是是是是   是是是是是是   是是是是是是", logger);
            //logger("写数据测试");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void stop()
        {
            curAction = null;
            isLive = false;

            if (looper != null)
            {
                looper.Abort();
                looper = null;
            }

            if (dm != null)
            {
                dm.UnBindWindow();
                dm = null;
            }

            logger("动作停止");
        }

        public int getWinPosX()
        {
            return winPosX;
        }

        public int getWinPosY()
        {
            return winPosY;
        }

        public void startSpeaker(int speed, int speakCount, String speakText)
        {
            checkNeedInit();
            curAction = new SpeakerAction(this, logger, speed, speakCount, speakText);
        }

        public void sendString(String str)
        {
            CommUtils.sendMessage(hwnd, str);
        }

        public bool findStr(String str,String color, int x1, int y1, int x2, int y2)
        {
            Object x;
            Object y;
            bool result = dm.FindStr(x1, y1, x2, y2, str, color, 0.7, out x, out y) >= 0;
            logger("x:" + x + ", y:" + y);
            return result;
        }

        public void pressKey(int key)
        {
            int status = dm.KeyPress(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="picturePath"></param>
        /// <param name="x1">区域的左上X坐标</param>
        /// <param name="y1">区域的左上Y坐标</param>
        /// <param name="x2">区域的右下X坐标</param>
        /// <param name="y2">区域的右下Y坐标</param>
        /// <returns></returns>
        public bool findPic(String picturePath, int x1, int y1, int x2, int y2)
        {
            Object x;
            Object y;
            return dm.FindPic(x1, y1, x2, y2, picturePath, "000000", SIMILARITY, 0, out x, out y) >= 0;
        }
    }
}
