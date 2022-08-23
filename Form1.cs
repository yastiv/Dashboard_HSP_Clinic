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
