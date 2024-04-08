using SignalR_Chat;   // ������������ ���� ������ ChatHub

var builder = WebApplication.CreateBuilder(args);
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
