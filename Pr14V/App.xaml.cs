using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Pr14V
{
    public partial class App : Application
{
    // Сюда мы запишем данные пользователя после успешного входа
    public static Users CurrentUser { get; set; }

    // Контекст вашей БД (проверьте имя вашего класса сущностей)
    public static CinemaDBEntities Context { get; } = new CinemaDBEntities();
}

}
