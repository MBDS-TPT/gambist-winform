using gambistWinForm.Models;
using gambistWinForm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gambistWinForm
{
    public partial class Login : Form
    {
        UserServices UserServices = new UserServices();

        public Login()
        {
            InitializeComponent();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                labelOperation.Text = "Opération en cours...";
                labelErrorMessage.Text = string.Empty;
                if (string.IsNullOrEmpty(emailTextBox.Text) && string.IsNullOrEmpty(passwordTextBox.Text))
                {
                    labelOperation.Text = string.Empty;
                    MessageBox.Show("Veuillez remplir les champs Email et Mot de passe");
                }
                else if (string.IsNullOrEmpty(emailTextBox.Text))
                {
                    labelOperation.Text = string.Empty;
                    MessageBox.Show("Veuillez remplir le champs Email");
                }
                else if (string.IsNullOrEmpty(passwordTextBox.Text))
                {
                    labelOperation.Text = string.Empty;
                    MessageBox.Show("Veuillez remplir le champs Mot de passe");
                }
                else
                {
                    if (UserServices.Login(new LoginModel() { username = emailTextBox.Text, password = passwordTextBox.Text }))
                    {
                        labelOperation.Text = string.Empty;
                        emailTextBox.Text = string.Empty;
                        passwordTextBox.Text = string.Empty;
                        this.Hide();
                        var accueilForm = new Accueil();
                        accueilForm.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        labelOperation.Text = string.Empty;
                        labelErrorMessage.Text = "Authentification échouée";
                    }
                }
            }
            catch (Exception ex)
            {
                labelOperation.Text = string.Empty;
                labelErrorMessage.Text = "Authentification échouée: \r\n" + ex.Message;
            }
        }
    }
}
