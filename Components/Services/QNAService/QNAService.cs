using BanSach.Components.Data;
using BanSach.Components.Model;
using Microsoft.EntityFrameworkCore;

namespace BanSach.Components.Services.QNAService
{
    public class QNAService : IQNAService
    {
        private readonly AppDbContext context;

        public QNAService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAnswerAsync(int questionId, string answer, string answererEmail)
        {
            var question = await context.QNA.FindAsync(questionId);
            if (question != null)
            {
                question.Answer = answer ?? "<Chưa có câu trả lời>";  // Thay thế nếu answer là null
                question.AnswererEmail = answererEmail ?? "No email provided";  // Thay thế nếu email là null
                await context.SaveChangesAsync();
            }
        }

        public async Task AddQuestionAsync(string question, string email)
        {
            var newQuestion = new QNA
            {
                Question = question,
                Answer = "No answer provided",  // Gán giá trị mặc định cho câu trả lời
                AnswererEmail = "No email provided",  // Gán email mặc định nếu cần
                Email = email,  // Lưu email của người hỏi
                CreatedAt = DateTime.Now
            };

            context.QNA.Add(newQuestion);
            await context.SaveChangesAsync();
        }

        public async Task<List<QNA>> GetQuestionsAsync()
        {
            var questions = await context.QNA
                .OrderByDescending(q => q.CreatedAt)
                .Select(q => new QNA
                {
                    Id = q.Id,
                    Question = q.Question,
                    // Kiểm tra nếu câu trả lời rỗng, không gán "No answer provided"
                    Answer = string.IsNullOrEmpty(q.Answer) ? "" : q.Answer,  // Trả về chuỗi rỗng nếu chưa có câu trả lời
                    AnswererEmail = q.AnswererEmail ?? "No email provided", // Kiểm tra NULL và thay thế
                    Email = q.Email ?? "No email provided", // Kiểm tra NULL và thay thế
                    CreatedAt = q.CreatedAt
                })
                .ToListAsync();

            return questions;
        }

         public async Task DeleteQuestionAsync(int questionId)
        {
            var question = await context.QNA.FindAsync(questionId);
            if (question != null)
            {
                context.QNA.Remove(question); // Xóa câu hỏi
                await context.SaveChangesAsync();
            }
        }


    }
}
