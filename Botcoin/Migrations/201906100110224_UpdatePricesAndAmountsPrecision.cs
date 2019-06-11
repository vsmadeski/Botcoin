namespace Botcoin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePricesAndAmountsPrecision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BuyOrderModels", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 10));
            AlterColumn("dbo.BuyOrderModels", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 10));
            AlterColumn("dbo.SellOrderModels", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 10));
            AlterColumn("dbo.SellOrderModels", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SellOrderModels", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SellOrderModels", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BuyOrderModels", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BuyOrderModels", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
