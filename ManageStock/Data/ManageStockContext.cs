using System;
using System.Collections.Generic;
using ManageStock.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageStock.Data;

public partial class ManageStockContext : DbContext
{
    public ManageStockContext()
    {
    }

    public ManageStockContext(DbContextOptions<ManageStockContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorie> Categories { get; set; }

    public virtual DbSet<Detailinventaire> Detailinventaires { get; set; }

    public virtual DbSet<Entreestock> Entreestocks { get; set; }

    public virtual DbSet<Entrepot> Entrepots { get; set; }

    public virtual DbSet<Fournisseur> Fournisseurs { get; set; }

    public virtual DbSet<Inventaire> Inventaires { get; set; }

    public virtual DbSet<Produit> Produits { get; set; }

    public virtual DbSet<Sortiestock> Sortiestocks { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.IdCategorie).HasName("categorie_pkey");

            entity.ToTable("categorie");

            entity.Property(e => e.IdCategorie)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_categorie");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Detailinventaire>(entity =>
        {
            entity.HasKey(e => e.IdDetail).HasName("detailinventaire_pkey");

            entity.ToTable("detailinventaire");

            entity.Property(e => e.IdDetail)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_detail");
            entity.Property(e => e.IdInventaire).HasColumnName("id_inventaire");
            entity.Property(e => e.IdProduit).HasColumnName("id_produit");
            entity.Property(e => e.QuantitéComptée).HasColumnName("quantité_comptée");

            entity.HasOne(d => d.IdInventaireNavigation).WithMany(p => p.Detailinventaires)
                .HasForeignKey(d => d.IdInventaire)
                .HasConstraintName("detailinventaire_id_inventaire_fkey");

            entity.HasOne(d => d.IdProduitNavigation).WithMany(p => p.Detailinventaires)
                .HasForeignKey(d => d.IdProduit)
                .HasConstraintName("detailinventaire_id_produit_fkey");
        });

        modelBuilder.Entity<Entreestock>(entity =>
        {
            entity.HasKey(e => e.IdEntree).HasName("entreestock_pkey");

            entity.ToTable("entreestock");

            entity.Property(e => e.IdEntree)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_entree");
            entity.Property(e => e.DateEntree)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_entree");
            entity.Property(e => e.IdEntrepot).HasColumnName("id_entrepot");
            entity.Property(e => e.IdFournisseur).HasColumnName("id_fournisseur");
            entity.Property(e => e.IdProduit).HasColumnName("id_produit");
            entity.Property(e => e.Quantité).HasColumnName("quantité");

            entity.HasOne(d => d.IdEntrepotNavigation).WithMany(p => p.Entreestocks)
                .HasForeignKey(d => d.IdEntrepot)
                .HasConstraintName("entreestock_id_entrepot_fkey");

            entity.HasOne(d => d.IdFournisseurNavigation).WithMany(p => p.Entreestocks)
                .HasForeignKey(d => d.IdFournisseur)
                .HasConstraintName("entreestock_id_fournisseur_fkey");

            entity.HasOne(d => d.IdProduitNavigation).WithMany(p => p.Entreestocks)
                .HasForeignKey(d => d.IdProduit)
                .HasConstraintName("entreestock_id_produit_fkey");
        });

