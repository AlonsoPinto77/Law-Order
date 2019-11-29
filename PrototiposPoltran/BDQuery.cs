using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PrototiposPoltran
{
    abstract class BDQuery
    {
        protected SqlCommand cmd;
        protected SqlDataAdapter dtAdp;
        protected SqlDataReader reader;

    }

    class BDCom : BDQuery
    {
        
        public DataTable GetAll()
        {
            Conexion.Open();
            cmd = new SqlCommand("SELECT id_comisaria, nombre_comisaria FROM comisaria where activo = 1 ORDER BY nombre_comisaria ASC", Conexion.sqlConexion);
            dtAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dtAdp.Fill(dt);
            Conexion.Close();
            return dt;
        }
        public DataTable obtenerCodorNom(String str)
        {
            Conexion.Open();
            cmd = new SqlCommand("SELECT id_comisaria, nombre_comisaria FROM comisaria " +
                "where activo = 1 AND (id_comisaria LIKE '" + str + "%' OR nombre_comisaria LIKE '" + str + "%') ORDER BY nombre_comisaria ASC", Conexion.sqlConexion);
            dtAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dtAdp.Fill(dt);
            Conexion.Close();
            return dt;
        }
        public DataTable GetTalonarioComixEfe(String codComi)
        {
            Conexion.Open();
            cmd = new SqlCommand("SELECT inicio, fin, cantidad, devueltas, faltantes, fecha_entrega, nombre_efectivo FROM RelTalonario " +
                "INNER JOIN efectivo ON (RelTalonario.cip=efectivo.cip) where RelTalonario.id_comisaria = '" + codComi + "'", Conexion.sqlConexion);
            dtAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dtAdp.Fill(dt);
            return dt;
        }
        ~BDCom()
        {
            cmd = null;
            dtAdp = null;
        }
    }
    class BDEfe : BDQuery
    {
        public DataTable GetAll()
        {
            Conexion.Open();
            cmd = new SqlCommand("SELECT cip, nombre_efectivo FROM efectivo where activo = 1 ORDER BY nombre_efectivo ASC", Conexion.sqlConexion);
            dtAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dtAdp.Fill(dt);
            Conexion.Close();
            return dt;
        }
        public DataTable obtenerCIPorNom(String str)
        {
            Conexion.Open();
            cmd = new SqlCommand("SELECT cip, nombre_efectivo FROM efectivo "+
                "where activo = 1 AND (cip LIKE '"+str+ "%' OR nombre_efectivo LIKE '"+str+"%') ORDER BY nombre_efectivo ASC", Conexion.sqlConexion);
            dtAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dtAdp.Fill(dt);
            Conexion.Close();
            return dt;
        }
        public String GetNom(string cip)
        {
            String str = " ";
            Conexion.Open();
            cmd = new SqlCommand("SELECT TOP 1 nombre_efectivo FROM efectivo where cip='" + cip+"' and activo=1", Conexion.sqlConexion);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                str = reader.GetString(0);
            }
            Conexion.Close();
            return str;
        }
        public DataTable GetTalonarioEfectivo(String codEfe)
        {
            Conexion.Open();
            cmd = new SqlCommand("SELECT nombre_comisaria, inicio, fin, cantidad, devueltas, faltantes, fecha_entrega FROM RelTalonario " +
                "INNER JOIN comisaria ON (RelTalonario.id_comisaria=comisaria.id_comisaria) where RelTalonario.cip = '" + codEfe + "'", Conexion.sqlConexion);
            dtAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dtAdp.Fill(dt);
            Conexion.Close();
            return dt;
        }
    }
    class BDUsu : BDQuery
    {
        public String Login(String user, String pwd)
        {
            String cod = null;
            Conexion.Open();
            cmd = new SqlCommand("SELECT TOP 1 id_usuario FROM usuario WHERE nombre_usuario='"+user+"' AND contrasenia='"+pwd+"'", Conexion.sqlConexion);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                cod = ""+(reader.GetInt32(0));
            }
            Conexion.Close();
            return cod;
        }
    }

    class BDOficio : BDQuery
    {
        public bool IngresarOficio(String str1, String tip, String str2, String str3)
        {
            bool fl = false;
            Conexion.Open();
            cmd = new SqlCommand("INSERT INTO oficio(numero_oficio,titulo,tipo,descripcion_oficio)"+
                " VALUES('"+str1+"','"+str2+"','"+tip+"','"+str3+"')",Conexion.sqlConexion);
            int c = cmd.ExecuteNonQuery();
            fl = (c > 0)?true : false;
            Conexion.Close();
            return fl;
        }
        public bool IngresarOficio(String str1, String tip, String str2, String str3, String ini, String fin, String anio)
        {
            bool fl = false;
            Conexion.Open();
            String text = "INSERT INTO oficio(numero_oficio,titulo,tipo,descripcion_oficio) " +
                " VALUES('" + str1 + "','" + str2 + "','" + tip + "','" + str3 + "')";
            cmd = new SqlCommand(text, Conexion.sqlConexion);
            int c = cmd.ExecuteNonQuery();
            if (c > 0)
            {
                cmd = new SqlCommand("UPDATE sistema SET nro_oficio=isnull(nro_oficio,0)+1 WHERE anio=" + anio, Conexion.sqlConexion);
                c = cmd.ExecuteNonQuery();
                if (c > 0)
                {
                    BDTal tal = new BDTal();
                    fl = (tal.IngresarOficioEn(str1, ini, fin)) ? true : false;
                }

            }
            Conexion.Close();

            return fl;
        }
    }

    class BDTal : BDQuery
    {
        public bool IngresarOficioEn(String nroofi, String ini, String fin)
        {
            Conexion.Open();
            cmd = new SqlCommand("UPDATE RelTalonario SET numero_oficio='" + nroofi+"' WHERE inicio='" + ini+"' AND fin='"+fin+"'", Conexion.sqlConexion);
            int c = cmd.ExecuteNonQuery();
            bool fl = (c > 0) ? true : false;
            Conexion.Close();
            return fl;
        }
        public bool IngresarTal(String ini, String fin, String fec, String cod, String nofi, int cant)
        {
            int cont = 0;
            Conexion.Open();
            cmd = new SqlCommand("INSERT INTO Talonario(inicio,fin,fecha_ingreso,id_usuario,numero_oficio)" +
                " VALUES('" + ini + "','" + fin + "','" + fec + "','" + cod + "','" + nofi + "')", Conexion.sqlConexion);
            bool fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
            if (fl)
            {
                for (int i = 0; i < cant; i++)
                {
                    BDPapeleta pap = new BDPapeleta();
                    String num = (int.Parse(ini) + i).ToString();

                    if (pap.ingresarPapeletas("(" + num + "," + 0 + "," + 1 + ",'" + nofi + "','A')"))
                        cont++;
                }
            }
            Conexion.Close();
            return (cont == cant && fl) ? true : false;
        }
        public String obtenerId(String ini, String fin)
        {
            String cod = null;
            Conexion.Open();
            cmd = new SqlCommand("SELECT id_talonario FROM Talonario WHERE inicio <= '" + ini +
                "' and fin >='" + ini + "' and inicio <= '" + fin + "' and fin >='" + fin + "'",
                Conexion.sqlConexion);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                cod = "" + (reader.GetInt64(0));
            }
            Conexion.Close();
            return cod;
        }
        public DataTable obtenerDatos(String ini,String fin)
        {
            Conexion.Open();
            cmd = new SqlCommand("SELECT comisaria.id_comisaria, comisaria.nombre_comisaria, efectivo.cip,"+
                " efectivo.nombre_efectivo, RelTalonario.inicio, RelTalonario.fin, RelTalonario.cantidad, RelTalonario.devueltas,"+
                " RelTalonario.faltantes, Convert(varchar(10), relTalonario.fecha_entrega,120) as fecha_entrega FROM RelTalonario " +
                "INNER JOIN comisaria ON (RelTalonario.id_comisaria=comisaria.id_comisaria)"+
                "INNER JOIN efectivo ON (RelTalonario.cip=efectivo.cip)" +
                " where RelTalonario.inicio = '" + ini + "' AND RelTalonario.fin = '"+fin+"'", Conexion.sqlConexion);
            dtAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dtAdp.Fill(dt);
            Conexion.Close();
            return dt;
        }
        public bool InsertRelTalonario(String ini, String fin, String fec, String cip, String com, String tal, String cusu, int cant){
            Conexion.Open();
            cmd = new SqlCommand("INSERT INTO RelTalonario(inicio,fin,cantidad, devueltas, faltantes,fecha_entrega,id_usuario,id_talonario,cip,id_comisaria)" +
                " VALUES('" + ini + "','" + fin + "','" + cant + "','" + 0 + "','" + cant + "','" + fec + "','" + cusu + "','" + tal + "','" + cip + "','" + com + "')", Conexion.sqlConexion);
            bool fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
            return fl;
        }
        public bool entregarPapeletas(String ini, String fin, String fec, String cip, String com, String tal, String cusu, int cant)
        {
            int cont = 0;
            String codrlTal="";
            bool fl = InsertRelTalonario(ini,fin,fec,cip,com,tal,cusu,cant);
            if (fl)
            {
                Conexion.Open();
                cmd = new SqlCommand("SELECT id_relTalonario FROM RelTalonario WHERE inicio='" + ini + "' AND fin='" + fin + "' ORDER BY id_RelTalonario DESC", Conexion.sqlConexion);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    codrlTal = "" + (reader.GetInt64(0));
                }

                for (int i = 0; i < cant; i++)
                {
                    BDPapeleta pap = new BDPapeleta();
                    String num = (int.Parse(ini) + i).ToString();
                    if (pap.asignarPapeleta(num, codrlTal))
                    {
                        cont++;
                    }
                }
            }
            Conexion.Close();
            return (fl && cont == cant)?true: false;
        }
        
        public bool ReasignarTalonario(int cant, String ini, String fin, String fec, String efe, String com, String cusu)
        {
            int cont = 0;
            bool fl = false;
            String codreltal = null;
            String codTal = null;
            String fecreltal = "", cipreltal = "", comreltal = "", ofireltal="";
            int inireltal = 0, finreltal = 0, cantreltal = 0, devreltal = 0, usureltal = 0;
            Conexion.Open();
            cmd = new SqlCommand("SELECT id_relTalonario, inicio, fin, cantidad, devueltas, fecha_entrega, id_usuario, "+
                "cip, id_comisaria, numero_oficio FROM relTalonario WHERE inicio <= '" + ini +
                "' and fin >='" + ini + "' and inicio <= '" + fin + "' and fin >='" + fin + "'",
                Conexion.sqlConexion);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                codreltal = "" + (reader.GetInt64(0));
                inireltal = Convert.ToInt32( reader.GetString(1));
                finreltal = Convert.ToInt32(reader.GetString(2));
                cantreltal = reader.GetInt32(3);
                devreltal = reader.GetInt32(4);
                DateTime fechaRel1 = reader.GetDateTime(5);
                fecreltal = fechaRel1.Year + "/" + fechaRel1.Month + "/" + fechaRel1.Day;
                usureltal = reader.GetInt32(6);
                cipreltal = reader.GetString(7);
                comreltal = reader.GetString(8);
                //ofireltal = reader.GetString(9);
                int iniAsigTal = Convert.ToInt32(ini);
                int finAsigTal = Convert.ToInt32(fin);
                codTal = obtenerId(ini, fin);
                DataTable dt = new DataTable();
                if(iniAsigTal == inireltal && finAsigTal == finreltal)
                {
                    Conexion.Open();
                    cmd = new SqlCommand("UPDATE RelTalonario SET  ini='" + (inireltal + cant) + "', devueltas='" + 0 +
                        "', faltantes='" + (cantreltal) + "', fecha_entrega='" + fec + "', id_usuario='" + cusu +
                        "', cip='" + efe + "', id_comisaria='" + com + "' WHERE id_relTalonario='" + codreltal + "'", 
                        Conexion.sqlConexion);
                    fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
                }
                if(iniAsigTal == inireltal && finAsigTal < finreltal)
                {
                    if (this.InsertRelTalonario(ini, fin, fec, efe, com, codTal, cusu, cant))
                    {
                        Conexion.Open();
                        cmd = new SqlCommand("UPDATE RelTalonario SET  inicio='" + (inireltal + cant) + "', cantidad='" + (cantreltal - cant) +
                            "', devueltas='" + (devreltal - cant) + "' WHERE id_relTalonario='" + codreltal + "'", Conexion.sqlConexion);
                        fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
                    }
                    else
                    {
                        fl = false;
                    }
                }
                if(inireltal < iniAsigTal && finAsigTal == finreltal)
                {
                    if(this.InsertRelTalonario(ini, fin, fec, efe, com, codTal, cusu, cant))
                    {
                        Conexion.Open();
                        cmd = new SqlCommand("UPDATE RelTalonario SET  fin='" + (finreltal - cant) + "', cantidad='" + (cantreltal - cant) +
                        "', devueltas='" + (devreltal - cant) + "' WHERE id_relTalonario='" + codreltal + "'", Conexion.sqlConexion);
                        fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
                    }
                    else
                    {
                        fl = false;
                    }
                }
                if(inireltal < iniAsigTal && finAsigTal < finreltal)
                {
                    if (this.InsertRelTalonario(ini, fin, fec, efe, com, codTal, cusu, cant))
                    {
                        int finPaptal1 = (iniAsigTal - 1);
                        int nroPaptal1 = iniAsigTal - inireltal;
                        BDPapeleta pap = new BDPapeleta();
                        int devPaptal1 = pap.cantDevuelta(inireltal.ToString(), finPaptal1.ToString());
                        Conexion.Open();
                        cmd = new SqlCommand("UPDATE RelTalonario SET  fin='" + finPaptal1 + "', cantidad='" + nroPaptal1 +
                        "', devueltas='" + devPaptal1 + "', faltantes='" + (nroPaptal1-devPaptal1) + "' WHERE id_relTalonario='" + 
                        codreltal + "'", Conexion.sqlConexion);
                        if(cmd.ExecuteNonQuery() > 0)
                        {
                            int iniPapTal2 = (finAsigTal + 1);
                            int nroPapTal2 = (finreltal - finAsigTal);
                            int devPapTal2 = pap.cantDevuelta(iniPapTal2.ToString(), finreltal.ToString());
                            Conexion.Open();
                            cmd = new SqlCommand("INSERT INTO RelTalonario(inicio,fin,cantidad, devueltas, faltantes,fecha_entrega,id_usuario,id_talonario,cip,id_comisaria)" +
                                " VALUES('" + iniPapTal2 + "','" + finreltal + "','" + nroPapTal2 + "','" + devPapTal2 + "','" + (nroPapTal2-devPapTal2) + "','" + fecreltal + 
                                "','" + usureltal + "','" + codTal + "','" + cipreltal + "','" + comreltal + "')", Conexion.sqlConexion);
                            fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
                        }
                        else
                        {
                            fl = false;
                        }
                    }
                    else
                    {
                        fl = false;
                    }
                }
                if (fl)
                {
                    String newCodAsigTal= "";
                    Conexion.Open();
                    cmd = new SqlCommand("SELECT id_relTalonario FROM relTalonario WHERE inicio='" + ini + "' AND fin='" + fin + "' ORDER BY id_relTalonario DESC", Conexion.sqlConexion);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        newCodAsigTal = "" + (reader.GetInt64(0));
                    }

                    for (int i = 0; i < cant; i++)
                    {
                        BDPapeleta pap = new BDPapeleta();
                        String num = (int.Parse(ini) + i).ToString();
                        if (pap.asignarPapeleta(num, newCodAsigTal))
                        {
                            cont++;
                        }
                    }
                }
            }
            Conexion.Close();

            return true;
        }
        public bool EliminarRelTalonario(int cant, String ini, String fin)
        {
            int cont = 0;
            bool fl = false;
            string codrlTal = "";
            Conexion.Open();
            cmd = new SqlCommand("SELECT id_relTalonario FROM RelTalonario WHERE inicio='" + ini + "' AND fin='" + fin + "'", Conexion.sqlConexion);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                codrlTal = "" + (reader.GetInt64(0));
                for (int i = 0; i < cant; i++)
                {
                    BDPapeleta pap = new BDPapeleta();
                    String num = (int.Parse(ini) + i).ToString();
                    if (pap.eliminarAsignacionPapeleta(num))
                    {
                        cont++;
                    }
                }
            }            
            if(cont == cant)
            {
                Conexion.Open();
                cmd = new SqlCommand("DELETE FROM RelTalonario WHERE id_relTalonario=" + codrlTal, Conexion.sqlConexion);
                 fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
                Conexion.Close();
            }
            Conexion.Close();
            return fl;
        }
        public bool ActualizarTalonario(String codTal)
        {
            Conexion.Open();
            cmd = new SqlCommand("UPDATE RelTalonario SET devueltas=isnull(devueltas,0)+1, faltantes=isnull(faltantes,0)-1 WHERE id_relTalonario=" + codTal, Conexion.sqlConexion);
            bool fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
            Conexion.Close();
            return fl;
        }
        public DataTable GetByIdAll(String cod, int tip)
        {
            DataTable dt = null;
            Conexion.Open();
            String id = "", id2 = "", nom = "", whe="", tb="";
            if(tip == 1)
            {
                tb = "comisaria";
                id = "comisaria.id_comisaria";
                nom = "comisaria.nombre_comisaria";
                whe = "cip";
                id2 = "id_comisaria";
            }
            if(tip == 2)
            {
                tb = "efectivo";
                id = "efectivo.cip";
                nom = "efectivo.nombre_efectivo";
                whe = "id_comisaria";
                id2 = "cip";
            }
            string cmmd = "SELECT papeleta.numero_papeleta, Convert(varchar(10), relTalonario.fecha_entrega,120) as fecha_entrega, papeleta.falta, estado.descripcion AS estado, " + id + " AS id, " + nom + " AS nombre FROM papeleta " +
                "INNER JOIN RelTalonario ON(papeleta.id_relTalonario = RelTalonario.id_relTalonario) " +
                "INNER JOIN " + tb + " ON(RelTalonario." + id2 + " = " + id + ") " +
                "INNER JOIN estado ON(papeleta.estado = estado.id_estado) WHERE papeleta.falta = 1 AND RelTalonario." + whe + "='" + cod + "'";
            cmd = new SqlCommand(
                cmmd, 
                Conexion.sqlConexion);
            dtAdp = new SqlDataAdapter(cmd);
            dt = new DataTable();
            dtAdp.Fill(dt);

            Conexion.Close();
            return dt;
        }
    }
    class BDPapeleta : BDQuery
    {
        public bool ingresarPapeletas(String cad)
        {
            Conexion.Open();
            cmd = new SqlCommand("INSERT INTO Papeleta(numero_papeleta,falta,fisico,numero_oficio,estado) VALUES"+cad, Conexion.sqlConexion);
            bool fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
            Conexion.Close();
            return fl;

        }
        public bool asignarPapeleta(String num, String codrlTal)
        {
            Conexion.Open();
            cmd = new SqlCommand("UPDATE Papeleta SET falta=1 ,fisico=0, estado='E', id_relTalonario='"+codrlTal+"' where numero_papeleta='"+num+"'", Conexion.sqlConexion);
            bool fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
            Conexion.Close();
            return fl;
        }
        public bool eliminarAsignacionPapeleta(String num)
        {
            Conexion.Open();
            cmd = new SqlCommand("UPDATE Papeleta SET falta=0 ,fisico=1, estado='A', id_relTalonario=null where numero_papeleta='" + num + "'", Conexion.sqlConexion);
            bool fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
            Conexion.Close();
            return fl;
        }
        public int isAsignado(String ini, String fin)
        {
            Conexion.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM papeleta WHERE falta=1 AND numero_papeleta BETWEEN '" + ini + "' AND '" + fin + "'", Conexion.sqlConexion);
            Int32 count = (Int32)cmd.ExecuteScalar();
            Conexion.Close();
            return count;
        }

        public int cantDevuelta(String ini, String fin)
        {
            Conexion.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM papeleta WHERE fisico=1 AND numero_papeleta BETWEEN '" + ini + "' AND '" + fin + "'", Conexion.sqlConexion);
            Int32 count = (Int32)cmd.ExecuteScalar();
            Conexion.Close();
            return count;
        }

        public int isReAsignado(String ini, String fin)
        {
            Conexion.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM papeleta WHERE estado='R' AND numero_papeleta BETWEEN '" + ini + "' AND '" + fin + "'", Conexion.sqlConexion);
            Int32 count = (Int32)cmd.ExecuteScalar();
            Conexion.Close();
            return count;
        }
        public int isIngresado(String ini, String fin)
        {
            Conexion.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM papeleta WHERE numero_papeleta BETWEEN '" + ini + "' AND '" + fin + "'", Conexion.sqlConexion);
            Int32 count = (Int32)cmd.ExecuteScalar();
            Conexion.Close();
            return count;
        }

        public bool isExistPap(String num)
        {
            bool flag = false;
            Conexion.Open();
            cmd = new SqlCommand("SELECT TOP 1 numero_papeleta FROM papeleta WHERE numero_papeleta='" + num + "'" ,Conexion.sqlConexion);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                flag = true;
            }
            Conexion.Close();
            return flag;
        }
        public bool isEstado(String num)
        {
            bool flag = false;
            Conexion.Open();
            cmd = new SqlCommand("SELECT estado FROM papeleta WHERE numero_papeleta='" + num 
                + "' and falta=1 and estado != 'A' and estado != 'R'", Conexion.sqlConexion);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                flag = true;
            }
            Conexion.Close();
            return flag;
        }
        
        public DataTable getById(String cod)
        {
            DataTable dt = null;
            Conexion.Open();
            // Se tiene que verificar existencia
            cmd = new SqlCommand(
                "SELECT numero_papeleta, papeleta.tipo_infraccion, nombre_comisaria, estado, papeleta.dni_conductor, brevete, nombre, ape_pat, ape_mat, papeleta.placa, clase, serie FROM papeleta " +
                "LEFT JOIN RelTalonario ON (papeleta.id_relTalonario=RelTalonario.id_relTalonario) " +
                "LEFT JOIN comisaria ON (RelTalonario.id_comisaria=comisaria.id_comisaria)  " +
                "LEFT JOIN conductor ON (papeleta.dni_conductor=papeleta.dni_conductor)  " +
                "LEFT JOIN vehiculo ON (papeleta.placa=vehiculo.id_placa) WHERE numero_papeleta='" + cod + "'", Conexion.sqlConexion);
            dtAdp = new SqlDataAdapter(cmd);
            dt = new DataTable();
            dtAdp.Fill(dt);
            
            Conexion.Close();
            return dt;
        }
        public DataTable PapeletasaAsignar()
        {
            Conexion.Open();
            // Se tiene que verificar existencia
            cmd = new SqlCommand(
                "SELECT papeleta.numero_papeleta, papeleta.fisico, papeleta.falta,papeleta.estado, "+
                "efectivo.nombre_efectivo, comisaria.nombre_comisaria, papeleta.fecha_envio FROM papeleta " +
                "INNER JOIN RelTalonario ON (papeleta.id_relTalonario=RelTalonario.id_relTalonario) " +
                "INNER JOIN comisaria ON (RelTalonario.id_comisaria=comisaria.id_comisaria)  " +
                "INNER JOIN efectivo ON (RelTalonario.cip=efectivo.cip)  WHERE estado='R'", Conexion.sqlConexion);
            dtAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dtAdp.Fill(dt);

            Conexion.Close();
            return dt;
        }
        public String getRelTalonario(String codpap)
        {
            String tal = null;
            Conexion.Open();
            cmd = new SqlCommand("SELECT TOP 1 id_relTalonario FROM relTalonario where  inicio <= '" + codpap + "' AND fin >= '" + codpap + "'", Conexion.sqlConexion);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tal = ""+reader.GetInt64(0);
            }
            Conexion.Close();
            return tal;
        }
        public bool DevolucionPapeleta(String num, String tip, String fec)
        {
            Conexion.Open();
            cmd = new SqlCommand("UPDATE papeleta SET fisico=1, falta=0, estado='" + tip + "', fecha_envio='" + fec + "' WHERE numero_papeleta='" + num + "'", Conexion.sqlConexion);
            bool fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
            if (fl)
            {
                BDTal tal = new BDTal();
                if (tal.ActualizarTalonario(getRelTalonario(num)))
                    fl = true;
                else
                    fl = false;
            }
            Conexion.Close();
            return fl;
        }
        public bool DevolucionPapeleta(String num, String tip, String inf, String fecImp,String fec,String cond,String veh)
        {
            Conexion.Open();
            cmd = new SqlCommand("UPDATE papeleta SET fisico=1, falta=0, estado='" + tip + 
                "', fecha_envio='" + fec + "', tipo_infraccion='" + inf + "', fecha_imposicion='" + fecImp
                + "', dni_conductor='" + cond + "', placa='" + veh + "' WHERE numero_papeleta='" + num + "'", Conexion.sqlConexion);
            bool fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
            if (fl)
            {
                BDTal tal = new BDTal();
                if (tal.ActualizarTalonario(getRelTalonario(num)))
                    fl = true;
                else
                    fl = false;
            }
            Conexion.Close();
            return fl;
        }

    }
    class BDSistema : BDQuery
    {
        public String Directorio()
        {
            String dir = null;
            Conexion.Open();
            cmd = new SqlCommand("SELECT TOP 1 directorio FROM sistema ORDER BY anio DESC", Conexion.sqlConexion);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                dir = reader.GetString(0);
            }
            Conexion.Close();
            return dir;
        }
        public DataTable sistemaDatos()
        {
            Conexion.Open();
            cmd = new SqlCommand("SELECT TOP 1 * FROM sistema ORDER BY anio DESC", Conexion.sqlConexion);
            dtAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dtAdp.Fill(dt);
            Conexion.Close();
            return dt;
        }
    }
    class BDInf : BDQuery
    {
        public DataTable GetAll()
        {
            Conexion.Open();
            cmd = new SqlCommand("SELECT tipo_infraccion, descripcion_infraccion FROM infraccion where activo = 1", Conexion.sqlConexion);
            dtAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dtAdp.Fill(dt);
            Conexion.Close();
            return dt;
        }
        
    }

    class BDCon : BDQuery
    {
        public bool IsExistCond(String cod, int tip)
        {
            String strTip = (tip == 1) ? "dni_conductor" : "brevete";
            bool flag = false;
            Conexion.Open();
            cmd = new SqlCommand("SELECT TOP 1 " + strTip + " FROM conductor WHERE " + strTip + "='" + cod + "'", Conexion.sqlConexion);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                flag = true;
            }
            Conexion.Close();
            return flag;
        }

        public DataTable GetById(String cod, int tip)
        {
            String strTip = (tip == 1) ? "dni_conductor" : "brevete";
            String strTip1 = (tip != 1) ? "dni_conductor" : "brevete";
            Conexion.Open();
            cmd = new SqlCommand(
                   "SELECT " + strTip1 + ", nombre, ape_pat, ape_mat FROM conductor WHERE " + strTip + "='" + cod + "'", Conexion.sqlConexion);
            dtAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dtAdp.Fill(dt);
            Conexion.Close();
            return dt;
        }
        public bool ingresarCon(String dni,String brevete, String nom, String apePat, String apeMat)
        {
            Conexion.Open();
            cmd = new SqlCommand("INSERT INTO conductor(dni_conductor,brevete,nombre,ape_pat,ape_mat)"+
                " VALUES ('" + dni + "','"+brevete+"','"+nom+"','"+apePat+"','" +apeMat+"')", 
                Conexion.sqlConexion);
            bool fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
            Conexion.Close();
            return fl;
        }
    }
    class BDVeh : BDQuery
    {
        public bool IsExistVeh(String cod)
        {
            bool flag = false;
            Conexion.Open();
            cmd = new SqlCommand("SELECT TOP 1 id_placa FROM vehiculo WHERE id_placa='" + cod + "'", Conexion.sqlConexion);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                flag = true;
            }
            Conexion.Close();
            return flag;
        }

        public DataTable GetById(String cod)
        {
            Conexion.Open();
            cmd = new SqlCommand(
                   "SELECT  clase, serie FROM vehiculo WHERE id_placa='" + cod + "'", Conexion.sqlConexion);
            dtAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dtAdp.Fill(dt);
            Conexion.Close();
            return dt;
        }
        public bool ingresarVeh(String placa, String clase, String serie)
        {
            Conexion.Open();
            cmd = new SqlCommand("INSERT INTO vehiculo(id_placa,clase,serie)" +
                " VALUES ('" + placa + "','" + clase + "','" + serie + "')",
                Conexion.sqlConexion);
            bool fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
            Conexion.Close();
            return fl;
        }
    }
}
