using galactica_test.Db.Accessors.Abstract;
using Microsoft.EntityFrameworkCore;

namespace galactica_test.Db.Accessors
{
    public class SecurityContextAccessor : ISecurityContextAccessor
    {
        private readonly IDbContextFactory<SecurityContext> _dbContextFactory;
        private static readonly AsyncLocal<SecurityContextHolder?> _securityContextCurrent = new();

        public SecurityContextAccessor(IDbContextFactory<SecurityContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public SecurityContext CurrentContext
        {
            get
            {
                if (_securityContextCurrent.Value is null)
                {
                    // Clear current context trapped in the AsyncLocals, as its done.
                    _securityContextCurrent.Value = new SecurityContextHolder
                    {
                        Context = _dbContextFactory.CreateDbContext()
                    };
                }

                return _securityContextCurrent.Value.Context;
            }
            set
            {
                if (_securityContextCurrent.Value != null)
                {
                    // Clear current HttpContext trapped in the AsyncLocals, as its done.
                    _securityContextCurrent.Value = null;
                }

                // Use an object indirection to hold the HttpContext in the AsyncLocal,
                // so it can be cleared in all ExecutionContexts when its cleared.
                _securityContextCurrent.Value = new SecurityContextHolder
                {
                    Context = value
                };
            }
        }

        public SecurityContext CreateDbContext()
            => _dbContextFactory.CreateDbContext();

        private sealed class SecurityContextHolder
        {
            public SecurityContext Context { get; set; }
        }
    }
}
