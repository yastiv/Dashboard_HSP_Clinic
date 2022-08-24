using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.IO;

namespace Build_4
{
    public partial class Main_Window : Form
    {
        
        public Main_Window()
        {
            InitializeComponent();
        }   
        //START
        readonly MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=123;persistsecurityinfo=True;database=hsp_group");
        
        private void Notification_Button_Click(object sender, EventArgs e)
        {
            //Makes the SIDE MENU BUTTONS the same colour...
            SideMenuButtons();
            //Makes the SIDE MENU BUTTON user selected DIFFERENT colour...
            Notification_Button.BackColor = Color.LightCyan;
            Notification_Button.ForeColor = Color.DarkCyan;
            //Makes ALL the HEADER PANELS HIDDEN...
            HeaderPanels();
            //Makes ALL OTHER PANELS HIDDEN...
            MainPanels();
            //Makes USER SELECTED ITEM VISIBLE...
            Notification_Page.Visible = true;
            //Clears ALL the TEXTBOXES, COMBOBOXES and DATEPICKERS...
            ClearAll();
            Medicine_View.Refresh();
            //Displays the upcoming appointments...
            UpcomingAppointments();
            //Displays the MEDICINE STOCK if its below 10 or expiry date is within 7 days of current date...
            con.Open();
            DateTime time = DateTime.Now;
            string format = "yyyy-MM-dd";
            MySqlCommand command = new MySqlCommand("Select medicine_name, stock_amount, measurement, expiry_date, batch_number from medicine where stock_amount < 10  or expiry_date < date_Add(curdate(), interval 7 day)", con);
            MySqlDataAdapter sd = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            Medicine_View.DataSource = dt;
            con.Close();
        }
        
        private void Student_Button_Click(object sender, EventArgs e)
        {
            //Makes the SIDE MENU BUTTONS the same colour...
            SideMenuButtons();
            //Makes the SIDE MENU BUTTON user selected DIFFERENT colour...
            Student_Button.BackColor = Color.LightCyan;
            Student_Button.ForeColor = Color.DarkCyan;
            //Makes ALL the HEADER PANELS HIDDEN...
            HeaderPanels();
            //Makes ALL OTHER PANELS HIDDEN...
            MainPanels();
            // Makes the Buttons on the Student Page Reset to selected Colours...
            StudentButtonReset();
            //Makes USER SELECTED ITEM VISIBLE...
            Student_Page.Visible = true;
            //Clears ALL the TEXTBOXES, COMBOBOXES and DATEPICKERS...
            ClearAll();
        }

        private void Treatment_Button_Click(object sender, EventArgs e)
        {
            //Makes the SIDE MENU BUTTONS the same colour...
            SideMenuButtons();
            //Makes the SIDE MENU BUTTON user selected DIFFERENT colour...
            Treatment_Button.BackColor = Color.LightCyan;
            Treatment_Button.ForeColor = Color.DarkCyan;
            //Makes ALL the HEADER PANELS HIDDEN...
            HeaderPanels();
            //Makes ALL OTHER PANELS HIDDEN...
            MainPanels();
            //Makes USER SELECTED ITEM VISIBLE...
            Treatment_Header_Panel.Visible = true;
            //Clears ALL the TEXTBOXES, COMBOBOXES and DATEPICKERS...
            ClearAll();
        }

        private void Diagnosis_Button_Click(object sender, EventArgs e)
        {
            //Makes the SIDE MENU BUTTONS the same colour...
            SideMenuButtons();
            //Makes the SIDE MENU BUTTON user selected DIFFERENT colour...
            Diagnosis_Button.BackColor = Color.LightCyan;
            Diagnosis_Button.ForeColor = Color.DarkCyan;
            //Makes ALL the HEADER PANELS HIDDEN...
            HeaderPanels();
            //Makes ALL OTHER PANELS HIDDEN...
            MainPanels();
            //Makes USER SELECTED ITEM VISIBLE...
            Diagnosis_Header_Panel.Visible = true;
            //Clears ALL the TEXTBOXES, COMBOBOXES and DATEPICKERS...
            ClearAll();
        }

        private void Medicine_Button_Click(object sender, EventArgs e)
        {
            //Makes the SIDE MENU BUTTONS the same colour...
            SideMenuButtons();
            //Makes the SIDE MENU BUTTON user selected DIFFERENT colour...
            Medicine_Button.BackColor = Color.LightCyan;
            Medicine_Button.ForeColor = Color.DarkCyan;
            //Makes ALL the HEADER PANELS HIDDEN...
            HeaderPanels();
            //Makes ALL OTHER PANELS HIDDEN...
            MainPanels();
            //Makes USER SELECTED ITEM VISIBLE...
            Medicine_Header_Panel.Visible = true;
            //Clears ALL the TEXTBOXES, COMBOBOXES and DATEPICKERS...
            ClearAll();
        }
        
