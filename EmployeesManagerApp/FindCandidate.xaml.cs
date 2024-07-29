using BL;
using DAL.Contex;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for FindCandidate.xaml
    /// </summary>
    public partial class FindCandidate : Window
    {
        InterviewsManagerBL interviewsManager;
        List<string> CandidateList = new List<string>();
        public FindCandidate()
        {
            InitializeComponent();
            interviewsManager = new InterviewsManagerBL();
            var candidate = interviewsManager.GetNameOfCandidate();
            ComboboxChooseCandidate.ItemsSource = candidate;
        }

        //private void ComboboxChooseCandidate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    string selectedCandidate = ComboboxChooseCandidate.SelectedItem as string;
        //    DataGridInterviews.Items.Clear();

        //    foreach (string ce in interviewsManager.GetNameOfCandidate())
        //    {
               
        //            CandidateList.Add(ce);
                
        //    }
        //}
        private void ComboboxChooseCandidate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var interview = interviewsManager.GetInterviewsDetails(ComboboxChooseCandidate.SelectedItem.ToString());
            DataGridInterviews.ItemsSource = interview;
        }
    }
}
