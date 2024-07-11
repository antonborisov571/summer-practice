# Выполнил работу: Борисов Антон 11-207

# Инструкция по запуску и развертыванию приложения с использованием Docker Compose

Эта инструкция поможет вам запустить и развернуть приложение, используя Docker Compose. Предполагается, что у вас уже есть репозиторий с файлом `docker-compose.yml`.

## Шаг 1: Установка Docker и Docker Compose

### Установите Docker

- **Для Windows и MacOS**: скачайте и установите Docker Desktop с официального сайта [Docker Desktop](https://www.docker.com/products/docker-desktop).
- **Для Linux**: выполните следующие команды в терминале:

  ```bash
  sudo apt-get update
  sudo apt-get install \
      apt-transport-https \
      ca-certificates \
      curl \
      software-properties-common
  curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -
  sudo add-apt-repository \
      "deb [arch=amd64] https://download.docker.com/linux/ubuntu \
      $(lsb_release -cs) \
      stable"
  sudo apt-get update
  sudo apt-get install docker-ce
  ```

### Установите Docker Compose

- **Для Windows и MacOS**:  Docker Compose устанавливается вместе с Docker Desktop.

- **Для Linux**: выполните следующие команды в терминале:

## Шаг 2: Клонирование репозитория

## Шаг 3: Сборка и запуск контейнеров

Теперь, когда все настроено, выполните сборку и запуск контейнеров:

  ```bash
  docker-compose up --build
  ```

Эта команда создаст и запустит все указанные в docker-compose.yml контейнеры. Приложение будет доступно по адресу http://localhost:8080.
