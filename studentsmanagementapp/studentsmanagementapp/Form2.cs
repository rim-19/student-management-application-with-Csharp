using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static studentsmanagementapp.studentmanagement;

namespace studentsmanagementapp
{
    public partial class seachform : Form
    {
        private studentmanagement _parentForm;
        public seachform(studentmanagement parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
        }

        private void seachform_Load(object sender, EventArgs e)
        {

        }

        private void searchok_Click(object sender, EventArgs e)
        {
            if (int.TryParse(searchcode.Text.Trim(), out int searchCode))
            {
                List<studentmanagement.Etudiant> studentList = _parentForm.Etudiants;
                studentmanagement.Etudiant foundStudent = default(studentmanagement.Etudiant); // Initialize with default value
                bool studentFound = false;

                foreach (var student in studentList)
                {
                    if (student.CodeMassar == searchCode)
                    {
                        foundStudent = student;
                        studentFound = true;
                        break; // Exit the loop once a student is found
                    }
                }

                // Update UI elements in SearchForm to display the result
                if (studentFound)
                {



                    // Assuming you have labels in SearchForm named:
                    // nameLabel, codeLabel, ageLabel, noteLabel
                    searchshowcode.Text = foundStudent.CodeMassar.ToString();
                    searchshowlname.Text = foundStudent.Nom;
                    searchshowfname.Text = foundStudent.Prenom;
                    
                    searchshowage.Text = foundStudent.Age.ToString();
                    searchshownote.Text = foundStudent.Note.ToString();
                }
                else
                {
                    // Display a "not found" message in the UI

                    MessageBox.Show("student not found"); 
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric Massar Code.", "Invalid Input");
            }
        }
    }
    }

