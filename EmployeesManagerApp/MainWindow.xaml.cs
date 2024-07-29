
using BL;
using DAL.Contex;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace UI
{
    public partial class MainWindow : Window
    {
        InterviewsManagerBL interviewsManager;
        ObservableCollection<Employee> employeesList;

        

        public MainWindow()
        {
            InitializeComponent();
            interviewsManager = new InterviewsManagerBL();
            employeesList = new ObservableCollection<Employee>();
            PopulateComboBox();
            DisplayEmployees();

            // Subscribe to the event in the constructor
            AddEmployee addEmployeeForm = new AddEmployee();
            addEmployeeForm.EmployeeAdded += AddEmployeeEventHandler;
        }
        private void PopulateComboBox()
        {
            IEnumerable<string> uniqueRoles = interviewsManager.GetRoleInCompanyWithoutRepetitions();
            ComboBoxFilterCategory.ItemsSource = uniqueRoles;
        }

        private void DisplayEmployees()
        {
            employeesList.Clear();
            foreach (Employee emp in interviewsManager.GetAllEmployees())
            {
                employeesList.Add(emp);
            }
            CandidateDataGrid.ItemsSource = employeesList;
        }

        private void ComboBoxFilterCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedRole = ComboBoxFilterCategory.SelectedItem as string;
            employeesList.Clear();
            foreach (Employee emp in interviewsManager.GetAllEmployees())
            {
                if (emp.RoleInCompany == selectedRole)
                {
                    employeesList.Add(emp);
                }
            }
            CandidateDataGrid.ItemsSource = employeesList;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxFilterOptions.Items.Add("city");
            ComboBoxFilterOptions.Items.Add("age");
            ComboBoxFilterOptions.Items.Add("start of work");
        }

        private void ComboBoxFilterOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxFilterOptions.SelectedItem != null)
            {
                string selectedOption = ComboBoxFilterOptions.SelectedItem.ToString();
                ClearAllFilters();

                switch (selectedOption)
                {
                    case "city":
                        // הוסף פילטר הראשון
                        interviewsManager.GetCity();
                        break;

                    case "age":
                        // הוסף פילטר השני
                        ComboBoxFilterOptions.Items.Add("20-30");
                        ComboBoxFilterOptions.Items.Add("30-40");
                        ComboBoxFilterOptions.Items.Add("40-50");
                        break;

                    case "start of work":
                        // הוסף פילטר השלישי
                        interviewsManager.GetStartYear();
                        break;

                    default:
                        break;
                }
            }
        }
        //
        private void ClearAllFilters()
        {
            ComboBoxFilterOptions.SelectedItem = null; // מנקה את ה-ComboBox הראשון
          
        }

    
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployeeWin = new AddEmployee();
            addEmployeeWin.ShowDialog();
        }

        private bool AddEmployeeEventHandler(Employee employee)
        {
            if(interviewsManager.Addemployee(employee))
            {
                DisplayEmployees();
                return true;
            }

            return false;
        }

   
    }
}
