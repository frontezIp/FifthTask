using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Configuration;
using Microsoft.Data.SqlClient;


namespace Fifth_task
{
    public class ReflectionHelper<T>
    {
        private Type _typeOfEntity = typeof(T);

        /// <summary>
        /// Get names of the properties of the current type without id
        /// </summary>
        /// <param name="typeOfEntity"></param>
        /// <returns></returns>
        public string GetNamesOfPropertiesWithoutId(Type typeOfEntity)
        {
            string properitesToReturn = "";
            foreach(PropertyInfo property in typeOfEntity.GetProperties())
            {
                if(property.Name != "Id")
                    properitesToReturn += property.Name + ", ";
               
            }
            properitesToReturn = properitesToReturn.Remove(properitesToReturn.Length - 2);
            return properitesToReturn;
        }
        /// <summary>
        /// Get names of the properties of the current type with id
        /// </summary>
        /// <param name="typeOfEntity"></param>
        /// <returns></returns>
        public string GetNamesOfPropertiesWithId(Type typeOfEntity)
        {
            string properitesToReturn = "";
            foreach (PropertyInfo property in typeOfEntity.GetProperties())
            {
                 properitesToReturn += property.Name + ", ";
            }
            properitesToReturn = properitesToReturn.Remove(properitesToReturn.Length - 2);
            return properitesToReturn;
        }

        /// <summary>
        /// Return id of the given object
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int FindIdOfTheEntity(T item)
        {
            return Convert.ToInt32(item.GetType().GetProperty("Id").GetValue(item));
        }

        /// <summary>
        /// Convert properitnes into sql format
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public string NamesOfPropertiesInSqlFormat(string properties)
        {
            return "@" + String.Join(" @", properties.Split(' ')).ToLower();
        }

        /// <summary>
        /// Creates entity of the object from DB
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public T CreateEntity(SqlDataReader reader)
        {
            var createdEntity = (T)Activator.CreateInstance(typeof(T));
            var properties = _typeOfEntity.GetProperties();

            foreach(PropertyInfo property in properties)
            {
                if (reader[property.Name] != DBNull.Value)
                    property.SetValue(createdEntity, reader[property.Name]);
                else
                    property.SetValue(createdEntity, null);
            }
            return createdEntity;
        }

        /// <summary>
        /// Generate sql parameters to protect from injections 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public SqlParameter[] GenerateSqlParameters(T item)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter> { };
            string[] properties = GetNamesOfPropertiesWithoutId(typeof(T)).Replace(",", "").Split(" ");
            foreach(var property in properties)
            {
                sqlParameters.Add(new SqlParameter(NamesOfPropertiesInSqlFormat(property), item.GetType().GetProperty(property).GetValue(item)));
            }
            return sqlParameters.ToArray();

        }

        /// <summary>
        /// Generate update string for update method in ORM
        /// </summary>
        /// <returns></returns>
        public string GenerateUpdateSetString()
        {
            StringBuilder ReturnString = new StringBuilder();
            string [] properties = GetNamesOfPropertiesWithoutId(typeof(T)).Replace(",", "").Split(" ");
            foreach(var property in properties)
            {
                ReturnString.Append($"{property}={NamesOfPropertiesInSqlFormat(property)}, ");
            }
            return ReturnString.Remove(ReturnString.Length-2,2).ToString();
        }

        /// <summary>
        /// Return name of the table
        /// </summary>
        /// <returns></returns>
        public string NameOfTheTable()
        {
            return _typeOfEntity.Name;
        }

    }
}
