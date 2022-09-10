using Castle.DynamicProxy;
using System.Reflection;
using POC.HexagonalKafkaCache.Core.Application;
using POC.HexagonalKafkaCache.Core.Domain.Entities;
using POC.HexagonalKafkaCache.Core.Ports.In.Commands;
using POC.HexagonalKafkaCache.Core.Ports.In.Queries;
using POC.HexagonalKafkaCache.Core.Ports.Out;
using POC.HexagonalKafkaCache.Core.Utils;
using POC.HexagonalKafkaCache.Infrastructure.KafkaRepository;
using POC.HexagonalKafkaCache.Infrastructure.SqlDapperRepository;
using POC.HexagonalKafkaCache.Middlewares;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<Client, ListAllClientsDtoResponse>();
});

builder.Services.AddMemoryCache();
builder.Services.AddSingleton(new ProxyGenerator());
builder.Services.AddScoped<IInterceptor, CacheInterceptor>();
builder.Services.AddProxiedScoped<IClientRepository, ClientRepositorySqlAdapter>();
builder.Services.AddScoped<IListAllClientsUseCase, ListAllClientsUseCase>();
builder.Services.AddScoped<ISaveClientUseCase, SaveClientUseCase>();
builder.Services.AddScoped<IKafkaMessageProducerRepository, KafkaMessageProducerRepository>();
builder.Services.AddHostedService<KafkaBackgroundListener>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
//app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();
app.Run();
