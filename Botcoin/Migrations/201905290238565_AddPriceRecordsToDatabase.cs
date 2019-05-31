namespace Botcoin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceRecordsToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Coin = c.String(nullable: false, maxLength: 5),
                        Price = c.Double(nullable: false),
                        DateRegistered = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PriceRecords");
        }
    }
}
