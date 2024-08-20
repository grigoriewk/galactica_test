namespace galactica_test.Models.Response
{
    /// <summary>
    /// Данные о работнике
    /// </summary>
    public class EmployeeResponse
    {
        public EmployeeResponse(long id, string name, string lastName)
        {
            Id = id;
            Name = name;
            LastName = lastName;
        }


        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
    }
}
