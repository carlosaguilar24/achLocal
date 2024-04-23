using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace RecepcionesApi.Config
{
    public class Configuraciones
    {
        public static string dbConexion;

        public Configuraciones cargarPropiedades()
        {
            Configuraciones propiedades = new Configuraciones();
            try
            {
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = @"C:\propertiesPopular\propertiesBcoPopular.config";
                Configuration config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
                dbConexion = config.AppSettings.Settings["CadenaDbConexion"].Value.Trim();
            }
            catch (Exception ex)
            {
               
            }

            return propiedades;
        }
    }
}
