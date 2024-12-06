<template>
    <div class="modal-body">
        <EmailField v-model:email="email" />
        <PasswordField v-model:password="password" />
        <button @click="loginUser()" class="btn btn-secondary">Login</button>
        <button @click="localCurrentPage = 'register'">Register</button>
    </div>
</template>

<style scoped>
.modal-body {
    display: flex;
    flex-direction: column;
    gap: 15px;
    padding: 20px;
    max-width: 400px;
    margin: 50px auto;
    background-color: #ffffff;
    border-radius: 10px;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
    border: 1px solid #eaeaea;
}

input,
button {
    padding: 12px;
    font-size: 14px;
    border-radius: 6px;
    border: 1px solid #cccccc;
    width: 100%;
    box-sizing: border-box;
}

input:focus {
    border-color: #007bff;
    outline: none;
}

button {
    background-color: #007bff;
    color: #ffffff;
    border: none;
    cursor: pointer;
    transition: background-color 0.2s;
}

button:hover {
    background-color: #0056b3;
}

button:active {
    background-color: #004085;
}

button:disabled {
    background-color: #cccccc;
    cursor: not-allowed;
}
</style>

<script>
import usersAPI from '@/usersAPI';

import EmailField from './EmailField.vue';
import PasswordField from './PasswordField.vue';

export default {
    props: ['role',
        'isAuthorized',
        'userId',
        'currentPage',
        'userToken'],
    components: {
        EmailField,
        PasswordField
    },
    data() {
        return {
            localRole: this.role,
            localIsAuthorized: this.isAuthorized,
            localCurrentPage: this.currentPage,
            email: '',
            password: '',
            token: this.userToken,
            newUserId: this.userId,
            parsedToken: ''
        };
    },
    watch: {
        localRole(newRole) {
            this.$emit('update:role', newRole); // Випромінювання оновлення ролі
        },
        localIsAuthorized(newIsAuthorized) {
            this.$emit('update:isAuthorized', newIsAuthorized);
        },        
        newUserId(newId) {
            this.$emit('update:userId', newId); // Випромінювання оновлення ID користувача
        },
        token(newToken) {
            this.$emit('update:userToken', newToken); // Випромінювання оновлення токена
        },
        localCurrentPage(newPage) {
            this.$emit('update:currentPage', newPage); // Випромінювання оновлення сторінки
        }
    },
    methods: {
        async loginUser() {
            let email = this.email;
            let password = this.password;

            this.token = await usersAPI.loginUser(email, password); // Отримання токена
            this.parsedToken = await this.parseJwt(); // Розбір токена

            // Оновлюємо локальні змінні
            await this.updateProps();

            // console.log("LoginPage: " + this.localRole, this.localIsAuthorized, this.localCurrentPage, this.newUserId, this.token);
        },
        async parseJwt() {
            const base64Url = this.token.split('.')[1];
            const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            const jsonPayload = decodeURIComponent(
                atob(base64).split('').map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)).join('')
            );

            return JSON.parse(jsonPayload);
        },
        async updateProps() {
            this.newUserId = this.parsedToken.id;
            this.localRole = (this.parsedToken.role).toLowerCase();
            this.localCurrentPage = 'main';
            this.localIsAuthorized = true;
        }
    }
}
</script>