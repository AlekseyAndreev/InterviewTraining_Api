using InterviewTraining.Application.CreateInterviewChatMessage.V10;
using InterviewTraining.Application.GetInterviewChatMessages.V10;
using InterviewTraining.Application.UpdateInterviewChatMessage.V10;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для работы с сообщениями чата для интервью
/// </summary>
public interface IInterviewChatMessageService
{
    /// <summary>
    /// Создать сообщение в чате интервью
    /// </summary>
    /// <remarks>
    /// Доступно кандидату, эксперту (участвующим в собеседовании) или администратору.
    /// Тип отправителя определяется автоматически на основе роли пользователя в собеседовании.
    /// </remarks>
    Task<CreateInterviewChatMessageResponse> CreateInterviewChatMessageAsync(CreateInterviewChatMessageRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Редактировать сообщение в чате интервью
    /// </summary>
    /// <remarks>
    /// Доступно только автору сообщения.
    /// При редактировании устанавливается признак IsEdited и дата модификации.
    /// </remarks>
    Task<UpdateInterviewChatMessageResponse> UpdateInterviewChatMessageAsync(UpdateInterviewChatMessageRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Получить сообщения чата интервью
    /// </summary>
    /// <remarks>
    /// Доступно кандидату, эксперту (участвующим в собеседовании) или администратору.
    /// </remarks>
    Task<GetInterviewChatMessagesResponse> GetInterviewChatMessagesAsync(GetInterviewChatMessagesRequest request, CancellationToken cancellationToken);
}
