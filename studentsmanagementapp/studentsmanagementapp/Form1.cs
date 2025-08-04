using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace studentsmanagementapp
{
    public partial class studentmanagement : Form
    {
        public class Etudiant
        {
            public int CodeMassar;
            public string Nom;
            public string Prenom;
            public int Age;
            public float Note;
        }
        private List<Etudiant> listeEtudiants = new List<Etudiant>();
        public List<Etudiant> Etudiants
        {
            get { return listeEtudiants; }
        }
        private const int MaxEtudiants = 1000;
        private const string Filename = "etudiants.txt";

        public studentmanagement()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void studentmanagement_Load(object sender, EventArgs e)
        {
            addbox.Visible = false;
            modifiebox.Visible = false;
            showbox.Visible = false;
            deletebox.Visible = false;

        }

        private void add_Click(object sender, EventArgs e)
        {
            addbox.Visible = true;
            modifiebox.Visible = false;
            showbox.Visible = false;
            deletebox.Visible = false;
        }

        private void submet_Click(object sender, EventArgs e)
        {

            Etudiant nouvelEtudiant = new Etudiant();
            nouvelEtudiant.CodeMassar = int.Parse(code.Text);
            nouvelEtudiant.Nom = lastname.Text;
            nouvelEtudiant.Prenom = firstname.Text;
            nouvelEtudiant.Age = int.Parse(age.Text);
            nouvelEtudiant.Note = float.Parse(note.Text);

            listeEtudiants.Add(nouvelEtudiant);
            MessageBox.Show("The student was added successfully.");

            code.Clear();
            lastname.Clear();
            firstname.Clear();
            age.Clear();
            note.Clear();

        }

        private void modifie_Click(object sender, EventArgs e)
        {
            modifiebox.Visible = true;
            addbox.Visible = false;
            showbox.Visible = false;
            deletebox.Visible = false;
        }
        private int currentIndex = 0;
        private void show_Click(object sender, EventArgs e)
        {
            showbox.Visible = true;
            addbox.Visible = false;
            modifiebox.Visible = false;
            deletebox.Visible = false;
            if (listeEtudiants.Count > 0)
            {
                DisplayStudent(currentIndex);
                showbox.Visible = true;
                // You would also need "Next" and "Previous" buttons and their click handlers
            }
            else
            {
                MessageBox.Show("No student has been added yet.");
                showbox.Visible = false;
            }
        }

        private void DisplayStudent(int index)
        {
            if (index >= 0 && index < listeEtudiants.Count)
            {
                Etudiant student = listeEtudiants[index];
                showcode.Text = student.CodeMassar.ToString();
                showlastname.Text = student.Nom;
                showfirstname.Text = student.Prenom;
                showage.Text = student.Age.ToString();
                shownote.Text = student.Note.ToString("F2");
            }

        }

        private void next_Click(object sender, EventArgs e)
        {
            if (currentIndex < listeEtudiants.Count - 1)
            {
                currentIndex++;
                DisplayStudent(currentIndex);
            }
        }

        private void previous_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                DisplayStudent(currentIndex);
            }

        }
        Etudiant studentToModify;
        private void ok_Click(object sender, EventArgs e)
        {
            foreach (var student in listeEtudiants)
            {
                if (student.CodeMassar.ToString() == oldcode.Text)
                {
                    studentToModify = student;

                    // Fill fields
                    newcode.Text = student.CodeMassar.ToString();
                    newlname.Text = student.Nom;
                    newfname.Text = student.Prenom;
                    newage.Text = student.Age.ToString();
                    newnote.Text = student.Note.ToString();

                    // Enable fields
                    newcode.Enabled = true;
                    newlname.Enabled = true;
                    newfname.Enabled = true;
                    newage.Enabled = true;
                    newnote.Enabled = true;
                    newsubmet.Enabled = true;
                    return;
                }
            }

            MessageBox.Show("Student not found.");
            ClearModificationFields();


        }



        private void ClearModificationFields()
        {
            newcode.Clear();
            newlname.Clear();
            newfname.Clear();
            newage.Clear();
            newnote.Clear();
            newlname.Enabled = false;
            newfname.Enabled = false;
            newage.Enabled = false;
            newnote.Enabled = false;
            newsubmet.Enabled = false;

        }

        private void newsubmet_Click(object sender, EventArgs e)
        {
            if (studentToModify != null)
            {
                // Try to convert age and note
                if (int.TryParse(newage.Text, out int age) &&
                    float.TryParse(newnote.Text, out float note) &&
                    int.TryParse(newcode.Text, out int code))
                {
                    // Update values
                    studentToModify.CodeMassar = code;
                    studentToModify.Nom = newlname.Text;
                    studentToModify.Prenom = newfname.Text;
                    studentToModify.Age = age;
                    studentToModify.Note = note;

                    MessageBox.Show("Student updated.");

                    // Update display if visible
                    if (showbox.Visible)
                    {
                        showcode.Text = studentToModify.CodeMassar.ToString();
                        showlastname.Text = studentToModify.Nom;
                        showfirstname.Text = studentToModify.Prenom;
                        showage.Text = studentToModify.Age.ToString();
                        shownote.Text = studentToModify.Note.ToString("F2");
                    }

                    ClearModificationFields();
                }
                else
                {
                    MessageBox.Show("Please enter valid numbers for age, note, and code.");
                }
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            showbox.Visible = false;
            addbox.Visible = false;
            modifiebox.Visible = false;
            deletebox.Visible = true;
        }

        private void donedelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(codetodelete.Text, out int codeMassar))
            {
                int initialCount = listeEtudiants.Count;
                listeEtudiants.RemoveAll(student => student.CodeMassar == codeMassar);

                if (listeEtudiants.Count < initialCount)
                {
                    MessageBox.Show($"the student with {codeMassar} has been deleted");
                }
                else
                {
                    MessageBox.Show("theres no student with that code.");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void search_Click(object sender, EventArgs e)
        {
            seachform searchForm = new seachform(this);
            searchForm.Show();
        }
        
        private void import_Click(object sender, EventArgs e)
        {
            listeEtudiants.Clear(); // Clear the current list before importing
            try
            {
                if (File.Exists(Filename))
                {
                    string[] lines = File.ReadAllLines(Filename);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(';');
                        if (parts.Length == 5)
                        {
                            if (int.TryParse(parts[0], out int codeMassar) &&
                                int.TryParse(parts[3], out int age) &&
                                float.TryParse(parts[4], NumberStyles.Float, CultureInfo.InvariantCulture, out float note))
                            {
                                listeEtudiants.Add(new Etudiant
                                {
                                    CodeMassar = codeMassar,
                                    Nom = parts[1],
                                    Prenom = parts[2],
                                    Age = age,
                                    Note = note
                                });
                            }
                            else
                            {
                                MessageBox.Show($"Skipping invalid line: {line}", "Import Warning");
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Skipping malformed line: {line}", "Import Warning");
                        }
                    }
                    MessageBox.Show($"Successfully imported {listeEtudiants.Count} students.", "Import Success");
                    // You might want to refresh your "show" section here to display the imported data
                }
                else
                {
                    MessageBox.Show($"File not found: {Filename}", "Import Warning");
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error reading from file: {ex.Message}", "Import Error");
            }


            currentIndex = 0; // Reset the index to the beginning of the list
            if (listeEtudiants.Count > 0)
            {
                DisplayStudent(currentIndex);
            }
            else
            {
               
                DisplayStudent(-1);
            }

        }

        private void export_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(Filename, false)) // 'false' overwrites the file
                {
                    foreach (var etudiant in listeEtudiants)
                    {
                        writer.WriteLine($"{etudiant.CodeMassar};{etudiant.Nom};{etudiant.Prenom};{etudiant.Age};{etudiant.Note.ToString(CultureInfo.InvariantCulture)}");
                    }
                }
                MessageBox.Show($"Student list exported successfully to {Filename}", "Export Success");
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error writing to file: {ex.Message}", "Export Error");
            }
        }
    }
}