        modelBuilder.Entity<Entrepot>(entity =>
        {
            entity.HasKey(e => e.IdEntrepot).HasName("entrepot_pkey");

            entity.ToTable("entrepot");

            entity.Property(e => e.IdEntrepot)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_entrepot");
            entity.Property(e => e.Adresse).HasColumnName("adresse");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Fournisseur>(entity =>
        {
            entity.HasKey(e => e.IdFournisseur).HasName("fournisseur_pkey");

            entity.ToTable("fournisseur");

            entity.Property(e => e.IdFournisseur)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_fournisseur");
            entity.Property(e => e.Adresse).HasColumnName("adresse");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nom)
                .HasMaxLength(150)
                .HasColumnName("nom");
            entity.Property(e => e.Téléphone)
                .HasMaxLength(20)
                .HasColumnName("téléphone");
        });

        modelBuilder.Entity<Inventaire>(entity =>
        {
            entity.HasKey(e => e.IdInventaire).HasName("inventaire_pkey");

            entity.ToTable("inventaire");

            entity.Property(e => e.IdInventaire)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_inventaire");
            entity.Property(e => e.Commentaire).HasColumnName("commentaire");
            entity.Property(e => e.DateInventaire)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("date_inventaire");
            entity.Property(e => e.IdEntrepot).HasColumnName("id_entrepot");

            entity.HasOne(d => d.IdEntrepotNavigation).WithMany(p => p.Inventaires)
                .HasForeignKey(d => d.IdEntrepot)
                .HasConstraintName("inventaire_id_entrepot_fkey");
        });

        modelBuilder.Entity<Produit>(entity =>
        {
            entity.HasKey(e => e.IdProduit).HasName("produit_pkey");

            entity.ToTable("produit");

            entity.HasIndex(e => e.CodeBarres, "produit_code_barres_key").IsUnique();

            entity.Property(e => e.IdProduit)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_produit");
            entity.Property(e => e.CodeBarres)
                .HasMaxLength(50)
                .HasColumnName("code_barres");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdCategorie).HasColumnName("id_categorie");
            entity.Property(e => e.IdFournisseur).HasColumnName("id_fournisseur");
            entity.Property(e => e.Nom)
                .HasMaxLength(150)
                .HasColumnName("nom");
            entity.Property(e => e.PrixUnitaire)
                .HasPrecision(10, 2)
                .HasColumnName("prix_unitaire");
            entity.Property(e => e.SeuilAlerte)
                .HasDefaultValue(0)
                .HasColumnName("seuil_alerte");

            entity.HasOne(d => d.IdCategorieNavigation).WithMany(p => p.Produits)
                .HasForeignKey(d => d.IdCategorie)
                .HasConstraintName("produit_id_categorie_fkey");

            entity.HasOne(d => d.IdFournisseurNavigation).WithMany(p => p.Produits)
                .HasForeignKey(d => d.IdFournisseur)
                .HasConstraintName("produit_id_fournisseur_fkey");
        });

        modelBuilder.Entity<Sortiestock>(entity =>
        {
            entity.HasKey(e => e.IdSortie).HasName("sortiestock_pkey");

            entity.ToTable("sortiestock");

            entity.Property(e => e.IdSortie)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_sortie");
            entity.Property(e => e.DateSortie)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_sortie");
            entity.Property(e => e.Destination).HasColumnName("destination");
            entity.Property(e => e.IdEntrepot).HasColumnName("id_entrepot");
            entity.Property(e => e.IdProduit).HasColumnName("id_produit");
            entity.Property(e => e.Quantité).HasColumnName("quantité");

            entity.HasOne(d => d.IdEntrepotNavigation).WithMany(p => p.Sortiestocks)
                .HasForeignKey(d => d.IdEntrepot)
                .HasConstraintName("sortiestock_id_entrepot_fkey");

            entity.HasOne(d => d.IdProduitNavigation).WithMany(p => p.Sortiestocks)
                .HasForeignKey(d => d.IdProduit)
                .HasConstraintName("sortiestock_id_produit_fkey");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.IdStock).HasName("stock_pkey");

            entity.ToTable("stock");

            entity.HasIndex(e => new { e.IdProduit, e.IdEntrepot }, "stock_id_produit_id_entrepot_key").IsUnique();

            entity.Property(e => e.IdStock)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_stock");
            entity.Property(e => e.IdEntrepot).HasColumnName("id_entrepot");
            entity.Property(e => e.IdProduit).HasColumnName("id_produit");
            entity.Property(e => e.QuantitéDisponible)
                .HasDefaultValue(0)
                .HasColumnName("quantité_disponible");

            entity.HasOne(d => d.IdEntrepotNavigation).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.IdEntrepot)
                .HasConstraintName("stock_id_entrepot_fkey");

            entity.HasOne(d => d.IdProduitNavigation).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.IdProduit)
                .HasConstraintName("stock_id_produit_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
