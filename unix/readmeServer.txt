X2go - это очень неплохой аналог rdp, vnc и тому подобным вещам. С помощью специальным клиента вы сможете управлять вашим рабочим сервером с установленной графической оболочкой.
Установка x2go сервера на Linux.
Вы можете все установить на VPS Linux.

Установка x2go Debian\Ubuntu. Все действия мы будем выполнять на базе Ubuntu 14.04 minimal.
1. Заходим на сервер по SSH и выполняем первоначальные команды.

apt-get update;apt-get install software-properties-common nano aptitude
2. Добавим репозиторий x2go.

add-apt-repository ppa:x2go/stable
У кого не заработало добавление репозиториев смотрим пример.
3. Установим x2goserver.

apt-get update;apt-get install x2goserver x2goserver-xsession
4. Установим графическое окружение XFCE.
Пример для Ubuntu 12.04.

apt-get install xubuntu-desktop
Графическое окружение может быть любое.
5. Перезагрузим сервер (на всякий случай).

reboot
Для подключения у вас должен клиент, о которой пойдет речь ниже.
6. Установка русского языка.
Локализация для основных программ, которые не зависят от окружения.

apt-get install language-pack-ru
Локализация для компонентов окружения Gnome.
]
apt-get install language-pack-gnome-ru
Настройка локализации. Для русификации достаточно выполнить эту команду и перезайти в систему.

update-locale LANG=ru_RU.UTF-8