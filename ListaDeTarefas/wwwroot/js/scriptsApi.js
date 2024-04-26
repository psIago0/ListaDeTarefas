const todoForm = document.querySelector("#todo-form");
const todoInput = document.querySelector("#todo-input");
const todoList = document.querySelector("#todo-list");
const editForm = document.querySelector("#edit-form");
const editInput = document.querySelector("#edit-input");
const cancelEditBtn = document.querySelector("#cancel-edit-btn");
const searchInput = document.querySelector("#search-input");
const eraseBtn = document.querySelector("#erase-button");
const filterBtn = document.querySelector("#filter-select");
var testeId

let oldInputValue;

const createTodoElement = (todo) => {
    const todoItem = document.createElement("div");
    todoItem.classList.add("todo");
    todoItem.id = todo.id;

    const todoTitle = document.createElement("h3");
    todoTitle.innerText = todo.nome;
    todoItem.appendChild(todoTitle);

    const doneBtn = document.createElement("button");
    doneBtn.classList.add("finish-todo");
    doneBtn.innerHTML = '<i class="fa-solid fa-check"></i>';
    todoItem.appendChild(doneBtn);

    const editBtn = document.createElement("button");
    editBtn.classList.add("edit-todo");
    editBtn.innerHTML = '<i class="fa-solid fa-pen"></i>';
    todoItem.appendChild(editBtn);

    const deleteBtn = document.createElement("button");
    deleteBtn.classList.add("remove-todo");
    deleteBtn.innerHTML = '<i class="fa-solid fa-xmark"></i>';
    todoItem.appendChild(deleteBtn);

    if (todo.done) {
        todoItem.classList.add("done");
    }

    return todoItem;
};

const toggleForms = () => {
    editForm.classList.toggle("hide");
    todoForm.classList.toggle("hide");
    todoList.classList.toggle("hide");
};

const saveTodo = async (text) => {
    const tarefa = {
        nome: text,
        status: false,
        DtCriacao: new Date()
    };

    try {
        const response = await fetch("https://localhost:7191/AdicionarTarefa", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(tarefa),
        });
        if (!response.ok) {
            throw new Error("Não foi possível salvar a tarefa");
        }
        const todo = await response.json();
        todoList.appendChild(createTodoElement(todo));
        todoInput.value = "";
    } catch (error) {
        console.error("Erro ao salvar a tarefa:", error.message);
    }
};

const removeTodo = async (todoId) => {
    try {
        const response = await fetch(`https://localhost:7191/DeletarTarefa/${todoId}`, {
            method: "DELETE",
        });
        if (!response.ok) {
            throw new Error("Não foi possível excluir a tarefa");
        }
        const todoItem = document.getElementById(todoId);
        todoList.removeChild(todoItem);
    } catch (error) {
        console.error("Erro ao excluir a tarefa:", error.message);
    }
};

const updateTodoStatus = async (todoId, done) => {
    const tarefaParaAlterar = {
        Id: todoId,
        Status: done,
        DtCriacao: new Date().toISOString()
    };

    try {
        const response = await fetch(`https://localhost:7191/AlterarTarefa`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(tarefaParaAlterar),
        });
        if (!response.ok) {
            throw new Error("Não foi possível atualizar o status da tarefa");
        }
        const todoItem = document.getElementById(todoId);
        if (done) {
            todoItem.classList.add("done");
        } else {
            todoItem.classList.remove("done");
        }
    } catch (error) {
        console.error("Erro ao atualizar o status da tarefa:", error.message);
    }
};

const updateTodo = async (todoId, newText) => {
    const tarefaParaAlterar = {
        Nome: newText,
        Id: todoId,
        DtCriacao: new Date().toISOString()
    };

    try {
        const response = await fetch(`https://localhost:7191/AlterarTarefa`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(tarefaParaAlterar),
        });
        if (!response.ok) {
            throw new Error("Não foi possível atualizar a tarefa");
        }
        const todoItem = document.getElementById(todoId);
        const todoTitle = todoItem.querySelector("h3");
        todoTitle.innerText = newText;
    } catch (error) {
        console.error("Erro ao atualizar a tarefa:", error.message);
    }
};

todoForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    const inputValue = todoInput.value;
    if (inputValue) {
        await saveTodo(inputValue);
    }
});

document.addEventListener("click", async (e) => {
    const targetEl = e.target;

    if (targetEl.classList.contains("finish-todo")) {
        const parentEl = targetEl.closest(".todo");
        const isDone = parentEl.classList.contains("done");
        await updateTodoStatus(parentEl.id, !isDone);
    }

    if (targetEl.classList.contains("remove-todo")) {
        const parentEl = targetEl.closest(".todo");
        await removeTodo(parentEl.id);
    }

    if (targetEl.classList.contains("edit-todo")) {
        const parentEl = targetEl.closest(".todo");
        toggleForms();
        editInput.value = parentEl.querySelector("h3").innerText;
        testeId = parentEl.id;
    }
});

cancelEditBtn.addEventListener("click", (e) => {
    e.preventDefault();
    toggleForms();
});

editForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    const editInputValue = editInput.value;
    if (editInputValue) {
        await updateTodo(testeId, editInputValue);
    }
    toggleForms();
});

searchInput.addEventListener("keyup", (e) => {
    const search = e.target.value.toLowerCase();
    const todos = document.querySelectorAll(".todo");
    todos.forEach((todo) => {
        const todoTitle = todo.querySelector("h3").innerText.toLowerCase();
        todo.style.display = todoTitle.includes(search) ? "flex" : "none";
    });
});

eraseBtn.addEventListener("click", (e) => {
    e.preventDefault();
    searchInput.value = "";
    searchInput.dispatchEvent(new Event("keyup"));
});

filterBtn.addEventListener("change", (e) => {
    const filterValue = e.target.value;
    const todos = document.querySelectorAll(".todo");
    todos.forEach((todo) => {
        switch (filterValue) {
            case "all":
                todo.style.display = "flex";
                break;
            case "done":
                todo.style.display = todo.classList.contains("done") ? "flex" : "none";
                break;
            case "todo":
                todo.style.display = !todo.classList.contains("done") ? "flex" : "none";
                break;
            default:
                break;
        }
    });
});
