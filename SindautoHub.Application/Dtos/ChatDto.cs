using SindautoHub.Application.Dtos.ChatMessageDtos;

namespace SindautoHub.Application.Dtos.ChatDtos;

public class CreateChatRequest
{
    public List<Guid> ParticipantIds { get; set; }
}

public class ChatResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<UserSummaryResponse> Participants { get; set; }
    public List<ChatMessageResponse> Messages { get; set; }
}

public class UserSummaryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}



    public class InitiateChatRequest
    {
        public Guid RecipientId { get; set; }
    }


