<template>
  <div class="table-container">
    <table>
      <thead>
        <tr>
          <th>#</th>
          <th>Email</th>
          <th>Password</th>
          <th>Surname</th>
          <th>Name</th>
          <th>Phone</th>
          <th>Birthday</th>
          <th>Sex</th>
          <th>Role</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="user in localUsers" :key="user.id" class="text-center">
          <td><input id="Id" type="checkbox" v-model="user.checked"></td>
          <td><input id="Email" type="text" v-model="user.email" disabled></td>
          <td><input id="Password" type="text" v-model="user.password" disabled></td>
          <td><input id="Surname" type="text" v-model="user.surname" disabled></td>
          <td><input id="Name" type="text" v-model="user.name" disabled></td>
          <td><input id="Phone" type="text" v-model="user.phone" disabled></td>
          <td><input id="DateOfBirth" type="text" v-model="user.birthday" disabled></td>
          <td><input id="Sex" type="text" v-model="user.sex" disabled></td>
          <td><input id="Role" type="text" v-model="user.role" disabled></td>
        </tr>
      </tbody>
      <tfoot>
        <button @click="duplicateAccount" class="btn btn-secondary">Duplicate</button>
        <span>/</span>
        <button @click="deleteAccount" class="btn btn-secondary">Delete</button>
      </tfoot>
    </table>
  </div>
</template>

<style scoped>
.table-container {
  margin: 20px;
  max-width: 100%;
  overflow-x: auto;
}

table {
  width: 100%;
  border-collapse: collapse;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
}

th, td {
  padding: 12px;
  text-align: center;
  border: 1px solid #ddd;
}

thead {
  background-color: #007bff;
  color: white;
}

tbody tr:nth-child(odd) {
  background-color: #f9f9f9;
}

tbody tr:hover {
  background-color: #f1f1f1;
}

/* Зменшуємо ширину для всіх інпутів */
input[type="text"] {
  width: 130px; /* зменшено до 130px */
  padding: 8px;
  font-size: 14px;
  border: 1px solid #ddd;
  background-color: #f9f9f9;
}

input[type="text"]:disabled {
  background-color: #e9ecef;
  cursor: not-allowed;
}

/* Зменшуємо розмір для чекбоксів */
input[type="checkbox"] {
  transform: scale(1.2); /* зменшено до 1.2 для компактнішого вигляду */
}

/* Зменшуємо ширину кнопок в футері */
tfoot button {
  padding: 6px 12px; /* зменшено відстань */
  font-size: 14px; /* зменшений шрифт */
}

tfoot {
  padding-top: 15px;
  text-align: center;
}

/* Кнопка дії */
.btn-action {
  background-color: #007bff;
  color: white;
  padding: 8px 16px; /* зменшено */
  font-size: 14px; /* зменшено */
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-action:hover {
  background-color: #0056b3;
}

.btn-action:active {
  background-color: #004085;
}

span {
  margin: 0 10px;
}
</style>


<script>
import { v4 as uuidv4 } from 'uuid';
export default {
  props: ['users'],
  data() {
    return {
      localUsers: this.users
    };
  },
  watch: {
    users(newUsers) {
      this.localUsers = newUsers; // Оновлюємо локальну змінну, коли змінюється пропс
    },
    localUsers(newLocalUsers) {
      this.$emit('update:users', newLocalUsers); // Випромінюємо оновлене значення users
    }
  },
  methods: {
    duplicateAccount() {
      let checkedUsers = this.localUsers.filter(user => user.checked === true);

      for (let i = 0; i < checkedUsers.length; i++) {
        let newUser = structuredClone(checkedUsers[i]);
        newUser.id = uuidv4();
        newUser.checked = false;
        this.localUsers.push(newUser)
      }
    },
    deleteAccount() {
      let checkedUsers = this.localUsers.filter(user => user.checked == true);
      this.localUsers = this.users.filter(user => !checkedUsers.some(checkedUser => checkedUser.id == user.id));
    },
  },
};
</script>