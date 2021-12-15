using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Fifth_task
{
    /// <summary>
    /// Represents ORM using DAO and reflection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericDao<T> : IDao<T> where T : class
    {
        private readonly Type _typeOfEntity = typeof(T);
        private readonly string _stringConnection;
        private readonly ReflectionHelper<T> reflection = new ReflectionHelper<T>();
        private static GenericDao<T> daoObject;

        public GenericDao(string stringConnection)
        { 

            _stringConnection = stringConnection;

        }

        public int Create(T item)
        {
            string sqlExpression = $"INSERT INTO {reflection.NameOfTheTable()} ({reflection.GetNamesOfPropertiesWithoutId(_typeOfEntity)}) " +
                $"VALUES ({reflection.NamesOfPropertiesInSqlFormat(reflection.GetNamesOfPropertiesWithoutId(_typeOfEntity))});" +
                $"SET @id=SCOPE_IDENTITY()";
            using(SqlConnection connection = new SqlConnection(_stringConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.Parameters.AddRange(reflection.GenerateSqlParameters(item));
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };
                command.Parameters.Add(idParam);
                command.ExecuteNonQuery();
                return Convert.ToInt32(idParam.Value);
            }
            
        }

        public void Delete(int id)
        {
            string sqlExpression = $"DELETE FROM {reflection.NameOfTheTable()} WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(_stringConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter sqlParameter = new SqlParameter("@id", id);
                command.Parameters.Add(sqlParameter);
                command.ExecuteNonQuery();
            }
        }

        public T Get(int id)
        {
            string sqlExpressinon = $"SELECT * FROM {reflection.NameOfTheTable()} WHERE Id = @id";
            T createdEntity = null;
            using (SqlConnection connection = new SqlConnection(_stringConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpressinon, connection);
                SqlParameter sqlParameter = new SqlParameter("@id", id);
                command.Parameters.Add(sqlParameter);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        createdEntity = reflection.CreateEntity(reader);
                    }
                }
            }
            return createdEntity;
        }

        public List<T> GetAll()
        {
            string sqlExpression = $"SELECT * FROM {reflection.NameOfTheTable()}";
            List<T> returnList = new List<T> { };
            using(SqlConnection connection = new SqlConnection(_stringConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        returnList.Add(reflection.CreateEntity(reader));
                    }
                }
            }
            return returnList;
        }

        public void Update(T item)
        {
            string sqlExpression = $"UPDATE {reflection.NameOfTheTable()} SET {reflection.GenerateUpdateSetString()} WHERE Id=@id";
            using(SqlConnection connection = new SqlConnection(_stringConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", reflection.FindIdOfTheEntity(item));
                command.Parameters.Add(idParam);
                command.Parameters.AddRange(reflection.GenerateSqlParameters(item));
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Gets single DAO object
        /// </summary>
        /// <param name="stringConnection"></param>
        /// <returns></returns>
        public static IDao<T> GetDao(string stringConnection)
        {
            if (daoObject == null)
            {
                daoObject = new GenericDao<T>(stringConnection);
            }
            return daoObject;
        }
    }
}
