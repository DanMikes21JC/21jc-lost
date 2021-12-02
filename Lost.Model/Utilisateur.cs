using System.ComponentModel.DataAnnotations.Schema;

namespace Lost.Model
{
    public class Utilisateur : IdEntity
    {
        [Column("discord_auth")]
        public string DiscordAuth { get; set; }

        [ForeignKey(nameof(PersonneId))]
        public Personne Personne { get; set; }

        [ForeignKey(nameof(Personne))]
        [Column("id_personne")]
        public long? PersonneId { get; set; }

        [Column("role")]
        public string Role { get; set; }
    }
}
