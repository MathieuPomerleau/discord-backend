namespace Injhinuity.Backend.Model.Domain.Requests
{
    public record RoleGuildSettingsRequest(string ReactionRoleChannelId, string ReactionRoleMessageId, string MuteRoleId);
}
