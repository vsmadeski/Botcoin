namespace Botcoin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePriceRecordsFromDatabase : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.PriceRecords");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PriceRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Coin = c.String(nullable: false, maxLength: 5),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 10),
                        DateRegistered = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
