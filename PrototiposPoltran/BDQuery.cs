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
            cmd = new SqlCommand("SELECT id_comisaria, nombre_comisaria FROM comisaria where activo = 1", Conexion.sqlConexion);
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
            cmd = new SqlCommand("SELECT cip, nombre_efectivo FROM efectivo where activo = 1", Conexion.sqlConexion);
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
            cmd = new SqlCommand("SELECT TOP 1 nombre_efectivo FROM efectivo where cip=" + cip+"' and activo=1", Conexion.sqlConexion);
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
            Conexion.Open();
            cmd = new SqlCommand("INSERT INTO oficio(numero_oficio,titulo,tipo,descripcion_oficio)"+
                " VALUES('"+str1+"','"+str2+"','"+tip+"','"+str3+"')",Conexion.sqlConexion);
            int c = cmd.ExecuteNonQuery();
            bool fl = (c > 0)? true : false;
            Conexion.Close();
            return fl;
        }
    }

    class BDTal : BDQuery
    {
        public bool IngresarTal(String ini,  String fin, String fec, String cod, String nofi, int cant)
        {
            int cont = 0;
            Conexion.Open();
            cmd = new SqlCommand("INSERT INTO Talonario(inicio,fin,fecha_ingreso,id_usuario,numero_oficio)" +
                " VALUES('" + ini + "','" + fin+ "','" + fec+ "','" + cod+ "','" + nofi+ "')", Conexion.sqlConexion);
            bool fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
            if (fl)
            {
                for(int i=0; i < cant; i++)
                {
                    BDPapeleta pap = new BDPapeleta();
                    String num = (int.Parse(ini) + i).ToString();
                    
                    if (pap.ingresarPapeletas("("+num+","+0+","+1 + ",'A')"))
                        cont++;
                }
            }
            Conexion.Close();
            return (cont == cant && fl)? true : false;
        }
        public String obtenerId(String ini , String fin)
        {
            String cod = null;
            Conexion.Open();
            cmd = new SqlCommand("SELECT id_talonario FROM Talonario WHERE inicio <= '" + ini + 
                "' and fin >='" + ini + "' and inicio <= '" + fin + "' and fin >='" + fin+"'",
                Conexion.sqlConexion);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                cod = "" + (reader.GetInt64(0));
            }
            Conexion.Close();
            return cod;
        }
        public bool entregarPapeletas(String ini, String fin, String fec, String cip, String com, String tal, String cusu, int cant)
        {
            int cont = 0;
            String codrlTal="";
            Conexion.Open();
            cmd = new SqlCommand("INSERT INTO RelTalonario(inicio,fin,cantidad, devueltas, faltantes,fecha_entrega,id_usuario,id_talonario,cip,id_comisaria)" +
                " VALUES('" + ini + "','" + fin + "','" + cant + "','" + 0 + "','" + cant + "','" + fec + "','" + cusu + "','" + tal + "','" + cip + "','" + com + "')", Conexion.sqlConexion);
            bool fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
            if (fl)
            {
                cmd = new SqlCommand("SELECT id_relTalonario FROM RelTalonario WHERE cip='" + cip + "' AND id_comisaria='" + com + "' ORDER BY id_RelTalonario DESC", Conexion.sqlConexion);
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
        public bool ActualizarTalonario(String codTal)
        {
            Conexion.Open();
            cmd = new SqlCommand("UPDATE RelTalonario SET devueltas=isnull(devueltas,0)+1, faltantes=isnull(faltantes,0)-1 WHERE id_relTalonario=" + codTal, Conexion.sqlConexion);
            bool fl = (cmd.ExecuteNonQuery() > 0) ? true : false;
            Conexion.Close();
            return fl;
        }
    }
    class BDPapeleta : BDQuery
    {
        public bool ingresarPapeletas(String cad)
        {
            Conexion.Open();
            cmd = new SqlCommand("INSERT INTO Papeleta(numero_papeleta,falta,fisico,estado) VALUES"+cad, Conexion.sqlConexion);
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
        public int isAsignado(String ini,String fin)
        {
            Conexion.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM papeleta WHERE falta=1 AND numero_papeleta BETWEEN '"+ini+"' AND '"+fin+"'", Conexion.sqlConexion);
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
            String dir=null;
            Conexion.Open();
            cmd = new SqlCommand("SELECT TOP 1 directorio FROM sistema ORDER BY anio DESC" , Conexion.sqlConexion);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                dir = reader.GetString(0);
            }
            Conexion.Close();
            return dir;
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
