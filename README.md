# AI.Back.ClaretBrain

API em C# (DDD + Clean Architecture) para integrar com o front Lovable.

## Endpoints
- `POST /auth/login` (token simples)
- `GET/POST/PATCH /agents`
- `GET/POST/PATCH /tasks`
- `GET/POST /feed/threads`
- `POST /feed/actions?threadId=`
- `GET /health`
- `WS /ws` (SignalR)

## Auth
Header: `x-panel-token: <TOKEN>` ou query `?token=` (para WS).
Configure em `appsettings.json` ou `Auth__Token`.

## Banco
Usa Postgres (claret). Configure `ConnectionStrings__Default`.

## Run
```bash
cd ClaretBrain.Api
export ConnectionStrings__Default="Host=localhost;Port=5432;Database=claret;Username=claret;Password=..."
export Auth__Token="seu-token"

 dotnet run
```
