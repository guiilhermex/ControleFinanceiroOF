using ControleFinanceiroOF.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiroOF.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<DespesaModel> Despesas { get; set; }
        public DbSet<ReceitaModel> Receitas { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // === USUARIOS ===
            modelBuilder.Entity<UsuarioModel>().ToTable("Usuarios", schema: "Seguranca");

            modelBuilder.Entity<UsuarioModel>()
                .Property(u => u.IdUsuario)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UsuarioModel>()
                .Property(u => u.Nome)
                .HasMaxLength(256)
                .IsRequired();

            modelBuilder.Entity<UsuarioModel>()
                .Property(u => u.Email)
                .HasMaxLength(256)
                .IsRequired();

            modelBuilder.Entity<UsuarioModel>()
                .Property(u => u.Senha)
                .HasMaxLength(256)
                .IsRequired();

            modelBuilder.Entity<UsuarioModel>()
                .Property(u => u.TokenID)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            // === DESPESAS ===
            modelBuilder.Entity<DespesaModel>().ToTable("Despesas", schema: "Sistema");

            modelBuilder.Entity<DespesaModel>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<DespesaModel>()
                .Property(d => d.TokenID)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            modelBuilder.Entity<DespesaModel>()
                .Property(d => d.Nome)
                .HasMaxLength(256)
                .IsRequired();

            modelBuilder.Entity<DespesaModel>()
                .Property(d => d.Valor)
                .HasColumnType("money")
                .IsRequired();

            modelBuilder.Entity<DespesaModel>()
                .Property(d => d.Categoria)
                .HasMaxLength(50);

            modelBuilder.Entity<DespesaModel>()
                .Property(d => d.Data)
                .IsRequired();

            modelBuilder.Entity<DespesaModel>()
                .HasOne(d => d.Usuario)
                .WithMany()
                .HasForeignKey(d => d.UsuarioId);

            // === RECEITAS ===
            modelBuilder.Entity<ReceitaModel>().ToTable("Receitas", schema: "Sistema");

            modelBuilder.Entity<ReceitaModel>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ReceitaModel>()
                .Property(r => r.TokenId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            modelBuilder.Entity<ReceitaModel>()
                .Property(r => r.Valor)
                .HasColumnType("money")
                .IsRequired();

            modelBuilder.Entity<ReceitaModel>()
                .Property(r => r.Data)
                .IsRequired();

            modelBuilder.Entity<ReceitaModel>()
                .Property(r => r.Descricao)
                .HasMaxLength(256);

            modelBuilder.Entity<ReceitaModel>()
                .HasOne(r => r.Usuario)
                .WithMany()
                .HasForeignKey(r => r.UsuarioId);
        }
    }
}
