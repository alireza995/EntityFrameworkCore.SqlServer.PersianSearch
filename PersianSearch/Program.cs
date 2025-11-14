using EntityFrameworkCore.SqlServer.PersianSearch;
using Microsoft.EntityFrameworkCore;
using PersianSearch;

await using var context = new Context();
await context.Database.MigrateAsync();

if (!await context.TblTests.AnyAsync())
{
  await context.AddAsync(new TblTest { FirstName = "فاطمه", LastName = "احمدی" });
  await context.AddAsync(new TblTest { FirstName = "فاطمه‌ئلیا", LastName = "حسینی" });
  await context.AddAsync(new TblTest { FirstName = "مریم", LastName = "رضایی" });
  await context.AddAsync(new TblTest { FirstName = "سارا", LastName = "كريمی" }); // ك = Arabic Kaf
  await context.AddAsync(new TblTest { FirstName = "نگین", LastName = "محمدی" });
  await context.AddAsync(new TblTest { FirstName = "نرگس", LastName = "ابراهیمی" });
  await context.AddAsync(new TblTest { FirstName = "آوا", LastName = "گل‌محمدی" }); // گ + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "یاسمن", LastName = "یاسینی" }); // ی (Persian Yeh)
  await context.AddAsync(new TblTest { FirstName = "لاله", LastName = "کاویانی" }); // ک (Persian Ke)
  await context.AddAsync(new TblTest { FirstName = "حسین", LastName = "گیتی" }); // گ
  await context.AddAsync(new TblTest { FirstName = "رضا", LastName = "فاطمی" });
  await context.AddAsync(new TblTest { FirstName = "امیر", LastName = "نجفی" });
  await context.AddAsync(new TblTest { FirstName = "علی", LastName = "قاسمی" });
  await context.AddAsync(new TblTest { FirstName = "محمد", LastName = "رضوی" });
  await context.AddAsync(new TblTest { FirstName = "امین", LastName = "شهیدی" });
  await context.AddAsync(new TblTest { FirstName = "آرش", LastName = "آزاد" });
  await context.AddAsync(new TblTest { FirstName = "كیان", LastName = "ایزدی" }); // ك = Arabic Kaf
  await context.AddAsync(new TblTest { FirstName = "گرشا", LastName = "گنجی" }); // گ
  await context.AddAsync(new TblTest { FirstName = "ارژن", LastName = "کامکار" }); // ک
  await context.AddAsync(new TblTest { FirstName = "یلدا", LastName = "يزدانی" }); // ي = Arabic Yeh
  await context.AddAsync(new TblTest { FirstName = "شیدا", LastName = "کریمی" });
  await context.AddAsync(new TblTest { FirstName = "نازنین", LastName = "کاظمی" });
  await context.AddAsync(new TblTest { FirstName = "بهار", LastName = "گلزار" });
  await context.AddAsync(new TblTest { FirstName = "پریا", LastName = "فراهانی" });
  await context.AddAsync(new TblTest { FirstName = "هانیه", LastName = "حسین‌زاده" }); // ZWNJ
  await context.AddAsync(new TblTest { FirstName = "فائزه", LastName = "رضایی‌پور" }); // ZWNJ + ی
  await context.AddAsync(new TblTest { FirstName = "طاهره", LastName = "محمد‌نژاد" }); // ZWNJ
  await context.AddAsync(new TblTest { FirstName = "مرضیه", LastName = "كاظمی" }); // ك
  await context.AddAsync(new TblTest { FirstName = "سحر", LastName = "یوسفی" });
  await context.AddAsync(new TblTest { FirstName = "ندا", LastName = "گودرزی" }); // گ
  await context.AddAsync(new TblTest { FirstName = "آزیتا", LastName = "كیانی" }); // ك
  await context.AddAsync(new TblTest { FirstName = "فریده", LastName = "گل‌پرور" }); // گ + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "زهرا", LastName = "ئلچی" }); // ئ = Yeh with Hamza
  await context.AddAsync(new TblTest { FirstName = "فاطمه‌ئ", LastName = "احمدیان" }); // ه‌ئ
  await context.AddAsync(new TblTest { FirstName = "مریم‌السادات", LastName = "حسینی" }); // ZWNJ + ا
  await context.AddAsync(new TblTest { FirstName = "ساره", LastName = "یزدی" });
  await context.AddAsync(new TblTest { FirstName = "نگار", LastName = "كوهستانی" }); // ك
  await context.AddAsync(new TblTest { FirstName = "هدیه", LastName = "کشاورز" }); // ک
  await context.AddAsync(new TblTest { FirstName = "فائزه", LastName = "گلشن" }); // گ
  await context.AddAsync(new TblTest { FirstName = "یاسمن", LastName = "يزدی" }); // ي
  await context.AddAsync(new TblTest { FirstName = "لاله", LastName = "گلزاری" }); // گ
  await context.AddAsync(new TblTest { FirstName = "رضا", LastName = "کریم‌زاده" }); // ک + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "حسین", LastName = "كاظم‌زاده" }); // ك + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "امیر", LastName = "گل‌محمدی" }); // گ + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "علی", LastName = "یعقوبی" }); // ی
  await context.AddAsync(new TblTest { FirstName = "محمد", LastName = "گرمساری" }); // گ
  await context.AddAsync(new TblTest { FirstName = "امین", LastName = "کرمانی" }); // ک
  await context.AddAsync(new TblTest { FirstName = "آرش", LastName = "گیلانی" }); // گ
  await context.AddAsync(new TblTest { FirstName = "كیان", LastName = "یزدی" }); // ك + ی
  await context.AddAsync(new TblTest { FirstName = "گرشا", LastName = "كیانی" }); // گ + ك
  await context.AddAsync(new TblTest { FirstName = "ارژن", LastName = "گنجوی" }); // گ
  await context.AddAsync(new TblTest { FirstName = "یلدا", LastName = "كاظمی" }); // ي + ك
  await context.AddAsync(new TblTest { FirstName = "شیدا", LastName = "گلپا" }); // گ
  await context.AddAsync(new TblTest { FirstName = "نازنین", LastName = "یوسف‌زاده" }); // ی + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "بهار", LastName = "فاطمی‌پور" }); // ZWNJ
  await context.AddAsync(new TblTest { FirstName = "پریا", LastName = "حسین‌پور" }); // ZWNJ
  await context.AddAsync(new TblTest { FirstName = "هانیه", LastName = "محمدی‌نیا" }); // ZWNJ + ی
  await context.AddAsync(new TblTest { FirstName = "فائزه", LastName = "گل‌نژاد" }); // گ + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "طاهره", LastName = "كشاورزی" }); // ك
  await context.AddAsync(new TblTest { FirstName = "مرضیه", LastName = "یزدانی" }); // ی
  await context.AddAsync(new TblTest { FirstName = "سحر", LastName = "گلزاری" }); // گ
  await context.AddAsync(new TblTest { FirstName = "ندا", LastName = "كیان‌پور" }); // ك + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "آزیتا", LastName = "گودرزی" }); // گ
  await context.AddAsync(new TblTest { FirstName = "فریده", LastName = "یوسفی‌زاده" }); // ی + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "زهرا", LastName = "كاظمی" }); // ك
  await context.AddAsync(new TblTest { FirstName = "فاطمه", LastName = "گل‌محمدی" }); // گ + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "مریم", LastName = "كیانی" }); // ك
  await context.AddAsync(new TblTest { FirstName = "سارا", LastName = "یزدی" }); // ی
  await context.AddAsync(new TblTest { FirstName = "نگین", LastName = "گلشن" }); // گ
  await context.AddAsync(new TblTest { FirstName = "نرگس", LastName = "كوهستانی" }); // ك
  await context.AddAsync(new TblTest { FirstName = "آوا", LastName = "یعقوبی" }); // ی
  await context.AddAsync(new TblTest { FirstName = "یاسمن", LastName = "گرمساری" }); // گ
  await context.AddAsync(new TblTest { FirstName = "لاله", LastName = "كیانی" }); // ك
  await context.AddAsync(new TblTest { FirstName = "حسین", LastName = "گیلانی" }); // گ
  await context.AddAsync(new TblTest { FirstName = "رضا", LastName = "یزدانی" }); // ی
  await context.AddAsync(new TblTest { FirstName = "امیر", LastName = "كاظم‌زاده" }); // ك + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "علی", LastName = "گل‌پرور" }); // گ + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "محمد", LastName = "کشاورز" }); // ک
  await context.AddAsync(new TblTest { FirstName = "امین", LastName = "گلزار" }); // گ
  await context.AddAsync(new TblTest { FirstName = "آرش", LastName = "یزدی" }); // ی
  await context.AddAsync(new TblTest { FirstName = "كیان", LastName = "گودرزی" }); // ك + گ
  await context.AddAsync(new TblTest { FirstName = "گرشا", LastName = "فاطمی" }); // گ
  await context.AddAsync(new TblTest { FirstName = "ارژن", LastName = "كیان‌پور" }); // ك + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "یلدا", LastName = "گل‌نژاد" }); // گ + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "شیدا", LastName = "یوسفی" }); // ی
  await context.AddAsync(new TblTest { FirstName = "نازنین", LastName = "حمدانی" });
  await context.AddAsync(new TblTest { FirstName = "بهار", LastName = "كاظمی" }); // ك
  await context.AddAsync(new TblTest { FirstName = "پریا", LastName = "گل‌محمدی" }); // گ + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "هانیه", LastName = "یزدانی" }); // ی
  await context.AddAsync(new TblTest { FirstName = "فائزه", LastName = "كیانی" }); // ك
  await context.AddAsync(new TblTest { FirstName = "طاهره", LastName = "گلزاری" }); // گ
  await context.AddAsync(new TblTest { FirstName = "مرضیه", LastName = "یعقوبی" }); // ی
  await context.AddAsync(new TblTest { FirstName = "سحر", LastName = "گرمساری" }); // گ
  await context.AddAsync(new TblTest { FirstName = "ندا", LastName = "كاظمی" }); // ك
  await context.AddAsync(new TblTest { FirstName = "آزیتا", LastName = "یوسفی" }); // ی
  await context.AddAsync(new TblTest { FirstName = "فریده", LastName = "گیلانی" }); // گ
  await context.AddAsync(new TblTest { FirstName = "زهرا", LastName = "كوهستانی" }); // ك
  await context.AddAsync(new TblTest { FirstName = "فاطمه", LastName = "گلشن" }); // گ
  await context.AddAsync(new TblTest { FirstName = "مریم", LastName = "یزدی" }); // ی
  await context.AddAsync(new TblTest { FirstName = "سارا", LastName = "كیانی" }); // ك
  await context.AddAsync(new TblTest { FirstName = "نگین", LastName = "گل‌پرور" }); // گ + ZWNJ
  await context.AddAsync(new TblTest { FirstName = "نرگس", LastName = "حسینی" });
  await context.AddAsync(new TblTest { FirstName = "آوا", LastName = "محمدی" });
  await context.AddAsync(new TblTest { FirstName = "یاسمن", LastName = "ابراهیمی" });
  await context.AddAsync(new TblTest { FirstName = "لاله", LastName = "رضایی" });
  await context.AddAsync(new TblTest { FirstName = "حسین", LastName = "احمدی" });

  await context.SaveChangesAsync();
}
Console.WriteLine("enter search text");
var searchText = "اوا يع";
var res = await context.TblTests.ContainsPersian(
    i => i.FirstName + " " + i.LastName,
    searchText)
  .ToListAsync();

foreach (var item in res) Console.WriteLine($"{item.FirstName} {item.LastName}");

Console.WriteLine("***end***");
Console.ReadKey();
