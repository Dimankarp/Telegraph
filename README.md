# Telegraph - Приложение Обмена Сообщениями
```

  _____     _                            _               _____ _              ___ _     _   __    __            
 /__   \___| | ___  __ _ _ __ __ _ _ __ | |__           /__   \ |__   ___    /___\ | __| | / / /\ \ \__ _ _   _ 
   / /\/ _ \ |/ _ \/ _` | '__/ _` | '_ \| '_ \   _____    / /\/ '_ \ / _ \  //  // |/ _` | \ \/  \/ / _` | | | |
  / / |  __/ |  __/ (_| | | | (_| | |_) | | | | |_____|  / /  | | | |  __/ / \_//| | (_| |  \  /\  / (_| | |_| |
  \/   \___|_|\___|\__, |_|  \__,_| .__/|_| |_|          \/   |_| |_|\___| \___/ |_|\__,_|   \/  \/ \__,_|\__, |
                   |___/          |_|                                                                     |___/ 

```
![LobbyFull](https://user-images.githubusercontent.com/28807607/121287648-8a321900-c914-11eb-9b56-4e550ec30b71.PNG)

### Warning:
*Telegraph* was created as a *school project* to demonstrate possible implementations of *RSA and ECDH encryptions*. It's based on  a little chat, I found years ago on the Internet, however
it was adopted to my needs, heavily rewritten and redesigned. As the school I will be presenting this project at is located in Russia, the whole **apllication is in Russian(alongside with the rest of this README)**.

## Введение:
*Телеграф* был разработан как дополнение к школьному проекту по теме - "Современная Криптография". Это приложение представляет из себя систему СЕРВЕР-КЛИЕНТ, позволяющую передавать текстовые сообщения по TCP протоколу с (и без) использованием
алгоритмов шифрования RSA и ECDH.

Система *Телеграф* состоит из двух независимых программ: Сервера - ***Telegraph Station*** и Клиента - ***Telegraph***.

## Telegraph Station

Сервер *Telegraph Station* представляет из себя консольное приложение, осуществляющее хостинг чата, передачу, шифровку/дешифровку сообщений, регистрацию пользователей. *Telegraph Station* поддерживает
одновременное подключение до 100 пользователей, алгоритм шифрования RSA с ключом размером 2048 и алгоритм шифрования ECDH с ключом размером 256.

![Server](https://user-images.githubusercontent.com/28807607/121287657-8c947300-c914-11eb-878e-b5a4d2d94956.PNG)

## Telegraph - Client

Клиент *Telegraph* является приложением Windows Forms и служит как инструмент обмена текстовыми сообщениями в рамках цепей КЛИЕНТ1-СЕРВЕР, СЕРВЕР-КЛИЕНТ1, КЛИЕНТ-СЕРВЕР-КЛИЕНТ2.
Пользователь может настроить следующие параметра клиента: IP адрес сервера, к которому производится подключение, использование шифрования RSA или ECDH при отправке сообщения.
Клиент поддерживает подключения к Серверу *Telegraph Station*, алгоритм шифрования RSA с ключом размером 2048 и алгоритм шифрования ECDH с ключом размером 256.

![Capture](https://user-images.githubusercontent.com/28807607/121287644-87cfbf00-c914-11eb-8ddb-970608232b7a.PNG)
