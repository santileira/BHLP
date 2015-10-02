private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
/*
 *          ESTO ES PARA CREATE E INSERT. EL Program.conect SE LO PODRIA REEMPLAZAR CON UNA VARIABLE STATIC
 *          INICIALIZADA EN Program.conect
 *          
 *          EL ExecuteReader() ES PARA EJECUTAR EL COMANDO SQL
 *          
            command.Connection = Program.conect;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "insert into hola (numero, nombre) values (4, 'lauri')";
            command.CommandTimeout = 0;

            command.ExecuteReader();*/


            /*
             * PARA UNA CONSULTA DE SELECT. LOS DATOS SE VEN EN EL COMPONENTE DATA GRID.
             * DataSource ASOCIA EL DATA GRID A UN ELEMENDO DE LA BD (TABLA, CONSULTA, ETC.) Y EL member
             * ASOCIA A QUÉ TABLA, O A QUÉ CONSULTA, ETC.
             */
            string queryselect = "SELECT * FROM dbo.hola WHERE nombre LIKE 'l%'";

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(queryselect, Program.conect);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dg.DataSource = ds;
            dg.DataMember = "Busqueda";

        }