namespace GtaGua
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tipsLabel = new System.Windows.Forms.Label();
            this.tipsTextBox = new System.Windows.Forms.TextBox();
            this.speedLabel = new System.Windows.Forms.Label();
            this.speedTextBox = new System.Windows.Forms.TextBox();
            this.speakCountTextBox = new System.Windows.Forms.TextBox();
            this.speakCountlabel = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.logLabel = new System.Windows.Forms.Label();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tipsLabel
            // 
            this.tipsLabel.AutoSize = true;
            this.tipsLabel.Location = new System.Drawing.Point(13, 13);
            this.tipsLabel.Name = "tipsLabel";
            this.tipsLabel.Size = new System.Drawing.Size(29, 12);
            this.tipsLabel.TabIndex = 0;
            this.tipsLabel.Text = "提示";
            // 
            // tipsTextBox
            // 
            this.tipsTextBox.Location = new System.Drawing.Point(12, 38);
            this.tipsTextBox.Multiline = true;
            this.tipsTextBox.Name = "tipsTextBox";
            this.tipsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tipsTextBox.Size = new System.Drawing.Size(380, 305);
            this.tipsTextBox.TabIndex = 1;
            this.tipsTextBox.Text = "123是是是";
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Location = new System.Drawing.Point(40, 364);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(29, 12);
            this.speedLabel.TabIndex = 2;
            this.speedLabel.Text = "速度";
            // 
            // speedTextBox
            // 
            this.speedTextBox.Location = new System.Drawing.Point(75, 361);
            this.speedTextBox.Name = "speedTextBox";
            this.speedTextBox.Size = new System.Drawing.Size(100, 21);
            this.speedTextBox.TabIndex = 3;
            this.speedTextBox.Text = "350";
            // 
            // speakCountTextBox
            // 
            this.speakCountTextBox.Location = new System.Drawing.Point(258, 361);
            this.speakCountTextBox.Name = "speakCountTextBox";
            this.speakCountTextBox.Size = new System.Drawing.Size(100, 21);
            this.speakCountTextBox.TabIndex = 5;
            this.speakCountTextBox.Text = "10";
            // 
            // speakCountlabel
            // 
            this.speakCountlabel.AutoSize = true;
            this.speakCountlabel.Location = new System.Drawing.Point(199, 364);
            this.speakCountlabel.Name = "speakCountlabel";
            this.speakCountlabel.Size = new System.Drawing.Size(53, 12);
            this.speakCountlabel.TabIndex = 4;
            this.speakCountlabel.Text = "喊话次数";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(115, 399);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 6;
            this.startBtn.Text = "开始F8";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(216, 399);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(75, 23);
            this.stopBtn.TabIndex = 7;
            this.stopBtn.Text = "停止F10";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Location = new System.Drawing.Point(406, 13);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(29, 12);
            this.logLabel.TabIndex = 8;
            this.logLabel.Text = "日志";
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(408, 38);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(380, 305);
            this.logTextBox.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 434);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.logLabel);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.speakCountTextBox);
            this.Controls.Add(this.speakCountlabel);
            this.Controls.Add(this.speedTextBox);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.tipsTextBox);
            this.Controls.Add(this.tipsLabel);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GtaGua";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tipsLabel;
        private System.Windows.Forms.TextBox tipsTextBox;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.TextBox speedTextBox;
        private System.Windows.Forms.TextBox speakCountTextBox;
        private System.Windows.Forms.Label speakCountlabel;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Label logLabel;
        private System.Windows.Forms.TextBox logTextBox;
    }
}

