# TssT
Проект-Домашка для тех кто изучает C#, dotnet core, aspnet core, mssql, ef core и так далее


Система для создания опросов по технологиям. Я решил назвать это TssT - что неочевидно расшифровывается Technology stack self-confidence Test.

Идея достаточно простая: есть ровно один вопрос. Насколько вы уверенно знаете технологию (вставь название технологии). И есть ответ в виде степени уверенности (Никогда не видел, слышал/читал, пару раз использовал в пет проектах, использовал в продакшене, уверенно использовал в продакшене, имею глубокое представление). Каждый пункт ответа по своему оценивается. Опрос или тест рассчитан на честность иначе он не будет работать.

Типы пользователей системы:

Админ - будет работать над созданием тестов
Юзер - будет проходить тесты и смотреть статистику, оставлять рейтинг
Функционал:

возможность регаться, логиниться
возможность под админом проводить CRUD тестов
возможность юзером открывать список тестов
возможность юзером начинать проходить тест (с промежуточным сохранением результата)
возможность юзером смотреть статистику. Думаю пока обезличено, чисто сравнить свое понимание технологий с другими.
Это описание MVP - который даст возможность собирать первичную стату по технологиям. Пока особенной цели у данного MVP нету.

Вторая версия будет включать в себя доработку для импорта и фильтрации вакансий по указанной полученной статистике.

В финале видится система куда ты можешь зайти, пройти опрос и увидеть насколько ты хуже или лучше чем рядовой разработчик определенного стека, определенной компании. Это также может косвенно помочь понять, что еще не хватает изучить до желаемой вакансии.
