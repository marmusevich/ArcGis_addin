namespace CadastralReference
{
    partial class frmNorthArrowtSetting
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbPositionVertical = new System.Windows.Forms.GroupBox();
            this.nudPosVer = new System.Windows.Forms.NumericUpDown();
            this.rbPosVer_Botton = new System.Windows.Forms.RadioButton();
            this.rbPosVer_Centr = new System.Windows.Forms.RadioButton();
            this.rbPosVer_Top = new System.Windows.Forms.RadioButton();
            this.gbPositionHorizontal = new System.Windows.Forms.GroupBox();
            this.nudPosHor = new System.Windows.Forms.NumericUpDown();
            this.rbPosHor_Right = new System.Windows.Forms.RadioButton();
            this.rbPosHor_Centr = new System.Windows.Forms.RadioButton();
            this.rbPosHor_Left = new System.Windows.Forms.RadioButton();
            this.gbAncorPoint = new System.Windows.Forms.GroupBox();
            this.rbAncorPoint_BR = new System.Windows.Forms.RadioButton();
            this.rbAncorPoint_BC = new System.Windows.Forms.RadioButton();
            this.rbAncorPoint_CC = new System.Windows.Forms.RadioButton();
            this.rbAncorPoint_BL = new System.Windows.Forms.RadioButton();
            this.rbAncorPoint_CL = new System.Windows.Forms.RadioButton();
            this.rbAncorPoint_TC = new System.Windows.Forms.RadioButton();
            this.rbAncorPoint_TR = new System.Windows.Forms.RadioButton();
            this.rbAncorPoint_CR = new System.Windows.Forms.RadioButton();
            this.rbAncorPoint_TL = new System.Windows.Forms.RadioButton();
            this.btnNorthArrowSetting = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbPositionVertical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosVer)).BeginInit();
            this.gbPositionHorizontal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosHor)).BeginInit();
            this.gbAncorPoint.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPositionVertical
            // 
            this.gbPositionVertical.Controls.Add(this.nudPosVer);
            this.gbPositionVertical.Controls.Add(this.rbPosVer_Botton);
            this.gbPositionVertical.Controls.Add(this.rbPosVer_Centr);
            this.gbPositionVertical.Controls.Add(this.rbPosVer_Top);
            this.gbPositionVertical.Location = new System.Drawing.Point(4, 12);
            this.gbPositionVertical.Name = "gbPositionVertical";
            this.gbPositionVertical.Size = new System.Drawing.Size(200, 90);
            this.gbPositionVertical.TabIndex = 2;
            this.gbPositionVertical.TabStop = false;
            this.gbPositionVertical.Text = "Позиция по вертикали";
            // 
            // nudPosVer
            // 
            this.nudPosVer.DecimalPlaces = 2;
            this.nudPosVer.Location = new System.Drawing.Point(130, 44);
            this.nudPosVer.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudPosVer.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudPosVer.Name = "nudPosVer";
            this.nudPosVer.Size = new System.Drawing.Size(64, 20);
            this.nudPosVer.TabIndex = 3;
            this.nudPosVer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // rbPosVer_Botton
            // 
            this.rbPosVer_Botton.AutoSize = true;
            this.rbPosVer_Botton.Location = new System.Drawing.Point(7, 68);
            this.rbPosVer_Botton.Name = "rbPosVer_Botton";
            this.rbPosVer_Botton.Size = new System.Drawing.Size(109, 17);
            this.rbPosVer_Botton.TabIndex = 2;
            this.rbPosVer_Botton.TabStop = true;
            this.rbPosVer_Botton.Text = "от нижнего края";
            this.rbPosVer_Botton.UseVisualStyleBackColor = true;
            // 
            // rbPosVer_Centr
            // 
            this.rbPosVer_Centr.AutoSize = true;
            this.rbPosVer_Centr.Location = new System.Drawing.Point(7, 44);
            this.rbPosVer_Centr.Name = "rbPosVer_Centr";
            this.rbPosVer_Centr.Size = new System.Drawing.Size(74, 17);
            this.rbPosVer_Centr.TabIndex = 1;
            this.rbPosVer_Centr.TabStop = true;
            this.rbPosVer_Centr.Text = "от центра";
            this.rbPosVer_Centr.UseVisualStyleBackColor = true;
            // 
            // rbPosVer_Top
            // 
            this.rbPosVer_Top.AutoSize = true;
            this.rbPosVer_Top.Location = new System.Drawing.Point(7, 20);
            this.rbPosVer_Top.Name = "rbPosVer_Top";
            this.rbPosVer_Top.Size = new System.Drawing.Size(112, 17);
            this.rbPosVer_Top.TabIndex = 0;
            this.rbPosVer_Top.TabStop = true;
            this.rbPosVer_Top.Text = "от верхнего края";
            this.rbPosVer_Top.UseVisualStyleBackColor = true;
            // 
            // gbPositionHorizontal
            // 
            this.gbPositionHorizontal.Controls.Add(this.nudPosHor);
            this.gbPositionHorizontal.Controls.Add(this.rbPosHor_Right);
            this.gbPositionHorizontal.Controls.Add(this.rbPosHor_Centr);
            this.gbPositionHorizontal.Controls.Add(this.rbPosHor_Left);
            this.gbPositionHorizontal.Location = new System.Drawing.Point(210, 12);
            this.gbPositionHorizontal.Name = "gbPositionHorizontal";
            this.gbPositionHorizontal.Size = new System.Drawing.Size(200, 90);
            this.gbPositionHorizontal.TabIndex = 4;
            this.gbPositionHorizontal.TabStop = false;
            this.gbPositionHorizontal.Text = "Позиция по горизонтали";
            // 
            // nudPosHor
            // 
            this.nudPosHor.DecimalPlaces = 2;
            this.nudPosHor.Location = new System.Drawing.Point(130, 44);
            this.nudPosHor.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudPosHor.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudPosHor.Name = "nudPosHor";
            this.nudPosHor.Size = new System.Drawing.Size(64, 20);
            this.nudPosHor.TabIndex = 3;
            this.nudPosHor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // rbPosHor_Right
            // 
            this.rbPosHor_Right.AutoSize = true;
            this.rbPosHor_Right.Location = new System.Drawing.Point(7, 68);
            this.rbPosHor_Right.Name = "rbPosHor_Right";
            this.rbPosHor_Right.Size = new System.Drawing.Size(107, 17);
            this.rbPosHor_Right.TabIndex = 2;
            this.rbPosHor_Right.TabStop = true;
            this.rbPosHor_Right.Text = "от правого края";
            this.rbPosHor_Right.UseVisualStyleBackColor = true;
            // 
            // rbPosHor_Centr
            // 
            this.rbPosHor_Centr.AutoSize = true;
            this.rbPosHor_Centr.Location = new System.Drawing.Point(7, 44);
            this.rbPosHor_Centr.Name = "rbPosHor_Centr";
            this.rbPosHor_Centr.Size = new System.Drawing.Size(74, 17);
            this.rbPosHor_Centr.TabIndex = 1;
            this.rbPosHor_Centr.TabStop = true;
            this.rbPosHor_Centr.Text = "от центра";
            this.rbPosHor_Centr.UseVisualStyleBackColor = true;
            // 
            // rbPosHor_Left
            // 
            this.rbPosHor_Left.AutoSize = true;
            this.rbPosHor_Left.Location = new System.Drawing.Point(7, 20);
            this.rbPosHor_Left.Name = "rbPosHor_Left";
            this.rbPosHor_Left.Size = new System.Drawing.Size(101, 17);
            this.rbPosHor_Left.TabIndex = 0;
            this.rbPosHor_Left.TabStop = true;
            this.rbPosHor_Left.Text = "от левого края";
            this.rbPosHor_Left.UseVisualStyleBackColor = true;
            // 
            // gbAncorPoint
            // 
            this.gbAncorPoint.Controls.Add(this.rbAncorPoint_BR);
            this.gbAncorPoint.Controls.Add(this.rbAncorPoint_BC);
            this.gbAncorPoint.Controls.Add(this.rbAncorPoint_CC);
            this.gbAncorPoint.Controls.Add(this.rbAncorPoint_BL);
            this.gbAncorPoint.Controls.Add(this.rbAncorPoint_CL);
            this.gbAncorPoint.Controls.Add(this.rbAncorPoint_TC);
            this.gbAncorPoint.Controls.Add(this.rbAncorPoint_TR);
            this.gbAncorPoint.Controls.Add(this.rbAncorPoint_CR);
            this.gbAncorPoint.Controls.Add(this.rbAncorPoint_TL);
            this.gbAncorPoint.Location = new System.Drawing.Point(1303, 12);
            this.gbAncorPoint.Name = "gbAncorPoint";
            this.gbAncorPoint.Size = new System.Drawing.Size(112, 123);
            this.gbAncorPoint.TabIndex = 5;
            this.gbAncorPoint.TabStop = false;
            this.gbAncorPoint.Text = "Точка привязки";
            this.gbAncorPoint.Visible = false;
            // 
            // rbAncorPoint_BR
            // 
            this.rbAncorPoint_BR.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAncorPoint_BR.Location = new System.Drawing.Point(74, 87);
            this.rbAncorPoint_BR.Name = "rbAncorPoint_BR";
            this.rbAncorPoint_BR.Size = new System.Drawing.Size(28, 28);
            this.rbAncorPoint_BR.TabIndex = 8;
            this.rbAncorPoint_BR.TabStop = true;
            this.rbAncorPoint_BR.Text = " ";
            this.rbAncorPoint_BR.UseVisualStyleBackColor = true;
            // 
            // rbAncorPoint_BC
            // 
            this.rbAncorPoint_BC.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAncorPoint_BC.Location = new System.Drawing.Point(40, 87);
            this.rbAncorPoint_BC.Name = "rbAncorPoint_BC";
            this.rbAncorPoint_BC.Size = new System.Drawing.Size(28, 28);
            this.rbAncorPoint_BC.TabIndex = 7;
            this.rbAncorPoint_BC.TabStop = true;
            this.rbAncorPoint_BC.Text = " ";
            this.rbAncorPoint_BC.UseVisualStyleBackColor = true;
            // 
            // rbAncorPoint_CC
            // 
            this.rbAncorPoint_CC.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAncorPoint_CC.Location = new System.Drawing.Point(40, 54);
            this.rbAncorPoint_CC.Name = "rbAncorPoint_CC";
            this.rbAncorPoint_CC.Size = new System.Drawing.Size(28, 28);
            this.rbAncorPoint_CC.TabIndex = 6;
            this.rbAncorPoint_CC.TabStop = true;
            this.rbAncorPoint_CC.Text = " ";
            this.rbAncorPoint_CC.UseVisualStyleBackColor = true;
            // 
            // rbAncorPoint_BL
            // 
            this.rbAncorPoint_BL.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAncorPoint_BL.Location = new System.Drawing.Point(6, 87);
            this.rbAncorPoint_BL.Name = "rbAncorPoint_BL";
            this.rbAncorPoint_BL.Size = new System.Drawing.Size(28, 28);
            this.rbAncorPoint_BL.TabIndex = 5;
            this.rbAncorPoint_BL.TabStop = true;
            this.rbAncorPoint_BL.Text = " ";
            this.rbAncorPoint_BL.UseVisualStyleBackColor = true;
            // 
            // rbAncorPoint_CL
            // 
            this.rbAncorPoint_CL.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAncorPoint_CL.Location = new System.Drawing.Point(6, 54);
            this.rbAncorPoint_CL.Name = "rbAncorPoint_CL";
            this.rbAncorPoint_CL.Size = new System.Drawing.Size(28, 28);
            this.rbAncorPoint_CL.TabIndex = 4;
            this.rbAncorPoint_CL.TabStop = true;
            this.rbAncorPoint_CL.Text = " ";
            this.rbAncorPoint_CL.UseVisualStyleBackColor = true;
            // 
            // rbAncorPoint_TC
            // 
            this.rbAncorPoint_TC.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAncorPoint_TC.Location = new System.Drawing.Point(40, 19);
            this.rbAncorPoint_TC.Name = "rbAncorPoint_TC";
            this.rbAncorPoint_TC.Size = new System.Drawing.Size(28, 28);
            this.rbAncorPoint_TC.TabIndex = 3;
            this.rbAncorPoint_TC.TabStop = true;
            this.rbAncorPoint_TC.Text = " ";
            this.rbAncorPoint_TC.UseVisualStyleBackColor = true;
            // 
            // rbAncorPoint_TR
            // 
            this.rbAncorPoint_TR.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAncorPoint_TR.Location = new System.Drawing.Point(74, 19);
            this.rbAncorPoint_TR.Name = "rbAncorPoint_TR";
            this.rbAncorPoint_TR.Size = new System.Drawing.Size(28, 28);
            this.rbAncorPoint_TR.TabIndex = 2;
            this.rbAncorPoint_TR.TabStop = true;
            this.rbAncorPoint_TR.Text = " ";
            this.rbAncorPoint_TR.UseVisualStyleBackColor = true;
            // 
            // rbAncorPoint_CR
            // 
            this.rbAncorPoint_CR.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAncorPoint_CR.Location = new System.Drawing.Point(74, 54);
            this.rbAncorPoint_CR.Name = "rbAncorPoint_CR";
            this.rbAncorPoint_CR.Size = new System.Drawing.Size(28, 28);
            this.rbAncorPoint_CR.TabIndex = 1;
            this.rbAncorPoint_CR.TabStop = true;
            this.rbAncorPoint_CR.Text = " ";
            this.rbAncorPoint_CR.UseVisualStyleBackColor = true;
            // 
            // rbAncorPoint_TL
            // 
            this.rbAncorPoint_TL.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAncorPoint_TL.Location = new System.Drawing.Point(6, 19);
            this.rbAncorPoint_TL.Name = "rbAncorPoint_TL";
            this.rbAncorPoint_TL.Size = new System.Drawing.Size(28, 28);
            this.rbAncorPoint_TL.TabIndex = 0;
            this.rbAncorPoint_TL.TabStop = true;
            this.rbAncorPoint_TL.Text = " ";
            this.rbAncorPoint_TL.UseVisualStyleBackColor = true;
            // 
            // btnNorthArrowSetting
            // 
            this.btnNorthArrowSetting.Location = new System.Drawing.Point(112, 108);
            this.btnNorthArrowSetting.Name = "btnNorthArrowSetting";
            this.btnNorthArrowSetting.Size = new System.Drawing.Size(190, 23);
            this.btnNorthArrowSetting.TabIndex = 7;
            this.btnNorthArrowSetting.Text = "Дополнительные свойства ...";
            this.btnNorthArrowSetting.UseVisualStyleBackColor = true;
            this.btnNorthArrowSetting.Click += new System.EventHandler(this.btnNorthArrowSetting_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(335, 147);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "Сохранить";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(4, 147);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmNorthArrowtSetting
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(422, 174);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnNorthArrowSetting);
            this.Controls.Add(this.gbAncorPoint);
            this.Controls.Add(this.gbPositionHorizontal);
            this.Controls.Add(this.gbPositionVertical);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmNorthArrowtSetting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Параметры стрелки севера";
            this.Load += new System.EventHandler(this.frmNorthArrowtSetting_Load);
            this.gbPositionVertical.ResumeLayout(false);
            this.gbPositionVertical.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosVer)).EndInit();
            this.gbPositionHorizontal.ResumeLayout(false);
            this.gbPositionHorizontal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosHor)).EndInit();
            this.gbAncorPoint.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPositionVertical;
        private System.Windows.Forms.NumericUpDown nudPosVer;
        private System.Windows.Forms.RadioButton rbPosVer_Botton;
        private System.Windows.Forms.RadioButton rbPosVer_Centr;
        private System.Windows.Forms.RadioButton rbPosVer_Top;
        private System.Windows.Forms.GroupBox gbPositionHorizontal;
        private System.Windows.Forms.NumericUpDown nudPosHor;
        private System.Windows.Forms.RadioButton rbPosHor_Right;
        private System.Windows.Forms.RadioButton rbPosHor_Centr;
        private System.Windows.Forms.RadioButton rbPosHor_Left;
        private System.Windows.Forms.GroupBox gbAncorPoint;
        private System.Windows.Forms.RadioButton rbAncorPoint_BR;
        private System.Windows.Forms.RadioButton rbAncorPoint_BC;
        private System.Windows.Forms.RadioButton rbAncorPoint_CC;
        private System.Windows.Forms.RadioButton rbAncorPoint_BL;
        private System.Windows.Forms.RadioButton rbAncorPoint_CL;
        private System.Windows.Forms.RadioButton rbAncorPoint_TC;
        private System.Windows.Forms.RadioButton rbAncorPoint_TR;
        private System.Windows.Forms.RadioButton rbAncorPoint_CR;
        private System.Windows.Forms.RadioButton rbAncorPoint_TL;
        private System.Windows.Forms.Button btnNorthArrowSetting;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}

