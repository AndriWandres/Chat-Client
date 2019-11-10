using AutoMapper;
using ChatApp.Api.Models.Domain;
using ChatApp.Api.Models.Dto.User;
using ChatApp.Api.Models.ViewModel.Contact;

namespace ChatApp.Api.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, RegisterDto>().ReverseMap();

            CreateMap<MessageRecipient, ContactViewModel>()
                .ForMember(cvm => cvm.UnreadMessagesCount, config =>
                {
                    config.MapFrom((mr, vm, member, context) => context.Items["UnreadMessagesCount"]);
                })
                .ForMember(cvm => cvm.Message, config =>
                {
                    config.MapFrom(mr => new ContactViewModel.LatestMessage
                    {
                        MessageId = mr.MessageId,
                        CreatedAt = mr.Message.CreatedAt,
                        TextContent = mr.Message.TextContent,
                        AuthorName = mr.Message.Author.DisplayName,
                    });
                })
                .ForMember(cvm => cvm.RecipientGroup, config =>
                {
                    config.Condition(mr => mr.RecipientGroup != null);
                    config.MapFrom(mr => new ContactViewModel.ContactRecipientGroup
                    {
                        GroupId = mr.RecipientGroup.GroupId,
                        Name = mr.RecipientGroup.Group.Name,
                    });
                })
                .ForMember(cvm => cvm.RecipientUser, config =>
                {
                    config.Condition(mr => mr.RecipientUser != null);
                    config.MapFrom(mr => new ContactViewModel.ContactRecipientUser
                    {
                        UserId = mr.RecipientUser.UserId,
                        DisplayName = mr.RecipientUser.DisplayName,
                    });
                });                
        }
    }
}
