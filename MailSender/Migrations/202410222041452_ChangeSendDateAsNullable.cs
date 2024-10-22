namespace MailSender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSendDateAsNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmailMessages", "SendDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmailMessages", "SendDate", c => c.DateTime(nullable: false));
        }
    }
}
