using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtaGua.core
{
    class SpeakerAction : GuaAction
    {
        private const int STATE_INIT = 0;
        private const int STATE_OPEN_CHAT_WIN = 1;
        private const int STATE_INPUT_TEXT = 3;
        private const int STATE_SEND = 4;
        private const int STATE_START_CHG_SERVER = 5;
        private const int STATE_WAIT_CHG_SERVER = 6;
        private const int STATE_DO_LOGIN = 7;


        private const String IMG_CHAT_WIN = "pic/winChat.bmp";
        private const String IMG_OP_MARK = "pic/opMark.bmp";
        private const String IMG_SELECT_ZX = "pic/select_zaixian.bmp";
        private const String IMG_ZX_LIST = "pic/zaixian_list.bmp";

        private const String IMG_P_UI = "pic/p_ui.bmp";
        private const String IMG_CHG_SEVER_BTN_SELECT = "pic/chg_server_btn.bmp";
        private const String IMG_CONFIRM_CHG_UI = "pic/confirm_Chg.bmp";
        private const String IMG_ERR_MSG = "pic/err_notify.bmp";
        private const String IMG_OFF_LINE_MAP = "pic/off_Line_map.bmp";
        private const String IMG_GO_ONLINE = "pic/go_online.bmp";
        private const String IMG_ENTER = "pic/enter.bmp";
        private const String IMG_LOGIN_JIANXUN = "pic/jianxun.bmp";
        private const String IMG_LOGIN_TONGJI = "pic/tongji.bmp";
        private const String IMG_LOGIN_SHEZHI = "pic/shezhi.bmp";
        private const String IMG_LOGIN_YOUXI = "pic/youxi.bmp";
        private const String IMG_LOGIN_ZAIXIAN = "pic/zaixian.bmp";
        private const String IMG_LOGIN_CONFIRM = "pic/login_confirm.bmp";



        private int speed;
        private int speakCount;
        private String speakText;
        //当前状态
        private int state;

        public SpeakerAction(Gua gua, Action<String> logger, int speed, int speakCount, String speakText)
            : base(gua, logger)
        {
            this.speed = speed;
            this.speakCount = speakCount;
            this.speakText = speakText;
            log("启动自动喊话");
            state = STATE_WAIT_CHG_SERVER;
        }

        protected override bool update()
        {
            if (initOp() || sendChatText() || changeSever() || doLogin() || waitChgServer())
            {
                return true;
            }
            return false;
        }

        private bool doLogin()
        {
            if (state != STATE_DO_LOGIN)
            {
                return false;
            }


            log("开始上线...");
            log("打开选项界面...");
            while (isRunning())
            {
                //按esc
                gua.pressKey(27);
                gua.delay(3000);
                if (!gua.findPic(IMG_OP_MARK, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    log("重试打开选项界面");
                    continue;
                }

                int chgTagState = chgTab("切换简讯选项卡", IMG_LOGIN_JIANXUN, null, IMG_LOGIN_TONGJI);
                if (chgTagState < 0)
                {
                    log("重试打开选项界面");
                    continue;
                }

                if (chgTagState == 0)
                {
                    chgTagState = chgTab("切换统计选项卡", IMG_LOGIN_TONGJI, IMG_LOGIN_JIANXUN, IMG_LOGIN_SHEZHI);
                    if (chgTagState < 0)
                    {
                        log("重试打开选项界面");
                        continue;
                    }
                }
                else
                {
                    chgTagState = 0;
                }

                if (chgTagState == 0)
                {
                    chgTagState = chgTab("切换设置选项卡", IMG_LOGIN_SHEZHI, IMG_LOGIN_TONGJI, IMG_LOGIN_YOUXI);
                    if (chgTagState < 0)
                    {
                        log("重试打开选项界面");
                        continue;
                    }
                }
                else
                {
                    chgTagState = 0;
                }

                if (!chgYouXiTabByStr(IMG_LOGIN_SHEZHI))
                {
                    log("重试打开选项界面");
                    continue;
                }

                if (!chgZaixianTabByStr())
                {
                    log("重试打开选项界面");
                    continue;
                }


                gua.delay(1000);
                log("进入在线选项卡");
                //按回车
                gua.pressKey(13);
                gua.delay(3000);
                log("切换到进入在线模式");
                //按W
                gua.pressKey(87);
                gua.delay(3000);
                if (!gua.findPic(IMG_GO_ONLINE, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    log("切换到进入在线模式失败，重试打开选项界面");
                    //按esc
                    gua.pressKey(27);
                    gua.delay(2000);
                    continue;
                }

                log("点击进入在线模式");
                //按回车
                gua.pressKey(13);
                gua.delay(3000);
                if (!gua.findPic(IMG_ENTER, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    log("点击进入在线模式失败，重试打开选项界面");
                    //按esc
                    gua.pressKey(27);
                    gua.delay(2000);
                    continue;
                }


                //按回车
                gua.pressKey(13);
                gua.delay(3000);
                if (!clickEnter())
                {
                    log("点击进入失败，重试打开选项界面");
                    //按esc
                    gua.pressKey(27);
                    gua.delay(2000);
                    continue;
                }

                if (!confirmChgWarPlace())
                {
                    log("确认进入失败，重试打开选项界面");
                    //按esc
                    gua.pressKey(27);
                    gua.delay(2000);
                    continue;
                }

                break;
            }

            log("切换上线操作完成");
            state = STATE_WAIT_CHG_SERVER;
            return false;
        }

        private bool initOp()
        {
            if (state != STATE_INIT)
            {
                return false;
            }

            gua.delay(1000);
            log("执行初始操作...");

            bool isOffLine = false;
            while (isRunning() && gua.findPic(IMG_ERR_MSG, getWinPosX(), getWinPosY(), 1500, 1500))
            {
                log("关闭注意界面");
                //按esc
                gua.pressKey(27);
                gua.delay(1000);
                isOffLine = true;
            }
            if (isOffLine)
            {
                state = STATE_WAIT_CHG_SERVER;
                return false;
            }

            for (int i = 0; i < 3; i++)
            {
                while (isRunning() && gua.findPic(IMG_P_UI, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    log("关闭选项界面");
                    //按esc
                    gua.pressKey(27);
                    gua.delay(1000);
                }

                while (isRunning() && gua.findPic(IMG_OP_MARK, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    log("关闭选项界面");
                    //按esc
                    gua.pressKey(27);
                    gua.delay(1000);
                }

                gua.delay(1000);
            }


            state = STATE_SEND;
            return true;
        }


        /*
         * 返回0为停留在目标tab
         * 返回1为停留在下一个tab
         * 返回-1为错误
         */
        private int chgTab(String opName, String imgPath, String preImg, String nextImg)
        {
            log(opName);
            for (int i = 0; i < 5; ++i)
            {
                //按D
                gua.pressKey(68);
                gua.delay(4000);
                if (gua.findPic(imgPath, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    return 0;
                }

                if (nextImg != null && gua.findPic(nextImg, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    return 1;
                }

                if (preImg == null)
                {
                    log("重试" + opName);
                    continue;
                }

                if (gua.findPic(preImg, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    log("重试" + opName);
                    continue;
                }

                break;
            }

            return -1;
        }


        /*
         * 返回0为停留在目标tab
         * 返回1为停留在下一个tab
         * 返回-1为错误
         */
        private bool chgZaixianTabByStr()
        {
            int winX = getWinPosX();
            int winY = getWinPosY();

            log("切换在线选项卡");
            for (int i = 0; i < 5; ++i)
            {
                //按D
                gua.pressKey(68);
                gua.delay(4000);
                if (gua.findStr("在线", "0e0e0e-7e7e7e", winX + 591, winY + 76, winX + 779, winY + 211))
                {
                    return true;
                }

                break;
            }

            return false;
        }


        /*
         * 返回0为停留在目标tab
         * 返回1为停留在下一个tab
         * 返回-1为错误
         */
        private bool chgYouXiTabByStr(String preImg)
        {
            int winX = getWinPosX();
            int winY = getWinPosY();
            

            log("切换游戏选项卡");
            for (int i = 0; i < 5; ++i)
            {
                //按D
                gua.pressKey(68);
                gua.delay(4000);
                if (gua.findStr("游戏", "0e0e0e-7e7e7e", winX + 450, winY + 50, winX + 730, winY + 211))
                {
                    return true;
                }


                if (preImg == null)
                {
                    log("重试切换游戏选项卡");
                    continue;
                }

                if (gua.findPic(preImg, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    log("重试切换游戏选项卡");
                    continue;
                }

                break;
            }

            return false;
        }

        private void resetStatus()
        {
            state = STATE_INIT;
        }

        private bool waitChgServer()
        {
            if (state != STATE_WAIT_CHG_SERVER)
            {
                return false;
            }

            while (isRunning())
            {
                log("切换服务器中...");
                //按esc
                gua.pressKey(27);
                gua.delay(2000);

                if (gua.findPic(IMG_OFF_LINE_MAP, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    log("切换服务器完成(下线状态)");
                    //按esc
                    gua.pressKey(27);
                    gua.delay(2000);
                    state = STATE_DO_LOGIN;
                    return false;
                }

                if (gua.findPic(IMG_OP_MARK, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    log("切换服务器完成");
                    //按esc
                    gua.pressKey(27);
                    gua.delay(2000);
                    state = STATE_INIT;
                    return false;
                }


            }

            return true;
        }

        private bool changeSever()
        {
            if (state != STATE_START_CHG_SERVER)
            {
                return false;
            }

            log("开始切换服务器...");
            log("打开选项界面...");
            while (isRunning())
            {
                //按esc
                gua.pressKey(27);
                gua.delay(2000);
                if (!gua.findPic(IMG_OP_MARK, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    log("重试打开选项界面");
                    continue;
                }

                log("切换在线选项卡");
                //按D
                gua.pressKey(68);
                gua.delay(2000);
                if (!gua.findPic(IMG_SELECT_ZX, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    log("切换在线选项卡失败，重试打开选项界面");
                    //按esc
                    gua.pressKey(27);
                    gua.delay(2000);
                    continue;
                }

                log("进入在线选项卡");
                //按回车
                gua.pressKey(13);
                gua.delay(2000);
                if (!gua.findPic(IMG_ZX_LIST, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    log("进入在线选项卡失败，重试打开选项界面");
                    //按esc
                    gua.pressKey(27);
                    gua.delay(2000);
                    continue;
                }

                log("切换到切换战场");
                //按W
                gua.pressKey(87);
                gua.delay(2000);
                //按W
                gua.pressKey(87);
                gua.delay(2000);
                //按W
                gua.pressKey(87);
                gua.delay(2000);
                if (!gua.findPic(IMG_CHG_SEVER_BTN_SELECT, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    log("切换到切换战场失败，重试打开选项界面");
                    //按esc
                    gua.pressKey(27);
                    gua.delay(2000);
                    continue;
                }

                if (!clickChgWarPlace())
                {
                    log("点击切换战场失败，重试打开选项界面");
                    //按esc
                    gua.pressKey(27);
                    gua.delay(2000);
                    continue;
                }

                if (!confirmChgWarPlace())
                {
                    log("确认切换战场失败，重试打开选项界面");
                    //按esc
                    gua.pressKey(27);
                    gua.delay(2000);
                    continue;
                }

                break;
            }

            log("切换服务器操作完成");
            state = STATE_WAIT_CHG_SERVER;
            return false;
        }

        private bool confirmChgWarPlace()
        {
            log("确认切换战场");
            while (isRunning())
            {
                //按回车
                gua.pressKey(13);
                gua.delay(2000);
                if (gua.findPic(IMG_CONFIRM_CHG_UI, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    log("重试确认切换战场");
                    continue;
                }

                return true;
            }

            return false;
        }


        private bool clickEnter()
        {
            log("点击进入");
            while (isRunning())
            {
                //按回车
                gua.pressKey(13);
                gua.delay(2000);
                if (gua.findPic(IMG_LOGIN_CONFIRM, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    return true;
                }

                log("重试点击进入");
            }

            return false;
        }

        private bool clickChgWarPlace()
        {
            log("点击切换战场");
            while (isRunning())
            {
                //按回车
                gua.pressKey(13);
                gua.delay(2000);
                if (gua.findPic(IMG_CONFIRM_CHG_UI, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    return true;
                }

                if (!gua.findPic(IMG_CHG_SEVER_BTN_SELECT, getWinPosX(), getWinPosY(), 1500, 1500))
                {
                    return false;
                }

                log("重试点击切换战场");
            }

            return false;
        }

        private bool sendChatText()
        {
            if (state != STATE_SEND)
            {
                return false;
            }

            int countSpeakCount = 0;
            while (countSpeakCount < speakCount)
            {
                ++countSpeakCount;
                log("开始喊话，第" + countSpeakCount + "次");

                log("打开聊天框...");
                int tryOpenChatWinCount = 0;
                while (isRunning())
                {
                    ++tryOpenChatWinCount;
                    if (tryOpenChatWinCount > 10)
                    {
                        log("重试打开聊天框失败，重置状态");
                        //重置状态
                        resetStatus();
                        return true;
                    }
                    //按T
                    gua.pressKey(84);
                    gua.delay(300);
                    if (!isChatWinOpen())
                    {
                        log("重试打开聊天框...");
                        continue;
                    }

                    break;
                }

                //输入内容
                log("输入喊话内容...");
                gua.sendString(speakText);
                gua.delay(200);


                log("发送喊话内容...");
                int trySendChatWinCount = 0;
                while (isRunning())
                {
                    ++trySendChatWinCount;
                    if (trySendChatWinCount > 10)
                    {
                        log("重试发送喊话失败，重置状态");
                        //重置状态
                        resetStatus();
                        return true;
                    }

                    //按回车
                    gua.pressKey(13);
                    gua.delay(300);
                    if (isChatWinOpen())
                    {
                        log("重试发送喊话...");
                        continue;
                    }

                    break;
                }

                log("发送喊话完成");
                log("");
                gua.delay(speed);
            }

            state = STATE_START_CHG_SERVER;
            return false;
        }

        private bool isChatWinOpen(){
            int winX = getWinPosX();
            int winY = getWinPosY();
            return gua.findStr("全部|全", "8b8a8a-31312f", winX + 300, winY + 400, winX + 795, winY + 650);
        }

        private bool testPic()
        {
            int winX = getWinPosX();
            int winY = getWinPosY();
            log("测试文字" + isChatWinOpen());
            log("测试文字2" + gua.findStr("全部", "8b8a8a-4d4744", winX + 300, winY + 400, winX + 795, winY + 650));
            log("测试文字3" + gua.findStr("全", "8b8a8a-4d4744", winX + 300, winY + 400, winX + 795, winY + 596));
            log("测试文字4" + gua.findStr("全", "8b8a8a-4d4744", 10, 10, 1500, 1500));
            log("测试文字5" + gua.findStr("全部", "8b8a8a-4d4744", 10, 10, 1500, 1500));
            log("测试文字6" + gua.findStr("全部|全", "8b8a8a-4d4744", 10, 10, 1500, 1500));
            gua.delay(200);
            return true;
        }


    }
}
