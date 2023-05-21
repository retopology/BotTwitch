# BotTwitch
Для использования, нужно обязательно скачать Visual Studio. (Желательно 2022)

Чтобы использовать бота, нужно подключаться к базе, 
в которой есть следующая структура:

DataBase: witch

Tabels:

1) commands

![image](https://github.com/retopology/BotTwitch/assets/88750093/97b214cf-d5cf-48bf-b3a0-981c2514e81c)

2) messages

![image](https://github.com/retopology/BotTwitch/assets/88750093/00a96cbd-41a4-4fa3-8c61-54f751a85060)


3) moderators

![image](https://github.com/retopology/BotTwitch/assets/88750093/5b70edfd-0492-47d3-a7fe-1b3b818b509f)

4) report

![image](https://github.com/retopology/BotTwitch/assets/88750093/ec49ba28-41a5-4e56-8c97-df5a4e2db656)

5) token

![image](https://github.com/retopology/BotTwitch/assets/88750093/ac6a0e28-6e65-4c38-9b74-98d80b522fce)


Если у вас нету базы, можете создать её, используя OpenSever - https://ospanel.io

Настройка базы происходит следующим образом, после установки OpenServer, запускаем его и заходим в настройки

![image](https://github.com/retopology/BotTwitch/assets/88750093/135bc53a-f9da-409c-a4b0-52e12dc4c571)

Заходим во вкладку Сервер, там снизу сриним порт MySQL, потом он нам понадобится.

Во вкладке Модули, желательно выставить следующие значения:

![image](https://github.com/retopology/BotTwitch/assets/88750093/a3131195-9550-4d5c-b578-7bff1e1f9718)

Запускаем сервер, заходим в PhpMyAdmin:

![image](https://github.com/retopology/BotTwitch/assets/88750093/fa4279af-bf9e-4760-adb0-e448c8e76f6b)

Пользователь - root

Пароля нет

Далее жемем Создать БД -> Импорт 

И там выбираем файл этот файл -> https://disk.yandex.ru/d/0nNpxQuT9_x8SA

Свертье со скринами в самом начале, что база стала именно такой.

Запускаем бота.

Путь к боту находится тут - BotTwitch\RetopBot\bin\Debug\RetopBot.exe (можно создать ярлык на рабочий стол)

Справа снизу настраиваем параметры пользователя.

Username - ваш ник на твиче

Token и ClinetId - можно найти тут -> https://twitchtokengenerator.com

(Обязательно нужно быть модератором у стримера, и при создании токена, выбрать все пункты на Yes, Так же, в самом боте, в начале токена нужно дописать oauth:)

Финальный вид токена - oauth:ваштокен

TwitchId - находим тут -> https://www.streamweasels.com/tools/convert-twitch-username-to-user-id/

Просто вписываем свой ник


Сохрнаяем данные, идем в левую нижнюю кнопку (Настройки подключения)

Жмем кнопку стандартные, и меняем только следующие поля:

Стример - ник стримера, к кому будет привязан бот на момент дейтсвия програрммы.

ID - userid стримера, ищем там же, где искали свой id.

Готово, можно запускать бота.



