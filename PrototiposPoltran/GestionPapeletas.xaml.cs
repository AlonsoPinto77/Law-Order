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
using System.Collections.ObjectModel;
using System.Data;

namespace PrototiposPoltran
{
    /// <summary>
    /// Lógica de interacción para GestionPapeletas.xaml
    /// </summary>
    public partial class GestionPapeletas : UserControl
    {
        String str = "No se encuentra almacenado. Ingrese datos ...";
        BDPapeleta pap;
        BDCon con;
        BDVeh veh;
        DataTable dtInf;
        int tipo = 1;
        ScrollViewer scrollContenedor;
        public GestionPapeletas(ScrollViewer scroll)
        {
            InitializeComponent();

            this.scrollContenedor = scroll;
            this.cmbFisico.SelectedValuePath = "Key";
            this.cmbFisico.DisplayMemberPath = "Value";
            cmbFisico.Items.Add(new KeyValuePair<String, String>("C", "Correctamente llenado"));
            cmbFisico.Items.Add(new KeyValuePair<String, String>("X", "Eliminada"));
            cmbFisico.Items.Add(new KeyValuePair<String, String>("R", "Devuelta para Re-Asignacion"));

            BDInf inf = new BDInf();
            dtInf = inf.GetAll();
            cmbInfraccion.ItemsSource = dtInf.DefaultView;
            cmbInfraccion.DisplayMemberPath = "tipo_infraccion";
            cmbInfraccion.SelectedValuePath = "tipo_infraccion";

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            scrollContenedor = null;
        }

        private void btnGrabar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pap = new BDPapeleta();
                if (lblErrNroPap.Content.ToString() == "" && cmbInfraccion.SelectedIndex >= 0)
                {
                    if (pap.isEstado(txtNroPapeleta.Text))
                    {
                        switch (cmbFisico.SelectedValue.ToString())
                        {
                            case "C":
                                int cont = 0;
                                if (cont == 0)
                                {
                                    if (lblErrDNI.Content.ToString() == str && lblErrBrevete.Content.ToString() == str)
                                    {
                                        con = new BDCon();
                                        if (con.ingresarCon(txtIdConductor.Text, txtBrevete.Text, txtNombres.Text, txtApePat.Text, txtApeMat.Text))
                                        {
                                            cont++;
                                            lblErrDNI.Foreground = Brushes.Red;
                                            lblErrDNI.Content = "";
                                            lblErrBrevete.Foreground = Brushes.Red;
                                            lblErrBrevete.Content = "";
                                        }
                                        else
                                        {
                                            MessageBox.Show("Error: No guardaron los datos del conductor");
                                        }
                                    }
                                    else
                                    {
                                        if (lblErrDNI.Content.ToString() == "" && lblErrBrevete.Content.ToString() == "" && cont == 0)
                                        {
                                            cont++;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ingrese datos del conductor");
                                        }
                                    }
                                }
                                if (cont == 1)
                                {
                                    if (lblErrPlaca.Content.ToString() == str)
                                    {
                                        veh = new BDVeh();
                                        if (veh.ingresarVeh(txtPlaca.Text, txtClase.Text, txtSerie.Text))
                                        {
                                            lblErrPlaca.Foreground = Brushes.Red;
                                            lblErrPlaca.Content = "";
                                            cont++;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Error No se guadaron los datos del vehiculo");
                                        }
                                    }
                                    else
                                    {
                                        if (lblErrPlaca.Content.ToString() == "" && cont == 1)
                                        {
                                            cont++;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ingrese datos del placa");
                                        }
                                    }
                                }
                                if (cont == 2)
                                {
                                    pap = new BDPapeleta();
                                    DateTime dt = dpFechaImposicion.SelectedDate.Value;
                                    String fecha = dt.Year + "/" + dt.Month + "/" + dt.Day;
                                    dt = DateTime.Today;
                                    String tod = dt.Year + "/" + dt.Month + "/" + dt.Day;
                                    if (pap.DevolucionPapeleta(txtNroPapeleta.Text, cmbFisico.SelectedValue.ToString(), cmbInfraccion.SelectedValue.ToString(), fecha, tod, txtIdConductor.Text, txtPlaca.Text))
                                    {
                                        MessageBox.Show("Se guardaron los datos exitosamente!");
                                        txtNroPapeleta.Clear();
                                        txtIdConductor.Clear();
                                        txtNombres.Clear();
                                        txtApePat.Clear();
                                        txtApeMat.Clear();
                                        txtPlaca.Clear();
                                        txtClase.Clear();
                                        txtSerie.Clear();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error: No se guadaron los datos de papeleta intente de nuevo");
                                    }
                                }
                                break;
                            case "R":
                            case "X":
                                DateTime f = DateTime.Today;
                                String fec = f.Year + "/" + f.Month + "/" + f.Day;
                                pap = new BDPapeleta();
                                if (pap.DevolucionPapeleta(txtNroPapeleta.Text, cmbFisico.SelectedValue.ToString(), fec))
                                {
                                    MessageBox.Show("Se Actualizacion los datos de la papeleta exitosamente");
                                }
                                else
                                {
                                    MessageBox.Show("Error: No se Realizo la Actualizacion de la papeleta");
                                }
                                break;
                            default:
                                MessageBox.Show("No se seleecciono Estado");
                                break;

                        }
                    }
                    else
                    {
                        MessageBox.Show("La papeleta ya ha sido ingresada o aun no ha sido Asignada a un efectivo");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: "+ex.ToString());
            }

        }

