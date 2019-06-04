namespace Botcoin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSellOrdersToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SellOrderModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Coin = c.String(nullable: false, maxLength: 5),
                        Amount = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        DateRegistered = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SellOrderModels");
        }
    }
}
