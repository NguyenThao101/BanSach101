using BanSach.Components.Model;

namespace BanSach.Components.Services.QNAService
{
    public interface IQNAService
    {
        Task<List<QNA>> GetQuestionsAsync();
        Task AddQuestionAsync(string question, string email);
        Task AddAnswerAsync(int questionId, string answer, string answererEmail);
        Task DeleteQuestionAsync(int questionId);
    }
}
