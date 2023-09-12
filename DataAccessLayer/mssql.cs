using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Klase;

namespace DataAccessLayer
{
    public class mssql:DbContext
    {
        public mssql() : base("con") { }

        public DbSet<HostingPaketi> HostingPaketi { get; set; }
        public DbSet<VpsPaketi> VpsPaketi { get; set; }
        public DbSet<DedicatedPaketi> DedicatedPaketi { get; set; }
        public DbSet<DomenePaketi> DomenePaketi { get; set; }
        public DbSet<SSLPaketi> SSLPaketi { get; set; }
        public DbSet<Klijenti> Klijenti { get; set; }
        public DbSet<KlijentLog> KlijentLog { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<AdminLog> AdminLog { get; set; }

        public DbSet<Grad> Grad { get; set; }
        public DbSet<Kanton> Kanton { get; set; }
        public DbSet<Drzave> Drzave { get; set; }

        public DbSet<Tiketi> Tiketi { get; set; }
        public DbSet<Poruke> Poruke { get; set; }

        public DbSet<DostupneIP> DostupneIP { get; set; }
        public DbSet<Rack> Rack { get; set; }
        public DbSet<Serveri> Serveri { get; set; }
        public DbSet<Domena> Domena { get; set; }

        public DbSet<Status> Status { get; set; }
        public DbSet<Ugovor> Ugovor { get; set; }
        public DbSet<Produzenja> Produzenja { get; set; }

    }
}
