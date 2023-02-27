namespace caro
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            main_game = new Panel();
            start_button = new Button();
            label1 = new Label();
            menuStrip2 = new MenuStrip();
            restartToolStripMenuItem = new ToolStripMenuItem();
            timer_bar = new ProgressBar();
            game_timer = new System.Windows.Forms.Timer(components);
            turn_text = new Label();
            ip_connect_btn = new Button();
            ip_input = new TextBox();
            server_status = new Label();
            menuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // main_game
            // 
            main_game.Location = new Point(326, 39);
            main_game.Name = "main_game";
            main_game.Size = new Size(605, 560);
            main_game.TabIndex = 0;
            main_game.Paint += panel1_Paint;
            // 
            // start_button
            // 
            start_button.Location = new Point(12, 183);
            start_button.Margin = new Padding(3, 2, 3, 2);
            start_button.Name = "start_button";
            start_button.Size = new Size(82, 30);
            start_button.TabIndex = 9;
            start_button.Text = "Start Game!!!";
            start_button.UseVisualStyleBackColor = true;
            start_button.Click += start_button_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 30F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(10, 58);
            label1.Name = "label1";
            label1.Size = new Size(175, 54);
            label1.TabIndex = 0;
            label1.Text = "Caro Pro";
            label1.Click += label1_Click;
            // 
            // menuStrip2
            // 
            menuStrip2.ImageScalingSize = new Size(20, 20);
            menuStrip2.Items.AddRange(new ToolStripItem[] { restartToolStripMenuItem });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(938, 24);
            menuStrip2.TabIndex = 2;
            menuStrip2.Text = "menuStrip2";
            // 
            // restartToolStripMenuItem
            // 
            restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            restartToolStripMenuItem.Size = new Size(55, 20);
            restartToolStripMenuItem.Text = "Restart";
            restartToolStripMenuItem.Click += restartToolStripMenuItem_Click;
            // 
            // timer_bar
            // 
            timer_bar.ForeColor = SystemColors.MenuHighlight;
            timer_bar.Location = new Point(12, 576);
            timer_bar.Name = "timer_bar";
            timer_bar.RightToLeftLayout = true;
            timer_bar.Size = new Size(192, 23);
            timer_bar.TabIndex = 3;
            timer_bar.Click += timer_bar_Click;
            timer_bar.Validated += timer_bar_Validated;
            // 
            // game_timer
            // 
            game_timer.Interval = 1000;
            game_timer.Tick += game_timer_Tick;
            // 
            // turn_text
            // 
            turn_text.AutoSize = true;
            turn_text.Font = new Font("Arial", 20.25F, FontStyle.Italic, GraphicsUnit.Point);
            turn_text.Location = new Point(10, 130);
            turn_text.Name = "turn_text";
            turn_text.Size = new Size(259, 31);
            turn_text.TabIndex = 4;
            turn_text.Text = "Press start to play!!!";
            turn_text.Click += label1_Click;
            // 
            // ip_connect_btn
            // 
            ip_connect_btn.Location = new Point(130, 25);
            ip_connect_btn.Margin = new Padding(3, 2, 3, 2);
            ip_connect_btn.Name = "ip_connect_btn";
            ip_connect_btn.Size = new Size(82, 22);
            ip_connect_btn.TabIndex = 6;
            ip_connect_btn.Text = "Connect";
            ip_connect_btn.UseVisualStyleBackColor = true;
            ip_connect_btn.Click += ip_connect_btn_Click;
            // 
            // ip_input
            // 
            ip_input.Location = new Point(12, 26);
            ip_input.Margin = new Padding(3, 2, 3, 2);
            ip_input.Name = "ip_input";
            ip_input.Size = new Size(110, 23);
            ip_input.TabIndex = 7;
            ip_input.Text = "127.0.0.1";
            // 
            // server_status
            // 
            server_status.AutoSize = true;
            server_status.Location = new Point(12, 49);
            server_status.Name = "server_status";
            server_status.Size = new Size(86, 15);
            server_status.TabIndex = 8;
            server_status.Text = "No connection";
            server_status.Click += label2_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(938, 611);
            Controls.Add(start_button);
            Controls.Add(server_status);
            Controls.Add(ip_input);
            Controls.Add(ip_connect_btn);
            Controls.Add(turn_text);
            Controls.Add(timer_bar);
            Controls.Add(menuStrip2);
            Controls.Add(label1);
            Controls.Add(main_game);
            Name = "Form1";
            RightToLeftLayout = true;
            Text = "Form1";
            Load += Form1_Load;
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private Panel main_game;
        private Label label1;
        private MenuStrip menuStrip2;
        private ProgressBar timer_bar;
        private System.Windows.Forms.Timer game_timer;
        private Label turn_text;
        private ToolStripMenuItem restartToolStripMenuItem;
        private Button ip_connect_btn;
        private TextBox ip_input;
        private Label server_status;
        private Button start_button;
    }
}