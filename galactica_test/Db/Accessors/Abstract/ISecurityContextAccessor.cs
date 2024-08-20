namespace galactica_test.Db.Accessors.Abstract
{
    /// <summary>
    /// Контекст БД
    /// </summary>
    public interface ISecurityContextAccessor
    {
        /// <summary>
        /// Текущий контекст БД
        /// </summary>
        SecurityContext CurrentContext { get; set; }

        /// <summary>
        /// Создать экземпляр контекста БД
        /// </summary>
        /// <returns></returns>
        SecurityContext CreateDbContext();
    }
}
