using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rubezh.Core;
using Rubezh.Data;
using System.IO;

namespace Rubezh
{
    /// <summary>
    /// Основная форма проекта
    /// </summary>
    public partial class frmMain : Form
    {
        private DataManager _manager = new DataManager();
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if(openFileDialogSource.ShowDialog(this) == DialogResult.OK)
            {
                strFileName.Text = openFileDialogSource.FileName;
            }
        }

        private void lnkCreateSampleData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Сформировать набор данных
            _manager.GenerateSampleData();

            // Загрузить в таблицу
            dsMain.Tables[0].Clear();
            dsMain.Tables[0].Load(_manager.GetTable().CreateDataReader());
            dgMain.Update();

            // Показать, что все готово
            savePicture.Visible = true;
            timerSave.Enabled = true;
        }

        private void lnkSaveData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(strFileName.Text))
            {
                _manager.SaveData(strFileName.Text);
                if (_manager.IsError)
                {
                    MessageBox.Show(this, $"Ошибка при выполнении операции! {Environment.NewLine }{_manager.ErrorText}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                savePicture.Visible = true;
                timerSave.Enabled = true;
            }
        }

        private void btnClearText_Click(object sender, EventArgs e)
        {
            strFileName.Text = "";
        }

        private void timerSave_Tick(object sender, EventArgs e)
        {
            savePicture.Visible = false;
            timerSave.Enabled = false;
        }

        private void btnPreviewAll_Click(object sender, EventArgs e)
        {
            bindingSourceMain.Filter = "";
        }

        private void btnPreviewErrors_Click(object sender, EventArgs e)
        {
            var _list = _manager.GetRoomWithErrors();
            if (_list.Any())
                bindingSourceMain.Filter = $"Code in ({string.Join(",", _list.ToArray())})";
            else
                // Специально делаю не верный фильтр, чтобы показать, что ничего нет
                bindingSourceMain.Filter = "1 = 2";
        }

        private void dgMain_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if(e != null)
            {
                var _data = dgMain.Rows[e.RowIndex].DataBoundItem as DataRowView;
                var _error = Convert.ToBoolean(_data["IsError"]);

                if (_error)
                {
                    dgMain.Rows[e.RowIndex].Cells["buildingNameDataGridViewTextBoxColumn"].Style.ForeColor = Color.Red;
                    dgMain.Rows[e.RowIndex].Cells["deviceCountDataGridViewTextBoxColumn"].Style.ForeColor = Color.Red;
                }
            }
        }

        private void bindingSourceMain_CurrentChanged(object sender, EventArgs e)
        {
            if(bindingSourceMain.Current != null)
            {
                var _data = bindingSourceMain.Current as DataRowView;
                var _codeRoom = Convert.ToInt32(_data["Code"]);
                var _room = _manager.GetRoomRef(_codeRoom);
                if (_room != null)

                    // Вывкдкм информацию
                    strInformation.Text = _room.ToString();
                else
                    strInformation.Text = "Нет данных ..";
            }
        }

        private void lnkOpenData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(!string.IsNullOrEmpty(strFileName.Text))
            {
                _manager.LoadData(strFileName.Text);
                if(_manager.IsError)
                {
                    MessageBox.Show(this, $"Ошибка при выполнении операции! {Environment.NewLine }{_manager.ErrorText}","Ошибка", MessageBoxButtons.OK,  MessageBoxIcon.Error );
                    return;
                }    

                // Загрузить в таблицу
                dsMain.Tables[0].Clear();
                dsMain.Tables[0].Load(_manager.GetTable().CreateDataReader());
                dgMain.Update();

                savePicture.Visible = true;
                timerSave.Enabled = true;
            }
        }
    }
}
