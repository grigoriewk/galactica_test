namespace galactica_test.Db.Accessors.Abstract
{
    public interface ISecurityContextAccessor
    {
        SecurityContext CurrentContext { get; set; }

        SecurityContext CreateDbContext();
    }
}
