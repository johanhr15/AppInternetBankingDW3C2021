﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class INTERNET_BANKING_DW1_3C2021Entities : DbContext
    {
        public INTERNET_BANKING_DW1_3C2021Entities()
            : base("name=INTERNET_BANKING_DW1_3C2021Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cuenta> Cuenta { get; set; }
        public virtual DbSet<Error> Error { get; set; }
        public virtual DbSet<Estadistica> Estadistica { get; set; }
        public virtual DbSet<Moneda> Moneda { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }
        public virtual DbSet<Sesion> Sesion { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Transferencia> Transferencia { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Ahorro> Ahorro { get; set; }
        public virtual DbSet<Certificado_Deposito> Certificado_Deposito { get; set; }
        public virtual DbSet<Compra_Casa> Compra_Casa { get; set; }
        public virtual DbSet<Credito> Credito { get; set; }
        public virtual DbSet<Inversion> Inversion { get; set; }
        public virtual DbSet<Marchamo> Marchamo { get; set; }
        public virtual DbSet<Sol_Tarjeta_Credito> Sol_Tarjeta_Credito { get; set; }
        public virtual DbSet<Traslado_Pensiones> Traslado_Pensiones { get; set; }
    }
}