        private void Treatment_Header_Add_Button_Click(object sender, EventArgs e)
        {
            //Makes the HEADER BUTTONS the same colour...
            TreatmentHeader();
            //Makes the HEADER BUTTON user selected DIFFERENT colour...
            Treatment_Header_Add_Button.BackColor = Color.LightCyan;
            Treatment_Header_Add_Button.ForeColor = Color.DarkCyan;
            //Makes ALL OTHER PANELS HIDDEN...
            MainPanels();
            //Resets all button colours on the page...
            TreatmentButtonReset();
            //Makes USER SELECTED ITEM VISIBLE... 
            Treatment_Add_Page.Visible = true;
            //Clears the TEXTBOXES, COMBOBOXES and DATEPICKERS from Treatment Search Page...
            ClearTreatmentSearch();
            //Preloads the set measurements...
            LoadMeasurment();
            //Database...
            con.Open();
            MySqlCommand combocmd = new MySqlCommand("Select diagnosis_id, diagnosis_type from diagnosis", con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = combocmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            Treatment_Add_Diagnosis_ComboBox.DataSource = dt;
            Treatment_Add_Diagnosis_ComboBox.DisplayMember = "diagnosis_type";
            Treatment_Add_Diagnosis_ComboBox.ValueMember = "diagnosis_id";
            MySqlCommand combocmd2 = new MySqlCommand("Select medicine_id, medicine_name from medicine", con);
            MySqlDataAdapter da2 = new MySqlDataAdapter();
            da2.SelectCommand = combocmd2;
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            Treatment_Add_MedicineName_ComboBox.DataSource = dt2;
            Treatment_Add_MedicineName_ComboBox.DisplayMember = "medicine_name";
            Treatment_Add_MedicineName_ComboBox.ValueMember = "medicine_id";            
            con.Close();
        }
        
         private void Treatment_Header_Search_Button_Click(object sender, EventArgs e)
        {
            //Makes the HEADER BUTTONS the same colour...
            TreatmentHeader();
            //Makes the HEADER BUTTON user selected DIFFERENT colour...
            Treatment_Header_Search_Button.BackColor = Color.LightCyan;
            Treatment_Header_Search_Button.ForeColor = Color.DarkCyan;
            //Makes ALL OTHER PANELS HIDDEN...
            MainPanels();
            //Resets all button colours on the page...
            TreatmentButtonReset();
            //Makes USER SELECTED ITEM VISIBLE... 
            Treatment_Search_Page.Visible = true;
            //Clears the TEXTBOXES, COMBOBOXES and DATEPICKERS from Treatment Add Page...
            ClearTreatmentAdd();
        }

        private void Diagnosis_Header_View_Button_Click(object sender, EventArgs e)
        {
            //Makes the HEADER BUTTONS the same colour...
            DiagnosisHeader();
            //Makes the HEADER BUTTON user selected DIFFERENT colour...
            Diagnosis_Header_View_Button.BackColor = Color.LightCyan;
            Diagnosis_Header_View_Button.ForeColor = Color.DarkCyan;
            //Makes ALL OTHER PANELS HIDDEN...
            MainPanels();
            //Restes button colours...
            DiagnosisButtonReset();
            //Makes USER SELECTED ITEM VISIBLE... 
            Diagnosis_View_Page.Visible = true;
            //Clears the TEXTBOXES, COMBOBOXES and DATEPICKERS from the other DIAGNOSIS Pages...
            ClearDiagnosisAdd();
            ClearDiagnosisStatistics();
            ClearDiagnosisView();
            //Database...
            con.Open();
            MySqlCommand command = new MySqlCommand("Select diagnosis_type from diagnosis", con);
            MySqlDataAdapter sd = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            Diagnosis_ViewAll.DataSource = dt;
            con.Close();
        }
        
        private void Diagnosis_Header_Add_Button_Click(object sender, EventArgs e)
        {
            //Makes the HEADER BUTTONS the same colour...
            DiagnosisHeader();
            //Makes the HEADER BUTTON user selected DIFFERENT colour...
            Diagnosis_Header_Add_Button.BackColor = Color.LightCyan;
            Diagnosis_Header_Add_Button.ForeColor = Color.DarkCyan;
            //Makes ALL OTHER PANELS HIDDEN...
            MainPanels();
            //Restes button colours...
            DiagnosisButtonReset();
            //Makes USER SELECTED ITEM VISIBLE... 
            Diagnosis_Add_Page.Visible = true;
            //Clears the TEXTBOXES, COMBOBOXES and DATEPICKERS from the other DIAGNOSIS Pages...
            ClearDiagnosisAdd();
            ClearDiagnosisStatistics();
            ClearDiagnosisView();
            //Database...
            con.Open();
            MySqlCommand combocmd = new MySqlCommand("Select diagnosis_id, diagnosis_type from diagnosis", con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = combocmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            Diagnosis_Add_Edit_ComboBox.DataSource = dt;
            Diagnosis_Add_Edit_ComboBox.DisplayMember = "diagnosis_type";
            Diagnosis_Add_Edit_ComboBox.ValueMember = "diagnosis_id";
            con.Close();
        }
        
        private void Diagnosis_Header_Statistics_Button_Click(object sender, EventArgs e)
        {
            //Makes the HEADER BUTTONS the same colour...
            DiagnosisHeader();
            //Makes the HEADER BUTTON user selected DIFFERENT colour...
            Diagnosis_Header_Statistics_Button.BackColor = Color.LightCyan;
            Diagnosis_Header_Statistics_Button.ForeColor = Color.DarkCyan;
            //Makes ALL OTHER PANELS HIDDEN...
            MainPanels();
            //Restes button colours...
            DiagnosisButtonReset();
            //Makes USER SELECTED ITEM VISIBLE... 
            Diagnosis_Statistic_Page.Visible = true;
            //Clears the TEXTBOXES, COMBOBOXES and DATEPICKERS from the other DIAGNOSIS Pages...
            ClearDiagnosisAdd();
            ClearDiagnosisStatistics();
            ClearDiagnosisView();
            //Database...
            con.Open();
            MySqlCommand combocmd = new MySqlCommand("Select diagnosis_id, diagnosis_type from diagnosis", con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = combocmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            Diagnosis_Statistic_DiagnosisType_ComboBox.DataSource = dt;
            Diagnosis_Statistic_DiagnosisType_ComboBox.DisplayMember = "diagnosis_type";
            Diagnosis_Statistic_DiagnosisType_ComboBox.ValueMember = "diagnosis_id";
            con.Close();
        }
        
        private void Medicine_Header_View_Button_Click(object sender, EventArgs e)
        {
            //Makes the HEADER BUTTONS the same colour...
            MedicineHeader();
            //Makes the HEADER BUTTON user selected DIFFERENT colour...
            Medicine_Header_View_Button.BackColor = Color.LightCyan;
            Medicine_Header_View_Button.ForeColor = Color.DarkCyan;
            //Makes ALL OTHER PANELS HIDDEN...
            MainPanels();
            //Resets Medicine button colours...
            MedicineButtonReset();
            //Makes USER SELECTED ITEM VISIBLE... 
            Medicine_View_Page.Visible = true;
            //Clears the TEXTBOXES, COMBOBOXES and DATEPICKERS from the other Medicine Pages...
            ClearMedicineEdit();
            ClearMedicineAdd();
            ClearMedicineView();
            //Database...
            con.Open();
            MySqlCommand command = new MySqlCommand("Select medicine_name, stock_amount, measurement, expiry_date, batch_number from medicine", con);
            MySqlDataAdapter sd = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            Medicine_ViewAll.DataSource = dt;
            con.Close();
        }
        
        private void Medicine_Header_Add_Button_Click(object sender, EventArgs e)
        {
            //Makes the HEADER BUTTONS the same colour...
            MedicineHeader();
            //Makes the HEADER BUTTON user selected DIFFERENT colour...
            Medicine_Header_Add_Button.BackColor = Color.LightCyan;
            Medicine_Header_Add_Button.ForeColor = Color.DarkCyan;
            //Makes ALL OTHER PANELS HIDDEN...
            MainPanels();
            //Resets Medicine button colours...
            MedicineButtonReset();
            //Makes USER SELECTED ITEM VISIBLE... 
            Medicine_Add_Page.Visible = true;
            //Clears the TEXTBOXES, COMBOBOXES and DATEPICKERS from the other Medicine Pages...
            ClearMedicineEdit();
            ClearMedicineAdd();
            ClearMedicineView();
            //Preloads the set measurements...
            LoadMeasurment();            
        }

        private void Medicine_Header_Edit_Button_Click(object sender, EventArgs e)
        {
            //Makes the HEADER BUTTONS the same colour...
            MedicineHeader();
            //Makes the HEADER BUTTON user selected DIFFERENT colour...
            Medicine_Header_Edit_Button.BackColor = Color.LightCyan;
            Medicine_Header_Edit_Button.ForeColor = Color.DarkCyan;
            //Makes ALL OTHER PANELS HIDDEN...
            MainPanels();
            //Resets Medicine button colours...
            MedicineButtonReset();
            //Makes USER SELECTED ITEM VISIBLE... 
            Medicine_Edit_Page.Visible = true;
            //Clears the TEXTBOXES, COMBOBOXES and DATEPICKERS from the other Medicine Pages...
            ClearMedicineEdit();
            ClearMedicineAdd();
            ClearMedicineView();
            //Database...
            con.Open();
            MySqlCommand combocmd = new MySqlCommand("Select medicine_id, medicine_name from medicine", con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = combocmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            Medicine_Edit_MedicineName_ComboBox.DataSource = dt;
            Medicine_Edit_MedicineName_ComboBox.DisplayMember = "medicine_name";
            Medicine_Edit_MedicineName_ComboBox.ValueMember = "medicine_id";
            con.Close();
        }
        
        private void Student_Search_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            StudentButtonReset();
            //SEARCH BUTTON changes color...
            Student_Search_Button.BackColor = Color.LightCyan;
            Student_Search_Button.ForeColor = Color.DarkCyan;
            //SYSTEM Should SEARCH THROUGH DATABASE TO FIND STUDENT NUMBER AND OTHER INFORMATION for display in these items...
            con.Open();
            MySqlCommand command = new MySqlCommand("Select first_name, last_name, address, email, phone_number, gender, DOB, allergies from users where student_number = '" + Student_StudentNumber_TextBox.Text + "'", con);
            MySqlDataReader srd = command.ExecuteReader();
            while (srd.Read())
            {
                Student_FirstName_TextBox.Text = srd.GetValue(0).ToString();
                Student_LastName_TextBox.Text = srd.GetValue(1).ToString();
                Student_Address_TextBox.Text = srd.GetValue(2).ToString();
                Student_Email_TextBox.Text = srd.GetValue(3).ToString();
                Student_PhoneNumber_TextBox.Text = srd.GetValue(4).ToString();
                if (srd.GetValue(5).ToString() == "M")
                {
                    Student_Gender_Male_RadioButton.Checked = true;
                }
                if (srd.GetValue(5).ToString() == "F")
                {
                    Student_Gender_Female_RadioButton.Checked = true;
                }
                Student_DateOfBirth_DateTimePicker.Text = srd.GetValue(6).ToString();
                Student_Allergies_TextBox.Text = srd.GetValue(7).ToString();
            }
            con.Close();
        }
        
        private void Student_Edit_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            StudentButtonReset();
            //SEARCH BUTTON changes color...
            Student_Edit_Button.BackColor = Color.LightCyan;
            Student_Edit_Button.ForeColor = Color.DarkCyan;
            //Database...
            string gender;
            if (Student_Gender_Male_RadioButton.Checked == true)
            {
                gender = "M";
                con.Open();
                MySqlCommand command = new MySqlCommand("Update users set first_name = '" + Student_FirstName_TextBox.Text + "', last_name = '" + Student_LastName_TextBox.Text + "', address = '" + Student_Address_TextBox.Text + "', email = '" + Student_Email_TextBox.Text + "', phone_number = '" + Student_PhoneNumber_TextBox.Text + "', gender = '" + gender + "', DOB = '" + Student_DateOfBirth_DateTimePicker.Value.Date.ToString("yyyyMMdd") + "', allergies = '" + Student_Allergies_TextBox.Text + "'where student_number = '" + Student_StudentNumber_TextBox.Text + "'", con);
                command.ExecuteNonQuery();
                con.Close();
            }
            if (Student_Gender_Female_RadioButton.Checked == true)
            {
                gender = "F";
                con.Open();
                MySqlCommand command = new MySqlCommand("Update users set first_name = '" + Student_FirstName_TextBox.Text + "', last_name = '" + Student_LastName_TextBox.Text + "', address = '" + Student_Address_TextBox.Text + "', email = '" + Student_Email_TextBox.Text + "', phone_number = '" + Student_PhoneNumber_TextBox.Text + "', gender = '" + gender + "', DOB = '" + Student_DateOfBirth_DateTimePicker.Value.Date.ToString("yyyyMMdd") + "', allergies = '" + Student_Allergies_TextBox.Text + "'where student_number = '" + Student_StudentNumber_TextBox.Text + "'", con);
                command.ExecuteNonQuery();
                con.Close();
            }
            //Message Box showing the user the action is completed...
            MessageBox.Show("USER EDITED IN THE DATABASE!!!");
        }
        
        private void Student_Delete_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            StudentButtonReset();
            //SEARCH BUTTON changes color...
            Student_Delete_Button.BackColor = Color.LightCoral;
            Student_Delete_Button.ForeColor = Color.Black;
            //Database...
            if (Student_StudentNumber_TextBox.Text != "")
            {
                con.Open();
                MySqlCommand command = new MySqlCommand("Delete from users where student_number = '" + Student_StudentNumber_TextBox.Text + "'", con);
                command.ExecuteNonQuery();
                con.Close();
                //Message Box showing the user the action is completed...
                MessageBox.Show("USER DELETED FROM THE DATABASE!!!");
            }
            else
            {
                MessageBox.Show("Please enter a student number");
            }
            //Message Box showing the user the action is completed...
            MessageBox.Show("USER DELETED FROM THE DATABASE!!!");
        }
        
        private void Treatment_Add_Add_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            TreatmentButtonReset();
            //ADD BUTTON changes color...
            Treatment_Add_Add_Button.BackColor = Color.LightCyan;
            Treatment_Add_Add_Button.ForeColor = Color.DarkCyan;
            //Saves to the DATABASE...
            DateTime time = DateTime.Now;
            string format = "yyyy-MM-dd HH:mm:ss";
            con.Open();
            MySqlCommand command = new MySqlCommand("Insert into treatment (student_number, curr_date, diagnosis_type, treatment_notes, medicine_name, amount_dispensed, measurement, next_appointment, issued_by) values ('" + Treatment_Add_StudentNumber_TextBox.Text + "','" + DateTime.Now.Date.ToString("yyyyMMdd") + "','" + Treatment_Add_Diagnosis_ComboBox.Text + "','" + Treatment_Add_TreatmentNotes_TextBox.Text + "','" + Treatment_Add_MedicineName_ComboBox.Text + "','" + Treatment_Add_AmountDispensed_TextBox.Text + "','" + Treatment_Add_Measurement_ComboBox.Text + "','" + Treatment_Add_NextAppointment_DateTimePicker.Value.Date.ToString("yyyyMMdd") + "','" + Treatment_Add_IssuedBy_TextBox.Text + "')", con);
            command.ExecuteNonQuery();
            MySqlCommand command2 = new MySqlCommand("Update medicine set stock_amount = medicine.stock_amount - '" + Treatment_Add_AmountDispensed_TextBox.Text + "'where medicine_name = '" + Treatment_Add_MedicineName_ComboBox.Text + "'", con);
            command2.ExecuteNonQuery();
            con.Close();
            //Message Box showing the user the action is completed...
            MessageBox.Show("TREATMENT HAS BEEN ADDED!!!");
        }
        
