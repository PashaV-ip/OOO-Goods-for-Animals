﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace OOO_Goods_for_Animals.DbEntity
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class TradeEntities : DbContext
{
    public TradeEntities()
        : base("name=TradeEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<CategoryItem> CategoryItem { get; set; }

    public virtual DbSet<ManufacturerItem> ManufacturerItem { get; set; }

    public virtual DbSet<NameItem> NameItem { get; set; }

    public virtual DbSet<Order> Order { get; set; }

    public virtual DbSet<OrderHasProduct> OrderHasProduct { get; set; }

    public virtual DbSet<PickupPoint> PickupPoint { get; set; }

    public virtual DbSet<ProviderItem> ProviderItem { get; set; }

    public virtual DbSet<Role> Role { get; set; }

    public virtual DbSet<StatusOrder> StatusOrder { get; set; }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<UserInfo> UserInfo { get; set; }

    public virtual DbSet<Product> Product { get; set; }

}

}

