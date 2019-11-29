using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;

namespace PrototiposPoltran
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            if(CorrectUser.IsVisible && CorrectPass.IsVisible)
            {
                BDUsu us = new BDUsu();
                String cod = us.Login(txtUsuario.Text, txtContraseña.Password);
                if(cod != null)
                {
                    VentanaPrincipal ventana = new VentanaPrincipal(cod);
                    ventana.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario y/o Contraseña incorrecto");
                }
                
            }
            else if(CorrectUser.IsVisible && txtContraseña.Password.Length < 5)
            {
                lblErrLogin.Content = "La contraseña es muy corta";
            }
            else
            {
                lblErrLogin.Content = "No se Ingreso usuario o Contraseña";
            }
        }


        private void txtUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtUsuario.Text != "")
            {
                CorrectUser.Visibility = Visibility.Visible;
            }
            else
            {
                CorrectUser.Visibility = Visibility.Hidden;
            }
        }

        private void txtContraseña_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtContraseña.Password  != "" && txtContraseña.Password.Length >= 5)
            {
                CorrectPass.Visibility = Visibility.Visible;
            }
            else
            {
                CorrectPass.Visibility = Visibility.Hidden;
            }
        }
        public static string EncodePassword(string originalPassword)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }
    }

}
