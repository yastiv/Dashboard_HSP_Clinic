﻿using System;
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
                      
        }
        
        private void Student_Search_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            StudentButtonReset();
            //SEARCH BUTTON changes color...
            Student_Search_Button.BackColor = Color.LightCyan;
            Student_Search_Button.ForeColor = Color.DarkCyan;
            //SYSTEM Should SEARCH THROUGH DATABASE TO FIND STUDENT NUMBER AND OTHER INFORMATION for display in these items...
           
        }
        
        private void Student_Edit_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            StudentButtonReset();
            //SEARCH BUTTON changes color...
            Student_Edit_Button.BackColor = Color.LightCyan;
            Student_Edit_Button.ForeColor = Color.DarkCyan;
            //Database...
            
        }
        
        private void Student_Delete_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            StudentButtonReset();
            //SEARCH BUTTON changes color...
            Student_Delete_Button.BackColor = Color.LightCoral;
            Student_Delete_Button.ForeColor = Color.Black;
            //Database...
            
        }
        
        private void Treatment_Add_Add_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            TreatmentButtonReset();
            //ADD BUTTON changes color...
            Treatment_Add_Add_Button.BackColor = Color.LightCyan;
            Treatment_Add_Add_Button.ForeColor = Color.DarkCyan;
            //Saves to the DATABASE...
            
        }
        
        private void Treatment_Search_Search_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            TreatmentButtonReset();
            //SEARCH BUTTON changes colour...
            Treatment_Search_Search_Button.BackColor = Color.LightCyan;
            Treatment_Search_Search_Button.ForeColor = Color.DarkCyan;
            //SEARCHES THE DATABASE and DISPLAYS...  
            
        }
        
        private void Diagnosis_Add_Add_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            DiagnosisButtonReset();
            //SEARCH BUTTON changes colour...
            Diagnosis_Add_Add_Button.BackColor = Color.LightCyan;
            Diagnosis_Add_Add_Button.ForeColor = Color.DarkCyan;
            //ADDS to the DATABASE...
            
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
           
        }
        
        private void Diagnosis_Statistic_Search_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            DiagnosisButtonReset();
            //SEARCH BUTTON changes colour...
            Diagnosis_Statistic_Search_Button.BackColor = Color.LightCyan;
            Diagnosis_Statistic_Search_Button.ForeColor = Color.DarkCyan;
            //SEARCHES THE DATABASE and DISPLAYS the NUMBER...   
           
        }

        private void Medicine_Add_Add_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            MedicineButtonReset();
            //ADD BUTTON changes colour...
            Medicine_Add_Add_Button.BackColor = Color.LightCyan;
            Medicine_Add_Add_Button.ForeColor = Color.DarkCyan;
            // ADDS ITEM TO THE DATABASE...
          
        }

        private void Medicine_Edit_Edit_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            MedicineButtonReset();
            //EDIT BUTTON changes colour...
            Medicine_Edit_Edit_Button.BackColor = Color.LightCyan;
            Medicine_Edit_Edit_Button.ForeColor = Color.DarkCyan;
            // ITEM HAS BEEN EDITIED IN DATABASE...
            
        }

        private void Medicine_Edit_Delete_Button_Click(object sender, EventArgs e)
        {
            //Resets all button colours on the page...
            MedicineButtonReset();
            //DELETE BUTTON changes colour...
            Medicine_Edit_Delete_Button.BackColor = Color.LightCoral;
            Medicine_Edit_Delete_Button.ForeColor = Color.Black;
            // ITEM HAS BEEN DELETED FROM THE DATABASE...
            
        }
        
        private void Medicine_Edit_MedicineName_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Database...
            
        }
        
        private void Treatment_Add_MedicineName_ComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //Database...
            
        }
        
        //END...       
    }
}