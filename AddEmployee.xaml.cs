using BL;
using DAL.Contex;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using static UI.MainWindow;

namespace UI
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        public delegate bool AddEmployeeEventHandler(Employee employee);
        InterviewsManagerBL interviewsManager;
        public AddEmployee()
        {
            InitializeComponent();
            interviewsManager = new InterviewsManagerBL();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateData())
            {
                Employee employee = new Employee();
                employee.Id = int.Parse(IdTextBox.Text);
                employee.Email= MailAddressTextBox.Text;
                employee.FirstName=FirstNameTextBox.Text;
                employee.LastName=LastNameTextBox.Text;
                employee.Street=StreetAddressTextBox.Text;
                employee.PhoneNumber=PhoneNumberTextBox.Text;
                employee.StartOfWorkYear = int.Parse(StartOfWorkingYearTextBox.Text);
                employee.RoleInCompany = JobTitleTextBox.Text;
                employee.City= CityAddressTextBox.Text;
                employee.Age = int.Parse(AgeTextBox.Text);
                bool isEmployeeAdded = EmployeeAdded?.Invoke(employee) ?? false;
                if (isEmployeeAdded)
                {
                    MessageBox.Show("Employee added successfully!");
                    this.Close();
                }
            }
        }
        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
        private bool ValidateData()
        {
            // Data validation logic
            if (!Regex.IsMatch(IdTextBox.Text, @"^\d{9}"))
            {
                MessageBox.Show("ID field must have 9 digits.");
                return false;
            }

            if (!Regex.IsMatch(FirstNameTextBox.Text, @"^[A-Za-z]{2,}") || !Regex.IsMatch(LastNameTextBox.Text, @"^[A-Za-z]{2,}"))
            {
                MessageBox.Show("First name and last name must contain at least 2 letters without numbers.");
                return false;
            }

            if (!int.TryParse(AgeTextBox.Text, out int age) || age < 18 || age > 67)
            {
                MessageBox.Show("Age field must be a number between 18 and 67.");
                return false;
            }

            if (!Regex.IsMatch(StartOfWorkingYearTextBox.Text, @"^\d{4}"))
            {
                MessageBox.Show("Start of working year field must be a 4 - digit year.");
                return false;
            }



            IEnumerable<string> RoleInCompany = interviewsManager.GetRoleInCompanyWithoutRepetitions();
            Regex regex = new Regex(JobTitleTextBox.Text);
            bool temp = false;
            foreach (string roleInCompany in RoleInCompany)
            {
                
                if (regex.IsMatch(roleInCompany))
                {
                    temp = true;
                    break;
                   
                   
                }
            

            }
            if(temp==false)
            {
                MessageBox.Show("role not found");
                return false;
            }



            // Example validation for Phone Number
            if (!Regex.IsMatch(PhoneNumberTextBox.Text, @"^\d{10}"))
            {
                MessageBox.Show("Phone number must be a 10 - digit number.");
                return false;
            }

            // Example validation for Email Address
            if (!Regex.IsMatch(MailAddressTextBox.Text, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}"))
            {
                MessageBox.Show("Invalid email address.");
                return false;
            }

            return true;
        }

        
        public event AddEmployeeEventHandler EmployeeAdded;
        private void IdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}
