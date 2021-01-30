using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtaGua.core
{
    abstract class GuaAction
    {
        protected Gua gua;
        private Action<String> logger;

        public GuaAction(Gua gua, Action<String> logger)
        {
            this.gua = gua;
            this.logger = logger;
        }

        protected abstract bool update();

        protected bool isRunning()
        {
            return gua.isRunning();
        }

        protected int getWinPosX()
        {
            return gua.getWinPosX();
        }

        protected int getWinPosY()
        {
            return gua.getWinPosY();
        }


        public void loop()
        {
            if (update())
            { }
        }
        protected void log(String msg)
        {
            logger(msg);
        }
    }
}