        private void txtNroPapeleta_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Validacion.IsValidCodPapeleta(txtNroPapeleta.Text) || (txtNroPapeleta.Text) == "")
            {
                lblErrNroPap.Content = "El Codigo de Papeleta no es Valido";
            }
            else
            {
                lblErrNroPap.Content = "";
                pap = new BDPapeleta();
                if(pap.isExistPap(txtNroPapeleta.Text)){
                    if (pap.isEstado(txtNroPapeleta.Text))
                    {
                        DataTable dt = pap.getById(txtNroPapeleta.Text);
                        if (dt != null)
                        {
                            DataRow row = dt.Rows[0];
                            txtComisaria.Text = row["nombre_comisaria"].ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("La papeleta ya ha sido ingresada o aun no ha sido Asignada a un efectivo");
                    }
                }
                else
                {
                    lblErrNroPap.Content = "NO EXISTE";
                }
            }
        }

        private void cmbFisico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbFisico.SelectedValue.ToString() == "X" || cmbFisico.SelectedValue.ToString() == "R")
            {
                groupBox1.IsEnabled = false;
                groupBox2.IsEnabled = false;
                cmbInfraccion.IsEnabled = false;
                dpFechaImposicion.IsEnabled = false;
            }
            else
            {
                groupBox1.IsEnabled = true;
                groupBox2.IsEnabled = true;
                cmbInfraccion.IsEnabled = true;
                dpFechaImposicion.IsEnabled = true;
            }
        }

        private void cmbInfraccion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String now = cmbInfraccion.SelectedValue.ToString();
            lblDescrip.Text =dtInf.Rows[cmbInfraccion.SelectedIndex]["descripcion_infraccion"].ToString();

        }

        private void txtIdConductor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Validacion.IsDNI(txtIdConductor.Text) || (txtIdConductor.Text) == "")
            {
                lblErrDNI.Foreground = Brushes.Red;
                lblErrDNI.Content = "El DNI no es Valido";
            }
            else
            {
                con = new BDCon();
                lblErrDNI.Content = "";
                if (con.IsExistCond(txtIdConductor.Text, 1))
                {
                    DataRow row = (con.GetById(txtIdConductor.Text, 1)).Rows[0];
                    txtNombres.Text = row["nombre"].ToString();
                    txtNombres.IsEnabled = false;
                    txtApePat.Text = row["ape_pat"].ToString();
                    txtApePat.IsEnabled = false;
                    txtApeMat.Text = row["ape_mat"].ToString();
                    txtApeMat.IsEnabled = false;
                    txtBrevete.Text = row["brevete"].ToString();
                    

                }
                else
                {
                    txtNombres.IsEnabled = true;
                    txtApePat.IsEnabled = true;
                    txtApeMat.IsEnabled = true;
                    txtBrevete.IsEnabled = true;
                    lblErrDNI.Foreground = Brushes.Green;
                    lblErrDNI.Content = str;
                }
            }
        }

        private void txtBrevete_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Validacion.IsBrevete(txtBrevete.Text) || (txtBrevete.Text) == "")
            {
                lblErrBrevete.Foreground = Brushes.Red;
                lblErrBrevete.Content = "El brevete no es Valido";
            }
            else
            {
                con = new BDCon();
                lblErrBrevete.Content = "";
                if (con.IsExistCond(txtBrevete.Text, 2))
                {
                    DataRow row = (con.GetById(txtBrevete.Text, 2)).Rows[0];
                    txtNombres.Text = row["nombre"].ToString();
                    txtNombres.IsEnabled = false;
                    txtApePat.Text = row["ape_pat"].ToString();
                    txtApePat.IsEnabled = false;
                    txtApeMat.Text = row["ape_mat"].ToString();
                    txtApeMat.IsEnabled = false;
                    txtIdConductor.Text = row["dni_conductor"].ToString();
                    
                }
                else
                {
                    txtNombres.IsEnabled = true;
                    txtApePat.IsEnabled = true;
                    txtApeMat.IsEnabled = true;
                    txtBrevete.IsEnabled = true;
                    lblErrBrevete.Foreground = Brushes.Green;
                    lblErrBrevete.Content = str;
                }
            }
        }

        private void txtPlaca_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Validacion.IsValidPlaca(txtPlaca.Text) || (txtPlaca.Text) == "")
            {

                lblErrPlaca.Foreground = Brushes.Red;
                lblErrPlaca.Content = "El brevete no es Valido";
            }
            else
            {
                veh = new BDVeh();
                lblErrPlaca.Content = "";
                if (veh.IsExistVeh(txtPlaca.Text))
                {
                    DataRow row = (veh.GetById(txtPlaca.Text)).Rows[0];
                    txtClase.Text = row["clase"].ToString();
                    txtClase.IsEnabled = false;
                    txtSerie.Text = row["serie"].ToString();
                    txtSerie.IsEnabled = false;
                }
                else
                {
                    txtClase.IsEnabled = true;
                    txtSerie.IsEnabled = true;

                    lblErrPlaca.Foreground = Brushes.Green;
                    lblErrPlaca.Content = str;
                }
            }
        }
    }
}
