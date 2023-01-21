
# Longo To Do App

It is a technical test for the company LongoMatch.

It is a small application that displays a list of things to do with several features such as adding, deleting or changing the state.

## Features

- Show list of Items
- Add new Item
- Delete Item
- Change state of Item
- Pull to Refresh list


## API Reference

#### Get all items

```http
  GET /api/todo
```

#### Get item by id

```http
  GET /api/todo?id={id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id is the key object|

#### Post item

```http
  POST /api/todo
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `TodoItem`| `object` | **Required**. TodoItem |

#### Put item

```http
  PUT /api/todo
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `TodoItem`| `object` | **Required**. TodoItem with key|

#### Delete item

```http
  DELETE /api/todo
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id is the key object|

## Documentation

[Documentation](https://github.com/Rodrigo262/LongoToDo/blob/develop/DocumentacionTest.pdf)


## Authors

- [@Rodrigo262](https://www.github.com/Rodrigo262)

