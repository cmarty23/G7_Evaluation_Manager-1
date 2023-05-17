using Evaluation_Manager.Models;
using Evaluation_Manager.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evaluation_Manager
{
    public partial class FrmEvaluation : Form
    {
        private Student selectedStudent;

        public FrmEvaluation(Student selectedStudent)
        {
            InitializeComponent();
            this.selectedStudent = selectedStudent;
            this.Text = $"{selectedStudent.FirstName} {selectedStudent.LastName}";
        }

        private void FrmEvaluation_Load(object sender, EventArgs e)
        {
            PopulateActivities();
        }

        private void PopulateActivities()
        {
            cboActivities.DataSource = ActivityRepository.GetActivities();
        }

        private void cboActivities_SelectedIndexChanged(object sender, EventArgs e)
        {
            Activity selectedActivity = cboActivities.SelectedItem as Activity;
            txtActivityDescription.Text = selectedActivity.Description;
            txtMinForGrade.Text = selectedActivity.MinPointsForGrade.ToString();
            txtMinForSignature.Text = selectedActivity.MinPointsForSignature.ToString();

            Evaluation evaluation = EvaluationRepository.GetEvaluation(selectedStudent, selectedActivity);

            numPoints.Value = evaluation.Points;
            txtTeacher.Text = evaluation.Evaluator.ToString();
            txtEvaluationDate.Text = evaluation.EvaluationDateTime.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //TODO - Save scrore to DB

            this.Close();
        }
    }
}