        private void Treatment_Search_Search_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            TreatmentButtonReset();
            //SEARCH BUTTON changes colour...
            Treatment_Search_Search_Button.BackColor = Color.LightCyan;
            Treatment_Search_Search_Button.ForeColor = Color.DarkCyan;
            //SEARCHES THE DATABASE and DISPLAYS...  
            con.Open();
            MySqlCommand command = new MySqlCommand("Select curr_date, diagnosis_type, treatment_notes, medicine_name, amount_dispensed, measurement from treatment where student_number = '" + Treatment_Search_StudentNumber_TextBox.Text + "'", con);
            MySqlDataAdapter sd = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            Treatment_Search_View.DataSource = dt;
            con.Close();
        }
        
        private void Diagnosis_Add_Add_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            DiagnosisButtonReset();
            //SEARCH BUTTON changes colour...
            Diagnosis_Add_Add_Button.BackColor = Color.LightCyan;
            Diagnosis_Add_Add_Button.ForeColor = Color.DarkCyan;
            //ADDS to the DATABASE...
            con.Open();
            MySqlCommand command = new MySqlCommand("Insert into diagnosis (diagnosis_type) values ('" + Diagnosis_Add_DiagnosisName_TextBox.Text + "')", con);
            command.ExecuteNonQuery();
            con.Close();
            //Message Box showing the user the action is completed...
            MessageBox.Show("DIAGNOSIS ADDED TO THE DATABASE!!!");
        } 
        
        string diagnosis;

        private void Diagnosis_Add_Edit_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            DiagnosisButtonReset();
            //SEARCH BUTTON changes colour...
            Diagnosis_Add_Edit_Button.BackColor = Color.LightCyan;
            Diagnosis_Add_Edit_Button.ForeColor = Color.DarkCyan;
            // EDITS ITEM FROM THE DATABASE...         
            con.Open();
            MySqlCommand command = new MySqlCommand("Update diagnosis set diagnosis_type = '" + Diagnosis_Add_Edit_ComboBox.Text +"' WHERE diagnosis_type ='"+ diagnosis +"'", con);
            command.ExecuteNonQuery();
            con.Close();
            //Message Box showing the user the action is completed...
            MessageBox.Show("ITEM HAS BEEN EDITED IN THE DATABASE!!!");
        } 
        
        private void Diagnosis_Add_Edit_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            diagnosis = Diagnosis_Add_Edit_ComboBox.Text;
        }
        
        private void Diagnosis_Add_Delete_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            DiagnosisButtonReset();
            //SEARCH BUTTON changes colour...
            Diagnosis_Add_Delete_Button.BackColor = Color.LightCoral;
            Diagnosis_Add_Delete_Button.ForeColor = Color.Black;
            // DELETES ITEM FROM THE DATABASE...
            con.Open();
            MySqlCommand command = new MySqlCommand("Delete from diagnosis where diagnosis_type = '" + Diagnosis_Add_Edit_ComboBox.Text + "'", con);
            command.ExecuteNonQuery();
            con.Close();
            //Message Box showing the user the action is completed...
            MessageBox.Show("ITEM HAS BEEN DELETED FROM THE DATABASE!!!");
        }
        
        private void Diagnosis_Statistic_Search_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            DiagnosisButtonReset();
            //SEARCH BUTTON changes colour...
            Diagnosis_Statistic_Search_Button.BackColor = Color.LightCyan;
            Diagnosis_Statistic_Search_Button.ForeColor = Color.DarkCyan;
            //SEARCHES THE DATABASE and DISPLAYS the NUMBER...   
            con.Open();
            MySqlCommand command = new MySqlCommand("Select count(*) from treatment where diagnosis_type = '" + Diagnosis_Statistic_DiagnosisType_ComboBox.Text + "' and curr_date between '" + Diagnosis_Statistic_StartDate_DateTimePicker.Value.Date.ToString("yyyyMMdd") + "' and '" + Diagnosis_Statistic_EndDate_DateTimePicker.Value.Date.ToString("yyyyMMdd") + "'", con);
            var countDiagnosis = command.ExecuteScalar();
            Diagnosis_Statistic_Label_5.Text = countDiagnosis.ToString();
            con.Close();
        }

        private void Medicine_Add_Add_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            MedicineButtonReset();
            //ADD BUTTON changes colour...
            Medicine_Add_Add_Button.BackColor = Color.LightCyan;
            Medicine_Add_Add_Button.ForeColor = Color.DarkCyan;
            // ADDS ITEM TO THE DATABASE...
            DateTime time = DateTime.Now;
            string format = "yyyy-MM-dd HH:mm:ss";
            con.Open();
            MySqlCommand command = new MySqlCommand("Insert into medicine (medicine_name, stock_amount, measurement, expiry_date, batch_number) values ('" + Medicine_Add_MedicineName_TextBox.Text + "','" + Medicine_Add_StockAmount_TextBox.Text + "','" + Medicine_Add_Measurement_ComboBox.Text + "','" + Medicine_Add_ExpiryDate_TimeDatePicker.Value.Date.ToString("yyyyMMdd") + "','" + Medicine_Add_BatchNumber_TextBox.Text + "')", con);
            command.ExecuteNonQuery();
            con.Close();
            //Message Box showing the user the action is completed...
            MessageBox.Show("ITEM HAS BEEN ADDED TO THE DATABASE!!!");
        }

        private void Medicine_Edit_Edit_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            MedicineButtonReset();
            //EDIT BUTTON changes colour...
            Medicine_Edit_Edit_Button.BackColor = Color.LightCyan;
            Medicine_Edit_Edit_Button.ForeColor = Color.DarkCyan;
            // ITEM HAS BEEN EDITIED IN DATABASE...
            con.Open();
            MySqlCommand command = new MySqlCommand("Update medicine set stock_amount = '" + Medicine_Edit_StockAmount_TextBox.Text + "', measurement = '" + Medicine_Edit_Measurement_ComboBox.Text + "', expiry_date = '" + Medicine_Edit_ExpiryDate_DateTimePicker.Value.Date.ToString("yyyyMMdd") + "', batch_number = '" + Medicine_Edit_BatchNumber_TextBox.Text + "'where medicine_name = '" + Medicine_Edit_MedicineName_ComboBox.Text + "'", con);
            command.ExecuteNonQuery();
            con.Close();
            //Message Box showing the user the action is completed...
            MessageBox.Show("ITEM HAS BEEN EDITIED IN THE DATABASE!!!");
        }

        private void Medicine_Edit_Delete_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            MedicineButtonReset();
            //DELETE BUTTON changes colour...
            Medicine_Edit_Delete_Button.BackColor = Color.LightCoral;
            Medicine_Edit_Delete_Button.ForeColor = Color.Black;
            // ITEM HAS BEEN DELETED FROM THE DATABASE...
            con.Open();
            MySqlCommand command = new MySqlCommand("Delete from medicine where medicine_name = '" + Medicine_Edit_MedicineName_ComboBox.Text + "'", con);
            command.ExecuteNonQuery();
            con.Close();
            //Message Box showing the user the action is completed...
            MessageBox.Show("ITEM HAS BEEN DELETED FROM THE DATABASE!!!");
        }
        
        private void Medicine_Edit_MedicineName_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Database...
            con.Close();
            con.Open();
            MySqlCommand command = new MySqlCommand("Select stock_amount, measurement, expiry_date, batch_number from medicine where medicine_name = '" + Medicine_Edit_MedicineName_ComboBox.Text + "'", con);
            MySqlDataReader srd = command.ExecuteReader();
            while (srd.Read())
            {
                Medicine_Edit_StockAmount_TextBox.Text = srd.GetValue(0).ToString();
                Medicine_Edit_Measurement_ComboBox.Text = srd.GetValue(1).ToString();
                Medicine_Edit_ExpiryDate_DateTimePicker.Text = srd.GetValue(2).ToString();
                Medicine_Edit_BatchNumber_TextBox.Text = srd.GetValue(3).ToString();
            }
            con.Close();
        }
        
        private void Treatment_Add_MedicineName_ComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //Database...
            con.Close();
            con.Open();
            MySqlCommand command = new MySqlCommand("Select measurement from medicine where medicine_name = '" + Treatment_Add_MedicineName_ComboBox.Text + "'", con);
            MySqlDataReader srd = command.ExecuteReader();
            while (srd.Read())
            {
                Treatment_Add_Measurement_ComboBox.Text = srd.GetValue(0).ToString();
            }
            con.Close();
        }
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        public void UpcomingAppointments() 
        {
            try
            {
                UserCredential credential;
                // Load client secrets.
                using (var stream = new System.IO.FileStream("client_secret_799015457665-9ck91256drtu81lgksedu7kh8m1lahch.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
                {
                    /* The file token.json stores the user's access and refresh tokens, and is created
                     automatically when the authorization flow completes for the first time. */
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                }

                // Create Google Calendar API service.
                var service = new CalendarService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName
                });

                // Define parameters of request.
                EventsResource.ListRequest request = service.Events.List("primary");
                request.TimeMin = DateTime.Now;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 10;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;


                // List events.
                Events events = request.Execute();
                if (events.Items != null && events.Items.Count > 0)
                {
                    AppointmentLabel.Text = "";
                    foreach (var eventItem in events.Items)
                    {
                        AppointmentLabel.Text += eventItem.Start.DateTime + " \t " +eventItem.Summary.Remove(eventItem.Summary.Length - 17) + Environment.NewLine;
                    }
                }
                else
                {
                    AppointmentLabel.Text = "No Upcoming Events";
                }
            }
            catch
            {
                AppointmentLabel.Text = "Error";
            }
        }

        private void Notification_Timer_Tick(object sender, EventArgs e)
        {
            UpcomingAppointments();
        }

