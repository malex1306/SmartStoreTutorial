using Microsoft.EntityFrameworkCore;
using MyOrg.HelloWorld.Domain;
using Smartstore.Core.Data; // Diese using-Direktive hinzufügen
using Smartstore.Data; // Diese using-Direktive hinzufügen

namespace MyOrg.HelloWorld.Extensions
{
    public static class SmartDbContextExtensions
    {
        public static DbSet<Notification> Notifications(this SmartDbContext db)
            => db.Set<Notification>();
    }
}