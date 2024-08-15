using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Model
{
    public class Setting
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