public void ClearStudent()
        {
            Student_StudentNumber_TextBox.Text = "";
            Student_FirstName_TextBox.Text = "";
            Student_LastName_TextBox.Text = "";
            Student_Address_TextBox.Text = "";
            Student_Email_TextBox.Text = "";
            Student_PhoneNumber_TextBox.Text = "";
            Student_Gender_Male_RadioButton.Checked = false;
            Student_Gender_Female_RadioButton.Checked = false;
            Student_DateOfBirth_DateTimePicker.Text = "";
            Student_Allergies_TextBox.Text = "";
        }

        public void ClearTreatmentSearch()
        {
            Treatment_Search_StudentNumber_TextBox.Text = "";
            Treatment_Search_View.DataSource = null;
        }

        public void ClearTreatmentAdd()
        {
            Treatment_Add_StudentNumber_TextBox.Text = "";
            Treatment_Add_CurrentDate_DateTimePicker.Text = "";
            Treatment_Add_Diagnosis_ComboBox.Text = "";
            Treatment_Add_TreatmentNotes_TextBox.Text = "";
            Treatment_Add_MedicineName_ComboBox.Text = "";
            Treatment_Add_AmountDispensed_TextBox.Text = "";
            Treatment_Add_Measurement_ComboBox.Text = "";
            Treatment_Add_NextAppointment_DateTimePicker.Text = "";
            Treatment_Add_IssuedBy_TextBox.Text = "";
        }

        public void ClearDiagnosisAdd()
        {
            Diagnosis_Add_DiagnosisName_TextBox.Text = "";
        }

        public void ClearDiagnosisStatistics()
        {
            Diagnosis_Statistic_DiagnosisType_ComboBox.Text = "";
            Diagnosis_Statistic_StartDate_DateTimePicker.Text = "";
            Diagnosis_Statistic_EndDate_DateTimePicker.Text = "";
            Diagnosis_Statistic_Label_5.Text = "0";
        }

        public void ClearDiagnosisView()
        {
            Diagnosis_ViewAll.DataSource = null;
        }

        public void ClearMedicineAdd()
        {
            Medicine_Add_MedicineName_TextBox.Text = "";
            Medicine_Add_StockAmount_TextBox.Text = "";
            Medicine_Add_Measurement_ComboBox.Text = "";
            Medicine_Add_ExpiryDate_TimeDatePicker.Text = "";
            Medicine_Add_BatchNumber_TextBox.Text = "";
        }

        public void ClearMedicineEdit()
        {
            Medicine_Edit_MedicineName_ComboBox.Text = "";
            Medicine_Edit_StockAmount_TextBox.Text = "";
            Medicine_Edit_Measurement_ComboBox.Text = "";
            Medicine_Edit_ExpiryDate_DateTimePicker.Text = "";
            Medicine_Edit_BatchNumber_TextBox.Text = "";
        }

        public void ClearMedicineView()
        {
            Medicine_ViewAll.DataSource = null;
        }

        public void ClearAll()
        {
            ClearStudent();
            ClearTreatmentAdd();
            ClearTreatmentSearch();
            ClearMedicineAdd();
            ClearMedicineEdit();
            ClearMedicineView();
            ClearDiagnosisAdd();
            ClearDiagnosisStatistics();
            ClearDiagnosisView();
        }

        public void LoadMeasurment()
        {
            Medicine_Add_Measurement_ComboBox.Items.Clear();
            Medicine_Add_Measurement_ComboBox.Items.Add("mcg (microgram)");
            Medicine_Add_Measurement_ComboBox.Items.Add("mg (milligram)");
            Medicine_Add_Measurement_ComboBox.Items.Add("g (gram)");
            Medicine_Add_Measurement_ComboBox.Items.Add("ml (millilitre)");
            Medicine_Add_Measurement_ComboBox.Items.Add("tablet/s");
            Medicine_Edit_Measurement_ComboBox.Items.Clear();
            Medicine_Edit_Measurement_ComboBox.Items.Add("mcg (microgram)");
            Medicine_Edit_Measurement_ComboBox.Items.Add("mg (milligram)");
            Medicine_Edit_Measurement_ComboBox.Items.Add("g (gram)");
            Medicine_Edit_Measurement_ComboBox.Items.Add("ml (millilitre)");
            Medicine_Edit_Measurement_ComboBox.Items.Add("tablet/s");
            Treatment_Add_Measurement_ComboBox.Items.Clear();
            Treatment_Add_Measurement_ComboBox.Items.Add("mcg (microgram)");
            Treatment_Add_Measurement_ComboBox.Items.Add("mg (milligram)");
            Treatment_Add_Measurement_ComboBox.Items.Add("g (gram)");
            Treatment_Add_Measurement_ComboBox.Items.Add("ml (millilitre)");
            Treatment_Add_Measurement_ComboBox.Items.Add("tablet/s");
        }
        //END...       
    }
}
