using Application.Admission.Imports;
using Application.Admission.Sheet;
using Application.Interfaces.Admission.Imports;
using Application.Interfaces.Admission.Sheet;
using Application.Interfaces.Services.Domain;
using Application.Interfaces.Services.Excel;
using Application.Interfaces.Services.Standard;
using Application.Interfaces.Utilities;
using Application.Services.Domain;
using Application.Services.Excel;
using Application.Services.Standard;
using Application.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC
{
    public static class ServicesIoC
    {
        public static void ApplicationServicesIoC(this IServiceCollection services)
        {
            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ITaskToDoService, TaskToDoService>();
            services.AddScoped<IFileToBinary, FileToBinary>();
            services.AddScoped<ISheetAdmissionService, SheetAdmissionService>();
            services.AddScoped<IValidationErros, ValidationErros> ();
            services.AddScoped<IExcelServices, ExcelServices>();
            services.AddScoped<ISheetAdmissionValidation, SheetAdmissionValidation>();
            services.AddScoped<IImportsService, ImportsService>();

        }
    }
}
