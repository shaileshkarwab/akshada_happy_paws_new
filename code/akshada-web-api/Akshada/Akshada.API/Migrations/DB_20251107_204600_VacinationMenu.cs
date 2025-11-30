using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251107204600)]
    public class DB_20251107_204600_VacinationMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 16,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Vacination Record",
                    seq_no = 3,
                    controller = DBNull.Value,
                    page = "list-vacination-record-details",
                    fa_icon = "fas fa-syringe",
                    parent_id = 5,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
