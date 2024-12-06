<!-- <template>
    <div class="modal-body">
        <EmailField v-model:email="email" />
        <PasswordField v-model:password="password" />
        <SurnameField v-model:surname="surname" />
        <NameRField v-model:name="name" />
        <PhoneField v-model:phone="phone" />
        <BirthdayField v-model:birthday="birthday" />
        <SexField v-model:sex="sex" />
        <RoleField v-model:role="role" />
        <button @click="createAccount" class="btn btn-secondary">Create Account</button>
    </div>
</template>

<style scoped>
.modal-body {
    display: flex;
    flex-direction: column;
    gap: 15px;
    padding: 20px;
    max-width: 500px;
    margin: 0 auto;
    background-color: #f9f9f9;
    border-radius: 8px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

input,
select,
button {
    padding: 10px;
    font-size: 16px;
    border-radius: 5px;
    border: 1px solid #ddd;
}

input:focus,
select:focus,
button:focus {
    outline: none;
    border-color: #007bff;
}

button {
    background-color: #007bff;
    color: white;
    border: none;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

button:hover {
    background-color: #0056b3;
}

button:active {
    background-color: #004085;
}

input,
select {
    width: 100%;
}

button {
    margin-top: 10px;
}
</style>

<script>
import EmailField from './EmailField.vue';
import PasswordField from './PasswordField.vue';
import SurnameField from './SurnameField.vue';
import NameRField from './NameRField.vue';
import PhoneField from './PhoneField.vue';
import BirthdayField from './BirthdayField.vue';
import SexField from './SexField.vue';
import RoleField from './RoleField.vue';

import usersAPI from '@/usersAPI';
export default {
    props: ['users'],
    components: {
        EmailField,
        PasswordField,
        SurnameField,
        NameRField,
        PhoneField,
        BirthdayField,
        SexField,
        RoleField,
    },
    data() {
        return {
            email: '',
            password: '',
            surname: '',
            name: '',
            phone: '',
            birthday: '',
            sex: '',
            role: '',
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
        async createAccount() {
            if (!this.email || !this.password || !this.surname || !this.name || !this.phone || !this.birthday || !this.sex || !this.role) {
                alert("Fill All Lines");                
            }
            else {
                await usersAPI.createUser(this.email, this.password, this.surname, this.name, this.phone, this.birthday, this.sex, this.role);
                this.resetFields();
            }            
        },
        resetFields() {
            this.email = '';
            this.password = '';
            this.surname = '';
            this.name = '';
            this.phone = '';
            this.birthday = '';
            this.sex = '';
            this.role = '';
        }
    },
};
</script> -->