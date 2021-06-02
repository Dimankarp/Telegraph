namespace Client
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.enterChat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nicknameData = new System.Windows.Forms.TextBox();
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.userList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.messageData = new System.Windows.Forms.TextBox();
            this.userMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nameData = new System.Windows.Forms.Label();
            this.rsachecker = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ServerStatusLabel = new System.Windows.Forms.Label();
            this.ecdhchecker = new System.Windows.Forms.CheckBox();
            this.IPEnteringBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // enterChat
            // 
            this.enterChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.enterChat.BackColor = System.Drawing.Color.White;
            this.enterChat.Enabled = false;
            this.enterChat.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.enterChat.ForeColor = System.Drawing.Color.DodgerBlue;
            this.enterChat.Location = new System.Drawing.Point(470, 4);
            this.enterChat.Name = "enterChat";
            this.enterChat.Size = new System.Drawing.Size(107, 23);
            this.enterChat.TabIndex = 0;
            this.enterChat.Text = "Подключиться";
            this.enterChat.UseVisualStyleBackColor = false;
            this.enterChat.Click += new System.EventHandler(this.enterChat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Для подключения к чату введите свой ник: ";
            // 
            // nicknameData
            // 
            this.nicknameData.BackColor = System.Drawing.Color.White;
            this.nicknameData.Enabled = false;
            this.nicknameData.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nicknameData.ForeColor = System.Drawing.Color.DodgerBlue;
            this.nicknameData.Location = new System.Drawing.Point(257, 5);
            this.nicknameData.Name = "nicknameData";
            this.nicknameData.Size = new System.Drawing.Size(207, 23);
            this.nicknameData.TabIndex = 2;
            // 
            // chatBox
            // 
            this.chatBox.BackColor = System.Drawing.Color.White;
            this.chatBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.chatBox.Enabled = false;
            this.chatBox.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chatBox.ForeColor = System.Drawing.Color.DodgerBlue;
            this.chatBox.Location = new System.Drawing.Point(7, 51);
            this.chatBox.Name = "chatBox";
            this.chatBox.ReadOnly = true;
            this.chatBox.Size = new System.Drawing.Size(408, 313);
            this.chatBox.TabIndex = 3;
            this.chatBox.Text = "";
            // 
            // userList
            // 
            this.userList.BackColor = System.Drawing.Color.White;
            this.userList.Enabled = false;
            this.userList.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.userList.ForeColor = System.Drawing.Color.DodgerBlue;
            this.userList.FormattingEnabled = true;
            this.userList.ItemHeight = 14;
            this.userList.Location = new System.Drawing.Point(421, 51);
            this.userList.Name = "userList";
            this.userList.Size = new System.Drawing.Size(156, 340);
            this.userList.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(9, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "Чат: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label3.Location = new System.Drawing.Point(424, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Список пользователей: ";
            // 
            // messageData
            // 
            this.messageData.BackColor = System.Drawing.Color.White;
            this.messageData.Enabled = false;
            this.messageData.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.messageData.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.messageData.Location = new System.Drawing.Point(7, 366);
            this.messageData.Name = "messageData";
            this.messageData.Size = new System.Drawing.Size(408, 22);
            this.messageData.TabIndex = 7;
            this.messageData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.messageData_KeyUp);
            // 
            // userMenu
            // 
            this.userMenu.Name = "userMenu";
            this.userMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // nameData
            // 
            this.nameData.AutoSize = true;
            this.nameData.Location = new System.Drawing.Point(198, 35);
            this.nameData.Name = "nameData";
            this.nameData.Size = new System.Drawing.Size(10, 13);
            this.nameData.TabIndex = 8;
            this.nameData.Text = " ";
            // 
            // rsachecker
            // 
            this.rsachecker.AutoSize = true;
            this.rsachecker.BackColor = System.Drawing.Color.White;
            this.rsachecker.Enabled = false;
            this.rsachecker.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rsachecker.ForeColor = System.Drawing.Color.DodgerBlue;
            this.rsachecker.Location = new System.Drawing.Point(7, 397);
            this.rsachecker.Name = "rsachecker";
            this.rsachecker.Size = new System.Drawing.Size(134, 18);
            this.rsachecker.TabIndex = 9;
            this.rsachecker.Text = "Использовать RSA";
            this.rsachecker.UseVisualStyleBackColor = false;
            this.rsachecker.CheckedChanged += new System.EventHandler(this.RSAStatusChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(417, 399);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Сервер:";
            // 
            // ServerStatusLabel
            // 
            this.ServerStatusLabel.AutoSize = true;
            this.ServerStatusLabel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ServerStatusLabel.ForeColor = System.Drawing.Color.Crimson;
            this.ServerStatusLabel.Location = new System.Drawing.Point(467, 400);
            this.ServerStatusLabel.Name = "ServerStatusLabel";
            this.ServerStatusLabel.Size = new System.Drawing.Size(80, 15);
            this.ServerStatusLabel.TabIndex = 11;
            this.ServerStatusLabel.Text = "Недоступен";
            // 
            // ecdhchecker
            // 
            this.ecdhchecker.AutoSize = true;
            this.ecdhchecker.BackColor = System.Drawing.Color.White;
            this.ecdhchecker.Enabled = false;
            this.ecdhchecker.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ecdhchecker.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ecdhchecker.Location = new System.Drawing.Point(147, 397);
            this.ecdhchecker.Name = "ecdhchecker";
            this.ecdhchecker.Size = new System.Drawing.Size(144, 18);
            this.ecdhchecker.TabIndex = 12;
            this.ecdhchecker.Text = "Использовать ECDH";
            this.ecdhchecker.UseVisualStyleBackColor = false;
            this.ecdhchecker.CheckedChanged += new System.EventHandler(this.ECDHStatusChanged);
            // 
            // IPEnteringBox
            // 
            this.IPEnteringBox.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IPEnteringBox.ForeColor = System.Drawing.Color.DodgerBlue;
            this.IPEnteringBox.FormattingEnabled = true;
            this.IPEnteringBox.Items.AddRange(new object[] {
            "127.0.0.1:2222"});
            this.IPEnteringBox.Location = new System.Drawing.Point(292, 394);
            this.IPEnteringBox.Name = "IPEnteringBox";
            this.IPEnteringBox.Size = new System.Drawing.Size(123, 22);
            this.IPEnteringBox.TabIndex = 13;
            this.IPEnteringBox.Text = "127.0.0.1:2222";
            this.IPEnteringBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.IPAdressBox_KeyUp);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(580, 422);
            this.Controls.Add(this.IPEnteringBox);
            this.Controls.Add(this.ecdhchecker);
            this.Controls.Add(this.ServerStatusLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rsachecker);
            this.Controls.Add(this.nameData);
            this.Controls.Add(this.messageData);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.userList);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.nicknameData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.enterChat);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Roboto Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.DodgerBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChatForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Telegraph";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button enterChat;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox nicknameData;
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.ListBox userList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox messageData;
        private System.Windows.Forms.ContextMenuStrip userMenu;
        private System.Windows.Forms.Label nameData;
        private System.Windows.Forms.CheckBox rsachecker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label ServerStatusLabel;
        private System.Windows.Forms.CheckBox ecdhchecker;
        private System.Windows.Forms.ComboBox IPEnteringBox;
    }
}

