using ClaretBrain.Domain.Entities;
using ClaretBrain.Domain.Enums;
using TaskStatusEnum = ClaretBrain.Domain.Enums.TaskStatus;
using ClaretBrain.Infrastructure.Persistence;

namespace ClaretBrain.Api.Seed;

public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Agents.Any() || db.Tasks.Any() || db.FeedThreads.Any()) return;

        db.Agents.AddRange(new[]
        {
            new Agent { Name = "Agent-Alpha", Status = AgentStatus.Busy, CurrentTask = "Analisar dataset de vendas Q4", Model = "GPT-4o" },
            new Agent { Name = "Agent-Beta", Status = AgentStatus.Online, CurrentTask = "Gerar relatório de sentimento", Model = "Claude 3.5" },
            new Agent { Name = "Agent-Gamma", Status = AgentStatus.Busy, CurrentTask = "Classificar tickets de suporte", Model = "Gemini Pro" },
            new Agent { Name = "Agent-Delta", Status = AgentStatus.Idle, Model = "Claude 3.5" },
            new Agent { Name = "Agent-Epsilon", Status = AgentStatus.Offline, Model = "GPT-4o" },
        });

        db.Tasks.AddRange(new[]
        {
            new KanbanTask { Title = "Analisar dataset de vendas Q4", Description = "Processar e analisar o dataset completo de vendas do quarto trimestre, identificando tendências, anomalias e oportunidades de crescimento.", Agent = "Agent-Alpha", Model = "GPT-4o", Status = TaskStatusEnum.Todo, Priority = Priority.Critical },
            new KanbanTask { Title = "Gerar relatório de sentimento", Description = "Analisar sentimento de todas as reviews de clientes recebidas no último mês e gerar relatório consolidado com insights.", Agent = "Agent-Beta", Model = "Claude 3.5", Status = TaskStatusEnum.Todo, Priority = Priority.High },
            new KanbanTask { Title = "Classificar tickets de suporte", Description = "Classificar automaticamente os tickets abertos por categoria, urgência e departamento responsável.", Agent = "Agent-Gamma", Model = "Gemini Pro", Status = TaskStatusEnum.Doing, Priority = Priority.Critical },
            new KanbanTask { Title = "Extrair entidades de contratos", Description = "Utilizar NER para extrair entidades relevantes (datas, valores, partes envolvidas) de contratos digitalizados.", Agent = "Agent-Alpha", Model = "GPT-4o", Status = TaskStatusEnum.Doing, Priority = Priority.Low },
            new KanbanTask { Title = "Resumir transcrições de reuniões", Description = "Gerar resumos executivos das transcrições de reuniões da última semana, destacando decisões e action items.", Agent = "Agent-Delta", Model = "Claude 3.5", Status = TaskStatusEnum.Done, Priority = Priority.Medium },
            new KanbanTask { Title = "Traduzir documentação técnica", Description = "Traduzir a documentação técnica do produto para inglês, espanhol e francês mantendo terminologia consistente.", Agent = "Agent-Beta", Model = "GPT-4o", Status = TaskStatusEnum.Done, Priority = Priority.Trivial },
        });

        db.FeedThreads.Add(new FeedThread
        {
            Agent = "Agent-Alpha",
            TaskTitle = "Analisar dataset de vendas Q4",
            Status = FeedThreadStatus.Running,
            Actions = new List<FeedAction>
            {
                new FeedAction { Type = FeedActionType.ModelInteraction, Message = "Enviou prompt de análise de sentimento", Model = "GPT-4o", TokensIn = 1200, TokensOut = 450, Duration = "2.4s" },
                new FeedAction { Type = FeedActionType.Command, Message = "Executou comando local", Command = "python scripts/preprocess.py --input data.csv", Output = "Processados 2.450 registros em 12.3s", Duration = "12.3s" }
            }
        });

        db.SaveChanges();
    }
}
