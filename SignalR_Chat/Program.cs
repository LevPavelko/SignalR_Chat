using Microsoft.EntityFrameworkCore;
using SignalR_Chat;   // ������������ ���� ������ ChatHub

var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<ChatContext>(options => options.UseSqlServer(connection));
// ��� ������������� ���������������� ���������� SignalR,
// � ���������� ���������� ���������������� ��������������� �������
builder.Services.AddSignalR();

var app = builder.Build();
// � ������� ������������ ������ ���������� UseDefaultFiles() ����� ���������
// �������� ����������� ���-������� �� ��������� ��� ��������� � ��� �� ������� ����.
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapHub<ChatHub>("/chat");   // ChatHub ����� ������������ ������� �� ���� /chat

app.Run();
